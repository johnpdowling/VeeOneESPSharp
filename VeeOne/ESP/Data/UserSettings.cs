using System;
using System.Collections.Generic;
using VeeOne.ESP.Utilities;

namespace VeeOne.ESP.Data
{
	public class UserSettings
	{
		public enum Constants
		{
			ON,
			OFF,
			NORMAL,
			RESPONSIVE,
			LEVER,
			ZERO,
			KNOB
		};

		Constants m_XBand;
		Constants m_KBand;
		Constants m_KaBand;
		Constants m_laser;

		Constants m_bargraph;

		Constants m_kaFalseGuard;
		Constants m_featureBGKMuting;

		Constants m_muteVolume;
		Constants m_postmuteBogeyLockVolume;

		String m_kMuteTimer;

		Constants m_KInitialUnmute4Lights;
		Constants m_KPersistentUnmute6Lights;
		Constants m_KRearMute;
		Constants m_KuBand;
		Constants m_Pop;
		Constants m_Euro;
		Constants m_EuroXBand;
		Constants m_Filter;
		Constants m_ForceLegacy;

		public Constants XBand
		{
			get
			{
				return m_XBand;
			}
		}

		public bool SetXBand(Constants _XBand)
		{
			if ((_XBand != Constants.ON) && (_XBand != Constants.OFF))
			{
				return false;
			}

			m_XBand = _XBand;
			return true;
		}

