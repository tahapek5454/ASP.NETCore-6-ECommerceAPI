using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product updatedProduct = await _productReadRepository.GetByIdAsync(request.Id);
            updatedProduct.Name = request.Name;
            updatedProduct.Price = request.Price;
            updatedProduct.Stock = request.Stock;

           _= await _productWriteRepository.SaveAsync();

            _logger.LogInformation("Product Updated");

            return new();
        }
    }
}
