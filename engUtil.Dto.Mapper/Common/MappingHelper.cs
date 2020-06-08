// --------------------------------------------------------------------------------
// <copyright filename="MappingHelper.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace EngUtil.Dto
{
    internal static class MappingHelper
    {
        /// <summary>
        /// Return the types from a Expression&lt;Func&lt;TIn, TOut&gt;&gt;
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>KeyValuePair&lt;typeof(TIn),typeof(TOut)&gt;</returns>
        public static TypePair GetExpressionInputOutputTypes(Expression expression)
        {
            TypePair inOutTypes;
            var lambda = (LambdaExpression)expression;
            var param = (ICollection<ParameterExpression>)lambda.Parameters;
            if (param.Count() != 1)
                throw new ArgumentException($"Wrong Parameterlenght. Expression must be Expression<Func<TIn,TOut>>.\r\nArgumentparameter lenght is [{param.Count}]! ");
            Type tTarget = lambda.Body.Type;
            Type tSource = lambda.Parameters[0].Type;
            inOutTypes = new TypePair(tSource, tTarget);
            return inOutTypes;
        }

        /// <summary>
        /// Mapping a object to TTarget-Type
        /// </summary>
        /// <typeparam name="TTarget">The Target Type</typeparam>
        /// <param name="instance">the object-instance that will be mapped to TTarget Type</param>
        internal static TTarget MapTo<TTarget>(IList<IDtoMap> mappings, object instance)
        {
            if (instance == null)
                return default;
            var instanceType = instance.GetType();
            var mappingExpression = mappings.FirstOrDefault(x => x.SourceType == instanceType && x.TargetType == typeof(TTarget));
            if (mappingExpression == null)
                throw new ArgumentException($"Mapping '{instanceType.Name}' not found!\r\n" +
                    "If you use AutoScan, make sure the Assembly is loaded in the current domain.\r\n" +
                    "Otherwise use the 'ScanForDtoMapping(assemblyWithDtoMappings)'- Method!");
            return (TTarget)mappingExpression?.MapObject(instance);
        }

        /// <summary>
        /// Mapping a IEnumerable object to TTarget-Type
        /// </summary>
        /// <typeparam name="TTarget">The Target Type</typeparam>
        /// <param name="enumerableInstance">the IEnumerable object-instance that will be mapped to TTarget Type</param>
        /// <returns></returns>
        internal static IEnumerable<TTarget> MapTo<TTarget>(IList<IDtoMap> mappings, IEnumerable enumerableInstance)
        {
            if (enumerableInstance == null)
                yield break;
            var instanceType = enumerableInstance.GetType().GetGenericArguments()[0];
            var mappingExpression = mappings.FirstOrDefault(x => x.SourceType == instanceType && x.TargetType == typeof(TTarget));
            if (mappingExpression == null)
                throw new ArgumentException($"Mapping '{instanceType.Name}' not found!\r\n" +
                    "If you use AutoScan, make sure the Assembly is loaded in the current domain.\r\n" +
                    "Otherwise use the 'ScanForDtoMapping(assemblyWithDtoMappings)'- Method!");
            foreach (var item in enumerableInstance)
                yield return (TTarget)mappingExpression?.MapObject(item);
        }
     
    }
}
