using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Totem;
using Totem.IO;

namespace Outermind.Html
{
	/// <summary>
	/// An M document
	/// </summary>
	public sealed class MDocument : MPart
	{
		internal MDocument(MClasses classes, MDocumentHead head, MElement body)
		{
			Classes = classes;
			Head = head;
			Body = body;
		}

		public MClasses Classes { get; private set; }
		public MDocumentHead Head { get; private set; }
		public MElement Body { get; private set; }
		public override MPartType PartType { get { return MPartType.Document; } }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenDocument(this);
		}

		public MDocument Rewrite(MClasses classes, MDocumentHead head, MElement body)
		{
			return head == Head && body == Body ? this : new MDocument(classes, head, body);
		}

		public sealed override Text ToText(bool indent)
		{
			return ToText(indent);
		}

		public Text ToText(bool indent = true, bool omitDeclaration = true)
		{
      var document = ((XDocument) ToXml());

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
			var html = new XElement("html", Head.ToXml(), Body.ToXml());

			if(Classes != MClasses.None)
			{
				html.SetAttributeValue("class", Classes);
			}

			return new XDocument(html);
		}

		//
		// Factory
		//

		public static MDocument From(XDocument xml)
		{
			return xml.ReadMDocument();
		}

		public static MDocument From(Stream xml, LoadOptions loadOptions = default(LoadOptions))
		{
			return From(XDocument.Load(xml, loadOptions));
		}

		public static MDocument From(Text xml, LoadOptions loadOptions = default(LoadOptions))
		{
			return From(xml.ToStream(), loadOptions);
		}

		public static MDocument From(Text xml, Encoding encoding, LoadOptions loadOptions = default(LoadOptions))
		{
			return From(xml.ToStream(encoding), loadOptions);
		}

		public static MDocument From(string xml, LoadOptions loadOptions = default(LoadOptions))
		{
			return From(Text.Of(xml), loadOptions);
		}

		public static MDocument From(string xml, Encoding encoding = null, LoadOptions loadOptions = default(LoadOptions))
		{
			return From(Text.Of(xml), encoding, loadOptions);
		}
	}
}