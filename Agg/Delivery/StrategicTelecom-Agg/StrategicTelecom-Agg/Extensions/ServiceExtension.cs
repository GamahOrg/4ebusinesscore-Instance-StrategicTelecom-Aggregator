

namespace _4EPlatform_StrategicTelecom_Aggregator.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionMiddleware, GlobalExceptionMiddleware>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IResponseHelper, ResponseHelper>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IMedalService, MedalService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IThankService, ThankService>();
            services.AddScoped<ICongratService, CongratsService>();

        }
        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning().AddApiExplorer(options =>
            {
                // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
            });
        }
        public static void AddRateLimitings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                var rateLimitSection = configuration.GetSection("RateLimit");
                var permitLimit = rateLimitSection.GetValue<int>("PermitLimit");
                var queueLimit = rateLimitSection.GetValue<int>("QueueLimit");
                var windowSeconds = rateLimitSection.GetValue<int>("WindowSeconds");
                options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(windowSeconds);
                    opt.PermitLimit = permitLimit;
                    opt.QueueLimit = queueLimit;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                }).RejectionStatusCode = 429;
            });
        }


        public static void AddLocalizationOptions(this IServiceCollection services)
        {
            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("ar")
                };



                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        public static void AddGrpcConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();
            services.AddGrpcReflection();
            //services.AddOrgGrpcClients(configuration["GrpcServiceSettings:OrgUrl"]);


        }
        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
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
        }

        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition(
                "token",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization
                }
                    );
                c.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
                }
                    );
            });
        }
        public static void AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        // the name of your api resources   
                        options.Audience = "UserManagementServer";
                        /// identity server url                    
                        options.Authority = configuration.GetValue<string>("IdentityUrl");
                        options.RequireHttpsMetadata = false;
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ResourceOwnerPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
                });

                options.AddPolicy("ClientCredentialsPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
                });
            });
        }

    }
}
