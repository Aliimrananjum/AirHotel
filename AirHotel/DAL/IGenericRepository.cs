using AirHotel.Models;
namespace AirHotel.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetEntitylById(int id);
        Task Create(T entity);   
        Task Update(T entity);
        Task<bool> Delete(int id);
    }
}
