using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Entities;

public class Admin:IEntity
{
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}