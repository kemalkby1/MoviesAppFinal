using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MovieService : ServiceBase, IService<Movie, MovieModel>
    {
        public MovieService(Db db) : base(db)
        {
        }

        public IQueryable<MovieModel> Query()
        {
            
            return _db.Movies
                      .Include(m => m.Director) 
                      .Include(p=>p.MovieGenres)
                      .ThenInclude(mg=>mg.Genre)
                      .OrderBy(p => p.ReleaseDate)
                      .ThenBy(p => p.Name)
                      .Select(p => new MovieModel() { Record = p });
        }

        public ServiceBase Create(Movie record)
        {
            if (_db.Movies.Any(p => p.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movie with the same name exists!");
            record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges();
            return Success("Movie created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.SingleOrDefault(p => p.Id == id);
            if (entity == null)
                return Error("Movie can't be found!");
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("Movie deleted successfully");
        }

        public ServiceBase Update(Movie record)
        {
            if (_db.Movies.Any(p => p.Id != record.Id && p.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Movie with the same name exists!");
            var entity = _db.Movies.Include(p=> p.MovieGenres).SingleOrDefault(p => p.Id == record.Id);
            if (entity == null)
                return Error("Movie not found");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.Name = record.Name?.Trim();
            entity.ReleaseDate = record.ReleaseDate;
            entity.TotalRevenue = record.TotalRevenue;
            entity.DirectorId = record.DirectorId;
            entity.MovieGenres = record.MovieGenres;
            _db.Movies.Update(entity);
            _db.SaveChanges();
            return Success("Movie updated successfully.");
        }
    }
}
