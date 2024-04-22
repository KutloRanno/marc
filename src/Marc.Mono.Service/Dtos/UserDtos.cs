using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Dtos;

public record UserDto (
    Guid Id, string Name, string CountryCode, DateTimeOffset RegisteredOn, int PhoneNumber, string Email, 
    List<string> FavouriteSports);

public record CreateUserDto(
   [Required] string Name, [Required]string CountryCode, [Required]int PhoneNumber,[Required][EmailAddress] string Email,
   List<string> FavouriteSportIds
);