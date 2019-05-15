using NewsApp.Models;
using System;
using System.Collections.Generic;

namespace NewsApp.DataAccess.Abstract
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        void Add(T item);
        ICollection<T> GetAll();
    }
}
