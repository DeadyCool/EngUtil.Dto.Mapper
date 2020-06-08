// --------------------------------------------------------------------------------
// <copyright filename="Mapper.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.Dto.Genercis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EngUtil.Dto
{
    public class Mapper : IMapper
    {
        #region fields

        private HashSet<string> _scannedAssemblies = new HashSet<string>();

        private HashSet<string> _scannedObjects = new HashSet<string>();

        #endregion     

        #region ctor

        public Mapper()
        {
            InitializeMapping();
        }

        public Mapper(bool autoScan)
        {
            InitializeMapping(autoScan);
        }

        #endregion

        #region properties: internal

        internal IList<IDtoMap> Mappings = new List<IDtoMap>();

        #endregion

        #region methods: public

        /// <summary>
        /// Inizializer to create mappings
        /// </summary>
        /// <param name="autoScan">Automatically Scan the CurrentDomain-Assemblies for Mapping-Classes</param>
        public virtual void InitializeMapping(bool autoScan = false)
        {
            ScanForExpressionMappings(GetType().Assembly.FullName);
            if (autoScan)
                ScanForExpressionMappings();
        }

        /// <summary>
        /// Gets the definition from object
        /// </summary>
        /// <param name="instance"></param>
        public void GetMapDefinition(object instance)
        {
            var objectTypeName = instance.GetType().FullName;
            if (_scannedObjects.Contains(objectTypeName))
                return;
            var properties = instance.GetType().GetProperties();
            // find mapattribute
            foreach (var property in properties)
            {
                var mappingAttribute = (MapAttribute)property.GetCustomAttribute(typeof(MapAttribute));
                if (mappingAttribute != null)
                {
                    Expression memberExpression = (Expression)property.GetValue(instance);
                    var lambda = (LambdaExpression)memberExpression;
                    var param = (ICollection<ParameterExpression>)lambda.Parameters;
                    if (param.Count() != 1)
                        throw new ArgumentException($"Wrong Parameterlenght. Expression must be Expression<Func<TIn,TOut>>.\r\nArgumentparameter lenght is [{param.Count}]! ");
                    Type targetType = lambda.Body.Type;
                    Type sourceType = lambda.Parameters[0].Type;
                    var dtoMap = CreateDtoMap(sourceType, targetType, memberExpression);
                    if (string.IsNullOrWhiteSpace(mappingAttribute.Description))
                        dtoMap.Description = $"{instance.GetType().Name} - {sourceType.Name} to {targetType.Name}";
                    else
                        dtoMap.Description = mappingAttribute.Description;
                    Mappings.Add(dtoMap);
                }
            }
            _scannedObjects.Add(objectTypeName);
        }

        /// <summary>
        /// Configurate the Mapper
        /// </summary>
        /// <param name="config"></param>
        public void Configure(Action<IMappingBuilder> config)
        {
            config.Invoke(new MappingBuilder(this));
        }

        /// <summary>
        /// Create a new Type-Mapping
        /// </summary>
        /// <typeparam name="TSource">Source-Class</typeparam>
        /// <typeparam name="TTarget">Target-Class</typeparam>
        /// <returns></returns>
        public DtoMap<TSource, TTarget> CreateMapping<TSource, TTarget>(string description = default)
            where TSource : class
            where TTarget : class
        {
            return new DtoMap<TSource, TTarget>()
            {
                Description = description
            };
        }


        /// <summary>
        /// Mapping a object to TTarget-Type
        /// </summary>
        /// <typeparam name="TTarget">The Target Type</typeparam>
        /// <param name="instance">the object-instance that will be mapped to TTarget Type</param>
        public TTarget MapTo<TTarget>(object instance)
            where TTarget : class
        {
            return MappingHelper.MapTo<TTarget>(Mappings, instance); 
        }

        /// <summary>
        /// Mapping a IEnumerable object to TTarget-Type
        /// </summary>
        /// <typeparam name="TTarget">The Target Type</typeparam>
        /// <param name="enumerableInstance">the IEnumerable object-instance that will be mapped to TTarget Type</param>
        /// <returns></returns>
        public IEnumerable<TTarget> MapTo<TTarget>(IEnumerable enumerableInstance)
            where TTarget : class
        {
            return MappingHelper.MapTo<TTarget>(Mappings, enumerableInstance);
        }

        /// <summary>
        /// Get the expression to transform the Source-Type to Target-Type 
        /// (returns the MemberExpression 'Expression&lt;Func&lt;TSource, TTarget&gt;&gt;')
        /// </summary>
        /// <param name="sourceType">The Source Type</param>
        /// <param name="targetType">The Target Type</param>
        /// <returns>MemberExpression 'Expression&lt;Func&lt;TSource, TTarget&gt;&gt;'</returns>
        public Expression GetExpressionMap(Type sourceType, Type targetType)
        {
            return Mappings.FirstOrDefault(x =>
               x.SourceType == sourceType &&
               x.TargetType == targetType)?.Expression;
        }

        public Expression<Func<TSource, TTarget>> GetExpressionMap<TSource, TTarget>()
            where TSource : class
            where TTarget : class
        {
            var mapping = GetExpressionMap(typeof(TSource), typeof(TTarget));
            if (mapping == null) return null;
            return (Expression<Func<TSource, TTarget>>)mapping;
        }

        public void ScanForExpressionMappings(string assemblyFullname = null)
        {
            if (string.IsNullOrEmpty(assemblyFullname))
                ScanAssembliesForDtoMappings();
            else
            {
                var currentDomain = AppDomain.CurrentDomain;
                var assembly = currentDomain.Load(assemblyFullname);
                GetMappingsByAttribute(assembly);
            }
        }

        #endregion

        #region methods: internal

        internal void AddMapping(IDtoMap mapping)
        {
            var typePair = MappingHelper.GetExpressionInputOutputTypes(mapping.Expression);
            if (Mappings.Any(x => x.SourceType == typePair.InType && x.TargetType == typePair.OutType))
                throw new ArgumentException($"Mapping from Type '{typePair.InType.Name}' to Type '{typePair.OutType.Name}' already exists");
            Mappings.Add(mapping);
        }

        #endregion

        #region methods: private

        private IDtoMap CreateDtoMap(Type sourceType, Type targetType, Expression expression)
        {
            Type constructedType = typeof(DtoMap<,>).MakeGenericType(sourceType, targetType);
            var dtoMap = (IDtoMap)Activator.CreateInstance(constructedType, expression );
            return dtoMap;
        }

        private void GetMappingsByAttribute(Assembly assembly)
        {
            if (_scannedAssemblies.Contains(assembly.FullName))
                return;
            var assemblyTypes = assembly
                .GetTypes()
                .Where(x => x.GetCustomAttribute<MapDefinitionAttribute>() != null);
            foreach (Type type in assemblyTypes)
            {
                if (type.IsAbstract)
                    continue;
                var mappingType = (DtoDefinition)BuildInstance(type);
                mappingType.Mapper = this;
                GetMapDefinition(mappingType);
            }
            _scannedAssemblies.Add(assembly.FullName);
        }

        private void ScanAssembliesForDtoMappings()
        {
            var currentDomain = AppDomain.CurrentDomain;
            var assemblies = currentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                GetMappingsByAttribute(assembly);
        }

        private object BuildInstance(Type type)
        {
            if (type.IsGenericType)
                return Activator.CreateInstance(type, type.GetGenericArguments());            
            return Activator.CreateInstance(type);
        }

        #endregion
    }

}
