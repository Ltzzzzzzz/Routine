using System;
using Routine.Api.Entities;

namespace Routine.Api.Models
{
    public class EmployeeUpdateDto
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
