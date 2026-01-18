using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubMembershipApp.Data
{
    public interface IRegister
    {
        bool Register(string[] fields);
        bool EmailExists(string emailAddress);
    }
}