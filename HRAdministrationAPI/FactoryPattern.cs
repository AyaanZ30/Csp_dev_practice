using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAdministrationAPI
{
    // T : concrete class (new-able) [Teacher, HeadMaster, ..]
    // K : Abstraction (interface IEmployee)
    public static class FactoryPattern<T, K> where T : class, K, new()
    {
        public static K getInstance()
        {
            return new T();
        }
    }
}