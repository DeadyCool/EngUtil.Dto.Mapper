using System;

namespace engUtil.SimpleMapper.Common
{
    internal struct TypePair
    {
        public TypePair(Type inType, Type outType)
        {
            InType = inType;
            OutType = outType;
        }

        public Type InType { get; private set; }

        public Type OutType { get; private set; }      

        public override string ToString()
        {     
            return $"[{InType.FullName}, {OutType.FullName}]";
        }
    }
}
