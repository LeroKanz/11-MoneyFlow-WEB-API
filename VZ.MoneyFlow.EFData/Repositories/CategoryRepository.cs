﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VZ.MoneyFlow.EFData.Data;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.IData.IRepositories;

namespace VZ.MoneyFlow.EFData.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext options) : base(options)
        {
            _context = options;
        }

        public async Task<List<Category>> GetAllWithChildrenAsync(string id)
        {
            var result = await _context.Categories.Where(c => c.UserId == id).ToListAsync();
            return result;
        }

        public async Task<Category> GetByIdWithChildrenAsync(int id, string userId)
        {
            var result = await _context.Categories.Where(c => c.UserId == userId).Include(ch => ch.ChildrenCategories).FirstOrDefaultAsync(acc => acc.Id == id);
            return result;
        }
    }
}
