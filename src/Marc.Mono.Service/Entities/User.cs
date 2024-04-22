using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Entities;

public class User:IEntity
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name;
    public string CountryCode { get; set; }
    public DateTimeOffset RegisteredOn { get; set; }
    public int PhoneNumber { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    public List<string> FavouriteSports { get; set; }

}