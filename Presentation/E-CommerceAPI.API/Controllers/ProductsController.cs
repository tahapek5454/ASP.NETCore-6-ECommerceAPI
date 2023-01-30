using E_CommerceAPI.Application.Repositories.ProductRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(
                    new()
                    {
                        new()
                        {
                            CreateDate= DateTime.UtcNow,
                            Id= Guid.NewGuid(),
                            Name = "Bardak",
                            Price= 5,
                            Stock = 5
                        },
                        new()
                        {
                            CreateDate= DateTime.UtcNow,
                            Id= Guid.NewGuid(),
                            Name = "Tabak",
                            Price= 5,
                            Stock = 5
                        }
                    }
                );

            var count = await _productWriteRepository.SaveAsync();
        }
    }
}
