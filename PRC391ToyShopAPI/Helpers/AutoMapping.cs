using AutoMapper;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // DAO to DTO
            CreateMap<Toy, ToyModel>();
            
            CreateMap<Category, CategoryModel>();


            // DTO to DAO

            CreateMap<CreateToyViewModel, Toy>();
            
        }
        
    }
}
