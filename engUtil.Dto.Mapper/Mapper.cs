using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;

namespace engUtil.Dto
{
    public class Mapper : IMapper
    {
        #region fields

        internal IList<IMap> MappingList = new List<IMap>();

        private HashSet<string> _scannedAssemblies = new HashSet<string>();

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
            var properties = instance.GetType().GetProperties();
            // find mapattribute
            foreach (var property in properties)
            {
                var mappingAttribute = (MapAttribute)property.GetCustomAttribute(typeof(MapAttribute));
                if (mappingAttribute != null)
                {
                    Expression exp = (Expression)property.GetValue(instance);
                    var lambda = (LambdaExpression)exp;
                    var param = (ICollection<ParameterExpression>)lambda.Parameters;
                    if (param.Count() != 1)
                        throw new ArgumentException($"Wrong Parameterlenght. Expression must be Expression<Func<TIn,TOut>>.\r\nArgumentparameter lenght is [{param.Count}]! ");
                    Type targetType = lambda.Body.Type;
                    Type sourceType = lambda.Parameters[0].Type;
                    var mapBuilder = CreateMapBuilder(sourceType, targetType);
                    mapBuilder.Description = mappingAttribute.Description;
                    mapBuilder.AddMap(exp);
                }
            }
        }

        /// <summary>
        /// Configurate the Mapper
        /// </summary>
        /// <param name="config"></param>
        public void Configure(Action<MappingConfiguration> config)
        {
            config.Invoke(new MappingConfiguration(this));
        }

        /// <summary>
        /// Create a new Type-Mapping
        /// </summary>
        /// <typeparam name="TSource">Source-Class</typeparam>
        /// <typeparam name="TTarget">Target-Class</typeparam>
        /// <returns></returns>
        public Map<TSource, TTarget> CreateMappingFor<TSource, TTarget>()
            where TSource : class
            where TTarget : class
        {   
            return new Map<TSource, TTarget>(this);
        }

        /// <summary>
        /// Mapping a object to TTarget-Type
        /// </summary>
        /// <typeparam name="TTarget">The Target Type</typeparam>
        /// <param name="instance">the object-instance that will be mapped to TTarget Type</param>
        public TTarget MapTo<TTarget>(object instance)
        {           
            var instanceType = instance.GetType(); 
            var mappingExpression = MappingList.FirstOrDefault(x => x.SourceType == instanceType && x.TargetType == typeof(TTarget));
            if (mappingExpression == null)
                throw new ArgumentException($"Mapping '{instanceType.Name}' not found!\r\n" +
                    "If you use AutoScan, make sure the Assembly is loaded in the current domain.\r\n" +
                    "Otherwise use the 'ScanForDtoMapping(assemblyWithDtoMappings)'- Method!");
            return (TTarget)mappingExpression?.MapObject(instance);
        }

        public IEnumerable<TTarget> MapTo<TTarget>(IEnumerable enumerableInstance)
        {
            var instanceType = enumerableInstance.GetType().GetGenericArguments()[0];
            var mappingExpression = MappingList.FirstOrDefault(x => x.SourceType == instanceType && x.TargetType == typeof(TTarget));
            if (mappingExpression == null)
                throw new ArgumentException($"Mapping '{instanceType.Name}' not found!\r\n" +
                    "If you use AutoScan, make sure the Assembly is loaded in the current domain.\r\n" +
                    "Otherwise use the 'ScanForDtoMapping(assemblyWithDtoMappings)'- Method!");
            foreach(var item in enumerableInstance)       
                yield return (TTarget)mappingExpression?.MapObject(item); 
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
            return MappingList.FirstOrDefault(x =>
               x.SourceType == sourceType &&
               x.TargetType == targetType)?.GetExpression();
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

        internal void AddMapping(IMap mapping)
        {
            var typePair = Helper.GetExpressionInputOutputTypes(mapping.GetExpression());
            if (MappingList.Any(x => x.SourceType == typePair.InType && x.TargetType == typePair.OutType))
                throw new ArgumentException($"Mapping from Type '{typePair.InType.Name}' to Type '{typePair.OutType.Name}' already exists");
            MappingList.Add(mapping);
        }

        #endregion

        #region methods: private

        private IMap CreateMapBuilder(Type sourceType, Type targetType)
        {
            Type constructedType = typeof(Map<,>).MakeGenericType(sourceType, targetType);
            var mapBuilder = (IMap)Activator.CreateInstance(constructedType, new object[] { this });            
            return mapBuilder;
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
                object mappingType = Activator.CreateInstance(type, this);
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



        #endregion
    }
}
