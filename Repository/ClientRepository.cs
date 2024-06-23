using Contract_Administrator.Data;
using Contract_Administrator.Interfaces;
using Contract_Administrator.Models;
using Microsoft.EntityFrameworkCore;

namespace Contract_Administrator.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Client client)
        {
            _context.Add(client);
            return Save();

        }

        public bool Delete(Client client)
        {
            _context.Remove(client);
            return Save();
        }

        /// <summary>
        /// Vrátí všechny klienty v databázi.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Client.ToListAsync();
        }

        /// <summary>
        /// Vrátí klienta podle id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Client.FirstOrDefaultAsync(i => i.Id == id);
        }

        /// <summary>
        /// Vrátí klienta podle id s vazbou na smlouvu.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Client> GetByIdWithContractsAsync(int id)
        {
            return await _context.Client.Include(c => c.Contracts).FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Client client)
        {
            _context.Update(client);
            return Save();
        }
    }
}
