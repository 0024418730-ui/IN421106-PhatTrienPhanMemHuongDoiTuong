using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Repositories
{
    public interface IClubRepository
    {
        void Add(Club club);
        Club? GetById(Guid id);
        List<Club> GetAll();
    }
}
