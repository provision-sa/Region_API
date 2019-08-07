using Microsoft.AspNetCore.Mvc;
using Region_API.Models;
using Region_API.Repositories;
using System.Transactions;

namespace Region_API.Controllers
{
    [Route("Region/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IRepository<Province> _Repo;

        public ProvinceController(IRepository<Province> repo)
        {
            _Repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_Repo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new OkObjectResult(_Repo.Get(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Province province)
        {
            using (var scope = new TransactionScope())
            {
                _Repo.Insert(province);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = province.Id }, province);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Province province)
        {
            if (province != null)
            {
                using (var scope = new TransactionScope())
                {
                    _Repo.Update(province);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _Repo.RemoveAtId(id);
            return new OkResult();
        }
    }
}
