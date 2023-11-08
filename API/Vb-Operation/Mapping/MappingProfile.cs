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
            CreateMap<Company, CompanyResponseShort>();

            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();

            CreateMap<DealerRequest, Dealer>();
            CreateMap<DealerServiceRequest, Dealer>();
            CreateMap<Dealer, DealerResponse>();
            CreateMap<Dealer, DealerResponseShort>();

            CreateMap<InvoiceRequest, Invoice>();
            CreateMap<Invoice, InvoiceResponse>();

            CreateMap<PaymentRequest, Payment>();
            CreateMap<Payment, PaymentResponse>();
            CreateMap<Payment, PaymentResponseShort>();

            CreateMap<OrderDetailRequest, OrderDetail>();
            CreateMap<OrderDetail, OrderDetailResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Order.OrderNumber));

            CreateMap<OrderRequest, Order>();
            CreateMap<Order, OrderResponse>();
            CreateMap<Order, OrderResponseShort>();

        }
    }
}
