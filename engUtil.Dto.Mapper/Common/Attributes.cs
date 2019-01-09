using System;

namespace engUtil.Dto
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
