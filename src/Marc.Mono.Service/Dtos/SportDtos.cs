using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Dtos;

public record SportDto
(
    Guid Id, 
    [Required][StringLength(50)]string Name,
    DateTimeOffset PostDate,
    string ImageUri
);
public record UpdateSportDto
(    
    [Required][StringLength(50)] string Name,
    [Url][StringLength(100)] string? ImageUri
);
public record CreateSportDto
(
[Required][StringLength(50)]string Name, 
string ImageUri
);

