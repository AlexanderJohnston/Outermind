using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Totem.Timeline.Mvc;
using Outermind.Html;
using Totem;
using Outermind.Queries;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Totem.Timeline.Area;
using Totem.Timeline.Runtime;
using Totem.Timeline.Client;
using Totem.Runtime.Json;
using System.Text;
using Outermind.Html.Flows;
using System.Xml.Linq;

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
    public async Task<IActionResult> Test2([FromServices] IQueryDb queries)
    {
      var query = await queries.ReadQuery<StackQuery>();

      var contract = new HtmlContractMediator();
      var props = contract.CreateProperties(query.GetType());

      var sb = new StringBuilder();
      foreach (var prop in props)
      {
        //var label = new MFlow();
        //sb.AppendLine(label.ToString());
        sb.Append(prop);
      }
      return Content(sb.ToString());
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
      var docClasses = "top-level-class custom-class";

      var data = M.Data(rawData);
      var classes = M.Classes(rawClasses);
      var main = M.Main(
        "m-query",
        "Put the value here",
        "Content ID?",
        "Content title?",
        "",
        "English",
        false,
        data
        );

      var link = M.DocumentLink("/api/card/stack", null, "0");
      var head = M.DocumentHead("Card Query", "http://localhost:5000/", link);

      var mTileFlow = new TitleFlow();
      var flowingHead = mTileFlow.WhenDocumentHead(head);


      TextReader tr = new StreamReader("./_scratchpad.html");
      var output = tr.ReadToEnd();
      var xDoc = XDocument.Parse(output);
      //var xDoc = XDocument.Load(tr);
      var mDoc = MDocumentReader.ReadMDocument(xDoc);

      var triangleFlow = new TriangleFlow();
      var triangleWhen = triangleFlow.When(mDoc);
      var triangleDoc = triangleFlow.WhenDocument(mDoc);


      var query = (QueryContentResult) await _queries.Get<StackQuery>();
      var text = query.Content.ReadData();
      var serial = new StreamReader(text).ReadToEnd();
      var body = M.DocumentBody("m-query", JsonConvert.SerializeObject(serial), "0");
      var document = M.Document(head, body);

      return Content(document.ToText());
    }
  }
}
