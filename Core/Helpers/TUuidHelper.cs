using Core.Models;
using System;


namespace Core.Helpers
{
    public static class UuidHelper
    {
        public static void GenerateUuid(this BaseModel model)
        {
            model.Id = Guid.NewGuid();
        }
    }
}
