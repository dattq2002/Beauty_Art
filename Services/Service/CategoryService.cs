using AutoMapper;
using Services.Entity;
using Services.Model;
using Services.Service.Interface;

namespace Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategory(CategoryModel req)
        {
            Category category = new Category()
            {
                Id = req.Id,
                Name = req.Name,
                CreationDate = DateTime.Now,
                IsDeleted = false,
            };
            await _unitOfWork.categoryRepo.CreateAsync(category);
            await _unitOfWork.SaveChangeAsync();
            return category;
        }
        public async Task<bool> UpdateCategory(CategoryModel req, string id)
        {
            Category category = await _unitOfWork.categoryRepo.GetEntityByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            category.Name = req.Name;
            category.ModificationDate = DateTime.Now;
            _unitOfWork.categoryRepo.UpdateAsync(category);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0? true: false;
        }
        public async Task<bool> DeleteCategory(string id)
        {
            Category category = await _unitOfWork.categoryRepo.GetEntityByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            category.DeletionDate = DateTime.Now;
            category.IsDeleted = true;
            _unitOfWork.categoryRepo.DeleteAsync(category);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<Category> GetCategoryById(string id)
        {
            Category category = await _unitOfWork.categoryRepo.GetEntityByIdAsync(id);
            return category;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> categories = (await _unitOfWork.categoryRepo.GetAllAsync()).ToList();
            List<Category> result = new List<Category>();
            foreach(var item in categories)
            {
                if(item.IsDeleted == false)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
