using System;
using System.Collections.Generic;
using System.Linq;

namespace Totem.Html
{
	/// <summary>
	/// A part of an M document representing an HTML element
	/// </summary>
	public sealed class MElement : MElementBase
	{
		private readonly MPartType _partType;

		internal MElement(MPartType partType)
		{
			_partType = partType;
		}

		public override MPartType PartType { get { return _partType; } }

		public override MPart Visit(MFlow flow)
		{
			switch(PartType)
			{
				case MPartType.DocumentBody: return flow.WhenDocumentBody(this);
				case MPartType.Main: return flow.WhenMain(this);
				case MPartType.Header: return flow.WhenHeader(this);
				case MPartType.Footer: return flow.WhenFooter(this);
				case MPartType.Navigation: return flow.WhenNavigation(this);
				case MPartType.Article: return flow.WhenArticle(this);
				case MPartType.Section: return flow.WhenSection(this);
				case MPartType.Aside: return flow.WhenAside(this);
				case MPartType.Heading1: return flow.WhenHeading1(this);
				case MPartType.Heading2: return flow.WhenHeading2(this);
				case MPartType.Heading3: return flow.WhenHeading3(this);
				case MPartType.Heading4: return flow.WhenHeading4(this);
				case MPartType.Heading5: return flow.WhenHeading5(this);
				case MPartType.Heading6: return flow.WhenHeading6(this);
				case MPartType.Paragraph: return flow.WhenParagraph(this);
				case MPartType.Code: return flow.WhenCode(this);
				case MPartType.Literal: return flow.WhenLiteral(this);
				case MPartType.Citation: return flow.WhenCitation(this);
				case MPartType.List: return flow.WhenList(this);
				case MPartType.ListItem: return flow.WhenListItem(this);
				case MPartType.DescriptionList: return flow.WhenDescriptionList(this);
				case MPartType.DescriptionTerm: return flow.WhenDescriptionTerm(this);
				case MPartType.Description: return flow.WhenDescription(this);
				default: throw new Exception($"UnsupportedPartType:{PartType}");
      }
		}

		protected override string GetXElementName()
		{
			switch(PartType)
			{
				case MPartType.DocumentBody: return "body";
				case MPartType.Main: return "main";
				case MPartType.Header: return "header";
				case MPartType.Footer: return "footer";
				case MPartType.Navigation: return "nav";
				case MPartType.Article: return "article";
				case MPartType.Section: return "section";
				case MPartType.Aside: return "aside";
				case MPartType.Heading1: return "h1";
				case MPartType.Heading2: return "h2";
				case MPartType.Heading3: return "h3";
				case MPartType.Heading4: return "h4";
				case MPartType.Heading5: return "h5";
				case MPartType.Heading6: return "h6";
				case MPartType.Paragraph: return "p";
				case MPartType.Code: return "code";
				case MPartType.Literal: return "pre";
				case MPartType.Citation: return "cite";
				case MPartType.List: return "ol";
				case MPartType.ListItem: return "li";
				case MPartType.DescriptionList: return "dl";
				case MPartType.DescriptionTerm: return "dt";
				case MPartType.Description: return "dd";
				default: throw new Exception($"UnsupportedPartType:{PartType}");
			}
		}

		public MElement Rewrite(
			MClasses classes,
			object content,
			string id,
			string title,
			string direction,
			string language,
			bool hidden,
			MData data)
		{
			return MGlobals.AreSame(this, classes, content, id, title, direction, language, hidden, data)
				? this
				: MGlobals.Init(new MElement(_partType), classes, content, id, title, direction, language, hidden, data);
		}

		public MElement Extend(
			MClasses classes = null,
			object content = null,
			string id = null,
			string title = null,
			string direction = null,
			string language = null,
			bool? hidden = null,
			MData data = null)
		{
			return Rewrite(
				Classes.Extend(classes),
				content ?? Content,
				id ?? Id,
				title ?? Title,
				direction ?? Direction,
				language ?? Language,
				hidden ?? Hidden,
				data ?? Data);
		}
	}
}