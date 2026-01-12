using System;
using System.Collections.Generic;
using System.Text;

namespace HRAdministrationAPI
{
    public interface IEmployee
    {
        int Id {get; set;}
        string firstName {get; set;}

        string lastName {get; set;}

        decimal salary {get; set;}
    }
}
