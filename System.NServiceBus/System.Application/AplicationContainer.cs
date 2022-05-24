using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Application.Features.Orders.Commands.CreateOrder;
using System.Reflection;

namespace System.Application
{
    public static class ApplicationContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<CreateOrderCommand>, CreateOredrValidator>();

            return services;
        }
    }
}
