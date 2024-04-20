
using Marc.Mono.Service.Dtos;

namespace Marc.Mono.Service.Entities;

    public static class EntityExtensions
    {
        public static SportDtoV1 AsDtoV1(this Sport sport)
        {
            return new SportDtoV1
            (
                sport.Id,
                sport.Name,
                sport.PostDate,
                sport.ImageUri
            );
        }

    } 
