using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Totem.IO;

namespace Outermind.Html
{
	/// <summary>
	/// A quotation of another source within an M document
	/// </summary>
	public sealed class MQuotation : MElementBase
	{
		internal MQuotation(Href cite)
		{
			Cite = cite;
		}

		public Href Cite { get; private set; }
		public override MPartType PartType { get { return MPartType.Quotation; } }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenQuotation(this);
		}

		protected override string GetXElementName()
		{
			return "blockquote";
		}

		protected override void SetNonGlobals(XElement element)
		{
			element.SetAttributeValue("cite", Cite.IsRoot ? null : Cite);
		}

		public MQuotation Rewrite(
			MClasses classes,
			object content,
			string id,
			string title,
			string direction,
			string language,
			bool hidden,
			MData data,
			Href cite)
		{
			return cite == Cite && MGlobals.AreSame(this, classes, content, id, title, direction, language, hidden, data)
				? this
				: M.Quotation(classes, content, id, title, direction, language, hidden, data, cite);
		}

		public MQuotation Extend(
			MClasses classes = null,
			object content = null,
			string id = null,
			string title = null,
			string direction = null,
			string language = null,
			bool? hidden = null,
			MData data = null,
			Href cite = null)
		{
			return Rewrite(
				Classes.Extend(classes),
				content ?? Content,
				id ?? Id,
				title ?? Title,
				direction ?? Direction,
				language ?? Language,
				hidden ?? Hidden,
				data ?? Data,
				cite ?? Cite);
		}
	}
}