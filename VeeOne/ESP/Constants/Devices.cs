using System;
using System.Collections.Generic;
using System.Linq;

namespace VeeOne.ESP.Constants
{
	public class Devices
	{
		public static readonly Devices CONCEALED_DISPLAY = new Devices(0x00, "Concealed Display");
		public static readonly Devices REMOTE_AUDIO = new Devices(0x01, "Remote Audio");
		public static readonly Devices SAVVY = new Devices(0x02, "Savvy");
		public static readonly Devices THIRD_PARTY_1 = new Devices(0x03, "Third Party 1");
		public static readonly Devices THIRD_PARTY_2 = new Devices(0x04, "Third Party 2");
		public static readonly Devices THIRD_PARTY_3 = new Devices(0x05, "Third Party 3");
		public static readonly Devices V1CONNECT = new Devices(0x06, "V1Connect");
		public static readonly Devices RESERVED = new Devices(0x07, "Reserved for Valentine Research");
		public static readonly Devices GENERAL_BROADCAST = new Devices(0x08, "General Broadcast");
		public static readonly Devices VALENTINE1_WITHOUT_CHECKSUM = new Devices(0x09, "Valentine1 without checksum");
		public static readonly Devices VALENTINE1_WITH_CHECKSUM = new Devices(0x0A, "Valentine1 with checksum");

		public static readonly Devices VALENTINE1_LEGACY = new Devices(0x98, "Legacy Valentine1");
		public static readonly Devices UNKNOWN = new Devices(0x99, "Unknown");

		public static IEnumerable<Devices> Values
		{
			get
			{
				yield return CONCEALED_DISPLAY;
				yield return REMOTE_AUDIO;
				yield return SAVVY;
				yield return THIRD_PARTY_1;
				yield return THIRD_PARTY_2;
				yield return THIRD_PARTY_3;
				yield return V1CONNECT;
				yield return RESERVED;
				yield return GENERAL_BROADCAST;
				yield return VALENTINE1_WITHOUT_CHECKSUM;
				yield return VALENTINE1_WITH_CHECKSUM;
				yield return VALENTINE1_LEGACY;
				yield return UNKNOWN;
			}
		}

		byte m_value;
		String m_name;

		Devices(byte _value, String _name)
		{
			m_value = _value;
			m_name = _name;
		}

		public byte ToByteValue()
		{
			return m_value;
		}

		public override String ToString()
		{
			return m_name;
		}

		public static Devices FromByteValue(byte val)
		{
			if (val == Devices.VALENTINE1_LEGACY.ToByteValue())
			{
				return Devices.VALENTINE1_LEGACY;
			}
			if (val == Devices.UNKNOWN.ToByteValue())
			{
				return Devices.UNKNOWN;
			}

			return Values.ElementAt(val);
		}
	}
}
