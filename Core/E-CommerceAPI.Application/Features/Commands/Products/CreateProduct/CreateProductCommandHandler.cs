using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _repository;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductHubService _productHubService;
        public CreateProductCommandHandler(IProductWriteRepository repository, ILogger<CreateProductCommandHandler> logger, IProductHubService productHubService)
        {
            _repository = repository;
            _logger = logger;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
  
            _ = await _repository.AddAsync(new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });

            _ = await _repository.SaveAsync();
            // add isleminde dondermeye gerek yok
            _logger.LogInformation("New Product Added");
            await _productHubService.ProductAddedMesageAsync($"{request.Name} isminde bir urun eklenmistir");
            return new();
        }
    }
}
