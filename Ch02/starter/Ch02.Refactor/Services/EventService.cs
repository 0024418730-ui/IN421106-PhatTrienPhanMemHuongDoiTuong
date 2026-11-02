using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Services
{
    public class EventService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IAppLogger _logger;

        public EventService(IClubRepository clubRepository, IAppLogger logger)
        {
            _clubRepository = clubRepository;
            _logger = logger;
        }

        public Event CreateEvent(Guid clubId, string title, DateTime date)
        {
            var club = _clubRepository.GetById(clubId)
                       ?? throw new InvalidOperationException("Club not found");

            var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
            _logger.Log($"Event created: {title}");

            return ev;
        }
    }
}
