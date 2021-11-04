using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using MIW_ProductService.Api.Mappers;
using MIW_ProductService.Core.Services.Interfaces;

namespace MIW_ProductService.Api.Controllers
{
    public class ProductController : ProductService.ProductServiceBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public override async Task<ProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Create Product invoked");
            try
            {
                return ProductMapper.ProductToProductResponse(
                    await _productService.Create(ProductMapper.CreateProductRequestToProduct(request)));
            }
            catch (Exception e)
            {
                _logger.LogError("{E}", e);
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }

        public override async Task<ProductResponse> GetSingleProduct(GetSingleProductRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Get Single Product invoked");
            try
            {
                return ProductMapper.ProductToProductResponse(
                    await _productService.Get(request.Id));
            }
            catch (Exception e)
            {
                _logger.LogError("{E}", e);
                throw new RpcException(new Status(StatusCode.Internal, e.Message));
            }
        }
    }
}