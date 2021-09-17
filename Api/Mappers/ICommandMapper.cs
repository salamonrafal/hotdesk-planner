namespace Api.Mappers
{
    public interface ICommandMapper<TClassInput, TClassOutput> where TClassInput : class where TClassOutput : class
    {
        public TClassOutput ConvertToModel(TClassInput command);
    }
}
