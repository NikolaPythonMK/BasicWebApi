using BasicWebApi.Domain.Entities;

namespace BasicWebApi.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);
        Task<IEnumerable<T>> GetAllPageable(int take, int skip);
    }
}
