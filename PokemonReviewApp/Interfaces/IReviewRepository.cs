using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int id);

        ICollection<Review> GetReviewsOfAPokemon(int id);

        bool ReviewExist(int id);

        bool CreateReview(Review review);

        bool UpdateReview(Review review);

        bool DeleteReview(Review reviewId);

        bool Save();
    }
}

