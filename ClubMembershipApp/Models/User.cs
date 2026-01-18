using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ClubMembershipApp.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    // db is responsible for auto-generating Id(not to be set manually)
        public int Id {get; set;}     // PRIMARY KEY (auto-increments each time new row is added)
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Password {get; set;}
        public string PhoneNumber {get; set;}
        public string AddressFirstLine {get; set;}
        public string AddressSecondLine {get; set;}
        public string AddressCity {get; set;}
        public string PostCode {get; set;}
        public DateTime DateOfBirth {get; set;}
    }
}