namespace RandomTurretWebApi
{
    using System.Text.Json.Serialization;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using RandomTurret.Business;
    using RandomTurret.DataAccess;
    using RandomTurret.IBusiness;
    using RandomTurret.IDataAccess;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContextPool<RandomTurretContext>(options => options
                .UseMySql(this.Configuration["ConnectionString"]));
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            this.RegisterDataAccess(services);
            this.RegisterBusiness(services);
        }

        private void RegisterBusiness(IServiceCollection services)
        {
            services.AddScoped<IStatBusiness, StatBusiness>();
            services.AddScoped<ITowerBusiness, TowerBusiness>();
            services.AddScoped<ITowerStatBusiness, TowerStatBusiness>();
            services.AddScoped<IPlayerBusiness, PlayerBusiness>();
            services.AddScoped<IMobBusiness, MobBusiness>();
            services.AddScoped<IMobStatBusiness, MobStatBusiness>();
            services.AddScoped<IWaveBusiness, WaveBusiness>();
            services.AddScoped<IWaveMobBusiness, WaveMobBusiness>();
            services.AddScoped<IRarityBusiness, RarityBusiness>();
            services.AddScoped<ITemplateBusiness, TemplateBusiness>();
            services.AddScoped<ITemplateStatBusiness, TemplateStatBusiness>();
            services.AddScoped<IGemBusiness, GemBusiness>();
            services.AddScoped<IGemStatBusiness, GemStatBusiness>();
            services.AddScoped<ITemplatePlayerBusiness, TemplatePlayerBusiness>();
            services.AddScoped<IGemPlayerBusiness, GemPlayerBusiness>();
        }

        private void RegisterDataAccess(IServiceCollection services)
        {
            services.AddScoped<IStatDataAccess, StatDataAccess>();
            services.AddScoped<ITowerDataAccess, TowerDataAccess>();
            services.AddScoped<ITowerStatDataAccess, TowerStatDataAccess>();
            services.AddScoped<IPlayerDataAccess, PlayerDataAccess>();
            services.AddScoped<IMobDataAccess, MobDataAccess>();
            services.AddScoped<IMobStatDataAccess, MobStatDataAccess>();
            services.AddScoped<IWaveDataAccess, WaveDataAccess>();
            services.AddScoped<IWaveMobDataAccess, WaveMobDataAccess>();
            services.AddScoped<IRarityDataAccess, RarityDataAccess>();
            services.AddScoped<ITemplateDataAccess, TemplateDataAccess>();
            services.AddScoped<ITemplateStatDataAccess, TemplateStatDataAccess>();
            services.AddScoped<IGemDataAccess, GemDataAccess>();
            services.AddScoped<IGemStatDataAccess, GemStatDataAccess>();
            services.AddScoped<ITemplatePlayerDataAccess, TemplatePlayerDataAccess>();
            services.AddScoped<IGemPlayerDataAccess, GemPlayerDataAccess>();
        }
    }
}