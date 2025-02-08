using RunGroupWebApp.Models;
using System.Threading.Tasks;

namespace RunGroupWebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race?> GetByIdAsync(int id);
        Task<Race?> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Race>> GetAllRacesByCity(string city);
        bool Add(Race race);
        bool Update(Race club);
        bool Delete(Race club);
        bool Save();
    }
}
