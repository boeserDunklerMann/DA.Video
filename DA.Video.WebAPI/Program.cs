using DA.Video.Model;

namespace DA.Video.WebAPI
{
    /// <ChangeLog>
    /// <Create Datum="14.01.2025" Entwickler="DA" />
    /// <Change Datum="15.01.2025" Entwickler="DA">DI stuff added</Change>    
    /// <Change Datum="16.01.2025" Entwickler="DA">DI bindings in separate method</Change>
    /// </ChangeLog>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);    // there is the connstring which will not be committed to git
                                                                                            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            CreateDIBindings(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        /// <ChangeLog>
        /// <Create Datum="16.01.2025" Entwickler="DA" />
        /// </ChangeLog>
        private static void CreateDIBindings(WebApplicationBuilder? builder)
        {
            if (builder == null)
                throw new NullReferenceException(nameof(builder));
            builder.Services.AddScoped<IDbContext, VideoContext>();
        }
    }
}