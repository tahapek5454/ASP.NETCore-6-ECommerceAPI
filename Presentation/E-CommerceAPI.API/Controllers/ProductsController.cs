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
using Microsoft.AspNetCore.Authorization;
using E_CommerceAPI.Application.Features.Commands.ProductImageFiles.ChangeShowCase;
using E_CommerceAPI.Application.Consts;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Enums;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes ="Admin")]  // tum metodları yetkilendirir
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
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionTypes.Writing, Definition = "Add Product")]// bizim olusturdugumuz authorize
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            _ = await _mediator.Send(createProductCommandRequest);

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionTypes.Writing, Definition = "Upload Product Image")]// bizim olusturdugumuz authorize
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {

            uploadProductImageCommandRequest.FormFileCollection = Request.Form.Files;
            _ = await _mediator.Send(uploadProductImageCommandRequest);

            return Ok();
        }

        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionTypes.Reading, Definition = "Get Product Images")]// bizim olusturdugumuz authorize
        public async Task<IActionResult> GetProductsImage([FromRoute] GetProductImagegQueryRequest getProductImagegQueryRequest)
        {
            List<GetProductImageQueryResponse> reponse = await _mediator.Send(getProductImagegQueryRequest);
            return Ok(reponse);

        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes ="Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionTypes.Updating, Definition = "Change Show Case")]// bizim olusturdugumuz authorize
        public async Task<IActionResult> ChangeShowCase([FromQuery]ChangeShowCaseCommandRequest changeShowCaseCommandRequest)
        {
            ChangeShowCaseCommandResponse response =  await _mediator.Send(changeShowCaseCommandRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{ProductId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionTypes.Deleting, Definition = "Delete Product Image")]// bizim olusturdugumuz authorize
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            _ = await _mediator.Send(deleteProductImageCommandRequest);

            return Ok();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionTypes.Updating, Definition = "Update Product")]// bizim olusturdugumuz authorize
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            //fromBody direkt bodyden gelecek
            _ = await _mediator.Send(updateProductCommandRequest);

            return Ok();
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionTypes.Deleting, Definition = "Delete Product")]// bizim olusturdugumuz authorize
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            _ = await _mediator.Send(deleteProductCommandRequest);
            return Ok();
        }


    }
}
