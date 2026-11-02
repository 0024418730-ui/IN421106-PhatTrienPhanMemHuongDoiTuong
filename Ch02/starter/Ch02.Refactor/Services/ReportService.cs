using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Services
{
    public class ReportService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IAppLogger _logger;

        public ReportService(IClubRepository clubRepository, IAppLogger logger)
        {
            _clubRepository = clubRepository;
            _logger = logger;
        }

        public void PrintClubReport(List<Member> members)
        {
            var clubs = _clubRepository.GetAll();
            foreach (var club in clubs)
            {
                var memberCount = members.Count(m => m.ClubId == club.Id);
                Console.WriteLine($"Club: {club.Name} | Members: {memberCount}");
                _logger.Log($"Report generated for {club.Name}");
            }
        }
    }
}
