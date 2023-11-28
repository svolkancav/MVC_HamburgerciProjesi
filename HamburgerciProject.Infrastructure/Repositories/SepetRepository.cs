using HamburgerciProject.Domain.Entities.Concrete;
using HamburgerciProject.Domain.Repositories;
using HamburgerciProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamburgerciProject.Infrastructure.Repositories
{
    public class SepetRepository : BaseRepository<Sepet>, ISepetRepository
    {
        private readonly AppDbContext _context;

        public SepetRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Sepet GetById(int id)
        {
            return _context.Set<Sepet>().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Sepet> GetSepetIncludeMenu(int userId)
        {
            return _context.Set<Sepet>().Where(x => x.UserId == userId).Include(x => x.Menu).ToList();
        }
    }
}
