using Authorization.Attributes;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        AppDbContext _context = new();

       

        [HttpGet("[action]")]

        public async Task<IActionResult> Get(string userName,CancellationToken cancellationToken)
        {
            User? user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync(cancellationToken);
            string token=JwtProvider.CreateToken(user);
            return Ok(new { Token = token });
        }
        [Role("GetAll")]
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetAll() 
        {
            IList<String> strings=new List<String>();
            for (int i = 0; i < 10; i++)
            {
                strings.Add("Name" + i);

            }
            return Ok(strings);
        }
        [Role("Admin")]
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetAllAdminRole()
        {
            IList<String> strings = new List<String>();
            for (int i = 0; i < 10; i++)
            {
                strings.Add("Name" + i);

            }
            return Ok(strings);
        }

    }
}
