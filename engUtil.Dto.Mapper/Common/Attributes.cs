using System;

namespace engUtil.SimpleMapper
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
