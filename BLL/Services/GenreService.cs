﻿using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GenreService : ServiceBase,IService<Genre, GenreModel>
    {
        public GenreService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Genre record)
        {
            throw new NotImplementedException();
        }

        public ServiceBase Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GenreModel> Query()
        {
            return _db.Genres.OrderBy(o => o.Name).Select(o => new GenreModel() { Record = o});
        }

        public ServiceBase Update(Genre record)
        {
            throw new NotImplementedException();
        }
    }
}
