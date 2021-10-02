using Api.Commands.Users;
using Core.Models;

namespace Api.Mappers
{
    public class GenericUserCommandMapper<TClassInput, TClassOutput> : ICommandMapper<TClassInput, TClassOutput>
        where TClassInput : CommonUserCommand where TClassOutput : User
    {
        public TClassOutput ConvertToModel(TClassInput command) =>  (TClassOutput) command;
    }
}
