using System;
using System.Collections.Generic;
using System.Linq;

namespace Totem.Html
{
	/// <summary>
	/// A part of an M document
	/// </summary>
	public abstract class MPart : Notion
	{
		public abstract MPartType PartType { get; }

		public abstract MPart Visit(MFlow flow);

		public sealed override Text ToText()
		{
			return ToText();
		}

		public abstract Text ToText(bool indent = true);

		public abstract object ToXml();
	}
}