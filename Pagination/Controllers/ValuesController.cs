using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        AppDbContext context = new();
        [HttpGet]

        public IActionResult Get(int pageNumber, int pageSize)
        {
            PaginationDto<Product> result = new();
            IList<Product> products =
                context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int count = context.Products.Count();
            result.Datas = products;
            result.PageNumber = pageNumber;
            result.PageSize = pageSize;
            result.IsFirstPage = pageNumber == 1 ? true : false;
            result.TotalPageCount = (int)Math.Ceiling(count / (double)pageSize);
            result.IsLastPage = pageNumber == result.TotalPageCount ? true : false;

            return Ok(result);
        }

    }
}
