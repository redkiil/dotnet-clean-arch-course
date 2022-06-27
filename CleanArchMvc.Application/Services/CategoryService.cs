using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }
        public async Task Add(CategoryDTO categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);
            await _repository.CreateAsync(entity);
        }

        public async Task Delete(int? id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var cat = await _repository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(cat);
        }

        public async Task Update(CategoryDTO categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);
            await _repository.UpdateAsync(entity);
        }
    }
}