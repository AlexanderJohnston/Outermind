using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Totem;

namespace Outermind.Html
{
	/// <summary>
	/// Base implementation of a part of an M document representing an HTML element
	/// </summary>
	public abstract class MElementBase : MPart
	{
		internal MElementBase()
		{}

		public MClasses Classes { get { return MGlobals.Classes.Get(this); } }
		public object Content { get { return MGlobals.Content.Get(this); } }
		public string Id { get { return MGlobals.Id.Get(this); } }
		public string Title { get { return MGlobals.Title.Get(this); } }
		public string Language { get { return MGlobals.Language.Get(this); } }
		public string Direction { get { return MGlobals.Direction.Get(this); } }
		public bool Hidden { get { return MGlobals.Hidden.Get(this); } }
		public MData Data { get { return MGlobals.Data.Get(this); } }

		public override Text ToText(bool indent)
		{
      var document = ((XElement)ToXml());
      StringBuilder sb = new StringBuilder();
      XmlWriterSettings xws = new XmlWriterSettings();
      xws.OmitXmlDeclaration = true;
      xws.Indent = true;

      using (XmlWriter xw = XmlWriter.Create(sb, xws))
      {
        document.Save(xw);
      }

      return sb.ToString();
    }

    public override object ToXml()
		{
			var element = new XElement(GetXElementName());

			SetNonGlobals(element);

			return MGlobals.InitXElement(this, element);
		}

		protected abstract string GetXElementName();

		protected virtual void SetNonGlobals(XElement element)
		{}
	}
}