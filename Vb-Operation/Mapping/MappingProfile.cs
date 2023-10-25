using AutoMapper;
using Vb_Data.Domain;
using Vb_Data.Domain.User;
using Vb_DTO;

namespace Vb_Operation.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponse>();

            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();

            CreateMap<DealerRequest, Dealer>();
            CreateMap<Dealer, DealerResponse>();

            CreateMap<InvoiceDetailRequest, InvoiceDetail>();
            CreateMap<InvoiceDetail, InvoiceDetailResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<OrderRequest, Order>();
            CreateMap<Order, OrderResponse>();

        }
    }
}
