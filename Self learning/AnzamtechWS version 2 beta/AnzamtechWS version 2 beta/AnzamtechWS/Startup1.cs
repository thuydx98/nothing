
using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AnzamtechWS.Startup1))]

namespace AnzamtechWS
{
    public class Startup1
    {
       
        public void  Configuration(IAppBuilder app)
        {
        
         
        }
    }
}
