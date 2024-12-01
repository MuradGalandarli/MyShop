using AutoMapper;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DtoProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();

            CreateMap<FavoriteProductDto, FavoriteProduct>();
            CreateMap<FavoriteProduct, FavoriteProductDto>();

            CreateMap<FeedbackScore, FeedbackScoreDto>();   
            CreateMap<FeedbackScoreDto, FeedbackScore>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<About, AboutDto>();
            CreateMap<AboutDto, About>();

            CreateMap<ContactDto, Contact>();
            CreateMap<Contact, ContactDto>();
        }
    }
}
