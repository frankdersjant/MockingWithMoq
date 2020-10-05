using DAL;
using Domain;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class DivingService : IDivingService
    {
        private readonly IDiveRepository _diveRepository;
        public DivingService(IDiveRepository diveRepo)
        {
            _diveRepository = diveRepo;
        }

        public void AddDive(Dive dive)
        {
            _diveRepository.Add(dive);
        }

        public IEnumerable<Dive> GetAll()
        {
            return _diveRepository.GetAll();
        }
    }
}
