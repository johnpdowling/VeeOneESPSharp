using System;
using System.Collections.Generic;
using System.Linq;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Packets;

namespace VeeOne
{
	public class PacketQueue
	{
		private static object m_inlock = new object();
		private static object m_outlock = new object();

		private static LinkedList<ESPPacket> m_inputQueue = new LinkedList<ESPPacket>();
		private static LinkedList<ESPPacket> m_outputQueue = new LinkedList<ESPPacket>();

		private static bool m_holdoffOutput = true;                      // If true, getNextOutputPacket will return null
		private static Devices m_v1Type = Devices.UNKNOWN;                  // This is used to rebuild ESP packets before they are written to the hardware if the V1 type changes while there are packets in the queue.

		private static List<Byte> m_busyPacketIds = new List<Byte>();

		private static Dictionary<PacketId, ESPPacket> m_lastSentPacket = new Dictionary<PacketId, ESPPacket>();

		private static List<ESPPacket> m_toSendAfterBusyClear = new List<ESPPacket>();

		public static ESPPacket getNextInputPacket()
		{
			ESPPacket rc;
			lock (m_inlock)
			{
				if (m_inputQueue.Count == 0)
				{
					rc = null;
				}
				else
				{
					rc = m_inputQueue.First.Value;
					m_inputQueue.RemoveFirst();
				}
			}
			return rc;
		}

		public static void pushInputPacketOntoQueue(ESPPacket packet)
		{
			lock (m_inlock)
			{
				m_inputQueue.AddLast(packet);
			}
		}

		public static void initOutputQueue(Devices v1Type, bool clearOutputQueue, bool holdoffOutput)
		{
			lock (m_outlock)
			{
				if (clearOutputQueue)
				{
					if (m_outputQueue.Count != 0)
					{
						if (ESPLibraryLogController.LOG_WRITE_DEBUG)
						{
							//Log.d("Valentine", "Deleting " + m_outputQueue.size() + " packets from output queue.");
						}
					}
					m_outputQueue.Clear();
				}
				m_v1Type = v1Type;
				m_holdoffOutput = holdoffOutput;
			}
		}

		public static void initInputQueue(bool clearInputQueue)
		{
			lock (m_inlock)
			{
				if (clearInputQueue)
				{
					if (m_inputQueue.Count != 0)
					{
						if (ESPLibraryLogController.LOG_WRITE_DEBUG)
						{
							//Log.d("Valentine", "Deleting " + m_inputQueue.size() + " packets from input queue.");
						}
					}
					m_inputQueue.Clear();
				}
			}
		}

		public static ESPPacket getNextOutputPacket()
		{
			ESPPacket rc = null;
			lock (m_outlock)
			{
				if (m_outputQueue.Count == 0)
				{
					// No packets to send or we don't have a V1 type yet			
					rc = null;
				}
				else if (m_holdoffOutput)
				{
					rc = null;

					// Check for packets destined for the V1connection and allow them to override the holdoff
					for (int i = 0; i < m_outputQueue.Count; i++)
					{
						ESPPacket p = m_outputQueue.ElementAt<ESPPacket>(i);

						if (p.Destination == Devices.V1CONNECT && p.Origin == Devices.V1CONNECT)
						{
							rc = p;
							m_outputQueue.Remove(p);
							break;
						}
					}
				}
				else {
					//int location = 0;
					//bool found = false;

					while (rc == null && m_outputQueue.Count > 0)
					{
						rc = m_outputQueue.First.Value;
						m_outputQueue.RemoveFirst();
						/*if (!found)
						{
							rc = m_outputQueue.remove(location);
						}
						else
						{
							location++;
						}

						if (location == m_outputQueue.Count)
						{
							break;
						}*/
					}
				}

				if (rc != null)
				{
					// Change the packet type if it doesn't match the last type specified.
					// Don't change if the last type specified is UNKNOWN because there is not a valid type to change it to.			
					if (rc.V1Type != m_v1Type && m_v1Type != Devices.UNKNOWN)
					{
						rc.SetNewV1Type(m_v1Type);
					}

				}
			}
			return rc;
		}

		public static void pushOutputPacketOntoQueue(ESPPacket packet)
		{
			lock (m_outlock)
			{
				bool addPacketToQueue = true;

				for (int i = 0; i < m_outputQueue.Count; i++)
				{
					ESPPacket curPacket = m_outputQueue.ElementAt(i);
					if (packet.IsSamePacket(curPacket))
					{
						// Don't put this packet into the queue
						addPacketToQueue = false;
						break;
					}
				}

				if (addPacketToQueue)
				{
					m_outputQueue.AddLast(packet);
				}

			}
		}

