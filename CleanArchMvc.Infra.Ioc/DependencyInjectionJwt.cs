
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Ioc
{
    public static class DependencyInjectionJwt
    {
        public static IServiceCollection AddInfrastuctureJWT(this IServiceCollection services,
             IConfiguration confirugation) 
        {
            // Informa o tipo de autenticação JWT - Bearer
            // Definir o Modelo de Desaio de Autenticação
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //Habilita a Autenticação JWT Usando o esquema e Desafio  definidos
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = confirugation["Jwt:Issuer"],
                    ValidAudience = confirugation["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(confirugation["Jwt:Secretkey"])),
                    ClockSkew=TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
