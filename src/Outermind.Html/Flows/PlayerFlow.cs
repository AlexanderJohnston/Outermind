using System;
using System.Collections.Generic;
using System.Text;

namespace Outermind.Html.Flows
{
  public class PlayerFlow : MFlow
  {
    public List<MPart> Sections = new List<MPart>();

    public override MPart WhenDocumentBody(MElement node)
    {
      return base.WhenDocumentBody(node);
    }

    public override MPart WhenSection(MElement node)
    {
      return base.WhenSection(node);
    }

    public override MPart WhenLink(MLink node)
    {
      return base.WhenLink(node);
    }
  }
}
