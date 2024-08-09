using System;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        bool OwnerExist(int id);

        Owner GetOwner(int ownerId);

        ICollection<Owner> GetOwnerOfAPokemon(int  pokeId);

        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
    }
}