		public static bool HoldOffOutput
		{
			get
			{
				bool retVal;
				lock (m_outlock)
				{
					retVal = m_holdoffOutput;
				}
				return retVal;
			}
			set
			{
				lock (m_outlock)
				{
					m_holdoffOutput = value;
				}
			}
		}

		public static Devices V1Type
		{
			get
			{
				byte typeByte;
				lock (m_outlock)
				{
					typeByte = m_v1Type.ToByteValue();
				}
				return DevicesUtils.DevicesFromByteValue(typeByte);
			}
			set
			{
				lock(m_outlock)
				{
					m_v1Type = value;
				}
			}
		}

		public static void setBusyPacketIds(ESPPacket newPacket)
		{
			m_busyPacketIds.Clear();
			String log = "";

			if (newPacket != null)
			{
				byte[] payload = newPacket.Payload;
				for (int i = 0; i < payload.Length; i++)
				{
					Byte newId = payload[i];
					m_busyPacketIds.Add(newId);
				}

				/*
				for (int i = 0; i < m_busyPacketIds.size(); i++)
				{
					log = log + "[" + Byte.toString(m_busyPacketIds.get(i)) + "] ";
				}
				*/
			}

			if (ESPLibraryLogController.LOG_WRITE_INFO)
			{
				//Log.i("Valentine", log);
			}
		}

		public static void removeFromBusyPacketIds(PacketId _id)
		{
			if (m_busyPacketIds.Count != 0)
			{
				byte id = _id.ToByteValue();
				int toRemove = -1;

				for (int i = 0; i < m_busyPacketIds.Count; i++)
				{
					if (id == m_busyPacketIds[i])
					{
						toRemove = 0;
						break;
					}
				}

				if (toRemove != -1)
				{
					m_busyPacketIds.Remove((byte)toRemove);
				}
			}
		}

		public static bool isPacketIdInBusyList(PacketId _id)
		{
			byte packetIdByte = _id.ToByteValue();

			if (m_busyPacketIds.Count == 0)
			{
				return false;
			}

			for (int i = 0; i < m_busyPacketIds.Count; i++)
			{
				if (m_busyPacketIds[i] == packetIdByte)
				{
					return true;
				}
			}

			return false;
		}

		public static void putLastWrittenPacketOfType(ESPPacket _packet)
		{
			m_lastSentPacket.Add(_packet.PacketIdentifier, _packet);
		}

		public static ESPPacket getLastWrittenPacketOfType(PacketId _id)
		{
			if (m_lastSentPacket.ContainsKey(_id))
			{
				return m_lastSentPacket[_id];
			}
			else
			{
				return null;
			}
		}

		public static void pushOnToSendAfterBusyQueue(ESPPacket _packet)
		{
			lock(m_outlock)
			{
				bool addToQueue = true;

				for (int i = 0; i < m_toSendAfterBusyClear.Count; i++)
				{
					ESPPacket curPacket = m_toSendAfterBusyClear[i];
					if (curPacket.PacketIdentifier.ToByteValue() == _packet.PacketIdentifier.ToByteValue())
					{
						// This packet is already in the queue, so don't resend it after we are busy
						addToQueue = false;
					}
				}

				if (addToQueue)
				{
					for (int i = 0; i < m_busyPacketIds.Count; i++)
					{
						if (_packet.PacketIdentifier.ToByteValue() == m_busyPacketIds[i])
						{
							// This packet is in the list of packets the V1 is working on, so don't add it to the queue
							addToQueue = false;
						}
					}
				}

				if (addToQueue)
				{
					m_toSendAfterBusyClear.Add(_packet);
				}
			}
		}

		public static void sendAfterBusyQueue()
		{
			lock(m_outlock)
			{
				if (m_toSendAfterBusyClear.Count > 0)
				{
					//Log.i("Valentine", "V1 not busy. Trying to resend " + m_toSendAfterBusyClear.size() + " packets");
				}
				for (int i = 0; i < m_toSendAfterBusyClear.Count; i++)
				{
					ESPPacket packet = m_toSendAfterBusyClear[i];
					PacketQueue.pushOutputPacketOntoQueue(packet);
				}
			}
			m_toSendAfterBusyClear.Clear();
		}

		public static void clearSendAfterBusyQueue()
		{
			lock(m_outlock)
			{
				m_toSendAfterBusyClear.Clear();
			}
		}

	}
}
