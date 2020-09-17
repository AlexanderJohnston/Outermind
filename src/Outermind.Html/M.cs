using System;
using System.Collections.Generic;
using System.Linq;
using Totem;
using Totem.IO;
using static Totem.Timeline.FlowCall;

namespace Totem.Html
{
	/// <summary>
	/// A language for defining and interpreting knowledge as HTML documents
	/// </summary>
	public static class M
	{
		public static MDocument Document(MClasses classes, MDocumentHead head, MElement body)
		{
			return new MDocument(classes, head, body);
		}

		public static MDocument Document(MDocumentHead head, MElement body)
		{
			return Document(MClasses.None, head, body);
		}

		//
		// DocumentHead
		//

		public static MDocumentHead DocumentHead(string title, Href baseHref, Many<MDocumentLink> links)
		{
			return new MDocumentHead(title, baseHref, links);
		}

		public static MDocumentHead DocumentHead(string title, Href baseHref, IEnumerable<MDocumentLink> links)
		{
			return new MDocumentHead(title, baseHref, links.ToMany());
		}

		public static MDocumentHead DocumentHead(string title, Href baseHref, params MDocumentLink[] links)
		{
			return new MDocumentHead(title, baseHref, links.ToMany());
		}

		public static MDocumentHead DocumentHead(string title, string baseHref, Many<MDocumentLink> links)
		{
			return DocumentHead(title, Href.From(baseHref), links);
		}

		public static MDocumentHead DocumentHead(string title, string baseHref, IEnumerable<MDocumentLink> links)
		{
			return DocumentHead(title, Href.From(baseHref), links);
		}

		public static MDocumentHead DocumentHead(string title, string baseHref, params MDocumentLink[] links)
		{
			return DocumentHead(title, Href.From(baseHref), links);
		}

		//
		// DocumentLink
		//

		public static MDocumentLink DocumentLink(
			Href href,
			MClasses classes = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			string hrefLanguage = "",
			string relationship = "",
			string contentType = "")
		{
			return MGlobals.Init(new MDocumentLink(href, hrefLanguage, relationship, contentType), classes, MContent.None, id, title, direction, language, hidden, data);
		}

		public static MDocumentLink DocumentLink(
			string href,
			MClasses classes = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			string hrefLanguage = "",
			string relationship = "",
			string contentType = "")
		{
			return DocumentLink(Href.From(href), classes, id, title, direction, language, hidden, data);
		}

		//
		// DocumentBody
		//

