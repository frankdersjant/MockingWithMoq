using Domain;
using System.Collections.Generic;

namespace ServiceLayer
{
    public interface IDivingService
    {
        IEnumerable<Dive> GetAll();
        void AddDive(Dive dive);
    }
}
