using Microsoft.AspNetCore.Mvc;
using Region_API.Models;
using Region_API.Repositories;
using System.Transactions;

namespace Region_API.Controllers
{
    [Route("Region/[controller]")]
    [ApiController]
    public class SuburbController : ControllerBase
    {
        private readonly ISuburbRepository _Repo;

        public SuburbController(ISuburbRepository repo)
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

        [HttpGet("{province}/{city}")]
        public IActionResult Get(string Province, string City)
        {
            return new OkObjectResult(_Repo.GetBy(Province, City));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Suburb suburb)
        {
            using (var scope = new TransactionScope())
            {
                _Repo.Insert(suburb);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = suburb.Id }, suburb);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Suburb suburb)
        {
            if (suburb != null)
            {
                using (var scope = new TransactionScope())
                {
                    _Repo.Update(suburb);
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
