using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Application.RequestParameters;
using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
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
        private readonly IFileService _fileService;

        // wwwroot un pathine ulasmak için -> statik filelara erisim saglar
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            int totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate
            }).Skip(pagination.Size * pagination.Page).Take(pagination.Size).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_CreateProduct model)
        {
            _ = await _productWriteRepository.AddAsync(new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });

            _ = await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("resource\\product-images", Request.Form.Files);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_UpdateProduct model)
        {
            Product updatedProduct = await _productReadRepository.GetByIdAsync(model.Id);
            updatedProduct.Name = model.Name;
            updatedProduct.Price = model.Price;
            updatedProduct.Stock = model.Stock;
 
            var a = await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var a = await _productWriteRepository.RemoveAsync(id);
            var b = await _productWriteRepository.SaveAsync();
            return Ok();
        }


    }
}
