using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VeeOne.ESP.Constants
{
	internal class ValueNameAttr : Attribute
	{
		internal ValueNameAttr(byte _value, String _name)
		{
			this.Value = _value;
			this.Name = _name;
		}
		public byte Value { get; private set; }
		public string Name { get; private set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public static class DevicesExtensions
	{
		public static byte ToByteValue(this Devices d)
		{
			ValueNameAttr attr = GetAttr(d);
			return attr.Value;
		}

		public static string ToString(this Devices d)
		{
			ValueNameAttr attr = GetAttr(d);
			return attr.Name;
		}

		private static ValueNameAttr GetAttr(Devices d)
		{
			return (ValueNameAttr)Attribute.GetCustomAttribute(ForValue(d), typeof(ValueNameAttr));
		}

		private static MemberInfo ForValue(Devices d)
		{
			return typeof(Devices).GetField(Enum.GetName(typeof(Devices), d));
		}

	}

	public enum Devices
	{
		[ValueNameAttr(0x00, "Concealed Display")]
		CONCEALED_DISPLAY,
		[ValueNameAttr(0x01, "Remote Audio")]
		REMOTE_AUDIO,
		[ValueNameAttr(0x02, "Savvy")]
		SAVVY,
		[ValueNameAttr(0x03, "Third Party 1")]
		THIRD_PARTY_1,
		[ValueNameAttr(0x04, "Third Party 2")]
		THIRD_PARTY_2,
		[ValueNameAttr(0x05, "Third Party 3")]
		THIRD_PARTY_3,
		[ValueNameAttr(0x06, "V1Connect")]
		V1CONNECT,
		[ValueNameAttr(0x07, "Reserved for Valentine Research")]
		RESERVED,
		[ValueNameAttr(0x08, "General Broadcast")]
		GENERAL_BROADCAST,
		[ValueNameAttr(0x09, "Valentine1 without checksum")]
		VALENTINE1_WITHOUT_CHECKSUM,
		[ValueNameAttr(0x0A, "Valentine1 with checksum")]
		VALENTINE1_WITH_CHECKSUM,

		[ValueNameAttr(0x98, "Legacy Valentine1")]
		VALENTINE1_LEGACY,
		[ValueNameAttr(0x99, "Unknown")]
		UNKNOWN
	}

	public static class DevicesUtils
	{
		public static Devices DevicesFromByteValue(byte val)
		{
			foreach (Devices thisDevice in Enum.GetValues(typeof(Devices)))
			{
				if (thisDevice.ToByteValue() == val)
				{
					return thisDevice;
				}
			}
			return Devices.UNKNOWN;
		}
	}
}
