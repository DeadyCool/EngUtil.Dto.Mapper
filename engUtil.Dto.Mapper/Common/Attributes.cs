// --------------------------------------------------------------------------------
// <copyright filename="MapDefinitionAttribute.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;

namespace EngUtil.Dto
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MapDefinitionAttribute : Attribute
    {        
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MapAttribute : Attribute
    {
        public string Description { get; set; }     
    }
}
