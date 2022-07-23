using n5.webApi.Models;
using n5.webApi.Test.Fixtures;
using n5.webApi.Test.Utils;
using Newtonsoft.Json;
using System.Net;

// Need to turn off test parallelization so we can validate the run order
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace n5.webApi.Test
{

    [TestCaseOrderer("XUnit.Project.Orderers.PriorityOrderer", "XUnit.Project")]
    public class PermissionControllerTest : IntegrationTest
    {
        public PermissionControllerTest(ApiWebApplicationFactory applicationFactory) : base(applicationFactory) { }

        [Fact, TestPriority(1)]
        public async Task Get_Permission_Success()
        {
            var response = await _client.GetAsync("api/v1/permission");

            var permissions = JsonConvert.DeserializeObject<Permission[]>(
                await response.Content.ReadAsStringAsync()
            );

            Assert.NotNull(permissions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.All(permissions, x => {
                Assert.NotNull(x);
                Assert.NotNull(x.EmployeeName);
                Assert.NotNull(x.EmployeeLastName);
            });

            Assert.True(permissions.Length > 0);
        }

        [Fact, TestPriority(0)]
        public async Task Request_Permission_Success()
        {
            var permission = new Permission() { EmployeeLastName = "Salvatierra", EmployeeName = "Anthony", PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };
            
            var response = await _client.PostAsync("api/v1/permission", ContentHelper.GetStringContent(permission));

            var permissionsReponse = JsonConvert.DeserializeObject<Permission>(
                await response.Content.ReadAsStringAsync()
            );

            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(permissionsReponse);
            Assert.True(permissionsReponse.Id != 0);
            Assert.NotNull(permissionsReponse.EmployeeName);
            Assert.NotNull(permissionsReponse.EmployeeLastName);
        }

        [Fact, TestPriority(2)]
        public async Task Modify_Permission_Success()
        {
            var name = "Prueba";
            var permission = new Permission() { EmployeeLastName = "Salvatierra", EmployeeName = name, PermissionDate = DateTime.Parse("2022-07-19"), PermissionTypeId = 1 };

            var response = await _client.PutAsync("api/v1/permission/1", ContentHelper.GetStringContent(permission));

            var permissionsReponse = JsonConvert.DeserializeObject<Permission>(
                await response.Content.ReadAsStringAsync()
            );


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(permissionsReponse);
            Assert.True(permissionsReponse.Id != 0);
            Assert.NotNull(permissionsReponse.EmployeeName);
            Assert.Equal(name, permissionsReponse.EmployeeName);
            Assert.NotNull(permissionsReponse.EmployeeLastName);
        }
    }
}
