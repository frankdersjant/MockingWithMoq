using Domain;
using System.Collections.Generic;

namespace ConsoleAppDALMoq.DAL
{
    public interface IDAL
    {
        IEnumerable<Customer> GetAll();
        Customer FindById(int id);
    }
}
