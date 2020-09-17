using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Totem.Runtime;

namespace Outermind.Html
{
	/// <summary>
	/// The global attributes, data attributes, and content associated with an M element
	/// </summary>
	public static class MGlobals
	{
		public static readonly Field<MClasses> Classes = Field.Declare(() => Classes, MClasses.None);
		public static readonly Field<object> Content = Field.Declare(() => Content, null);
		public static readonly Field<string> Id = Field.Declare(() => Id, "");
		public static readonly Field<string> Title = Field.Declare(() => Title, "");
		public static readonly Field<string> Language = Field.Declare(() => Language, "");
		public static readonly Field<string> Direction = Field.Declare(() => Direction, "");
		public static readonly Field<bool> Hidden = Field.Declare(() => Hidden, false);
		public static readonly Field<MData> Data = Field.Declare(() => Data, MData.None);

		internal static bool AreSame(
			MElementBase element,
			MClasses classes,
			object content,
			string id,
			string title,
			string direction,
			string language,
			bool hidden,
			MData data)
		{
			return classes == Classes.Get(element)
				&& content == Content.Get(element)
				&& id == Id.Get(element)
				&& title == Title.Get(element)
				&& direction == Direction.Get(element)
				&& language == Language.Get(element)
				&& hidden == Hidden.Get(element)
				&& data == Data.Get(element);
		}

		internal static TElement Init<TElement>(
			TElement element,
			MClasses classes = null,
			object content = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
			where TElement : MElementBase
		{
			Classes.Set(element, classes ?? MClasses.None);
			Content.Set(element, M.Content(content));
			Id.Set(element, id);
			Title.Set(element, title);
			Direction.Set(element, direction);
			Language.Set(element, language);
			Hidden.Set(element, hidden);
			Data.Set(element, data ?? MData.None);

			return element;
		}

		internal static MContent InitContent(
			MContent content,
			MClasses classes = null,
			string id = "",
			string title = "",
			string direction = "",
			string language = "",
			bool hidden = false,
			MData data = null)
		{
			Classes.Set(content, classes ?? MClasses.None);
			Id.Set(content, id);
			Title.Set(content, title);
			Direction.Set(content, direction);
			Language.Set(content, language);
			Hidden.Set(content, hidden);
			Data.Set(content, data ?? MData.None);

			return content;
		}

		internal static XElement InitXElement(MElementBase element, XElement xElement)
		{
			xElement.SetAttributeValue("class", element.Classes == MClasses.None ? null : element.Classes);
			xElement.SetAttributeValue("id", element.Id == "" ? null : element.Id);
			xElement.SetAttributeValue("title", element.Title == "" ? null : element.Title);
			xElement.SetAttributeValue("dir", element.Direction == "" ? null : element.Direction);
			xElement.SetAttributeValue("lang", element.Language == "" ? null : element.Language);
			xElement.SetAttributeValue("hidden", !element.Hidden ? null : element.Hidden.ToString().ToLower());

			var part = element.Content as MPart;

			xElement.Add(part != null ? part.ToXml() : element.Content);

			return xElement;
		}
	}
}