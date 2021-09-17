using Api.Commands.Desk;
using DeskModel = Core.Models.Desk;

namespace Api.Mappers
{
    public class GenericDeskCommandMapper<TClassInput, TClassOutput> : ICommandMapper<TClassInput, TClassOutput>
        where TClassInput : CommonDeskCommand where TClassOutput : DeskModel
    {
        public TClassOutput ConvertToModel(TClassInput command) => (TClassOutput)command;
    }
}
