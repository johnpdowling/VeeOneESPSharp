using System;
using System.Collections.Generic;
using System.Linq;

namespace VeeOne.ESP.Constants
{
	public class PacketIdLookup
	{
		static Dictionary<Byte, PacketId> m_values = new Dictionary<byte, PacketId>();

		public static PacketId getConstant(byte _value)
		{
			if (m_values.Count == 0)
			{
				setUpValues();
			}

			PacketId retId = m_values[_value];

			if (retId == null)
			{
				retId = PacketId.unknownPacketType;
			}

			return retId;
		}

		private static void setUpValues()
		{
			PacketId[] array = (PacketId[])Enum.GetValues(typeof(PacketId));// PacketId.Values.Cast<PacketId>().ToArray();

			for (int i = 0; i < array.Length; i++)
			{
				m_values.Add(array[i].ToByteValue(), array[i]);
			}
		}
	}
}
