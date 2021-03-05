using AutoMapper;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Repositories.Repository;
using PRC391ToyShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Services
{
    public interface IToyService
    {
        Task<List<ToyModel>> GetAllToys();
        Task<ToyModel> FindToyByID(int id);
        Task<int> CreateNewToy(CreateToyViewModel model);
        Task<bool> UpdateToy(CreateToyViewModel model, int id);
        Task<bool> DeleteToy(int id);
    }

    public class ToyService : IToyService
    {
        private readonly IToyRepository _toy;
        private readonly IMapper _mapper;

        public ToyService(IToyRepository toy, IMapper mapper)
        {
            _toy = toy;
            _mapper = mapper;
        }

        public async Task<int> CreateNewToy(CreateToyViewModel model)
        {
            Toy toy = _mapper.Map<Toy>(model);

            toy.IsDeleted = false;

            int id = await _toy.CreateNewToy(toy);

            return id;
        }

        public async Task<bool> DeleteToy(int id)
        {
            bool status = await _toy.DeleteToy(id);

            return status;
        }

        public async Task<ToyModel> FindToyByID(int id)
        {
            var toy = await _toy.FindToyByID(id);
            if(toy == null)
            {
                return null;
            } else
            {
                var model = _mapper.Map<ToyModel>(toy);
                return model;
            }
        }

        public async Task<List<ToyModel>> GetAllToys()
        {
            var result = await _toy.GetAllToys();

            List<ToyModel> list = new List<ToyModel>();
            
            for(int i =0; i < result.Count(); i++)
            {
                var model = _mapper.Map<ToyModel>(result[i]);

                list.Add(model);
            }
         

            return list;
        }

        public async Task<bool> UpdateToy(CreateToyViewModel model, int id)
        {
            bool isUpdated = await _toy.UpdateToy(model, id);

            return isUpdated;
        }
    }
}
