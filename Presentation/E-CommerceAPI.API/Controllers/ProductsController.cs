using E_CommerceAPI.Application.Features.Commands.ProductImageFiles.DeleteProductImage;
using E_CommerceAPI.Application.Features.Commands.ProductImageFiles.UploadProductImage;
using E_CommerceAPI.Application.Features.Commands.Products.CreateProduct;
using E_CommerceAPI.Application.Features.Commands.Products.DeleteProduct;
using E_CommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using E_CommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImage;
using E_CommerceAPI.Application.Features.Queries.Products.GetAllProduct;
using E_CommerceAPI.Application.Features.Queries.Products.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //artık uzun uzun yazmaya son
        // application katmanında serviceRegistrationu tanımlamıstık ordan ilgil bagımlılıklar IOC ye eklenecek zaten
        private readonly IMediator _mediator;


        public ProductsController(IMediator mediator)
        {       
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
        {
            //FromQuery -> ?id=5 gibi
            // biz sende metodu kullanrak ona request nesnesini yolladık
            // artık mediator bizim application yaptıgımız tanımlamara gore IOC den de ilgil bagımlılı alarak handler çalıştırıcak
            GetAllProductQueryResponse response =  await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            //FromRoute -> .../.../5 gibi
            GetByIdProductQueryResponse reponse = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(reponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            _ = await _mediator.Send(createProductCommandRequest);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {

            uploadProductImageCommandRequest.FormFileCollection = Request.Form.Files;
            _ = await _mediator.Send(uploadProductImageCommandRequest);

            return Ok();
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductsImage([FromRoute] GetProductImagegQueryRequest getProductImagegQueryRequest)
        {
            List<GetProductImageQueryResponse> reponse = await _mediator.Send(getProductImagegQueryRequest);
            return Ok(reponse);

        }

        [HttpDelete("[action]/{ProductId}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            _ = await _mediator.Send(deleteProductImageCommandRequest);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            //fromBody direkt bodyden gelecek
            _ = await _mediator.Send(updateProductCommandRequest);

            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            _ = await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }


    }
}
