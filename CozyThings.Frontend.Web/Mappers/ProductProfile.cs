using AutoMapper;
using CozyThings.Frontend.Web.Models;
using CozyThings.Frontend.Web.Models.Product;

namespace CozyThings.Frontend.Web.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();
            CreateMap<ProductViewModel, ProductCreateDto>();
            CreateMap<ProductViewModel, ProductUpdateDto>();
        }
    }
}
