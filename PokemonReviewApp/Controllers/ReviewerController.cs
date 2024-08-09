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
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewer = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);

        }


        [HttpGet("{reviewersId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewersId)
        {
            if (!_reviewerRepository.ReviewerExist(reviewersId))
                return NotFound();

            var reviewers = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewersId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);

        }

        [HttpGet("{reviewersId}/ reviews")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer( int reviewersId)
        {
            if (!_reviewerRepository.ReviewerExist(reviewersId))
                return NotFound();

            var reviewers = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewersId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewers);
        }

    }
}

