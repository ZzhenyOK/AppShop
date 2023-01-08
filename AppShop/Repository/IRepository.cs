using AppShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppShop.Repository
{
    public interface IRepository<T> where T :  BaseModel
    {
        //Create
        Task<T> AddAsync(T model);

        //Read
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        //Update
        Task<T> UpdateAsync(int id, T model);

        //Delete
        Task DeleteAsync(int id);
    }
}
