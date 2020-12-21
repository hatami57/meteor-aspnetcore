using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Meteor.AspCore.Filters;
using Meteor.AspCore.ModelBinders;
using Meteor.Utils.JsonConverters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Meteor.AspCore.Utils
{
    public static class Extensions
    {
        public static long GetUserId(this ClaimsPrincipal user) =>
            Convert.ToInt64(user?.FindFirstValue("sub")
                            ?? user?.FindFirstValue(ClaimTypes.NameIdentifier)
                            ?? "0");
        public static string[] GetUserRoles(this ClaimsPrincipal user) =>
            user.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray();
        
        public static long[] GetUserEnrollmentIds(this ClaimsPrincipal user) =>
            user.FindAll("enrollment").Select(x => long.Parse(x.Value)).ToArray();

        public static MvcOptions AddMeteorModelBinder(this MvcOptions mvcOptions)
        {
            mvcOptions.ModelBinderProviders.Insert(0, new ModelBinderProvider());
            return mvcOptions;
        }

        public static MvcOptions AddMeteorFilters(this MvcOptions mvcOptions)
        {
            mvcOptions.Filters.Add(new AssignUserActionFilter());
            return mvcOptions;
        }

        public static IMvcBuilder AddMeteorJsonConverters(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new ErrorJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new LocalTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new LocalDateJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new InstantJsonConverter());
            });
            return mvcBuilder;
        }

        public static IServiceCollection AddMeteorJwtAuthentication(this IServiceCollection services,
            bool validateAudience = false, bool validateIssuer = false, bool validateActor = false)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = validateAudience,
                        ValidateIssuer = validateIssuer,
                        ValidateActor = validateActor,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvVars.Require<string>(EnvVarKeys.JwtKey))),
                        ValidateIssuerSigningKey = true
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Log.Debug(context.Exception, "authentication failed");
                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }
    }
}