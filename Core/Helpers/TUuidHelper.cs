using Core.Models;
using System;


namespace Core.Helpers
{
    public static class TUuidHelper
    {
        public static void GenerateUuid(this BaseModel model)
        {
            model.Id = Guid.NewGuid();
        }
    }
}
