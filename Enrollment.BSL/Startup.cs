using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.Bsl.Flow;
using Enrollment.Bsl.Flow.Cache;
using Enrollment.Bsl.Flow.Services;
using Enrollment.BSL.AutoMapperProfiles;
using Enrollment.Common.Configuration.Json;
using Enrollment.Contexts;
using Enrollment.Domain.Json;
using Enrollment.Repositories;
using Enrollment.Stores;
using Enrollment.Utils;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Enrollment.BSL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            System.Collections.Generic.List<System.Reflection.Assembly> assemblies = new System.Collections.Generic.List<System.Reflection.Assembly>
            {
                typeof(Parameters.Expansions.SelectExpandDefinitionParameters).Assembly,
                typeof(Utils.TypeHelpers).Assembly,
                typeof(Domain.BaseModelClass).Assembly,
                typeof(Data.BaseDataClass).Assembly,
                typeof(DirectorBase).Assembly,
                typeof(string).Assembly
            };

            rulesCache = Bsl.Flow.Rules.RulesService.LoadRules().Result;
        }

        public IConfiguration Configuration { get; }
        private readonly IRulesCache rulesCache;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddControllers().AddJsonOptions
            (
                options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DescriptorConverter());
                    options.JsonSerializerOptions.Converters.Add(new ModelConverter());
                    options.JsonSerializerOptions.Converters.Add(new ObjectConverter());
                }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Enrollment.BSL", Version = "v1" });
            })
            .AddDbContext<EnrollmentContext>
            (
                options => options.UseSqlServer
                (
                    Configuration.GetConnectionString("DefaultConnection")
                )
            )
            .AddScoped<IEnrollmentStore, EnrollmentStore>()
            .AddScoped<IEnrollmentRepository, EnrollmentRepository>()
            .AddSingleton<AutoMapper.IConfigurationProvider>
            (
                new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<EnrollmentProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                })
            )
            .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
            .AddScoped<IFlowManager, FlowManager>()
            .AddScoped<FlowActivityFactory, FlowActivityFactory>()
            .AddScoped<DirectorFactory, DirectorFactory>()
            .AddScoped<ICustomActions, CustomActions>()
            .AddScoped<FlowDataCache, FlowDataCache>()
            .AddScoped<Progress, Progress>()
            .AddScoped<IGetItemFilterBuilder, GetItemFilterBuilder>()
            .AddSingleton<IRulesCache>(sp => rulesCache);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enrollment.BSL v1"));
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
