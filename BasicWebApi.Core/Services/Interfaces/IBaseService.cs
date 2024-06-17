namespace BasicWebApi.Core.Services.Interfaces
{
    public interface IBaseService<T, TD>
    {
        Task<int> Create(TD company);
        Task<int> Update(int id, TD company);
        Task<int> Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllPageable(int take, int skip);
    }
}
