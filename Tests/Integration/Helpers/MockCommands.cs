using System;
using Api.Commands.Desk;
using Core.Models;

namespace Integration.Helpers
{
    public static class MockCommands
    {
        public static class DeskModel
        {
            public static InsertDeskCommand CreateInsertDeskCommand
            (
                string description = "Description",
                bool isBlocked = false,
                int x = 0,
                int y = 0,
                int floor = 1,
                string outbuilding = "A"
            ) => new InsertDeskCommand ()
            {
                Description = description,
                IsBlocked = isBlocked,
                Localization = new Localization ()
                {
                    Coordination = new Coordination ()
                    {
                        X = x,
                        Y = y
                    },
                    Floor = floor,
                    Outbuilding = outbuilding
                }
            };

            public static UpdateDeskCommand CreateUpdateDeskCommand
            (
                string description = "Description Update",
                bool isBlocked = false,
                int x = 1,
                int y = 1,
                int floor = 2,
                string outbuilding = "B"
            ) => new UpdateDeskCommand ()
            {
                Description = description,
                IsBlocked = isBlocked,
                Localization = new Localization ()
                {
                    Coordination = new Coordination ()
                    {
                        X = x,
                        Y = y
                    },
                    Floor = floor,
                    Outbuilding = outbuilding
                }
            };

            public static DeleteDeskCommand CreateDeleteDeskCommand (Guid id) => new DeleteDeskCommand ()
            {
                Id = id
            };

            public static SearchDeskCommand CreateSearchDeskCommand (string query) => new SearchDeskCommand ()
            {
                Query = query
            };

            public static GetDeskCommand CreateGetDeskCommand (Guid id) => new GetDeskCommand ()
            {
                Id = id
            };
            
        }
        
        
    }
}