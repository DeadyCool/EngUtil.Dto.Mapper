namespace engUtil.SimpleMapper
{
    public interface IMapDefinition
    {
        TTarget MapTo<TTarget>(object instance);
    }
}
