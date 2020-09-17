using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Totem.IO;

namespace Outermind.Html
{
	/// <summary>
	/// A hyperlink within an M document
	/// </summary>
	public sealed class MLink : MElementBase
	{
		internal MLink(Href href, string hrefLanguage, string relationship, string contentType, string download)
		{
			Href = href;
			HrefLanguage = hrefLanguage;
			Relationship = relationship;
			ContentType = contentType;
			Download = download;
		}

		public Href Href { get; private set; }
		public string HrefLanguage { get; private set; }
		public string Relationship { get; private set; }
		public string ContentType { get; private set; }
		public string Download { get; private set; }
		public override MPartType PartType { get { return MPartType.Link; } }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenLink(this);
		}

		protected override string GetXElementName()
		{
			return "a";
		}

		protected override void SetNonGlobals(XElement element)
		{
			element.SetAttributeValue("href", Href.IsRoot ? null : Href);
			element.SetAttributeValue("hrefLanguage", HrefLanguage == "" ? null : HrefLanguage);
			element.SetAttributeValue("relationship", Relationship == "" ? null : Relationship);
			element.SetAttributeValue("type", ContentType == "" ? null : ContentType);
			element.SetAttributeValue("download", Download == "" ? null : Download);
		}

		public MLink Rewrite(
			Href href,
			MClasses classes,
			object content,
			string id,
			string title,
			string direction,
			string language,
			bool hidden,
			MData data,
			string hrefLanguage,
			string relationship,
			string contentType,
			string download)
		{
			var same = href == Href
				&& hrefLanguage == HrefLanguage
				&& relationship == Relationship
				&& contentType == ContentType
				&& download == Download
				&& MGlobals.AreSame(this, classes, content, id, title, direction, language, hidden, data);

			return same ? this : M.Link(href, classes, content, id, title, direction, language, hidden, data, hrefLanguage, relationship, contentType, download);
		}

		public MLink Extend(
			Href href = null,
			MClasses classes = null,
			object content = null,
			string id = null,
			string title = null,
			string direction = null,
			string language = null,
			bool? hidden = null,
			MData data = null,
			string hrefLanguage = null,
			string relationship = null,
			string contentType = null,
			string download = null)
		{
			return Rewrite(
				href ?? Href,
				Classes.Extend(classes),
				content ?? Content,
				id ?? Id,
				title ?? Title,
				direction ?? Direction,
				language ?? Language,
				hidden ?? Hidden,
				data ?? Data,
				hrefLanguage ?? HrefLanguage,
				relationship ?? Relationship,
				contentType ?? ContentType,
				download ?? Download);
		}
	}
}