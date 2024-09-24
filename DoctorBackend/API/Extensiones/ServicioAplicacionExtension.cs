﻿using Data.Interfaces;
using Data.Servicios;
using Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Errores;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;
using Utilidades;
using BLL.Servicios.Interfaces;
using BLL.Servicios;

namespace API.Extensiones
{
    public static class ServicioAplicacionExtension
    {
        public static IServiceCollection AgregarServiciosApplicacion(this IServiceCollection services, IConfiguration config)
        {
              services.AddEndpointsApiExplorer();
              services.AddSwaggerGen(options =>
              {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingresar Bearer [Espacio] token \r\n\r\n" +
                                  "Ejemplo: Bearer erererere",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }

                });
             });

            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddCors();
            services.AddScoped<ITokenServicio, TokenServicio>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errores = ActionContext.ModelState
                                    .Where(e => e.Value.Errors.Count() > 0)
                                    .SelectMany(x => x.Value.Errors)
                                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidacionErrorResponse
                    {
                        Errores = errores
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IEspecialidadServicio, EspecialidadServicio>();
            services.AddScoped<IMedicoServicio, MedicoServicio>();

            return services;
        }
    }
}
