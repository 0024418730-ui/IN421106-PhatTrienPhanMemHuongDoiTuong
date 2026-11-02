using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Repositories
{
    public class InMemoryClubRepository : IClubRepository
    {
        private readonly List<Club> _clubs = new();

        public void Add(Club club) => _clubs.Add(club);
        public Club? GetById(Guid id) => _clubs.FirstOrDefault(c => c.Id == id);
        public List<Club> GetAll() => _clubs.ToList();
    }
}
