using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.models;
using PokemonReviewApp.Services.Concrete;
using PokemonReviewApp.Services.IService;

namespace PokemonReviewApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly IMapper _mapper;


        public PokemonController(IPokemonService pokemonService, IMapper mapper)
        {
            _pokemonService = pokemonService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonService.GetPokemones());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);


        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type=typeof(PokemonDto))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int id)
        {
            if (!_pokemonService.IsPokemonExitst(id))
            {
                return NotFound();
            }
            var pokemon = _mapper.Map<PokemonDto>(_pokemonService.GetPokemon(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemon);
        }

        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int  id)
        {
            if (!_pokemonService.IsPokemonExitst(id))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var rating = _pokemonService.GetPokemonRatings(id);

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemon)
        {
            if(pokemon == null)
                return BadRequest(ModelState);
            var pokemonMap = _mapper.Map<Pokemon>(pokemon);

            //var pokemones = _pokemonService.GetPokemones()
            //    .Where(c => c.Name.Trim().ToUpper() == pokemonMap.Name.Trim().ToUpper())
            //    .FirstOrDefault();

            var pokemones = _pokemonService.GetPokemonTrimToUpper(pokemon);

                

            if (!_pokemonService.CreatePokemon(ownerId, catId, pokemonMap))
            {
                return StatusCode(500, ModelState);
            }

            if(pokemones != null)
            {
                ModelState.AddModelError("", "Already Exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok("Pokemon created");

        }
    }
}
