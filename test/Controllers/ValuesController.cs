using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ValuesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [EnableCors("Public")]
        [HttpGet]
        public IActionResult GetListAccount()
        {
            try
            {
                var acc = _dataContext.Accounts.ToList();
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi lấy danh sách Account");
            }
        }
    }
}
