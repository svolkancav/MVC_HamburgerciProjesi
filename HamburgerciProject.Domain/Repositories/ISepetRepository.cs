using HamburgerciProject.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Domain.Repositories
{
    public interface ISepetRepository : IBaseRepository<Sepet>
    {
        Sepet GetById(int id);
        List<Sepet> GetSepetIncludeMenu(int userId);
    }
}
