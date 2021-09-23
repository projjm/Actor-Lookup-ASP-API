using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace ActorLookupREST.Middleware
{
    public static class AngularSPA
    {
        public static IApplicationBuilder UseAngularSPA(this IApplicationBuilder app)
        {
            app.Use(async (context, next) => 
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/index.html";
                }
                await next();
            });

            return app;
        }
    }
}
