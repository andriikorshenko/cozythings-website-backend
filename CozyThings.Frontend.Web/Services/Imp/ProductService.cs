using CozyThings.Frontend.Web.Models;
using CozyThings.Frontend.Web.Models.Product;

namespace CozyThings.Frontend.Web.Services.Imp
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IConfiguration configuration;

        public ProductService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpClientFactory)
        {
            this.configuration = configuration;
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = StaticDetails.ProductApiBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = StaticDetails.ProductApiBase + "/api/products/" + id,
                AccessToken = token
            });
        }

        public async Task<T> CreateProductAsync<T>(ProductCreateDto dto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = dto,
                Url = StaticDetails.ProductApiBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductUpdateDto dto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = dto,
                Url = StaticDetails.ProductApiBase + "/api/products",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = StaticDetails.ProductApiBase + "/api/products/" + id,
                AccessToken = token
            });
        }  
    }
}
