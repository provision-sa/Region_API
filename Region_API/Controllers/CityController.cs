using Microsoft.AspNetCore.Mvc;
using Region_API.Models;
using Region_API.Repositories;
using System.Transactions;

namespace Region_API.Controllers
{
    [Route("Region/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _Repo;

        public CityController(ICityRepository repo)
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

        [HttpGet("getby/{value}")]
        public IActionResult Get(string value)
        {
            return new OkObjectResult(_Repo.GetBy(value));
        }

        [HttpPost]
        public IActionResult Post([FromBody] City city)
        {
            using (var scope = new TransactionScope())
            {
                _Repo.Insert(city);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = city.Id }, city);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] City city)
        {
            if (city != null)
            {
                using (var scope = new TransactionScope())
                {
                    _Repo.Update(city);
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
