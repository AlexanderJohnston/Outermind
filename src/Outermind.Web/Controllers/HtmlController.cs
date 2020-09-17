using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Totem.Timeline.Mvc;
using Outermind.Html;
using Totem;

namespace Outermind.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HtmlController : ControllerBase
  {
    public ICommandServer _commands;
    public IQueryServer _queries;

    public HtmlController(ICommandServer commands, IQueryServer queries)
    {
      _commands = commands;
      _queries = queries;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
      return Ok("hello world");
    }

    [HttpGet("[action]")]
    [Produces("text/html")]
    public async Task<IActionResult> Test()
    {
      var rawData = new Dictionary<string, string>();
      rawData.Add("Key1", "Value1");
      rawData.Add("Key2", "Value2");
      rawData.Add("Key3", "Value3");
      rawData.Add("Key4", "Value4");
      rawData.Add("Key5", "Value5");
      rawData.Add("Key6", "Value6");
      rawData.Add("Key7", "Value7");

      var rawClasses = Many.Of<string>(new[] { "Class1", "Class2", "Class3" });

      var data = M.Data(rawData);
      var classes = M.Classes(rawClasses);
      var main = M.Main(
        classes,
        "Put the value here",
        "Content ID?",
        "Content title?",
        "",
        "English",
        false,
        data
        );

      var head = M.DocumentHead("Outermind Title", "Should be an Href", new MDocumentLink[0]);
      var document = M.Document(head, main);
      return Content(document.ToText());
    }
  }
}
