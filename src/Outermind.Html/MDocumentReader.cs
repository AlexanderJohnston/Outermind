using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Totem.IO;

namespace Outermind.Html
{
	/// <summary>
	/// Reads an M document from HTML text using LINQ to XML
	/// </summary>
	public static class MDocumentReader
	{
		public static MDocument ReadMDocument(this XDocument xml)
		{
			//xml.Root.ExpectName("html");

			return M.Document(
				xml.ReadDocumentClasses(),
				xml.ReadDocumentHead(),
				xml.ReadDocumentBody());
		}

		private static MClasses ReadDocumentClasses(this XDocument xml)
		{
			var classes = xml.Root.Attribute("class");

			return classes == null ? MClasses.None : M.Classes((string) classes);
		}

		private static MDocumentHead ReadDocumentHead(this XDocument xml)
		{
			var head = xml.Root.Element("head");

			return M.DocumentHead(
				head.Element("title").ToString(),
				head.ReadDocumentBase(),
				head.Elements("link").Select(ReadDocumentLink));
		}

		private static Href ReadDocumentBase(this XElement head)
		{
			var @base = head.Element("base");

			return @base == null ? HttpResource.Root : @base.RequiredHref();
		}

		private static MDocumentLink ReadDocumentLink(this XElement element)
		{
			var href = element.RequiredHref();
			var hrefLanguage = element.Attribute("hrefLanguage");
			var relationship = element.Attribute("relationship");
			var type = element.Attribute("type");

			return element.InitGlobals(M.DocumentLink(
				href,
				hrefLanguage: hrefLanguage == null ? "" : (string) hrefLanguage,
				relationship: relationship == null ? "" : (string) relationship,
				contentType: type == null ? "" : (string) type));
		}

		private static Href RequiredHref(this XElement element, string attributeName = "href")
		{
			return Href.From(element.Attribute(attributeName).ToString());
		}

		private static MElement ReadDocumentBody(this XDocument html)
		{
			return html.Root.Element("body").ReadElement(MPartType.DocumentBody);
		}

		//
		// Elements
		//

		private static T InitGlobals<T>(this XElement element, T part) where T : MElementBase
		{
			var classes = element.Attribute("class");
			var id = element.Attribute("id");
			var title = element.Attribute("title");
			var direction = element.Attribute("dir");
			var language = element.Attribute("lang");
			var hidden = element.Attribute("hidden");

			return MGlobals.Init(
				part,
				classes == null ? null : M.Classes((string) classes),
				element.ReadContent(),
				id == null ? "" : (string) id,
				title == null ? "" : (string) title,
				direction == null ? "" : (string) direction,
				language == null ? "" : (string) language,
				hidden != null && (bool) hidden,
				element.ReadData());
		}

		private static MContent ReadContent(this XElement element)
		{
			return M.Content(element.Elements().Select(ReadElement).ToList());
		}

    private static MData ReadData(this XElement element)
		{
			return M.Data(
				from attribute in element.Attributes()
				let name = attribute.Name.ToString()
				where name.Length > 5 && name.StartsWith("data-")
				select new KeyValuePair<string, string>(attribute.Name.ToString(), (string) attribute));
		}

		private static MElementBase ReadElement(this XElement element)
		{
			switch(element.Name.ToString())
			{
				case "span":
				case "div": return element.ReadElement(MPartType.Section);/*element.ReadContent();*/
				case "main": return element.ReadElement(MPartType.Main);
				case "header": return element.ReadElement(MPartType.Header);
				case "footer": return element.ReadElement(MPartType.Footer);
				case "nav": return element.ReadElement(MPartType.Navigation);
				case "article": return element.ReadElement(MPartType.Article);
				case "section": return element.ReadElement(MPartType.Section);
				case "aside": return element.ReadElement(MPartType.Aside);
				case "h1": return element.ReadElement(MPartType.Heading1);
				case "h2": return element.ReadElement(MPartType.Heading2);
				case "h3": return element.ReadElement(MPartType.Heading3);
				case "h4": return element.ReadElement(MPartType.Heading4);
				case "h5": return element.ReadElement(MPartType.Heading5);
				case "h6": return element.ReadElement(MPartType.Heading6);
				case "p": return element.ReadElement(MPartType.Paragraph);
				case "a": return element.ReadLink();
				case "code": return element.ReadElement(MPartType.Code);
				case "pre": return element.ReadElement(MPartType.Literal);
				case "blockquote": return element.ReadQuotation();
				case "cite": return element.ReadElement(MPartType.Citation);
				case "ul":
				case "ol": return element.ReadElement(MPartType.List);
				case "li": return element.ReadElement(MPartType.ListItem);
				case "dl": return element.ReadElement(MPartType.DescriptionList);
				case "dt": return element.ReadElement(MPartType.DescriptionTerm);
				case "dd": return element.ReadElement(MPartType.Description);
				case "form": return element.ReadForm();
				default: throw new Exception($"UnsupportedElement:{element.Name}");
			}
		}

		private static MElement ReadElement(this XElement element, MPartType type)
		{
			return element.InitGlobals(new MElement(type));
		}

		private static MLink ReadLink(this XElement element)
		{
			var href = element.RequiredHref();
			var hrefLanguage = element.Attribute("hrefLanguage");
			var relationship = element.Attribute("rel");
			var contentType = element.Attribute("type");

			return element.InitGlobals(M.Link(
				href,
				hrefLanguage: hrefLanguage == null ? "" : (string) hrefLanguage,
				relationship: relationship == null ? "" : (string) relationship,
				contentType: contentType == null ? "" : (string) contentType));
		}

		private static MQuotation ReadQuotation(this XElement element)
		{
			var cite = element.Attribute("cite");

			return element.InitGlobals(M.Quotation(cite: cite == null ? "" : (string) cite));
		}

		private static MForm ReadForm(this XElement element)
		{
			var name = element.Attribute("name").ToString();
			var action = element.RequiredHref("action");
			var contentType = element.Attribute("enctype");

			return element.InitGlobals(M.Form(name, action, contentType: contentType == null ? "" : (string) contentType));
		}
	}
}