using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public static class Factory
    {
        static public IBl Get()
        {
            IBl bl = new Bl();
            return bl;
        }
    }
}
