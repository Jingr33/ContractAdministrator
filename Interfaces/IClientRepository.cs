using Contract_Administrator.Models;

namespace Contract_Administrator.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAll(); // všichni klienti
        Task<Client> GetByIdAsync(int id); // klient podle id
        Task<Client> GetByIdWithContractsAsync(int id); // klient podle id s propojením na smlouvy
        bool Add(Client client);
        bool Update(Client client);
        bool Delete(Client client);
        bool Save();
    }
}
