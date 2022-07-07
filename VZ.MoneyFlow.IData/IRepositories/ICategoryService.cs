using System.Collections.Generic;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;

namespace VZ.MoneyFlow.IData.IRepositories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllWithChildrenAsync(string id);
        Task<Category> GetByIdWithChildrenAsync(int id, string userId);

        Task AddAsync(Category entity);
        Task UpdateAsync(int id, Category entity);
        Task DeleteAsync(int id);
    }
}
