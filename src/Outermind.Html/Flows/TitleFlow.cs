using System;
using System.Collections.Generic;
using System.Text;

namespace Outermind.Html.Flows
{
  public class TitleFlow : MFlow
  {
    public override MPart WhenDocumentHead(MDocumentHead node)
    {
      var words = node.Title.Split(' ');
      var sb = new StringBuilder();
      foreach (var word in words)
      {
        sb.Append($"{word}({word.Length}) ");
      }
      var transform = node.Rewrite(sb.ToString(), node.BaseHref, node.Links);
      return base.WhenDocumentHead(transform);
    }
  }
}
