using KanbanApiEfc.Dto;
using KanbanApiEfc.Exceptions;
using KanbanApiEfc.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KanbanApiEfc.Controllers
{
    [Route("api/Boards/{boardId}/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private ColumnRepository Repository;

        public ColumnsController(ColumnRepository repository) {
            Repository = repository;
        }

        // GET: api/<ColumnsController>
        [HttpGet]
        public ActionResult List(int boardId)
        {
            try
            {
                return Ok(Repository.ListColumnsByBoardId(boardId));
            } catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // GET api/<ColumnsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int boardId, int id)
        {
            try
            {
                var column = Repository.GetColumn(boardId, id);
                if (column == null)
                {
                    throw new NotFoundException();
                }
                return Ok(column);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/<ColumnsController>
        [HttpPost]
        public ActionResult Post(int boardId, [FromBody] ColumnParams data)
        {
            try
            {
                var result = Repository.Insert(boardId, data);
                return Created($"/Boards/{boardId}/Columns/{result.Id}", result);
            } catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // PUT api/<ColumnsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int boardId, int id, [FromBody] ColumnParams data)
        {
            try
            {
                var result = Repository.Update(boardId, id, data);
                return Ok(result);
            } catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int boardId, int id)
        {
            Repository.DeleteColumn(boardId, id);
            return NoContent();
        }
    }
}
