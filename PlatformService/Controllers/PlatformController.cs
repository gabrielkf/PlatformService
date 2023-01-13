using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }
        
        [HttpGet("all")]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            var allPlatforms = _platformRepo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(allPlatforms));
        }

        [HttpGet("{id:int}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _platformRepo.GetPlatformById(id);
            if (platform is null) return NotFound();
            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> AddPlatform(PlatformCreateDto createDto)
        {
            var platformModel = _mapper.Map<Platform>(createDto);
            _platformRepo.CreatePlatform(platformModel);
            if (!_platformRepo.SaveChanges()) return UnprocessableEntity();

            return CreatedAtRoute(nameof(GetPlatformById),
                new { Id = platformModel.Id },
                _mapper.Map<PlatformReadDto>(platformModel));
        }
    }
}