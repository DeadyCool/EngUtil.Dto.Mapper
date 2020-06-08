// --------------------------------------------------------------------------------
// <copyright filename="TypePair.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;

namespace EngUtil.Dto
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
