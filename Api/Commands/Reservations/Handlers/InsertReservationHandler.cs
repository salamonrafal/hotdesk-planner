using Api.Mappers;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Commands.Reservations.Handlers
{
    public class InsertReservationHandler : IRequestHandler<InsertReservationCommand, Guid>
    {
        private readonly IReservationService _service;

        public InsertReservationHandler(IReservationService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(InsertReservationCommand command, CancellationToken cancellationToken)
        {
            if (command == InsertReservationCommand.Empty) throw new CommandEmptyException("Command is Empty");

            GenericReservationCommandMapper<InsertReservationCommand, Reservation> mapper = new();
            var desk = mapper.ConvertToModel(command);
            return await _service.Add(desk);
        }
    }
}
