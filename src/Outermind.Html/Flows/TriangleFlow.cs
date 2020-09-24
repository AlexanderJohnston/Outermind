using System;
using System.Collections.Generic;
using System.Text;

namespace Outermind.Html.Flows
{
  public class TriangleFlow : MFlow
  {
    List<MPart> Sections = new List<MPart>();

    public override MPart WhenDocumentBody(MElement node)
    {
      var baseNode = base.WhenDocumentBody(node);
      //var newPart = WhenSection(node);
      return baseNode;
    }

    public override MPart WhenSection(MElement node)
    {
      MPart baseNode = base.WhenSection(node);
      Sections.Add(baseNode);
      return baseNode;
    }

    public override MPart WhenLink(MLink node)
    {
      Console.WriteLine(node.Content);
      return base.WhenLink(node);
    }
  }
}
