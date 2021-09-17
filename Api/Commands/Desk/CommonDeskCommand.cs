using Core.Models;
using System;
using DeskModel = Core.Models.Desk;

namespace Api.Commands.Desk
{
    public class CommonDeskCommand
    {
        public string Description { get; set; }
        public Localization Localization { get; set; }
        public bool IsBlocked { set; get; }

        public static implicit operator DeskModel(CommonDeskCommand command) => new()
        {
            Description = command.Description,
            Localization = command.Localization,
            IsBlocked = command.IsBlocked
        };

    }
}
