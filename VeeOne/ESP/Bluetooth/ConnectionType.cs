using System;
using System.Collections.Generic;
using System.Reflection;
using VeeOne.ESP.Constants;

namespace VeeOne.ESP.Bluetooth
{
	public enum ConnectionType
	{
		[ValueNameAttr(0, "UNKNOWN")]
		UNKNOWN,
		[ValueNameAttr(1, "V1Connection")]
		V1Connection,
		[ValueNameAttr(2, "V1Connection_LE")]
		V1Connection_LE
	}

	public static class ConnectionTypeExtensions
	{
		public static byte ToByteValue(this ConnectionType c)
		{
			ValueNameAttr attr = GetAttr(c);
			return attr.Value;
		}

		public static string ToString(this ConnectionType c)
		{
			ValueNameAttr attr = GetAttr(c);
			return attr.Name;
		}

		private static ValueNameAttr GetAttr(ConnectionType c)
		{
			return (ValueNameAttr)Attribute.GetCustomAttribute(ForValue(c), typeof(ValueNameAttr));
		}

		private static MemberInfo ForValue(ConnectionType c)
		{
			return typeof(ConnectionType).GetField(Enum.GetName(typeof(ConnectionType), c));
		}

	}
}
