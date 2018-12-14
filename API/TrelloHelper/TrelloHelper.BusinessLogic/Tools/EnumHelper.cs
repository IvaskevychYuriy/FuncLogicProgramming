using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace TrelloHelper.BusinessLogic.Tools
{
	public static class EnumHelper<T>
		where T : struct
	{
		private static IDictionary<string, T> _values;

		static EnumHelper()
		{
			_values = GetValues();
		}
		
		public static T? ParseDisplayValues(string value)
		{
			string key = value.ToLower();
			if (_values.ContainsKey(key))
			{
				return _values[key];
			}

			return null;
		}

		private static IDictionary<string, T> GetValues()
		{
			var enumValues = new Dictionary<string, T>();

			foreach (var fi in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				string key = fi.Name;

				var display = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
				if (display != null)
				{
					key = (display.Length > 0) ? display[0].Description : fi.Name;
				}

				key = key.ToLower();
				if (!enumValues.ContainsKey(key))
				{
					enumValues[key] = (T)fi.GetRawConstantValue();
				}
			}

			return enumValues;
		}
	}
}
