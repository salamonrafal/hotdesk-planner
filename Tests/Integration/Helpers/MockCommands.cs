using System;
using System.Collections.Generic;
using Api.Commands.Desk;
using Api.Commands.Reservations;
using Api.Commands.Users;
using Core.Models;

namespace Integration.Helpers
{
    public static class MockCommands
    {
        public static class DeskModel
        {
            public static InsertDeskCommand CreateInsertCommand
            (
                string description = "Description",
                bool isBlocked = false,
                int x = 0,
                int y = 0,
                int floor = 1,
                string outbuilding = "A"
            ) => new ()
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

            public static UpdateDeskCommand CreateUpdateCommand
            (
                string description = "Description Update",
                bool isBlocked = false,
                int x = 1,
                int y = 1,
                int floor = 2,
                string outbuilding = "B"
            ) => new ()
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
        }
        
        public static class ReservationModel
        {
            public static InsertReservationCommand CreateInsertCommand
            (
                Guid userId = new Guid (),
                DateTime endDate = new DateTime (),
                DateTime startDate = new DateTime (),
                bool isPeriodical = false,
                PeriodicDetail? periodicDetail = null
            ) => new ()
            {
                AssignedTo = userId,
                EndDate = endDate,
                StartDate = startDate,
                IsPeriodical = isPeriodical,
                PeriodicDetail = periodicDetail ?? new PeriodicDetail ()
            };

            public static UpdateReservationCommand CreateUpdateCommand
            (
                DateTime endDate = new DateTime (),
                DateTime startDate = new DateTime (),
                bool isPeriodical = false,
                PeriodicDetail? periodicDetail = null
            ) => new ()
            {
                StartDate = startDate,
                EndDate = endDate,
                IsPeriodical = isPeriodical,
                PeriodicDetail = periodicDetail
            };
        }

        public static class UserModel
        {
            public static InsertUserCommand CreateInsertCommand(
                string email = "test@mailinator.com",
                string name = "Jan",
                string password = "123456",
                List<UserRole>? role = null,
                string surname = "Kowalski",
                string urlAvatar = "http://wwww.google.pl/"
            ) => new ()
            {
                Email = email,
                Name = name,
                Password = password,
                Role = role ?? new List<UserRole> (){},
                Surname = surname,
                UrlAvatar = urlAvatar
            };

            public static UpdateUserCommand CreateUpdateCommand(
                string email = "test-update@mailinator.com",
                string name = "Adam",
                string password = "123456-update",
                string surname = "Kowalski-update",
                string urlAvatar = "http://wwww.google.pl/-update"
            ) => new ()
            {
                Email = email,
                Name = name,
                Password = password,
                Surname = surname,
                UrlAvatar = urlAvatar
            };
        }
    }
}