using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Enums;
using PRC391ToyShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Repositories.Repository
{
    public interface IToyRepository
    {
        Task<List<Toy>> GetAllToys();
        Task<Toy> FindToyByID(int id);
        Task<int> CreateNewToy(Toy toy);
        Task<bool> UpdateToy(CreateToyViewModel model, int id);
        Task<bool> DeleteToy(int id);
   
        
        
    }

    public class ToyRepository : IToyRepository
    {
        private readonly PRC391_ToyShopContext _context;
        private readonly IMapper _mapper;

        public ToyRepository(PRC391_ToyShopContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
            
        }
        public async Task<List<Toy>> GetAllToys()
        {
            var result = await _context.Toys.Where(toy => toy.IsDeleted == false)
                .Include(data => data.Category)
                .ToListAsync();

            return result;
        }

        public async Task<Toy> FindToyByID(int id)
        {
            Toy toy = await _context.Toys.Include(c => c.Category)
                .Where(item => item.ToyId == id).FirstOrDefaultAsync();

            return toy;
        }
        public async Task<int> CreateNewToy(Toy model)
        {
             int id = SystemStatusCode.FAIL ;

            try
            {
                _context.Toys.Add(model);
               var response = await _context.SaveChangesAsync();
                if(response == SystemStatusCode.SUCCESS)
                {
                    id = model.ToyId;
                }
            } catch (DbUpdateException e)
            {
                id = SystemStatusCode.ERROR;
            }

            return id;

        }
        public async Task<bool> UpdateToy(CreateToyViewModel model, int id)
        {
            bool isUpdated = false;

            Toy toy = await _context.Toys.FindAsync(id);


            if (toy != null)
            {
                _context.Entry(toy).State = EntityState.Detached;
                toy = _mapper.Map<Toy>(model);
                toy.ToyId = id;
                _context.Entry(toy).State = EntityState.Modified;


                _context.Toys.Update(toy);

                var response = await _context.SaveChangesAsync();
                if (response == SystemStatusCode.SUCCESS)
                {
                    isUpdated = true;
                }

            }
            return isUpdated;
        }
        public async Task<bool> DeleteToy(int id)
        {
            bool isDeleted = false;

            Toy toy = await _context.Toys.FindAsync(id);

            if (toy != null)
            {
                toy.IsDeleted = true;
                var response = await _context.SaveChangesAsync();
                if (response == SystemStatusCode.SUCCESS)
                {
                    isDeleted = true;
                }

            }
            return isDeleted;
        }
    }
}
