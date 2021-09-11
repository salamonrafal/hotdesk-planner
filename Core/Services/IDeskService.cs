using Core.Models;


namespace Core.Services
{
    public interface IDeskService
    {
        public Desk Get(int deskId);
        public int Add(Desk desk);
        public void Remove(Desk desk);
        public void ChnageState(bool isBlocked, int deskId);
    }
}
