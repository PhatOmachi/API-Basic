using APISHOPING.Data;
using APISHOPING.Enitty;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISHOPING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AccountController (DataContext dataContext)
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











        [EnableCors("Public")]
        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            try
            {
                var acc = _dataContext.Accounts.Find(id);
                return Ok(acc);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi lấy thông tin Account '"+id+"'");
            }

        }


        [EnableCors("Public")]
        [HttpPost]
        public IActionResult SaveAccount(Account account)
        {
            try
            {
                if (account == null) 
                {
                    return BadRequest("khong duoc de acc null");   
                }
                _dataContext.Accounts.Add(account);
                _dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi lưu Account");
            }

        }

        [EnableCors("Public")]
        [HttpPut("{id}")]
        public IActionResult UpdateAccountById(int id , Account account) 
        {
            if(account.id.Equals(""))
            {
                return BadRequest("Vui lòng nhập ID");
            }

            if(account == null)
            {
                return BadRequest("Vui lòng nhập thông tin Account");
            }

            try
            {
                var accUpdate = _dataContext.Accounts.Find(id);
                accUpdate.name = account.name;
                accUpdate.email = account.email;
                accUpdate.password = account.password;

                _dataContext.SaveChanges();

                return Ok();
            }
            catch  (Exception ex) 
            {
                   return BadRequest(ex.Message);
            }
 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            if (id.Equals(""))
            {
                return NotFound("Không được để trống id");
            }
            var accDelete = _dataContext.Accounts.Find(id);
            if(accDelete == null)
            {
                return BadRequest("Không có account '" + accDelete.id + "'");
            }

            try
            {
                _dataContext.Remove(accDelete);
                _dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest("Lỗi");
            }
 
        }
    }
}
