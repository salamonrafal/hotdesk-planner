using Api.Commands.Desk;
using Core.Models;

namespace Api.Mappers
{
    public interface ICommandMapper<TClassInput, TClassOutput> where TClassInput : class where TClassOutput : class
    {
        public TClassOutput InsertCommandMapToModel(TClassInput command);
        public TClassOutput UpdateCommandMapToModel(TClassInput command);
        public TClassOutput DeleteCommandMapToModel(TClassInput command);
        public TClassOutput SearchCommandMapToModel(TClassInput command);
    }
}
