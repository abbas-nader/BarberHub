using System;
using System.Collections.Generic;
using System.Text;

namespace BarberHub.Domain.Exceptions
{
    public class InvalidLastNameException() : Exception("The last name cannot be null, empty, or invalid.")
    {
    }
}
