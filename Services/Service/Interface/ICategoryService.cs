using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(CategoryModel req);
        Task<bool> UpdateCategory(CategoryModel req, string id);
        Task<bool> DeleteCategory(string id);
        Task<Category> GetCategoryById(string id);
        Task<List<Category>> GetCategoriesAsync();
    }
}
