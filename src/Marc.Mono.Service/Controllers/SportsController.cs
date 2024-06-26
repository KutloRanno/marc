using System.ComponentModel;
using Marc.Mono.Service.Dtos;
using Marc.Mono.Service.Entities;
using Marc.Mono.Service.Logging;
using Marc.Mono.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Marc.Mono.Service.Controllers;

[ApiController]
[Route("sports")]
public class SportsController(IRepository<Sport> sportsRepository,ILogger<SportsController> logger) : ControllerBase
{
    private readonly IRepository<Sport> _sportsRepository = sportsRepository;
    private readonly ILogger _logger=logger;

    [HttpPost]
    public async Task<ActionResult<SportDto>> CreateSportAsync(CreateSportDto sportDto)
    {
        Sport sport = new()
        {
            Name = sportDto.Name,
            PostDate = DateTimeOffset.UtcNow,
            ImageUri = sportDto.ImageUri ?? "https://placehold.co/100",
            AdminId = sportDto.AdminId
        };

        await _sportsRepository.CreateAsync(sport);

        _logger.LogInformation("Hey Jude");

        return CreatedAtAction(nameof(GetSportByIdAsync), new { id = sport.Id }, sport);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<SportDto>>> GetSportsAsync()
    {
        var sports = (await _sportsRepository.GetAllAsync())
           .Select(sport => sport.AsDto());

        var time = DateTimeOffset.UtcNow; 

        _logger.LogInformation("The sports collection was accessed on {DateofAccess}",time);

        return  Ok(sports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SportDto>> GetSportByIdAsync(Guid id)
    {
        var sport = await _sportsRepository.GetAsync(id);

        if (sport == null) return NotFound();

        return sport.AsDto();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSportAsync(Guid id)
    {
        var itemToDelete = await _sportsRepository.GetAsync(id);

        if(itemToDelete is null) return NotFound();

        await _sportsRepository.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSportAsync(Guid id, UpdateSportDto updatedSportDto)
    {
        var existingSport = await _sportsRepository.GetAsync(id);

        if(existingSport is null) return NotFound();

        existingSport.Name = updatedSportDto.Name;
        existingSport.ImageUri = updatedSportDto.ImageUri?? existingSport.ImageUri;

        await _sportsRepository.UpdateAsync(existingSport);

        var upDated = await _sportsRepository.GetAsync(id);

        return Ok(upDated.AsDto());

        // return NoContent();
    }

}