using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ILocalizationService
    {
        public Localization Get(int localizationId);
        public int Add(Localization localization);
        public void Remove(Localization localization);
    }
}
