using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreRepository;
using CoreRepository.Models;
using DataRepository.Context;

namespace HappyCompany_API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : BaseController
    {
        private readonly IHostEnvironment _env;
        private DataContext _context;
        private readonly ILogger<AdminDashboardController> _logger;
        public AdminDashboardController (DataContext context,  IConfiguration configuration, IUnitOfWork unitOfWork, IHostEnvironment env, ILogger<AdminDashboardController> logger) : base(configuration, unitOfWork)
        {
            _env = env;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("Warehouses/Get")]
        public IActionResult GetWarehouses([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                int count = 0;
                List<WarehousStatistics> warehouses = _context.Warehouse.Select(x => new WarehousStatistics() { Id = x.Id, warehouse = x.Name, count = x.Items.Count() }).ToList();
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Items/Get")]
        public IActionResult GetItems([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] int Asc)
        {
            try
            {
                int count = 0;
                List<Item> Items;
                if (Asc > 0)
                {
                    Items = _unitOfWork.Items.GetOrderdPaged(page, pageSize, out count, x => x.Qty).ToList();
                }
                else
                {
                    Items = _unitOfWork.Items.GetOrderdDesPaged(page, pageSize, out count, x => x.Qty).ToList();
                }
                return Ok(new { Items, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        [HttpGet]
        [Route("ReadLogs")]
        public IActionResult ReadLogs()
        {
            try
            {

                var items = new List<string>();
                string currentLine = "";
                var path = Path.Combine(_env.ContentRootPath, "Log\\Webapi_20230531.log");

                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            currentLine = currentLine + line;
                            if (currentLine.EndsWith("]") && !currentLine.EndsWith("INF]"))
                            {
                                items.Add(currentLine.Replace("][", "] \n ["));
                                currentLine = "";
                            }
                        }
                    }
                }

                return Ok( items );
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
