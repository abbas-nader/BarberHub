using System;
using System.Collections.Generic;
using System.Text;

namespace BarberHub.Domain.Exceptions
{
    public class InvalidMobileNumberException() : Exception("The mobile number cannot be null or empty.")
    {
    }
}
