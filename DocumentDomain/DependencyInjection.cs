using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDomain {
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplicationDI(IServiceCollection services)
        {
            return services;
        }
    }
}
