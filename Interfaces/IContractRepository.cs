using Contract_Administrator.Models;

namespace Contract_Administrator.Interfaces
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAll(); // všechny smlouvy
        Task<IEnumerable<Client>> GetAllClients(); // všichni klienti
        Task<IEnumerable<Adviser>> GetAllAdvisers(); // všichni poradci
        Task<Contract> GetByIdAsync(int id); // smlouva podle id
        Task<Contract> GetByIdWithAdvisersAndClientsAsync(int id); // smlouva podle id s vazbami
        bool Add(Contract contract);
        bool Update(Contract contract);
        bool Delete(Contract contract);
        bool Save();

    }
}
