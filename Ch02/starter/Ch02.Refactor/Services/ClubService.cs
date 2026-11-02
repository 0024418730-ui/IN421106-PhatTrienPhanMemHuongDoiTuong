using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Repositories;

namespace EduHub.Ch02.Services
{
    public class ClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IAppLogger _logger;
        private readonly List<Member> _members = new();

        public ClubService(IClubRepository clubRepository, IAppLogger logger)
        {
            _clubRepository = clubRepository;
            _logger = logger;
        }

        public void CreateClub(string name, string description, string presidentEmail, NotificationChannel channel = NotificationChannel.Email)
        {
            // Luật tên được ủy quyền cho Domain Entity Club.Create
            var club = Club.Create(name, description);
            _clubRepository.Add(club);

            _logger.Log($"Club created: {name}");

            var notifier = NotificationServiceFactory.Create(channel);
            notifier.Send(presidentEmail, $"Club '{name}' successfully created!");
        }

        public void AddMemberToClub(Guid clubId, string memberName, string email)
        {
            var club = _clubRepository.GetById(clubId)
                ?? throw new InvalidOperationException("Club not found");

            int currentCount = _members.Count(m => m.ClubId == clubId);

            // Gọi hành vi HasRoom từ Domain Entity Club
            if (!club.HasRoom(currentCount))
                throw new InvalidOperationException("Club is full (max 50 members)");

            var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
            _members.Add(member);

            _logger.Log($"Member {memberName} joined {club.Name}");

            var emailNotifier = NotificationServiceFactory.Create(NotificationChannel.Email);
            emailNotifier.Send(email, $"Welcome email to {email}");

            var slackNotifier = NotificationServiceFactory.Create(NotificationChannel.Slack);
            slackNotifier.Send("general", $"{memberName} joined {club.Name}");
        }

        public List<Club> GetAllClubs() => _clubRepository.GetAll();
        public List<Member> GetAllMembers() => _members;
        public List<Member> GetMembersByClubId(Guid clubId) => _members.Where(m => m.ClubId == clubId).ToList();
    }
}
