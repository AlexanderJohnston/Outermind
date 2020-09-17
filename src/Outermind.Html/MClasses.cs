using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Totem;
using Totem.Runtime;

namespace Outermind.Html
{
	/// <summary>
	/// The classes specifying the semantics of an M element
	/// </summary>
	public sealed class MClasses : Notion, IReadOnlyCollection<string>
	{
		public static readonly MClasses None = new MClasses(new Many<string>());

		private readonly Many<string> _names;

		internal MClasses(Many<string> names)
		{
			_names = names;
		}

		public int Count { get { return _names.Count; } }

		public IEnumerator<string> GetEnumerator()
		{
			return _names.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Text ToText()
		{
			return _names.ToTextSeparatedBy(" ");
		}

		public bool Contains(string name)
		{
			return _names.Contains(name);
		}

		public MClasses Extend(MClasses other)
		{
			if(other == null || other.Count == 0)
			{
				return this;
			}

			var combinedNames = _names.Concat(other._names).Distinct().ToMany();

			return combinedNames.Count == Count ? this : new MClasses(combinedNames);
		}

		public static implicit operator MClasses(string names)
		{
			return M.Classes(names);
		}
	}
}