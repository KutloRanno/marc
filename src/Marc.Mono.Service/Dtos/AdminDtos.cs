using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Dtos;

public record AdminDto (
    Guid Id, string Username, string Password);

public record CreateAdminDto(
    [Required] string Username, [Required]string Password);

public record LoginAdminDto([Required]string Username, [Required]string Password);