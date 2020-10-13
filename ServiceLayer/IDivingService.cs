using Domain;
using System.Collections.Generic;

namespace ServiceLayer
{
    public interface IDivingService
    {
        IEnumerable<Dive> GetAll();
        Dive GetDive(int Id);
        void AddDive(Dive dive);
    }
}
