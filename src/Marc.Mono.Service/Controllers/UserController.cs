using Marc.Mono.Service.Dtos;
using Marc.Mono.Service.Entities;
using Marc.Mono.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Marc.Mono.Service.Controllers;

[ApiController]
[Route("users")]
public class UserController(IRepository<User> userRepository) : ControllerBase
{
    private readonly IRepository<User> _userRepository = userRepository;

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto userDto)
    {
        User user = new()
        {
            CountryCode = userDto.CountryCode,
            PhoneNumber = userDto.PhoneNumber,
            Email = userDto.Email,
            RegisteredOn = DateTimeOffset.UtcNow,
            FavouriteSports = userDto.FavouriteSportIds.Select(id => id)
                                                        .ToList()
        };

        await _userRepository.CreateAsync(user);

        return CreatedAtAction(nameof(GetByIdAsync),new {id=user.Id},user);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);

        if(user == null) return NotFound();

        return user.AsDto();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserDto>>> GetAllUsersAsync()
    {
        var users= (await _userRepository.GetAllAsync())
           .Select(user => user.AsDto());
        return Ok(users);
    }
}