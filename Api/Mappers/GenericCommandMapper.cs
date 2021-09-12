namespace Api.Mappers
{
    public class GenericCommandMapper<TClassInput, TClassOutput> : ICommandMapper<TClassInput, TClassOutput>
        where TClassInput : class where TClassOutput : class
    {
        public TClassOutput DeleteCommandMapToModel(TClassInput command)
        {
            throw new System.NotImplementedException();
        }

        public TClassOutput InsertCommandMapToModel(TClassInput command)
        {
            throw new System.NotImplementedException();
        }

        public TClassOutput SearchCommandMapToModel(TClassInput command)
        {
            throw new System.NotImplementedException();
        }

        public TClassOutput UpdateCommandMapToModel(TClassInput command)
        {
            throw new System.NotImplementedException();
        }
    }
}
