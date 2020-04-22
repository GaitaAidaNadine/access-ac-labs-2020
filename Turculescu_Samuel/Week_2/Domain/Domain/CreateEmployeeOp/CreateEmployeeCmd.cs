﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domain.CreateEmployeeOp
{
    public struct CreateEmployeeCmd
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Phone { get; }
        public string IdEmployee { get; }

        public CreateEmployeeCmd(string firstName, string lastName, string email, string phone, string idEmployee)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            IdEmployee = idEmployee;
        }
    }
}
