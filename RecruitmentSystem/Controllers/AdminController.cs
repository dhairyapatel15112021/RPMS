using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Models;
using RecruitmentSystem.Services.Role;
using RecruitmentSystem.Services.RoleMap;

namespace RecruitmentSystem.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IRoleService roleService;
    private readonly IRoleMapService roleMapService;

    public AdminController(IRoleService roleService, IRoleMapService roleMapService)
    {
        this.roleService = roleService;
        this.roleMapService = roleMapService;
    }

    [HttpPost]
    [Route("role/add")]
    public async Task<ActionResult> addRoles([FromBody] RoleModel roleModel)
    {
        try
        {
            bool is_saved = await roleService.addRoles(roleModel);
            if (is_saved)
            {
                return Ok("Role Added");
            }
            throw new Exception("role is not added! maybe role is already exist");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("role/employee/map/{id}")]
    public async Task<ActionResult> mapRoles(int id, [FromBody] int roleId)
    {
        try
        {
            await roleMapService.mapRoles(id, roleId);
            return Ok("Role Mapped To Employee");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("role/employee/map/{id}")]
    [Authorize("admin")]
    public async Task<ActionResult> getRoles(int id)
    {
        try
        {
            return Ok(await roleMapService.getRoles(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}