using KanbanAPI.Dto;
using KanbanAPI.Models;
using KanbanAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanbanAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ICardRepository repository;

    public CardsController(ICardRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public IEnumerable<Card> Get() => repository.GetAll();

    [HttpGet("{id}")]
    public ActionResult Get(Guid id)
    {
        var card = repository.Get(id);
        if (card != null)
        {
            return Ok(card);
        }
        return NotFound();
    }

    [HttpPost]
    public Card Post([FromBody] CreateCardDto dto) => repository.Insert(dto);

    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] UpdateCardDto dto) {
        var card = repository.Update(id, dto);
        if (card != null)
        {
            return Ok(card);
        }
        return NotFound();
    }


    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return NoContent();
    }
}
