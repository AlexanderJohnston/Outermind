using System;
using System.Collections.Generic;
using System.Linq;
using Totem;
using Totem.Runtime;

namespace Outermind.Html
{
	/// <summary>
	/// A part of an M document
	/// </summary>
	public abstract class MPart : Notion
	{
		public abstract MPartType PartType { get; }

		public abstract MPart Visit(MFlow flow);

		public Text ToText()
		{
			return ToText();
		}

		public abstract Text ToText(bool indent = true);

		public abstract object ToXml();
	}
}