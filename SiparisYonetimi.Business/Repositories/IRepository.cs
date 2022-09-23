using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Business.Repositories
{
    public interface IRepository<T> // <T> kullanamrak interface i generic hale getirdik. Buradaki T dışarıdan parametre olarak gönderilecek class ları(brand, category vb.) simgeliyor
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        T Find(int id);
        T Get(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int SaveChanges();
    }
}
