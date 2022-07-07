using System.Collections.Generic;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.IData.IRepositories;

namespace VZ.MoneyFlow.EFData.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllWithChildrenAsync(string id)
        {
            var result = await _categoryRepository.GetAllWithChildrenAsync(id);
            return result;

        }

        public async Task<Category> GetByIdWithChildrenAsync(int id, string userId)
        {
            var result = await _categoryRepository.GetByIdWithChildrenAsync(id, userId);
            return result;
        }


        public async Task AddAsync(Category entity)
        {
            await _categoryRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(int id, Category entity)
        {
            await _categoryRepository.UpdateAsync(id, entity);
        }
    }
}
