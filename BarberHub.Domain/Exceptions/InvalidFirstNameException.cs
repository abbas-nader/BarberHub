using System;
using System.Collections.Generic;
using System.Text;

namespace BarberHub.Domain.Exceptions
{
    public class InvalidFirstNameException() : Exception("The first name cannot be null, empty, or invalid.")
    {
    }
}
