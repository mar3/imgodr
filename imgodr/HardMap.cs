using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	sealed class HardMap : System.Collections.Generic.Dictionary<string, object>
	{
		public new object this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (key == null || key.Length == 0)
					return;
				if (value == null)
					return;
				if (value.ToString().Length == 0)
					return;
				base[key] = value;
			}
		}

		public override string ToString()
		{
			StringBuilder text = new StringBuilder("{");
			int n = 0;
			foreach (string key in this.Keys)
			{
				if (n != 0)
					text.Append(", ");
				text.Append(key);
				text.Append("=");
				text.Append(this[key]);
				n++;
			}
			text.Append("}");
			return text.ToString();
		}
	}
}


