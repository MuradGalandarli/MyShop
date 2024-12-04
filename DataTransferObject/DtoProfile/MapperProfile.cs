using AutoMapper;
using DataTransferObject.DtoEntity;
using DataTransferObject.ResponseDto;
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

            CreateMap<Category, ResponseCategory>();
            CreateMap<ResponseCategory, Category>();

            CreateMap<ResponseComment, Comment>();
            CreateMap<Comment, ResponseComment>();

            CreateMap<ResponseFavoriteProduct, FavoriteProduct>();
            CreateMap<FavoriteProduct, ResponseFavoriteProduct>();

            CreateMap<FeedbackScore, ResponseFeedbackScore>();
            CreateMap<ResponseFeedbackScore, FeedbackScore>();

            CreateMap<Order, ResponseOrder>();
            CreateMap<ResponseOrder, Order>();

            CreateMap<ResponseProduct, Product>();
            CreateMap<Product, ResponseProduct>();

            CreateMap<User, ResponseUser>();
            CreateMap<ResponseUser, User>();

            CreateMap<About, ResponseAbout>();
            CreateMap<ResponseAbout, About>();

            CreateMap<ResponseContact, Contact>();
            CreateMap<Contact, ResponseContact>();
        }
    }
}
