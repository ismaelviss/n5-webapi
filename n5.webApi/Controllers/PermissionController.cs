using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using n5.webApi.Extensions;
using n5.webApi.Models;
using n5.webApi.Services;
using n5.webApi.Services.Interface;

namespace n5.webApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PermissionController : ControllerBase
    {
        IPermissionService permissionService;

        public PermissionController(IPermissionService permissionService) 
        {
            this.permissionService = permissionService;
            
        }

        [HttpGet]
        public IActionResult GetPermissions()
        {
            var result = permissionService.Get();
            if (result != null && result.Any())
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetPermission(int id)
        {
            var result = permissionService.GetById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult RequestPermission([FromBody] Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return StatusCode((int)HttpStatusCode.Created, permissionService.Save(permission));
            }
            catch(ExceptionService ex)
            {
                return BadRequest(ContentHelper.GetStringContent(new { errorCode = HttpStatusCode.BadRequest, message = ex.Message }));
            }
        }

        [HttpPut("{id}")]
        public IActionResult ModifyPermission(int id,[FromBody] Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(permissionService.Update(id, permission));
            }
            catch (ExceptionService ex)
            {
                return BadRequest(ContentHelper.GetStringContent(new { errorCode = HttpStatusCode.BadRequest, message = ex.Message }));
            }
        }
    }
}