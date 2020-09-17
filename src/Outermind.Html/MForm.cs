using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Totem.IO;

namespace Outermind.Html
{
	/// <summary>
	/// A set of inputs within an M document
	/// </summary>
	public sealed class MForm : MElementBase
	{
		internal MForm(string name, Href action, string contentType)
		{
			Name = name;
			Action = action;
			ContentType = contentType;
		}

		public string Name { get; private set; }
		public Href Action { get; private set; }
		public string ContentType { get; private set; }
		public override MPartType PartType { get { return MPartType.Form; } }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenForm(this);
		}

		protected override string GetXElementName()
		{
			return "form";
		}

		protected override void SetNonGlobals(XElement element)
		{
			element.SetAttributeValue("name", Name);
			element.SetAttributeValue("action", Action);
			element.SetAttributeValue("enctype", ContentType == "" ? null : ContentType);
		}

		public MForm Rewrite(
			string name,
			Href action,
			MClasses classes,
			object content,
			string id,
			string title,
			string direction,
			string language,
			bool hidden,
			MData data,
			string contentType)
		{
			var same = name == Name
				&& action == Action
				&& contentType == ContentType
				&& MGlobals.AreSame(this, classes, content, id, title, direction, language, hidden, data);

			return same ? this : M.Form(name, action, classes, content, id, title, direction, language, hidden, data, contentType);
		}

		public MForm Extend(
			string name = null,
			Href action = null,
			MClasses classes = null,
			object content = null,
			string id = null,
			string title = null,
			string direction = null,
			string language = null,
			bool? hidden = null,
			MData data = null,
			string contentType = null)
		{
			return Rewrite(
				name ?? Name,
				action ?? Action,
				Classes.Extend(classes),
				content ?? Content,
				id ?? Id,
				title ?? Title,
				direction ?? Direction,
				language ?? Language,
				hidden ?? Hidden,
				data ?? Data,
				contentType ?? ContentType);
		}
	}
}