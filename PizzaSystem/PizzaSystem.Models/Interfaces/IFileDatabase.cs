using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces
{
    public interface IFileDatabase
    {
        void Write<T>(string fileLocation, T obj);
        T Read<T>(string fileLocation);
    }
}
