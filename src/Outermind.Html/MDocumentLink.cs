using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Totem.Http;

namespace Totem.Html
{
	/// <summary>
	/// A relationship to an external resource of an M document
	/// </summary>
	public sealed class MDocumentLink : MElementBase
	{
		internal MDocumentLink(Href href, string hrefLanguage, string relationship, string contentType)
		{
			Href = href;
			HrefLanguage = hrefLanguage;
			Relationship = relationship;
			ContentType = contentType;
		}

		public Href Href { get; private set; }
		public string HrefLanguage { get; private set; }
		public string Relationship { get; private set; }
		public string ContentType { get; private set; }
		public override MPartType PartType { get { return MPartType.DocumentLink; } }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenDocumentLink(this);
		}

		public MDocumentLink Rewrite(
			Href href,
			MClasses classes,
			string id,
			string title,
			string direction,
			string language,
			bool hidden,
			MData data,
			string hrefLanguage,
			string relationship,
			string contentType)
		{
			var same = href == Href
				&& hrefLanguage == HrefLanguage
				&& relationship == Relationship
				&& contentType == ContentType
				&& MGlobals.AreSame(this, classes, MContent.None, id, title, direction, language, hidden, data);

			return same ? this : M.DocumentLink(href, classes, id, title, direction, language, hidden, data, hrefLanguage, relationship, contentType);
		}

		public MDocumentLink Extend(
			Href href = null,
			MClasses classes = null,
			string id = null,
			string title = null,
			string direction = null,
			string language = null,
			bool? hidden = null,
			MData data = null,
			string hrefLanguage = null,
			string relationship = null,
			string contentType = null)
		{
			return Rewrite(
				href ?? Href,
				Classes.Extend(classes),
				id ?? Id,
				title ?? Title,
				direction ?? Direction,
				language ?? Language,
				hidden ?? Hidden,
				data ?? Data,
				hrefLanguage ?? HrefLanguage,
				relationship ?? Relationship,
				contentType ?? ContentType);
		}

		protected override string GetXElementName()
		{
			return "link";
		}

		protected override void SetNonGlobals(XElement element)
		{
			element.SetAttributeValue("href", Href);
			element.SetAttributeValue("hrefLanguage", HrefLanguage == "" ? null : HrefLanguage);
			element.SetAttributeValue("rel", Relationship == "" ? null : Relationship);
			element.SetAttributeValue("type", ContentType == "" ? null : ContentType);
		}
	}
}