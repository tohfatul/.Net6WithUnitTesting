using PokemonReviewApp.data;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.models;
using PokemonReviewApp.Services.IService;

namespace PokemonReviewApp.Services.Concrete
{
    public class PokemonService : IPokemonService
    {
        private readonly DataContext _context;
        public PokemonService(DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.PokemonSet.Where(p => p.Id == id).FirstOrDefault();

        }

        public Pokemon GetPokemon(string name)
        {
            return _context.PokemonSet.Where(p=>p.Name == name).FirstOrDefault();

        }

        public ICollection<Pokemon> GetPokemones()
        {
            return _context.PokemonSet.OrderBy(p => p.Id).ToList();

        }

        public decimal GetPokemonRatings(int id)
        {
            var review = _context.ReviewSet.Where(p=>p.Pokemon.Id == id);

            if (review.Count() <=0)
                return 0;

            return (decimal) review.Sum(r=>r.Rating)/review.Count();


        }

        public bool IsPokemonExitst(int id)
        {
            return _context.PokemonSet.Any(p => p.Id == id);
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _context.OwnerSet.Where(a=>a.Id == ownerId).FirstOrDefault();
            var category = _context.CategorySet.Where(a=>a.Id==categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };
            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);

            _context.Add(pokemon);
            return Save();

        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >0 ? true: false;
        }

        public Pokemon GetPokemonTrimToUpper(PokemonDto pokemonDto)
        {
            return 
                GetPokemones()
                .Where(c => c.Name.Trim().ToUpper() == pokemonDto.Name.Trim().ToUpper())
                .FirstOrDefault();

        }
    }
}
