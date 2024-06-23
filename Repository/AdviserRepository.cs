using Contract_Administrator.Data;
using Contract_Administrator.Interfaces;
using Contract_Administrator.Models;
using Microsoft.EntityFrameworkCore;

namespace Contract_Administrator.Repository
{
    public class AdviserRepository : IAdviserRepository
    {
        private readonly ApplicationDbContext _context;

        public AdviserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Adviser adviser)
        {
            _context.Add(adviser);
            return Save();

        }

        public bool Delete(Adviser adviser)
        {
            _context.Remove(adviser);
            return Save();
        }

        /// <summary>
        /// Vrátí všechny poradce v databázi.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Adviser>> GetAll()
        {
            return await _context.Adviser.Include(i => i.ContractAdviser).ToListAsync();
        }

        /// <summary>
        /// Vrátí poradce podle zadaného id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Adviser> GetByIdAsync(int id)
        {
            return await _context.Adviser.Include(i => i.ContractAdviser).FirstOrDefaultAsync(i => i.Id == id);
        }

        /// <summary>
        /// Vrátí poradce podle zadaného id s vazbami na smlouvy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Adviser> GetByIdWithContractsAsync(int id)
        {
            return await _context.Adviser.Include(c => c.ContractAdviser).ThenInclude(ca => ca.Contract).FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Adviser adviser)
        {
            _context.Update(adviser);
            return Save();
        }
    }
}
