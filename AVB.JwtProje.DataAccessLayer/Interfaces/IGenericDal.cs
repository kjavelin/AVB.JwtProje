﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AVB.JwtProje.Entities.Interfaces;

namespace AVB.JwtProje.DataAccessLayer.Interfaces
{
    public interface IGenericDal<TEntity> where TEntity : class,ITable,new()
    {

        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAllByFilter(Expression<Func<TEntity,bool>> filter);

        Task<TEntity> GetById(int id);

        Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);

        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        

    }
}
