using DAL.DbContext;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DiveRepository : EntityBaseRepository<Dive>, IDiveRepository
    {
        public DiveRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
