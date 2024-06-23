using Contract_Administrator.Data;
using Contract_Administrator.Interfaces;
using Contract_Administrator.Models;
using Microsoft.EntityFrameworkCore;

namespace Contract_Administrator.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _context;

        public ContractRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Contract contract)
        {
            _context.Add(contract);
            return Save();
        }

        public bool Delete(Contract contract)
        {
            _context.Remove(contract);
            return Save();
        }

        /// <summary>
        /// Vrátí všechny smlouvy uložené v databázi.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Contract>> GetAll()
        {
            return await _context.Contract.Include(i => i.Client).Include(j => j.ContractAdviser).ToListAsync();
        }

        /// <summary>
        /// Vrátí všechny klienty v databázi.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _context.Client.ToListAsync();
        }

        /// <summary>
        /// Vrátí všechny poradce v databázi.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Adviser>> GetAllAdvisers()
        {
            return await _context.Adviser.ToListAsync();
        }

        /// <summary>
        /// Vrátí smlouvu podle zadaného id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contract> GetByIdAsync(int id)
        {
            return await _context.Contract.Include(c => c.Client).Include(ca => ca.ContractAdviser).FirstOrDefaultAsync(i => i.Id == id);
        }

        /// <summary>
        /// Vrátí smlouvu podle zadaného id s vazbami na provázané záznamy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contract> GetByIdWithAdvisersAndClientsAsync(int id)
        {
            return await _context.Contract.Include(ca => ca.ContractAdviser).ThenInclude(a => a.Adviser).Include(c => c.Client).FirstOrDefaultAsync(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Contract contract)
        {
            _context.Update(contract);
            return Save();
        }

    }
}