		public bool XBandAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_XBand);
			}
			set
			{
				m_XBand = getOnOffFromBoolean(value);
			}
		}

		public Constants KBand
		{
			get
			{
				return m_KBand;
			}
		}

		public bool SetKBand(Constants _KBand)
		{
			if ((_KBand != Constants.ON) && (_KBand != Constants.OFF))
			{
				return false;
			}
			m_KBand = _KBand;
			return true;
		}

		public bool KBandAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_KBand);
			}
			set
			{
				m_KBand = getOnOffFromBoolean(value);
			}
		}

		public Constants KaBand
		{
			get
			{
				return m_KaBand;
			}
		}

		public bool SetKaBand(Constants _KaBand)
		{
			if ((_KaBand != Constants.ON) && (_KaBand != Constants.OFF))
			{
				return false;
			}
			m_KaBand = _KaBand;
			return true;
		}

		public bool KaBandAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_KaBand);
			}
			set
			{
				m_KaBand = getOnOffFromBoolean(value);
			}
		}

		public Constants Laser
		{
			get
			{
				return m_laser;
			}
		}

		public bool SetLaser(Constants _laser)
		{
			if ((_laser != Constants.ON) && (_laser != Constants.OFF))
			{
				return false;
			}
			m_laser = _laser;
			return true;
		}

		public bool LaserAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_laser);
			}
			set
			{
				m_laser = getOnOffFromBoolean(value);
			}
		}

		public Constants KRearMute
		{
			get
			{
				return m_KRearMute;
			}
		}

		public bool SetKRearMute(Constants _KRearMute)
		{
			if ((_KRearMute != Constants.ON) && (_KRearMute != Constants.OFF))
			{
				return false;
			}
			m_KRearMute = _KRearMute;
			return true;
		}

		public bool KRearMuteAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_KRearMute);
			}
			set
			{
				m_KRearMute = getOnOffFromBoolean(value);
			}
		}

		public Constants Bargraph
		{
			get
			{
				return m_bargraph;
			}
		}

		public bool SetBargraph(Constants _bargraph)
		{
			if ((_bargraph != Constants.NORMAL) && (_bargraph != Constants.RESPONSIVE))
			{
				return false;
			}
			m_bargraph = _bargraph;
			return true;
		}

		public bool BargraphAsBoolean
		{
			get
			{
				if (m_bargraph == Constants.RESPONSIVE)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			set
			{
				if (value)
				{
					m_bargraph = Constants.RESPONSIVE;
				}
				else
				{
					m_bargraph = Constants.NORMAL;
				}
			}
		}

		public Constants KaFalseGuard
		{
			get
			{
				return m_kaFalseGuard;
			}
		}

		public bool SetKaFalseGuard(Constants _kaFalseGuard)
		{
			if ((_kaFalseGuard != Constants.ON) && (_kaFalseGuard != Constants.OFF))
			{
				return false;
			}
			m_kaFalseGuard = _kaFalseGuard;
			return true;
		}

		public bool KaFalseGuardAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_kaFalseGuard);
			}
			set
			{
				m_kaFalseGuard = getOnOffFromBoolean(value);
			}
		}

		public Constants FeatureBGKMuting
		{
			get
			{
				return m_featureBGKMuting;
			}
		}

		public bool SetFeatureBGKMuting(Constants _featureBGKMuting)
		{
			if ((_featureBGKMuting != Constants.ON) && (_featureBGKMuting != Constants.OFF))
			{
				return false;
			}
			m_featureBGKMuting = _featureBGKMuting;
			return true;
		}

		public bool FeatureBGKMutingAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_featureBGKMuting);
			}
			set
			{
				m_featureBGKMuting = getOnOffFromBoolean(value);
			}
		}

		public Constants MuteVolume
		{
			get
			{
				return m_muteVolume;
			}
		}

		public bool SetMuteVolume(Constants _muteVolume)
		{
			if ((_muteVolume != Constants.LEVER) && (_muteVolume != Constants.ZERO))
			{
				return false;
			}
			m_muteVolume = _muteVolume;
			return true;
		}

		public bool MuteVolumeAsBoolean
		{
			get
			{
				if (m_muteVolume == Constants.LEVER)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			set
			{
				if (value)
				{
					m_muteVolume = Constants.LEVER;
				}
				else
				{
					m_muteVolume = Constants.ZERO;
				}
			}
		}

		public Constants PostMuteBogeyLockVolume
		{
			get
			{
				return m_postmuteBogeyLockVolume;
			}
		}

		public bool SetPostmuteBogeyLockVolume(Constants _postmuteBogeyLockVolume)
		{
			if ((_postmuteBogeyLockVolume != Constants.LEVER) && (_postmuteBogeyLockVolume != Constants.KNOB))
			{
				return false;
			}
			m_postmuteBogeyLockVolume = _postmuteBogeyLockVolume;
			return true;
		}

		public bool PostMuteBogeyLockVolumeAsBoolean
		{
			get
			{
				if (m_postmuteBogeyLockVolume == Constants.KNOB)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			set
			{
				if (value)
				{
					m_postmuteBogeyLockVolume = Constants.KNOB;
				}
				else
				{
					m_muteVolume = Constants.LEVER;
				}
			}
		}

		public String KMuteTimer
		{
			get
			{
				return m_kMuteTimer;
			}
		}

		public bool SetKMuteTimer(String _kMuteTimer)
		{
			m_kMuteTimer = _kMuteTimer;
			return true;
		}

		public Constants KInitialUnmute4Lights
		{
			get
			{
				return m_KInitialUnmute4Lights;
			}
		}

		public bool SetKInitialUnmute4Lights(Constants _KInitialUnmute4Lights)
		{
			if ((_KInitialUnmute4Lights != Constants.ON) && (_KInitialUnmute4Lights != Constants.OFF))
			{
				return false;
			}
			m_KInitialUnmute4Lights = _KInitialUnmute4Lights;
			return true;
		}

		public bool KInitialUnmute4LightsAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_KInitialUnmute4Lights);
			}
			set
			{
				m_KInitialUnmute4Lights = getOnOffFromBoolean(value);
			}
		}

		public Constants KPersistentUnmute6Lights
		{
			get
			{
				return m_KPersistentUnmute6Lights;
			}
		}

		public bool SetKPersistentUnmute6Lights(Constants _KPersistentUnmute6Lights)
		{
			if ((_KPersistentUnmute6Lights != Constants.ON) && (_KPersistentUnmute6Lights != Constants.OFF))
			{
				return false;
			}
			m_KPersistentUnmute6Lights = _KPersistentUnmute6Lights;
			return true;
		}

		public bool KPersistentUnmute6LightsAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_KPersistentUnmute6Lights);
			}
			set
			{
				m_KPersistentUnmute6Lights = getOnOffFromBoolean(value);
			}
		}

		public Constants KuBand
		{
			get
			{
				return m_KuBand;
			}
		}

		public bool SetKuBand(Constants _KuBand)
		{
			if ((_KuBand != Constants.ON) && (_KuBand != Constants.OFF))
			{
				return false;
			}
			m_KuBand = _KuBand;
			return true;
		}

		public bool KuBandAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_KuBand);
			}
			set
			{
				m_KuBand = getOnOffFromBoolean(value);
			}
		}

		public Constants Pop
		{
			get
			{
				return m_Pop;
			}
		}

		public bool SetPop(Constants _Pop)
		{
			if ((_Pop != Constants.ON) && (_Pop != Constants.OFF))
			{
				return false;
			}
			m_Pop = _Pop;
			return true;
		}

		public bool PopAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_Pop);
			}
			set
			{
				m_Pop = getOnOffFromBoolean(value);
			}
		}

		public Constants Euro
		{
			get
			{
				return m_Euro;
			}
		}

		public bool SetEuro(Constants _Euro)
		{
			if ((_Euro != Constants.ON) && (_Euro != Constants.OFF))
			{
				return false;
			}
			m_Euro = _Euro;
			return true;
		}

		public bool EuroAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_Euro);
			}
			set
			{
				m_Euro = getOnOffFromBoolean(value);
			}
		}

		public Constants EuroXBand
		{
			get
			{
				return m_EuroXBand;
			}
		}

		public bool SetEuroXBand(Constants _EuroXBand)
		{
			if ((_EuroXBand != Constants.ON) && (_EuroXBand != Constants.OFF))
			{
				return false;
			}
			m_EuroXBand = _EuroXBand;
			return true;
		}

		public bool EuroXBandAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_EuroXBand);
			}
			set
			{
				m_EuroXBand = getOnOffFromBoolean(value);
			}
		}

		public Constants Filter
		{
			get
			{
				return m_Filter;
			}
		}

		public bool SetFilter(Constants _Filter)
		{
			if ((_Filter != Constants.ON) && (_Filter != Constants.OFF))
			{
				return false;
			}
			m_Filter = _Filter;
			return true;
		}

		public bool FilterAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_Filter);
			}
			set
			{
				m_Filter = getOnOffFromBoolean(value);
			}
		}

		public Constants ForceLegacy
		{
			get
			{
				return m_ForceLegacy;
			}
		}

		public bool SetForceLegacy(Constants _ForceLegacy)
		{
			if ((_ForceLegacy != Constants.ON) && (_ForceLegacy != Constants.OFF))
			{
				return false;
			}
			m_ForceLegacy = _ForceLegacy;
			return true;
		}

		public bool ForceLegacyAsBoolean
		{
			get
			{
				return getOnOffAsBoolean(m_ForceLegacy);
			}
			set
			{
				m_ForceLegacy = getOnOffFromBoolean(value);
			}
		}

		public void BuildFromBytes(byte[] _data)
		{
			/*
			 PayloadBytes
			 0 User Byte 0
			 1 User Byte 1
			 2 User Byte 2
			 3 User Byte 3
			 4 User Byte 4
			 5 User Byte 5

			 Feature 	Name						Values				Bytes 		Bit		Factory Default
			 1 			X band 						On/Off				User 0 		0 		On
			 2 			K band 						On/Off 				User 0 		1 		On
			 3 			Ka band 					On/Off 				User 0 		2 		On
			 4 			Laser 						On/Off 				User 0 		3 		On
			 5 			Bargraph 					Normal/Responsive 	User 0 		4 		Normal
			 6 			Ka False Guard 				On/Off 				User 0 		5 		On
			 7 			Feature bG(K Muting) 		On/Off 				User 0 		6 		Off
			 8 			Mute Volume 				Lever/Zero 			User 0 		7 		Lever
			 A 			Postmute Bogey Lock Volume 	Lever/Knob 			User 1 		0 		Knob
			 b 			K Mute Timer 									User 1 		1 		10 sec
			 C 			 												User 1 		2
			 d 			 												User 1 		3
			 E 			K Initial Unmute 4 lights 						User 1 		4 		On
			 F 			K Persistent Unmute 6 lights 					User 1 		5 		On
			 G 			K Rear Mute 				On/Off 				User 1 		6 		Off
			 H 			Ku band 					On/Off 				User 1 		7 		Off
			 J 			Pop 						On/Off 				User 2 		0 		On
			 u 			Euro 						On/Off 				User 2 		1 		Off
			 u bar 		Euro X band 				On/Off 				User 2 		2 		Off
			 t 			Filter 						On/Off 				User 2 		3 		Off
			 L 			Force Legacy CD 								User 2 		4 		Off
			*/

			byte userByte0 = _data[0];
			byte userByte1 = _data[1];
			byte userByte2 = _data[2];

			SetXBand(getOnOffValue(getBitFromByte(userByte0, 0)));
			SetKBand(getOnOffValue(getBitFromByte(userByte0, 1)));
			SetKaBand(getOnOffValue(getBitFromByte(userByte0, 2)));
			SetLaser(getOnOffValue(getBitFromByte(userByte0, 3)));

			SetBargraph(getNormalResponsiveValue(getBitFromByte(userByte0, 4)));

			SetKaFalseGuard(getOnOffValue(getBitFromByte(userByte0, 5)));
			SetFeatureBGKMuting(getOnOffValueReverse(getBitFromByte(userByte0, 6)));

			SetMuteVolume(getLeverZero(getBitFromByte(userByte0, 7)));

			SetPostmuteBogeyLockVolume(getLeverKnobValue(getBitFromByte(userByte1, 0)));

			SetKMuteTimer(convertTimeFromBytes(getBitFromByte(userByte1, 1), getBitFromByte(userByte1, 2), getBitFromByte(userByte1, 3)));

			SetKInitialUnmute4Lights(getOnOffValue(getBitFromByte(userByte1, 4)));
			SetKPersistentUnmute6Lights(getOnOffValue(getBitFromByte(userByte1, 5)));
			SetKRearMute(getOnOffValueReverse(getBitFromByte(userByte1, 6)));
			SetKuBand(getOnOffValueReverse(getBitFromByte(userByte1, 7)));

			SetPop(getOnOffValue(getBitFromByte(userByte2, 0)));
			SetEuro(getOnOffValueReverse(getBitFromByte(userByte2, 1)));
			SetEuroXBand(getOnOffValueReverse(getBitFromByte(userByte2, 2)));
			SetFilter(getOnOffValueReverse(getBitFromByte(userByte2, 3)));
			SetForceLegacy(getOnOffValueReverse(getBitFromByte(userByte2, 4)));
		}

		public byte[] buildBytes()
		{
			/*
			 PayloadBytes
			 0 User Byte 0
			 1 User Byte 1
			 2 User Byte 2
			 3 User Byte 3
			 4 User Byte 4
			 5 User Byte 5

			 Feature 	Name						Values				Bytes 		Bit		Factory Default
			 1 			X band 						On/Off				User 0 		0 		On
			 2 			K band 						On/Off 				User 0 		1 		On
			 3 			Ka band 					On/Off 				User 0 		2 		On
			 4 			Laser 						On/Off 				User 0 		3 		On

			 5 			Bargraph 					Normal/Responsive 	User 0 		4 		Normal

			 6 			Ka False Guard 				On/Off 				User 0 		5 		On
			 7 			Feature bG(K Muting) 		On/Off 				User 0 		6 		Off
			 8 			Mute Volume 				Lever/Zero 			User 0 		7 		Lever
			 A 			Postmute Bogey Lock Volume 	Lever/Knob 			User 1 		0 		Knob
			 b 			K Mute Timer 									User 1 		1 		10 sec
			 C 			 												User 1 		2
			 d 			 												User 1 		3
			 E 			K Initial Unmute 4 lights 						User 1 		4 		On
			 F 			K Persistent Unmute 6 lights 					User 1 		5 		On
			 G 			K Rear Mute 				On/Off 				User 1 		6 		Off
			 H 			Ku band 					On/Off 				User 1 		7 		Off
			 J 			Pop 						On/Off 				User 2 		0 		On
			 u 			Euro 						On/Off 				User 2 		1 		Off
			 u bar 		Euro X band 				On/Off 				User 2 		2 		Off
			 t 			Filter 						On/Off 				User 2 		3 		Off
			 L 			Force Legacy CD 								User 2 		4 		Off
			*/

			byte[] rc = new byte[6];
			rc[0] = setBit(rc[0], 0, getOnOffValue(XBand));
			rc[0] = setBit(rc[0], 1, getOnOffValue(KBand));
			rc[0] = setBit(rc[0], 2, getOnOffValue(KaBand));
			rc[0] = setBit(rc[0], 3, getOnOffValue(Laser));
			rc[0] = setBit(rc[0], 4, getNormalResponsiveValue(Bargraph));
			rc[0] = setBit(rc[0], 5, getOnOffValue(KaFalseGuard));
			rc[0] = setBit(rc[0], 6, getOnOffValueReverse(FeatureBGKMuting));
			rc[0] = setBit(rc[0], 7, getLeverZeroBoolean(MuteVolume));


			rc[1] = setBit(rc[1], 0, getLeverKnobValueBoolean(PostMuteBogeyLockVolume));

			rc[1] = setTimeoutBitsBits(rc[1]);

			rc[1] = setBit(rc[1], 4, getOnOffValue(KInitialUnmute4Lights));
			rc[1] = setBit(rc[1], 5, getOnOffValue(KPersistentUnmute6Lights));
			rc[1] = setBit(rc[1], 6, getOnOffValueReverse(KRearMute));
			rc[1] = setBit(rc[1], 7, getOnOffValueReverse(KuBand));

			rc[2] = setBit(rc[2], 0, getOnOffValue(Pop));
			rc[2] = setBit(rc[2], 1, getOnOffValueReverse(Euro));
			rc[2] = setBit(rc[2], 2, getOnOffValueReverse(EuroXBand));
			rc[2] = setBit(rc[2], 3, getOnOffValueReverse(Filter));
			rc[2] = setBit(rc[2], 4, getOnOffValueReverse(ForceLegacy));

			//setting unused bits
			rc[2] = setBit(rc[2], 5, true);
			rc[2] = setBit(rc[2], 6, true);
			rc[2] = setBit(rc[2], 7, true);

			rc[3] = setBit(rc[3], 0, true);
			rc[3] = setBit(rc[3], 1, true);
			rc[3] = setBit(rc[3], 2, true);
			rc[3] = setBit(rc[3], 3, true);
			rc[3] = setBit(rc[3], 4, true);
			rc[3] = setBit(rc[3], 5, true);
			rc[3] = setBit(rc[3], 6, true);
			rc[3] = setBit(rc[3], 7, true);

			rc[4] = setBit(rc[4], 0, true);
			rc[4] = setBit(rc[4], 1, true);
			rc[4] = setBit(rc[4], 2, true);
			rc[4] = setBit(rc[4], 3, true);
			rc[4] = setBit(rc[4], 4, true);
			rc[4] = setBit(rc[4], 5, true);
			rc[4] = setBit(rc[4], 6, true);
			rc[4] = setBit(rc[4], 7, true);

			rc[5] = setBit(rc[5], 0, true);
			rc[5] = setBit(rc[5], 1, true);
			rc[5] = setBit(rc[5], 2, true);
			rc[5] = setBit(rc[5], 3, true);
			rc[5] = setBit(rc[5], 4, true);
			rc[5] = setBit(rc[5], 5, true);
			rc[5] = setBit(rc[5], 6, true);
			rc[5] = setBit(rc[5], 7, true);

			return rc;
		}

		public static List<String> AllowedTimeoutValues()
		{
			List<String> rc = new List<String>();

			rc.Add("10");
			rc.Add("30");
			rc.Add("20");
			rc.Add("15");
			rc.Add("7");
			rc.Add("5");
			rc.Add("4");
			rc.Add("3");

			return rc;
		}


		public static UserSettings GenerateFactoryDefaults()
		{
			UserSettings rc = new UserSettings();

			byte[] data = { (byte)0xff, (byte)0xff, (byte)0xff, (byte)0xff, (byte)0xff, (byte)0xff };

			rc.BuildFromBytes(data);

			if (V1VersionSettingLookup.DefaultTMFState)
			{
				// The 0xFF value set above will turn off the TMF feature, so we only need to take action
				// if the TMF feature should be turned on by default
				rc.SetFilter(Constants.ON);
			}

			return rc;
		}

		private Constants getOnOffFromBoolean(bool _value)
		{
			if (_value)
			{
				return Constants.ON;
			}
			else
			{
				return Constants.OFF;
			}
		}

		private bool getOnOffAsBoolean(Constants _value)
		{
			if (_value == Constants.ON)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private byte getBitFromByte(byte _byte, int _whichBit)
		{
			byte mask;

			switch (_whichBit)
			{
				case 0:
					mask = 1;
					break;
				case 1:
					mask = 2;
					break;
				case 2:
					mask = 4;
					break;
				case 3:
					mask = 8;
					break;
				case 4:
					mask = 16;
					break;
				case 5:
					mask = 32;
					break;
				case 6:
					mask = 64;
					break;
				case 7:
					mask = (byte)128;
					break;
				default:
					mask = 0;
					break;
			}

			byte newValue = (byte)(_byte & mask);

			newValue = (byte)(newValue >> _whichBit);

			return newValue;
		}

		private UserSettings.Constants getOnOffValue(byte _value)
		{
			UserSettings.Constants rc;
			if (_value == 0)
			{
				rc = UserSettings.Constants.OFF;
			}
			else
			{
				rc = UserSettings.Constants.ON;
			}

			return rc;
		}

		private UserSettings.Constants getOnOffValueReverse(byte _value)
		{
			UserSettings.Constants rc;
			if (_value == 0)
			{
				rc = UserSettings.Constants.ON;
			}
			else
			{
				rc = UserSettings.Constants.OFF;
			}

			return rc;
		}

		private UserSettings.Constants getNormalResponsiveValue(byte _value)
		{
			UserSettings.Constants rc;
			if (_value == 0)
			{
				rc = UserSettings.Constants.RESPONSIVE;
			}
			else
			{
				rc = UserSettings.Constants.NORMAL;
			}

			return rc;
		}

		private UserSettings.Constants getLeverKnobValue(byte _value)
		{
			UserSettings.Constants rc;
			if (_value == 0)
			{
				rc = UserSettings.Constants.LEVER;
			}
			else
			{
				rc = UserSettings.Constants.KNOB;
			}

			return rc;
		}

		private UserSettings.Constants getLeverZero(byte _value)
		{
			UserSettings.Constants rc;
			if (_value == 0)
			{
				rc = UserSettings.Constants.ZERO;
			}
			else
			{
				rc = UserSettings.Constants.LEVER;
			}

			return rc;
		}

		private byte setBit(byte _byte, int _whichBit, bool value)
		{
			byte mask;

			if (value)
			{
				switch (_whichBit)
				{
					case 0:
						mask = 1;
						break;
					case 1:
						mask = 2;
						break;
					case 2:
						mask = 4;
						break;
					case 3:
						mask = 8;
						break;
					case 4:
						mask = 0x10;
						break;
					case 5:
						mask = 0x20;
						break;
					case 6:
						mask = 0x40;
						break;
					case 7:
						mask = (byte)0x80;
						break;
					default:
						mask = 0;
						break;
				}

				_byte = (byte)(_byte | mask);

				return _byte;
			}

			return _byte;
		}

		private bool getOnOffValue(UserSettings.Constants _value)
		{
			bool rc;
			if (_value == UserSettings.Constants.OFF)
			{
				rc = false;
			}
			else
			{
				rc = true;
			}

			return rc;
		}

		private bool getOnOffValueReverse(UserSettings.Constants _value)
		{
			bool rc;
			if (_value == UserSettings.Constants.ON)
			{
				rc = false;
			}
			else
			{
				rc = true;
			}

			return rc;
		}

		private bool getNormalResponsiveValue(UserSettings.Constants _value)
		{
			bool rc;
			if (_value == UserSettings.Constants.NORMAL)
			{
				rc = true;
			}
			else
			{
				rc = false;
			}

			return rc;
		}

		private bool getLeverKnobValueBoolean(UserSettings.Constants _value)
		{
			bool rc;
			if (_value == UserSettings.Constants.LEVER)
			{
				rc = false;
			}
			else
			{
				rc = true;
			}

			return rc;
		}

		private bool getLeverZeroBoolean(UserSettings.Constants _value)
		{
			bool rc;
			if (_value == UserSettings.Constants.LEVER)
			{
				rc = true;
			}
			else
			{
				rc = false;
			}

			return rc;
		}

		private String convertTimeFromBytes(byte _one, byte _two, byte _three)
		{
			String rc = "10";

			int setting = getSetting(_one, _two, _three);

			rc = getTimeForSettingDefault(setting).ToString();

			return rc;
		}

		private Int32 getTimeForSettingDefault(int _setting)
		{
			switch (_setting)
			{
				case 1:
					return 10;
				case 2:
					return 30;
				case 3:
					return 20;
				case 4:
					return 15;
				case 5:
					return 7;
				case 6:
					return 5;
				case 7:
					return 4;
				case 8:
					return 3;
			}

			return 0;
		}

		private int getSetting(byte _one, byte _two, byte _three)
		{
			bool one = false;
			bool two = false;
			bool three = false;

			if (_one > 0)
			{
				one = true;
			}

			if (_two > 0)
			{
				two = true;
			}

			if (_three > 0)
			{
				three = true;
			}

			if (one & two & three)
			{
				return 1;
			}
			else if (!one & two & three)
			{
				return 2;
			}
			else if (one & !two & three)
			{
				return 3;
			}
			else if (!one & !two & three)
			{
				return 4;
			}
			else if (one & two & !three)
			{
				return 5;
			}
			else if (!one & two & !three)
			{
				return 6;
			}
			else if (one & !two & !three)
			{
				return 7;
			}
			else //if (!one & !two & !three)
			{
				return 8;
			}
		}

		private byte setTimeoutBitsBits(byte _byte)
		{
			int which = 0;

			which = getSettingForTimeForDefault(KMuteTimer);

			switch (which)
			{
				case 1:
					_byte = (byte)(_byte | 0x0E);
					break;
				case 2:
					_byte = (byte)(_byte | 0x0C);
					break;
				case 3:
					_byte = (byte)(_byte | 0x0A);
					break;
				case 4:
					_byte = (byte)(_byte | 0x08);
					break;
				case 5:
					_byte = (byte)(_byte | 0x06);
					break;
				case 6:
					_byte = (byte)(_byte | 0x04);
					break;
				case 7:
					_byte = (byte)(_byte | 0x02);
					break;
				case 8:
					_byte = (byte)(_byte | 0x00);
					break;
			}

			return _byte;
		}

		private Int32 getSettingForTimeForDefault(String _time)
		{
			int timeInt = Int32.Parse(_time);
			switch (timeInt)
			{
				case 10:
					return 1;
				case 30:
					return 2;
				case 20:
					return 3;
				case 15:
					return 4;
				case 7:
					return 5;
				case 5:
					return 6;
				case 4:
					return 7;
				case 3:
					return 8;
			}

			return 0;
		}



		/*
		public UserSettings()
		{
		}
		*/
	}
}
