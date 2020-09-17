using System;
using System.Collections.Generic;
using System.Linq;

namespace Totem.Html
{
	/// <summary>
	/// Content within an M document
	/// </summary>
	public sealed class MContent : MPart
	{
		public static readonly MContent None = new MContent(null);

		internal MContent(object value)
		{
			Value = value;
			IsNone = Value == null;
			IsPart = !IsNone && Value is MPart;
			IsMany = !IsPart && Value is IReadOnlyList<object>;
		}

		public override MPartType PartType { get { return MPartType.Content; } }

		public object Value { get; private set; }
		public bool IsNone { get; private set; }
		public bool IsPart { get; private set; }
		public bool IsMany { get; private set; }

		public override MPart Visit(MFlow flow)
		{
			return flow.WhenContent(this);
		}

		public MContent Rewrite(object value)
		{
			return value == Value ? this : M.Content(value);
		}

		public override Text ToText(bool indent)
		{
			if(IsNone)
			{
				return Text.Of(string.Empty);
			}

			if(IsPart)
			{
				return (Value as MPart).ToText(indent);
			}

			if(!IsMany)
			{
				return Text.Of(Value);
			}

			IEnumerable<object> items;

			var elementList = Value as IReadOnlyList<MElementBase>;

			if(elementList != null)
			{
				items = elementList.Select(item => item.ToXml());
			}
			else
			{
				items =
					from item in Value as IReadOnlyList<object>
					let element = item as MElementBase
					select element != null ? element.ToText(indent) : Text.Of(item);
			}

			return items.ToTextSeparatedBy(indent ? Text.Line : Text.Of(", "));
		}

		public override object ToXml()
		{
			if(IsNone)
			{
				return null;
			}



			// TODO: Wrap the value in a div if it has any globals



			if(IsPart)
			{
				return (Value as MPart).ToXml();
			}

			if(!IsMany)
			{
				return Value;
			}

			var items =
				from item in (IReadOnlyList<object>) Value
				let part = item as MPart
				select part != null ? part.ToXml() : part;

			return items.ToList();
		}
	}
}