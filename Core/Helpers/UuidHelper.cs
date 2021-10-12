using Core.Models;
using System;
using MongoDB.Bson;


namespace Core.Helpers
{
    public static class UuidHelper
    {
        public static void GenerateUuid(this BaseModel model)
        {
            model.Id = Guid.NewGuid();
            model.DocumentId = ObjectId.GenerateNewId();
        }
    }
}
