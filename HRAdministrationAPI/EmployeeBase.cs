using System;
using System.Collections.Generic;
using System.Text;

namespace HRAdministrationAPI
{
    public class EmployeeBase : IEmployee
    {
        public int Id{get ; set ;}
        public string firstName{get; set;}
        public string lastName{get; set;}
        public virtual decimal salary{get; set;}   
    }
}