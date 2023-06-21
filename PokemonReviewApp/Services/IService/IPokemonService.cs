using PokemonReviewApp.Dtos;
using PokemonReviewApp.models;

namespace PokemonReviewApp.Services.IService
{
    public interface IPokemonService
    {
        ICollection<Pokemon> GetPokemones();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRatings(int id);
        bool IsPokemonExitst (int id);
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool Save();

        Pokemon GetPokemonTrimToUpper (PokemonDto pokemonDto);

    }
}
