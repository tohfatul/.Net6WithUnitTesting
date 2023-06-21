using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Controllers;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.models;
using PokemonReviewApp.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonReviewApp.Tests.Controller
{
    public class PokemonControllerTests
    {
        private readonly IPokemonService _pokemonService;
        private readonly IMapper _mapper;

        public PokemonControllerTests()
        {
            _pokemonService = A.Fake<IPokemonService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PokemonController_GetPokemons_ReturnOk()
        {
            //arrange 
            var pokemons = A.Fake<ICollection<PokemonDto>>();
            var pokemonList = A.Fake<List<PokemonDto>>();
            A.CallTo(() => _mapper.Map<List<PokemonDto>>(pokemons))
                .Returns(pokemonList);
            var controller = new PokemonController(_pokemonService, _mapper);
            //act
            var result = controller.GetPokemons();
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            //assert

        }

        [Fact]
        public void PokemonController_CreatePokemon_ReturnOk()
        {
            //arrange
            int ownerId = 1;
            int catId = 2;
            var pokemon = A.Fake<Pokemon>();

            var pokemonDto = A.Fake<PokemonDto>();
            var pokemons = A.Fake<ICollection<PokemonDto>>();
            var pokemonList = A.Fake<IList<PokemonDto>>();

            A.CallTo(() => _pokemonService.GetPokemonTrimToUpper(pokemonDto));

            A.CallTo(() => _mapper.Map<Pokemon>(pokemonDto)).Returns(pokemon);
            A.CallTo(() => _pokemonService.CreatePokemon(ownerId, catId, pokemon))
                .Returns(true);
            var controller = new PokemonController(_pokemonService, _mapper);
            //act
            var result = controller.CreatePokemon(ownerId, catId, pokemonDto);

            //assert
            result.Should().NotBeNull();

        }
    }
}
