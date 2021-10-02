using System.Collections.Generic;
using Api.Commands.Users;
using Api.Mappers;
using Core.Enums;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Unit.Api.Mappers
{
    [TestFixture]
    public class GenericUserCommandMapperTest
    {
        [Test]
        public void ShouldReturnMappedModel()
        {
            var userRole = new UserRole ()
            {
                DisplayName = "user",
                RoleType = RoleType.User
            };
            
            var command = new InsertUserCommand ()
            {
                Email = "test@test.be",
                Name = "test",
                Surname = "test",
                Password = "test",
                Role = new List<UserRole> ()
                {
                    userRole
                }
            };

            var mapper = new GenericUserCommandMapper <InsertUserCommand, User> ();
            var model = mapper.ConvertToModel (command);
            
            model.Should ().BeOfType<User> ();
            model.Email.Should ().Be ("test@test.be");
            model.Name.Should ().Be ("test");
            model.Surname.Should ().Be ("test");
            model.Password.Should ().Be ("test");
            model.Role.Should ().BeOfType<List<UserRole>> ();
            model.Role.Should ().HaveCount (1);
            model.Role.Should ().HaveElementAt(0, userRole);
        }
    }
}
