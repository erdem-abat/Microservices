using Microservices.Web.Models;

namespace Microservices.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductAsync(string ProductCode);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductsAsync(ProductDto ProductDto);
        Task<ResponseDto?> UpdateProductsAsync(ProductDto ProductDto);
        Task<ResponseDto?> DeleteProductsAsync(int id);
    }
}