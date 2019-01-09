using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace engUtil.Dto
{
    internal static class Helper
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
    }
}