		public static MElement DocumentBody(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.DocumentBody, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Element
		//

		internal static MElement Element(
			MPartType partType,
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return MGlobals.Init(new MElement(partType), classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Content
		//

		public static MContent Content(
			object value,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return MGlobals.InitContent(new MContent(Flatten(value)), MClasses.None, id, title, direction, language, hidden, data);
		}

		public static MContent Content(
			MClasses classes,
			object value,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return MGlobals.InitContent(new MContent(Flatten(value)), classes, id, title, direction, language, hidden, data);
		}

		private static object Flatten(object item)
		{
			if(item == null)
			{
				return null;
			}

			var content = item as MContent;

			if(content != null)
			{
				return Flatten(content);
			}

			var items = item as IReadOnlyList<object>;

			if(items != null)
			{
				return Flatten(items);
			}

			return item;
		}

		private static object Flatten(MContent content)
		{
			return content.Rewrite(Flatten(content.Value));
		}

		private static object Flatten(IReadOnlyList<object> items)
		{
			switch(items.Count)
			{
				case 0: return null;
				case 1: return Flatten(items[0]);
				default: return items
					.Select(Flatten)
					.Where(item => item != null)
					.ToArray();
			}
		}

		//
		// Classes
		//

		public static MClasses Classes(Many<string> names)
		{
			return new MClasses(names);
		}

		public static MClasses Classes(IEnumerable<string> names)
		{
			return Classes(names.ToMany());
		}

		public static MClasses Classes(string names)
		{
			return Classes(names.Split(' '));
		}

		//
		// Data
		//

		public static MData Data(Dictionary<string, string> data)
		{
			return new MData(data);
		}

		public static MData Data(IEnumerable<KeyValuePair<string, string>> pairs)
		{
			var data = new Dictionary<string, string>();

			foreach(var pair in pairs)
			{
				data.Add(pair.Key, pair.Value);
			}

			return Data(data);
		}

		public static MData Data(string key, string value)
		{
			return Data(new Dictionary<string, string>
			{
				{ key, value }
			});
		}

		//
		// Main
		//
#pragma warning disable 0028	// has the wrong signature to be an entry point
		public static MElement Main(
#pragma warning restore 0028
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Main, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Header
		//

		public static MElement Header(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Header, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Footer
		//

		public static MElement Footer(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Footer, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Navigation
		//

		public static MElement Navigation(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Navigation, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Article
		//

		public static MElement Article(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Article, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Section
		//

		public static MElement Section(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Section, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Aside
		//

		public static MElement Aside(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Aside, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Heading1
		//

		public static MElement Heading1(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Heading1, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Heading2
		//

		public static MElement Heading2(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Heading2, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Heading3
		//

		public static MElement Heading3(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Heading3, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Heading4
		//

		public static MElement Heading4(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Heading4, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Heading5
		//

		public static MElement Heading5(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Heading5, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Heading6
		//

		public static MElement Heading6(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Heading6, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Paragraph
		//

		public static MElement Paragraph(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Paragraph, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Link
		//

		public static MLink Link(
			Href href,
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			string relationship = "",
			string contentType = null,
			string download = "",
			string hrefLanguage = "")
		{
			var link = new MLink(href, hrefLanguage, relationship, contentType, download);

			return MGlobals.Init(link, classes, content, id, title, direction, language, hidden, data);
		}

		public static MLink Link(
			string href,
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			string relationship = "",
			string contentType = null,
			string download = "",
			string hrefLanguage = "")
		{
			return Link(Href.From(href), classes, content, id, title, direction, language, hidden, data, relationship, contentType, download, hrefLanguage);
		}

		//
		// Code
		//

		public static MElement Code(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Code, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Literal
		//

		public static MElement Literal(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Literal, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Time
		//

		public static MTime Time(
			When? when,
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return MGlobals.Init(new MTime(when), classes, content, id, title, direction, language, hidden, data);
		}

		public static MTime Time(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return MGlobals.Init(new MTime(null), classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Quotation
		//

		public static MQuotation Quotation(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			Href cite = null)
		{
			return MGlobals.Init(new MQuotation(cite), classes, content, id, title, direction, language, hidden, data);
		}

		public static MQuotation Quotation(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			string cite = null)
		{
			return Quotation(classes, content, id, title, direction, language, hidden, data, cite == null ? null : Href.From(cite));
		}

		//
		// Citation
		//

		public static MElement Citation(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Citation, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// List
		//

		public static MElement List(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.List, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// ListItem
		//

		public static MElement ListItem(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.ListItem, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// DescriptionList
		//

		public static MElement DescriptionList(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.DescriptionList, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// DescriptionTerm
		//

		public static MElement DescriptionTerm(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.DescriptionTerm, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Description
		//

		public static MElement Description(
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			return Element(MPartType.Description, classes, content, id, title, direction, language, hidden, data);
		}

		//
		// Form
		//

		public static MForm Form(
			string name,
			Href action,
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			string contentType = "text/html")
		{
			return MGlobals.Init(new MForm(name, action, contentType), classes, content, id, title, direction, language, hidden, data);
		}

		public static MForm Form(
			string name,
			string action,
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null,
			string contentType = "text/html")
		{
			return Form(name, Href.From(action), classes, content, id, title, direction, language, hidden, data, contentType);
		}
	}
}