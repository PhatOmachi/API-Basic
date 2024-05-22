using APISHOPING.Data;
using APISHOPING.Enitty;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISHOPING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [EnableCors("Public")]
        [HttpGet]
        public IActionResult GetListProduct()
        {
            try
            {
                var product = _dataContext.Products.ToList();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi lấy danh sách Product");
            }
        }


        [EnableCors("Public")]
        [HttpGet("{id}")]
        public IActionResult GetProducttById(int id)
        {
            try
            {
                var product = _dataContext.Products.Find(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi lấy thông tin Product '" + id + "'");
            }

        }


        [EnableCors("Public")]
        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("khong duoc de acc null");
                }
                _dataContext.Products.Add(product);
                _dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi lưu Product");
            }

        }

        [EnableCors("Public")]
        [HttpPut("{id}")]
        public IActionResult UpdateProductById(int id, Product product)
        {
            if (product.id.Equals(""))
            {
                return BadRequest("Vui lòng nhập ID");
            }

            if (product == null)
            {
                return BadRequest("Vui lòng nhập thông tin Product");
            }

            try
            {
                var ProductUpdate = _dataContext.Products.Find(id);
                ProductUpdate.name = product.name;
                ProductUpdate.description = product.description;
                ProductUpdate.quantity = product.quantity;
                ProductUpdate.price = product.price;

                _dataContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id.Equals(""))
            {
                return NotFound("Không được để trống id");
            }
            var ProductDelete = _dataContext.Products.Find(id);
            if (ProductDelete == null)
            {
                return BadRequest("Không có Product '" + ProductDelete.id + "'");
            }

            try
            {
                _dataContext.Remove(ProductDelete);
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
