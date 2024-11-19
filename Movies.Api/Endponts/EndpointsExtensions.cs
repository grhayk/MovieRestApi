using Movies.Api.Endponts.Movies;
using Movies.Api.Endponts.Ratings;

namespace Movies.Api.Endponts;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapMovieEndpoints();
        app.MapRatingEndpoints();
        return app;
    }
}