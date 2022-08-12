using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mapping;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchMvc.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services,
            IConfiguration confiruation)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(confiruation.
                GetConnectionString("DefaulConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext)
                 .Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
                        options.AccessDeniedPath = "/Account/Login");

            //Repositorios 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //Serviços
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();

            //Autenticação / Permissão
            services.AddScoped<IAuthenticate,AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial,SeedUserRoleInitial>();

            // Nome do Arquivo que foi feita as configurações do Automapper
            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

            var myhandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(myhandlers);


            return services;
        }

    }
}
