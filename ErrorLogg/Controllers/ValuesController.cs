﻿using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ErrorLogg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("[action]")]

        public IActionResult Calculate(int x,int y) 
        {
            int result = x / y;
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task< IActionResult> GetErrorLogs()
        {
            AppDbContext context = new();
            IList<ErrorLog> errorLogs=await
            context.ErrorLogs.OrderByDescending(p =>
            p.CreatedDate).ToListAsync();
            return Ok(errorLogs);
        }
    }
}
