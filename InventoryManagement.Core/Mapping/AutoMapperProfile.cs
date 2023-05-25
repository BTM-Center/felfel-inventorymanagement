using AutoMapper;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Dtos.WrapperDtos;
using InventoryManagement.Data.Entities;
using Shared.Enums;

namespace InventoryManagement.Core.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDelivery, CustomerDeliveryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderLine, OrderLineDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductBatch, ProductBatchDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            
            CreateMap<ProductBatchWithFreshnessStatusDto, (ProductBatch, ProductBatchFreshnessStatus)>().ReverseMap()
                .ForPath(dest => dest.ProductBatch, opt => opt.MapFrom(src => src.Item1))
                .ForPath(dest => dest.FreshnessStatus, opt => opt.MapFrom(src => src.Item2));

            CreateMap<ProductBatchHistoryDto, (Order, IList<CustomerDelivery>)>().ReverseMap()
                .ForPath(dest => dest.Order, opt => opt.MapFrom(src => src.Item1))
                .ForPath(dest => dest.CustomerDeliveries, opt => opt.MapFrom(src => src.Item2));
            
            CreateMap<CreateOrderDto, Order>()
                .ForPath(dest => dest.Supplier.Id, opt => opt.MapFrom(src => src.SupplierId));
            
            CreateMap<CreateOrderLineDto, OrderLine>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId));
            
            CreateMap<CreateCustomerDeliveryDto, CustomerDelivery>()
                .ForPath(dest => dest.Customer.Id, opt => opt.MapFrom(src => src.CustomerId))
                .ForPath(dest => dest.ProductBatch.Id, opt => opt.MapFrom(src => src.ProductBatchId));
            
            CreateMap<UpdateProductBatchDto, ProductBatch>();
        }
    }
}
