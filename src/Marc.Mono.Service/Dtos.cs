using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Dtos;

public record SportDtoV1
(
int Id, 
[Required][StringLength(50)]string Name,
DateTime PostDate,
string ImageUri
);
public record UpdateSportDto
(    
    [Required][StringLength(50)] string Name,
    DateTime PostDate,
    [Url][StringLength(100)] string? ImageUri
);
public record CreateSportDto
(
[Required][StringLength(50)]string Name, 
string ImageUri
);

