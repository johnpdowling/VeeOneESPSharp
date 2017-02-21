using System;
using System.Collections.Generic;

namespace VeeOne.ESP.Bluetooth
{
	public class ConnectionType
	{
		public static readonly ConnectionType UNKNOWN = new ConnectionType(0, "UNKNOWN");
		public static readonly ConnectionType V1Connection = new ConnectionType(1, "V1Connection");
		public static readonly ConnectionType V1Connection_LE = new ConnectionType(2, "V1Connection_LE");

		private readonly int mValue;
		private readonly String mName;

		public static IEnumerable<ConnectionType> Values
		{
			get
			{
				yield return UNKNOWN;
				yield return V1Connection;
				yield return V1Connection_LE;
			}
		}

		private ConnectionType(int value, String name)
		{
			this.mValue = value;
			this.mName = name;
		}

		public override string ToString()
		{
			return mName;
		}

		public int Value
		{
			get
			{
				return mValue;
			}
		}
	}
}
