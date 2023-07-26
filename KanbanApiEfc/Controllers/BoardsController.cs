using KanbanAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using KanbanApiEfc.Dto;
using KanbanApiEfc.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KanbanApiEfc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private BoardRepository Repo;

        public BoardsController(BoardRepository repo)
        {
            Repo = repo;
        }
        
        [HttpGet]
        public IEnumerable<BoardWithColumnDto> Get()
        {
            return Repo.List(1);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = Repo.Get(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // POST api/<BoardsController>
        [HttpPost]
        public ActionResult Post([FromBody] BoardParams par)
        {
            var created = Repo.Insert(par);
            return Created($"/api/Boards/{created.Id}", created);
        }

        // PUT api/<BoardsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] BoardParams par)
        {
            var result = Repo.Update(id, par);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // DELETE api/<BoardsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Repo.Delete(id);
            return NoContent();
        }
    }
}
