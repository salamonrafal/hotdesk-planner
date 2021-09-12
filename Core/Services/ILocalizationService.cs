using Core.Models;

namespace Core.Services
{
    public interface ILocalizationService
    {
        public Localization Get(int localizationId);
        public int Add(Localization localization);
        public void Remove(Localization localization);
    }
}
