using Contract_Administrator.Models;

namespace Contract_Administrator.Interfaces
{
    public interface IAdviserRepository
    {
        Task<IEnumerable<Adviser>> GetAll(); // všichni poradci
        Task<Adviser> GetByIdAsync(int id); // poradce podle id
        Task<Adviser> GetByIdWithContractsAsync(int id); // poradce podle id s propojením na smlouvy
        bool Add(Adviser client);
        bool Update(Adviser client);
        bool Delete(Adviser client);
        bool Save();

    }
}
