using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreRepository;
using CoreRepository.Models;

namespace HappyCompany_API.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    [Authorize]
    public class WarehouseController : BaseController
    {
        private readonly ILogger<WarehouseController> _logger;
        public WarehouseController(IConfiguration configuration, IUnitOfWork unitOfWork , ILogger<WarehouseController> logger) : base(configuration, unitOfWork ) {
            _logger = logger;
        }

        [HttpPost]
        [Route("AddWarehouse")]
   
        public IActionResult AddWarehouse([FromBody] Warehouse warehouse)
        {
            try
            {
                if (warehouse.Id != 0)
                {
                    _unitOfWork.Warehouses.Update(warehouse);
                }
                else
                {
                    _unitOfWork.Warehouses.Add(warehouse);
                }
                _unitOfWork.Complete();
                return Ok(new {id= warehouse.Id.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetWarehouses")]
        [AllowAnonymous]
      
        public IActionResult GetWarehouses([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                _logger.LogInformation(" GetWarehouses ");
                int count = 0;
                List<Warehouse> warehouses = _unitOfWork.Warehouses.GetPaged(page, pageSize, out count).ToList();
                return Ok(new { warehouses, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetWarehouse")]
      
        public IActionResult GetWarehouse([FromQuery] Int64 warehouseId)
        {
            try
            {
                Warehouse warehouse = _unitOfWork.Warehouses.GetEntity((warehouse) => warehouse.Id == warehouseId);
                return Ok( warehouse );
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("DeleteWarehouse")]
     
        public IActionResult DeleteWarehouse([FromQuery] Int64 warehouseId)
        {
            try
            {
                _unitOfWork.Warehouses.DeleteEntity((x) => x.Id == warehouseId);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("AddItem")]
      
        public IActionResult AddItem([FromBody] Item item)
        {
            try
            {
                if (item.Id != 0)
                {
                    _unitOfWork.Items.Update(item);
                }
                else
                {
                    _unitOfWork.Items.Add(item);
                }
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetItems")]
      
        public IActionResult GetItems([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] Int64 wherhouseId)
        {
            try
            {
                int count = 0;
                List<Item> items = _unitOfWork.Items.GetPaged(page, pageSize, out count, (item) => item.Warehouse.Id == wherhouseId).ToList();
                return Ok(new { items, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetItem")]
  
        public IActionResult GetItem([FromQuery] Int64 itemId)
        {
            try
            {
                Item item = _unitOfWork.Items.GetEntity((item) => item.Id == itemId);
                return Ok(new { item });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("DeleteItem")]

        public IActionResult DeleteItem([FromQuery] Int64 itemId)
        {
            try
            {
                _unitOfWork.Items.DeleteEntity((x) => x.Id == itemId);
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
