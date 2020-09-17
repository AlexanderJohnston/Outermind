using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Totem.Http;

namespace Totem.Html
{
	/// <summary>
	/// General information about an M document
	/// </summary>
	public sealed class MDocumentHead : MPart
	{
		internal MDocumentHead(string title, Href baseHref, Many<MDocumentLink> links)
		{
			Title = title;
			BaseHref = baseHref;
			Links = links;
		}

		public string Title { get; private set; }
		public Href BaseHref { get; private set; }
		public Many<MDocumentLink> Links { get; private set; }
		public override MPartType PartType { get { return MPartType.DocumentHead; } }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenDocumentHead(this);
		}

		public MDocumentHead Rewrite(string title, Href baseHref, Many<MDocumentLink> links)
		{
			return title == Title && baseHref == BaseHref && links == Links ? this : new MDocumentHead(title, baseHref, links);
		}

		public MDocumentHead Extend(string title = null, Href baseHref = null, Many<MDocumentLink> links = null)
		{
			return Rewrite(
				title ?? Title,
				baseHref ?? BaseHref,
				links ?? Links);
		}

		public override Text ToText(bool indent)
		{
			return ((XElement) ToXml()).ToText(indent);
		}

		public override object ToXml()
		{
			return new XElement("head", GetXElementContent());
		}

		private IEnumerable<object> GetXElementContent()
		{
			yield return new XElement("title", Title);

			if(!BaseHref.IsRoot)
			{
				yield return new XElement("base", new XAttribute("href", BaseHref));
			}

			foreach(var link in Links)
			{
				yield return link.ToXml();
			}
		}
	}
}