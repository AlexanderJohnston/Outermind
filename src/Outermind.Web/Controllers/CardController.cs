using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Outermind;
using Outermind.Queries;
using Totem.Timeline.Mvc;

namespace Outermind.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CardController : ControllerBase
  {
    public ICommandServer _commands;
    public IQueryServer _queries;

    public CardController(ICommandServer commands, IQueryServer queries)
    {
      _commands = commands;
      _queries = queries;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Stack()
    {
      return await _queries.Get<StackQuery>();
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Create([FromBody] Card card)
    {
      var creation = new CreateCard(card);
      Console.WriteLine("Created card {0}.", card.Id);
      return await _commands.Execute(creation, When<CardCreated>.ThenOk);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Move([FromBody] Card card)
    {
      var moving = new MoveCard(card);
      Console.WriteLine("Moving card {0}.", card.Id);
      return await _commands.Execute(moving, When<CardMoved>.ThenOk);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Resize([FromBody] Card card)
    {
      var resizing = new ResizeCard(card);
      Console.WriteLine("Resizing card {0}.", card.Id);
      return await _commands.Execute(resizing, When<CardResized>.ThenOk);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Remove([FromBody] Card card)
    {
      var removing = new RemoveCard(card);
      Console.WriteLine("Removing card {0}.", card.Id);
      return await _commands.Execute(removing, When<CardResized>.ThenOk);
    }
  }
}
