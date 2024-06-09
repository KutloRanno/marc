using Marc.Mono.Service.Dtos;
using Marc.Mono.Service.Entities;
using Marc.Mono.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Marc.Mono.Service.Controllers;

[ApiController]
[Route("admin")]

public class AdminController (IRepository<Admin> adminRepository) : ControllerBase
{
    private  readonly IRepository<Admin> _adminRepository = adminRepository;

    [HttpPost("register")]
    public async Task<ActionResult<AdminDto>> CreateAdminAsync(CreateAdminDto adminDto)
    {
        Admin admin = new()
        {
            Id = Guid.NewGuid(),
            Username = adminDto.Username,
            Password = adminDto.Password
        };

        await _adminRepository.CreateAsync(admin);

        return CreatedAtAction(nameof(GetAdminByIdAsync), new { id = admin.Id }, admin.AsDto());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<AdminDto>> GetAdminByIdAsync(Guid id)
    {
        var admin = await _adminRepository.GetAsync(id);

        if(admin is null) return NotFound();

        return admin.AsDto();
    }

    [HttpPost("login")]
    public async Task<ActionResult<AdminDto>> LoginAdminAsync(LoginAdminDto loginDto)
    {
        var admin = await _adminRepository.GetAsync(admin=>admin.Username==loginDto.Username);

        if (admin == null||admin.Password != loginDto.Password) return Unauthorized();

        return Ok(admin.AsDto());
    }
}