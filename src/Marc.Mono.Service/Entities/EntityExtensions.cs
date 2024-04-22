
using Marc.Mono.Service.Dtos;

namespace Marc.Mono.Service.Entities;

    public static class EntityExtensions
    {
        public static SportDto AsDto(this Sport sport)
        {
            return new SportDto
            (
                sport.Id,
                sport.Name,
                sport.PostDate,
                sport.ImageUri,
                sport.AdminId
            );
        }

        public static UserDto AsDto(this User user)
        {
            return new UserDto
            (
                user.Id,
                user.Name,
                user.CountryCode,
                user.RegisteredOn,
                user.PhoneNumber,
                user.Email,
                user.FavouriteSports
            );
        }

        public static AdminDto AsDto(this Admin admin)
        {
            return new AdminDto
            (
                admin.Id,
                admin.Username,
                admin.Password
            );
        }

    } 
