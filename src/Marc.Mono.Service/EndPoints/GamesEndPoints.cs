// using System.Text.RegularExpressions;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Marc.Mono.Service.Dtos;
// using Marc.Mono.Service.Entities;
// using Marc.Mono.Service.Repositories;
// // using Microsoft.AspNetCore.Mvc.Versioning;

// namespace Marc.Mono.Service.EndPoints;

//     public static class SportsEndPoints
//     {
//         const string GetSportsV1EndPoint = "GetSportsV1";
//         const string PlaceHolderImageUri = "https://placehold.co/100";


//         public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes)
//         {
//             var group = routes.MapGroup("/sports")
//                                         .WithTags("Sports")
//                                         .WithDescription("Endpoints for sports operations");
                                                

//                             // V1 GET ENDPOINTS
//         group.MapGet("/", async (
//             ISportsRepository repository,
//             HttpContext http) =>
//         {
//             // var totalCount = await repository.CountAsync(request.Filter);
//             // http.Response.AddPaginationHeader(totalCount, request.PageSize);

//             return Results.Ok((await repository.GetAllAsync())
//                                                 .Select(sport => sport.AsDtoV1()));
//         })
//         .WithSummary("Gets all sports")
//         .WithDescription("Gets all available sports without any special filter");
        

//         group.MapPost("/", async Task<CreatedAtRoute<SportDtoV1>> (ISportsRepository repository, CreateSportDto sportDto) =>
//         {
//             Sport sport = new()
//             {
//                 Name = sportDto.Name,
//                 PostDate = DateTimeOffset.UtcNow,
//                 ImageUri = sportDto.ImageUri ?? PlaceHolderImageUri
//             };

//             await repository.CreateAsync(sport);
//             return TypedResults.CreatedAtRoute(sport.AsDtoV1(), GetSportsV1EndPoint, new { id = sport.Id });
//         })

//         .WithSummary("Creates a new sport")
//         .WithDescription("Creates a new sport with the specified properties");  

//         return group;
//     }
    
// }
