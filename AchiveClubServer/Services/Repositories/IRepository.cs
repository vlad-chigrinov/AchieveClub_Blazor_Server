using System.Collections.Generic;

namespace AchieveClubServer.Services
{
    public interface IRepository<TValue>
    {
        public List<TValue> GetAll();
        public TValue GetById(int id);
        public int Insert(TValue value);
        public bool Update(TValue value);
        public bool Delete(int id);
    }
}