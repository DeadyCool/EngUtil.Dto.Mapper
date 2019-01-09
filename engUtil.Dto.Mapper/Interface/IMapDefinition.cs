namespace engUtil.Dto
{
    public interface IMapDefinition
    {
        TTarget MapTo<TTarget>(object instance);
    }
}
