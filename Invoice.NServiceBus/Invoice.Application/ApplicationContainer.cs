using FluentValidation;
using Invoice.Application.Features.Invoices.Commands;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Invoice.Application
{
    public static class ApplicationContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<CreateInvoiceCommand>, CreateInvoiceCommandValidator>();
           

            return services;
        }
    }
}

