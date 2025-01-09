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
    public interface IDirectorsService
    {
        public IQueryable<DirectorsModel> Query();

        public ServiceBase Create(Directors record);
        public ServiceBase Update(Directors record);
        public ServiceBase Delete(int id);
    }
    public class DirectorsService : ServiceBase , IDirectorsService
    {
        public DirectorsService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Directors record)
        {
            if (_db.Directors.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim() && s.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Director with the same name exists!");
            record.Name = record.Name?.Trim();
            record.Surname = record.Surname?.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges();
            return Success("Directors created successfully");
        }
        public ServiceBase Update(Directors record)
        {
            if (_db.Directors.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim() && s.Surname.ToUpper() == record.Surname.ToUpper().Trim()))
                return Error("Director with the same name exists!");
            var entity = _db.Directors.Find(record.Id);
            if (entity == null)
                return Error("Directors cant found"!);
            entity.Name = record.Name?.Trim();
            entity.Surname = record.Surname?.Trim();
            _db.Directors.Update(entity);
            _db.SaveChanges();
            return Success("Directors updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(s => s.Movies).SingleOrDefault(s => s.Id == id);
            if (entity == null)
                return Error("Directors cant found!");
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Directors deleted successfully");
        }

        public IQueryable<DirectorsModel> Query()
        {
            return _db.Directors.OrderBy(s => s.Name).ThenBy(s => s.Surname).Select(s => new DirectorsModel() { Record = s });
        }
    }
}
