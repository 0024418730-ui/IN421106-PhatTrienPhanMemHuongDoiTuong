using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Repositories
{
    public class LoggingClubRepository : IClubRepository
    {
        private readonly IClubRepository _inner;
        private readonly IAppLogger _logger;

        public LoggingClubRepository(IClubRepository inner, IAppLogger logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public void Add(Club club)
        {
            _inner.Add(club);
            _logger.Log($"[repo] Added club: {club.Name}");
        }

        public Club? GetById(Guid id)
        {
            _logger.Log($"[repo] Querying club: {id}");
            return _inner.GetById(id);
        }

        public List<Club> GetAll()
        {
            _logger.Log("[repo] Retrieving all clubs.");
            return _inner.GetAll();
        }
    }
}
