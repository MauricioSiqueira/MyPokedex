using System;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int id)// eu qro todas as reviews de um pokemon
        {
            return _context.Reviews.Where(p => p.Pokemon.Id == id).ToList();
        }

        public bool ReviewExist(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }
    }
}

