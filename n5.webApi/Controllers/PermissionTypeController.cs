using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using n5.webApi.Extensions;
using n5.webApi.Models;
using n5.webApi.Services;
using n5.webApi.Services.Interface;

namespace n5.webApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PermissionTypeController : ControllerBase
    {
        IPermissionService permissionService;

        public PermissionTypeController(IPermissionService permissionService) 
        {
            this.permissionService = permissionService;
            
        }

        [HttpGet]
        public IActionResult GetPermission()
        {
            var result = permissionService.GetPermissionTypes();
            if (result != null && result.Any())
                return Ok(result);
            else
                return NotFound();
        }
    }
}
