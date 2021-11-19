using Newtonsoft.Json;
using PizzaSystem.Models.Interfaces;
using System;
using System.IO;

namespace PizzaSystem.Utils
{
    public class FileDatabase : IFileDatabase
    {
        public void Write<T>(string fileLocation, T obj)
        {
            var location = System.AppDomain.CurrentDomain.BaseDirectory + "Dependency//" + fileLocation;
            System.IO.File.WriteAllText(location, JsonConvert.SerializeObject(obj));
        }
        public T Read<T>(string fileLocation)
        {
            var location = System.AppDomain.CurrentDomain.BaseDirectory + "Dependency//" + fileLocation;

            if (System.IO.File.Exists(location))
                return JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(location));
            return default(T);
        }
    }
}
