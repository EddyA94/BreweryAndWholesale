﻿using BreweryWholesale.Domain.Models.DBO;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Infrastructure.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BrewerWholesaleDBContext _context;

        public BeerRepository(BrewerWholesaleDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            return await _context.Set<Beer>().ToListAsync();
        }

        public async Task<IEnumerable<Beer>> GetBeersByBreweryNameAndBeerNameAsync(int breweryID, string beerName)
        {
            return await _context.Set<Beer>().Where(W => W.BreweryID == breweryID && W.Name == beerName).ToListAsync();
        }

        public async Task<IEnumerable<Beer>> GetBeersByBeerIdAsync(int beerId)
        {
            return await _context.Set<Beer>().Where(W => W.BeerID == beerId).ToListAsync();
        }

        public async Task AddBeerAsync(Beer beer)
        {
            _context.Add<Beer>(beer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBeerAsync(Beer beer)
        {
            _context.Remove<Beer>(beer);
            await _context.SaveChangesAsync();
        }
    }
}
