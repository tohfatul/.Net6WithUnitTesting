using AutoMapper;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>().ReverseMap();

        }
    }
}
