﻿using MoviesApi.Models;

namespace MoviesApi.Sevices
{
    public interface IMoviesServices
    {
        Task<IEnumerable<Movie>> GetAll(byte genraId=0);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);      
        Movie Update(Movie movie);
        Movie Delete(Movie movie);
    }
}
