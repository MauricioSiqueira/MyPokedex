using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();

        Pokemon GetPokemon(int Id);

        Pokemon GetPokemon(string name);

        Pokemon GetPokemonTrimToUpper(PokemonDto pokemonCreate);

        decimal GetPokemonRating(int pokeId);

        bool PokemonExist(int pokeId);

        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);

        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon);

        bool DeletePokemon(Pokemon pokeId);

        bool Save();
    }
}

