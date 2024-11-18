using System.Text.Json;
using Movies.Api.Sdk;
using Refit;

var moviesApi = RestService.For<IMoviesApi>("http://localhost:5010");

var movie = await moviesApi.GetMoviesAsync("first-film-2024");

Console.WriteLine(JsonSerializer.Serialize(movie));