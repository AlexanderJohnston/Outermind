using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static Totem.Timeline.FlowCall;

namespace Totem.Html
{
	/// <summary>
	/// A quotation of another source within an M document
	/// </summary>
	public sealed class MTime : MElementBase
	{
		internal MTime(When? when)
		{
			When = when;
		}

		public When? When { get; private set; }
		public override MPartType PartType { get { return MPartType.Time; } }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenTime(this);
		}

		protected override string GetXElementName()
		{
			return "time";
		}

		protected override void SetNonGlobals(XElement element)
		{
			element.SetAttributeValue("datetime", When);
		}

		public MTime Rewrite(
			When? when,
			MClasses classes,
			object content,
			string id,
			string title,
			string direction,
			string language,
			bool hidden,
			MData data)
		{
			return when == When && MGlobals.AreSame(this, classes, content, id, title, direction, language, hidden, data)
				? this
				: M.Time(when, classes, content, id, title, direction, language, hidden, data);
		}

		public MTime Extend(
			When? when = null,
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
				when ?? When,
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