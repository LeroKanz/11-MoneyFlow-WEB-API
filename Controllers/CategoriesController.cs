using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VZ.MoneyFlow.Entities.DbSet;
using VZ.MoneyFlow.IData.IRepositories;
using VZ.MoneyFlow.Models.Models.Dtos.Requests;

namespace VZ.MoneyFlow.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseAuthController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, UserManager<IdentityUser> userManager) 
            : base(userManager)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var user = await GetUserAsync();

            var categories = await _categoryService.GetAllWithChildrenAsync(user.Id);
            
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var user = await GetUserAsync();

            var category = await _categoryService.GetByIdWithChildrenAsync(id, user.Id);
            if (category == null) return NotFound();
            if (category.UserId == user.Id) return Ok(category);
            return BadRequest("Provided data is invalid");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(RequestCreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Provided data is invalid");
            }

            var user = await GetUserAsync();

            var newCategory = _mapper.Map<Category>(categoryDto);
            newCategory.UserId = user.Id;                        

            var categories = await _categoryService.GetAllWithChildrenAsync(user.Id);
            var parentCategory = categories.FirstOrDefault(c => c.UserId == user.Id && c.Id == newCategory.ParentCategoryId);

            await _categoryService.AddAsync(newCategory);

            if (parentCategory != null && newCategory.ParentCategoryId == parentCategory.Id)
            {
                parentCategory.ChildrenCategories.Add(newCategory);
                await _categoryService.UpdateAsync(parentCategory.Id, parentCategory);
            }

            return Ok(newCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, RequestUpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Provided data is invalid");
            }
            if (categoryDto == null) return NotFound();

            var user = await GetUserAsync();

            var categories = await _categoryService.GetAllWithChildrenAsync(user.Id);
            var oldCategory = categories.FirstOrDefault(c => c.Id == id && c.UserId == user.Id);
            var updatedCategory = _mapper.Map(categoryDto, oldCategory);

            await _categoryService.UpdateAsync(id, updatedCategory);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var user = await GetUserAsync();

            var category = await _categoryService.GetByIdWithChildrenAsync(id, user.Id);

            if (category == null) return NotFound();
            if (category.UserId == user.Id)
            {
                await _categoryService.DeleteAsync(id);
                return Ok("Deleted successfully");
            }

            return BadRequest("Provided data is invalid");
        }
    }
}
