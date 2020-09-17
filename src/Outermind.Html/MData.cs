using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Totem.Html
{
	/// <summary>
	/// The data attributes associated with an M element
	/// </summary>
	public sealed class MData : Notion, IReadOnlyDictionary<string, string>
	{
		public static readonly MData None = new MData(new Dictionary<string, string>());

		private readonly Dictionary<string, string> _data;

		internal MData(Dictionary<string, string> data)
		{
			_data = data;
		}

		public IEnumerable<string> Keys { get { return _data.Keys; } }
		public IEnumerable<string> Values { get { return _data.Values; } }
		public string this[string key] { get { return _data[key]; } }
		public int Count { get { return _data.Count; } }

		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public bool ContainsKey(string key)
		{
			return _data.ContainsKey(GetDataKey(key));
		}

		public bool TryGetValue(string key, out string value)
		{
			return _data.TryGetValue(GetDataKey(key), out value);
		}

		private static string GetDataKey(string key)
		{
			return key.Length > 5 && key.StartsWith("data-") ? key : "data-" + key;
		}
	}
}