using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using HamburgerciProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Infrastructure.Repositories
{
    public class SiparisRepository : BaseRepository<Siparis>, ISiparisRepository
    {
        public SiparisRepository(AppDbContext context) : base (context)
        {
            
        }
    }
}
