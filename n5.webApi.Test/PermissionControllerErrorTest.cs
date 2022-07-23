using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using n5.webApi.Models;
using n5.webApi.Test.Fixtures;
using n5.webApi.Test.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace n5.webApi.Test
{
    public class PermissionControllerErrorTest : IntegrationTest
    {
        static int permissionTypeId = 12345;
        public PermissionControllerErrorTest(ApiWebApplicationFactory applicationFactory) : base(applicationFactory) { }

        [Fact]
        public async Task Get_Permission_NotFoundAsync()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PermissionContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<PermissionContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<PermissionContext>();
                        var logger = scopedServices.GetRequiredService<ILogger<PermissionControllerErrorTest>>();

                        db.Database.EnsureCreated();

                        try
                        {
                            db.Permissions.RemoveRange(db.Permissions);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                        }
                    }
                });
            }).CreateClient();

            var response = await client.GetAsync("api/v1/permission");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Request_Permission_Error()
        {
            var permission = new Permission() { EmployeeLastName = "Salvatierra", EmployeeName = "Anthony", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = permissionTypeId };

            var response = await _client.PostAsync("api/v1/permission", ContentHelper.GetStringContent(permission));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Modify_Permission_Error_permissionType()
        {
            var name = "Prueba";
            var permission = new Permission() { EmployeeLastName = "Salvatierra", EmployeeName = name, PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 2 };

            var response = await _client.PutAsync("api/v1/permission/1", ContentHelper.GetStringContent(permission));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Modify_Permission_Error_permission()
        {
            var name = "Prueba";
            var permission = new Permission() { EmployeeLastName = "Salvatierra", EmployeeName = name, PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };

            var response = await _client.PutAsync("api/v1/permission/" + permissionTypeId, ContentHelper.GetStringContent(permission));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
