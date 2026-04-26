namespace _4EPlatform_StrategicTelecom_Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Configuration)
          .Enrich.FromLogContext()
          .CreateLogger();
            builder.Host.UseSerilog(logger);
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 7288, o => o.Protocols = HttpProtocols.Http2);

                options.Listen(IPAddress.Any, 5024, o => o.Protocols = HttpProtocols.Http1);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddServices();
            builder.Services.AddCorsConfig();
            builder.Services.AddIdentityConfig(builder.Configuration);
            builder.Services.AddGrpcConfig(builder.Configuration);
            builder.Services.AddSwaggerConfig();
            builder.Services.AddLocalizationOptions();
            builder.Services.AddVersioning();
            builder.Services.AddRateLimitings(builder.Configuration);
            builder.Services.AddMedalGrpcClients(builder.Configuration["GrpcServiceSettings:Medal"]);
            builder.Services.AddThanksGrpcClients(builder.Configuration["GrpcServiceSettings:thanks"]);
            builder.Services.AddServiceOrgMemberGrpcClients(builder.Configuration["GrpcServiceSettings:orgUrl"]);

            builder.Services.AddUserGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagmentUrl"]);
            builder.Services.AddCPAggregatorGrpcClients(builder.Configuration["GrpcServiceSettings:ControlPanelAggUrl"]);
            builder.Services.AddServiceCongratulationsGrpcClients(builder.Configuration["GrpcServiceSettings:Congratulations"]);
            builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagmentUrl"]);

            builder.Services.AddHostedService<AddPermissionsMiddleware>();
            builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();
            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowCors",
                    builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                    });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            #region Localization Middleware
            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            #endregion
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseStaticFiles();
            app.MapGrpcReflectionService();
            app.UseCors("AllowCors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.AddgRPCServices();
            app.UseSerilogRequestLogging();
            app.UseRateLimiter();

            app.UseHttpsRedirection();

            app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();



            app.Run();
        }
    }
}

