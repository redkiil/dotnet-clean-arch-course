using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, Product>
    {
        private readonly IProductRepository _repository;
        public ProductDeleteCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Product> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if(product == null)
            {
                throw new ApplicationException($"Entity could not be found!");
            }else{
                
                return await _repository.DeleteAsync(product);
            }
        }
    }
}