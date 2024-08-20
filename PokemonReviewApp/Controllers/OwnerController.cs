using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper, ICountryRepository countryRepository)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{onwerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int onwerId)
        {
            if (!_ownerRepository.OwnerExist(onwerId))
                return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(onwerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{onwerId}/ pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner( int onwerId)
        {
            if (!_ownerRepository.OwnerExist(onwerId))
                return NotFound();
            var pokemonByOwner = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonByOwner(onwerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemonByOwner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner( [FromQuery] int CountryId, [FromBody] OwnerDto CreateOwner)
        {
            if (CreateOwner == null)
                return BadRequest(ModelState);

            var Owner = _ownerRepository.GetOwners()
                .Where(c => c.LastName.Trim().ToUpper() == CreateOwner.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if( Owner != null)
            {
                ModelState.AddModelError("", "Owner  already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var OwnerMap = _mapper.Map<Owner>(CreateOwner);

            OwnerMap.Country = _countryRepository.GetCountry(CountryId);

            if (!_ownerRepository.CreateOwner(OwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully created");
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto ownerUpdate)
        {
            if (ownerUpdate == null)
                return BadRequest(ModelState);

            if (ownerId != ownerUpdate.Id)
                return BadRequest(ModelState);

            if (!_countryRepository.CountryExist(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var OwnerMap = _mapper.Map<Owner>(ownerUpdate);

            if (!_ownerRepository.UpdateOwner(OwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExist(ownerId))
                return NotFound();

            var ownerToDelete = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong on delete");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}

