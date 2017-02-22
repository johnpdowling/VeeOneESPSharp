using System;
using System.Collections.Generic;
using System.Text;
using VeeOne.ESP.Bluetooth;
using VeeOne.ESP.Constants;
using VeeOne.ESP.Factories;


namespace VeeOne.ESP.Packets
{
	public abstract class ESPPacket
	{
		private static readonly String LOG_TAG = "ValentineESP/ESPPacket";

		protected static readonly byte frameDelimitedConstant = 0x7F;
		protected static readonly byte frameDataEscapeConstant = 0x7D;

		protected static readonly byte startOfFrameConstant = (byte)0xAA;
		protected static readonly byte destinationIdentifierBaseConstant = (byte)0xD0;
		protected static readonly byte originationIdentifierBaseConstant = (byte)0xE0;
		protected static readonly byte endOfFrameConstant = (byte)0xAB;

		protected static readonly byte originationSourceId = Devices.V1CONNECT.ToByteValue();

		protected static readonly byte valentine1DestinationId = (byte)0x0A;

		protected byte headerDelimter;
		protected byte packetLength;

		protected byte startOfFrame;
		protected byte destinationIdentifier;
		protected byte originatorIdentifier;
		protected byte packetIdentifier;
		protected byte payloadLength;
		protected byte[] payloadData;
		protected byte checkSum;
		protected byte endOfFrame;

		protected byte packetChecksum;
		protected byte endDelimter;

		protected byte m_destination;
		protected Devices m_valentineType;
		protected long m_timeStamp;

		protected bool m_resent = false;

		private static List<Byte> mLastStartBuffer = new List<Byte>();
		private static List<Byte> mLastEndBuffer = new List<Byte>();

		private static ConnectionType mConnectionType = ConnectionType.UNKNOWN;

		private enum ProcessState
		{
			START_PACK_BYTE,
			PACKET_LENGTH,
			SOF,
			DESTINATION,
			ORIGINATOR,
			PACKET_ID,
			PAYLOAD_LENGTH,
			PAYLOAD,                        // Note: This includes the ESP checksum byte
			PACKET_CHEKSUM,
			EOF,
			BT_CHECKSUM,
			END_PACK_BYTE
		};

		public bool IsSamePacket(ESPPacket rhs)
		{
			if (headerDelimter != rhs.headerDelimter ||
				packetLength != rhs.packetLength ||
				startOfFrame != rhs.startOfFrame ||
				destinationIdentifier != rhs.destinationIdentifier ||
				originatorIdentifier != rhs.originatorIdentifier ||
				packetIdentifier != rhs.packetIdentifier ||
				payloadLength != rhs.payloadLength ||
				checkSum != rhs.checkSum ||
				endOfFrame != rhs.endOfFrame ||
				packetChecksum != rhs.packetChecksum ||
				endDelimter != rhs.endDelimter ||
				m_destination != rhs.m_destination)
			{
				// One of the primitives doesn't match. We don't care which one
				return false;
			}

			if ((payloadData == null && rhs.payloadData != null) || (payloadData != null && rhs.payloadData == null))
			{
				// We have payload data for one of the arrays, but not the other
				return false;
			}

			if (payloadData != null && rhs.payloadData != null)
			{
				// Duplicate null check, but that is OK
				try
				{
					for (int i = 0; i < payloadLength; i++)
					{
						if (payloadData[i] != rhs.payloadData[i])
						{
							// Payload data mismatch
							return false;
						}
					}
				}
				catch
				{
					// Let's call this a mismatch
					return false;
				}
			}

			// If we get here, the comparison was successful
			return true;
		}

		public PacketId PacketIdentifier
		{
			get
			{
				return PacketIdLookup.getConstant(packetIdentifier);
			}
		}

		public Devices Destination
		{
			get
			{
				return DevicesLookup.getConstant(destinationIdentifier);
			}
		}

		public Devices Origin
		{
			get
			{
				return DevicesLookup.getConstant(originatorIdentifier);
			}
		}

		public Devices V1Type
		{
			get
			{
				return m_valentineType;
			}
		}

		public int PayloadLength
		{
			get
			{
				return packetLength;
			}
		}

		public int PacketLength
		{
			get
			{
				return packetLength;
			}
		}

		public byte[] Payload
		{
			get
			{
				return payloadData;
			}
		}

		private static void mCopyBuffer(List<Byte> src, List<Byte> dest)
		{
			dest.Clear();

			for (int i = 0; i < src.Count; i++)
			{
				byte b = src[i];//.get(i);
				dest.Add(b);
			}
		}

		protected readonly static char[] hexArray = "0123456789ABCDEF".ToCharArray();
		public static String GetBufferLogString(List<Byte> buffer)
		{
			StringBuilder sb = new StringBuilder();
			char[] hexChars = new char[2];

			for (int i = 0; i < buffer.Count; i++)
			{
				if (i > 0)
				{
					// Add the delimiter
					sb.Append(" 0x");
				}
				else {
					// Don't add the delimiter
					sb.Append("0x");
				}

				int v = buffer[i] & 0xFF;
				hexChars[0] = hexArray[v >> 4];
				hexChars[1] = hexArray[v & 0x0F];

				sb.Append(new String(hexChars));
			}

			return sb.ToString();
		}

		public static void SetConnectionType(ConnectionType connType)
		{
			mConnectionType = connType;
		}

		public static ESPPacket MakeFromBuffer(List<Byte> buffer, ConnectionType type, Devices lastV1Type)
		{
			if (buffer == null)
			{
				return null;
			}
			if (type == ConnectionType.V1Connection_LE)
				return makeFromBufferLE(buffer, lastV1Type);
			else
				return makeFromBufferSPP(buffer, lastV1Type);
		}

		protected static ESPPacket makeFromBufferLE(List<Byte> buffer, Devices lastV1Type)
		{
			if (buffer.Count == 0)
			{
				return null;
			}
			int bufferSize = buffer.Count;

			if (buffer[0] != startOfFrameConstant || buffer[bufferSize - 1] != endOfFrameConstant)
			{
				return null;
			}

			bool dataError = false;
			ProcessState processState = ProcessState.SOF;
			ESPPacket retPacket = null;
			byte espChecksum = 0;
			byte tempDest = 0;
			byte tempOrigin = 0;
			int payloadIdx = 0;

			for (int i = 0; i < bufferSize; i++)
			{
				byte curByte = buffer[i];
				switch (processState)
				{
					case ProcessState.SOF:
						if (curByte != startOfFrameConstant)
						{
							dataError = true;
						}
						espChecksum += curByte;
						processState = ProcessState.DESTINATION;
						break;
					case ProcessState.DESTINATION:
						if ((curByte & destinationIdentifierBaseConstant) != destinationIdentifierBaseConstant)
						{
							dataError = true;
						}

						tempDest = curByte;
						espChecksum += curByte;
						processState = ProcessState.ORIGINATOR;
						break;
					case ProcessState.ORIGINATOR:
						if ((curByte & originationIdentifierBaseConstant) != originationIdentifierBaseConstant)
						{
							// This is not a valid originator
							dataError = true;
						}
						tempOrigin = curByte;
						espChecksum += curByte;
						processState = ProcessState.PACKET_ID;
						break;
					case ProcessState.PACKET_ID:
						// Make the packet
						retPacket = PacketFactory.getPacket(PacketIdLookup.getConstant(curByte));
						if (retPacket == null)
						{
							// We couldn't build the packet so stop trying
							dataError = true;
						}
						else {
							// We have a good packet so fill it up
							retPacket.headerDelimter = frameDelimitedConstant;  //<- We can't get here if this wasn't true
																				// The packet length for LE is equal to the size of the buffer..
							retPacket.packetLength = (byte)bufferSize;

							retPacket.startOfFrame = startOfFrameConstant;   //<- We can't get here if this wasn't true
							retPacket.destinationIdentifier = tempDest;
							// Don't store the upper nibble of the destinations
							retPacket.m_destination = (byte)(tempDest - destinationIdentifierBaseConstant);
							// Don't store the upper nibble of the origin
							retPacket.originatorIdentifier = (byte)(tempOrigin - originationIdentifierBaseConstant);
							retPacket.packetIdentifier = curByte;

							// If the packet is from a V1 set the ESPPacket V1 type to the appropriate Device type. 
							if (IsPacketFromV1(retPacket.originatorIdentifier))
							{
								retPacket.m_valentineType = Devices.FromByteValue(retPacket.originatorIdentifier);
							}
							else {
								retPacket.m_valentineType = lastV1Type;
							}
							// If the last known V1 type is unknown check to see if the ESPPacket is V1connection version response. 
							if (retPacket.m_valentineType == Devices.UNKNOWN)
							{
								if (retPacket.packetIdentifier != PacketId.respVersion.ToByteValue() || retPacket.originatorIdentifier != Devices.V1CONNECT.ToByteValue())
								{
									// Always allow the V1connection version responses to pass through
									// Don't process any other data until we know what type of V1 we are working with
									dataError = true;
									if (ESPLibraryLogController.LOG_WRITE_ERROR)
									{
										//Log.e(LOG_TAG, "Ignore packet id 0x" + String.Format("%02X ", curByte) + " because the V1 type is unknown");
									}
								}
							}
						}
						espChecksum += curByte;
						processState = ProcessState.PAYLOAD_LENGTH;
						break;
					case ProcessState.PAYLOAD_LENGTH:
						if (curByte != 0)
						{
							byte tmp;
							// If this packet is from a V1 that supports checksum, we want to decrement the payload length by 1, to make packet match packets from
							// Legacy and non-checksum V1's
							if ((retPacket.m_valentineType == Devices.VALENTINE1_LEGACY) || (retPacket.m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
							{
								tmp = curByte;
							}
							else
							{
								// If this packet is from a V1 that supports checksum, we want to decrement the packet length by 1
								// to make packet match packets from Legacy and non-checksum V1's
								tmp = (byte)(curByte - 1);
							}

							retPacket.payloadLength = tmp;
							// If payloadLength is zero, then the next byte in the buffer will be the packet checksum. For non-checksum V1 devices
							// the payloadLength is greater than zero so the next byte will be payload data.
							if (retPacket.payloadLength == 0)
							{
								processState = ProcessState.PACKET_CHEKSUM;
							}
							else {
								retPacket.payloadData = new byte[retPacket.payloadLength];
								processState = ProcessState.PAYLOAD;

							}
							// Always include the payload length in the packet data.
							espChecksum += curByte;
						}
						else {
							// There is no payload data so go to the end of frame.
							processState = ProcessState.EOF;
						}
						break;
					case ProcessState.PAYLOAD:
						retPacket.payloadData[payloadIdx] = curByte;
						payloadIdx++;
						// Update the ESP checksum.
						espChecksum += curByte;
						// If we have reached the end of the payload data, move on to the next byte in the buffer. 
						if (payloadIdx == retPacket.payloadLength)
						{
							if ((retPacket.m_valentineType == Devices.VALENTINE1_LEGACY) || (retPacket.m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
							{
								// Get the EOF byte next
								processState = ProcessState.EOF;
							}
							else
							{
								processState = ProcessState.PACKET_CHEKSUM;
							}
						}
						break;
					case ProcessState.PACKET_CHEKSUM:
						if (espChecksum != curByte)
						{
							// The checksum does not match
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Bad ESP checksum. Expected 0x" + String.Format("%02X ", espChecksum) + " but found 0x" + String.Format("%02X ", curByte));
							}
						}
						else {
							// Store the checksum 
							retPacket.checkSum = curByte;
						}

						// Get the EOF byte next
						processState = ProcessState.EOF;
						break;
					case ProcessState.EOF:
						if (curByte != endOfFrameConstant)
						{
							// Bad data so let's bail
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Unable to find EOF at the expected index: " + i);
							}
						}

						retPacket.endOfFrame = curByte;
						break;

					case ProcessState.BT_CHECKSUM:
					case ProcessState.END_PACK_BYTE:
					case ProcessState.PACKET_LENGTH:
					case ProcessState.START_PACK_BYTE:
					default:
						// We should never get here, so something went wrong
						dataError = true;
						break;
				}
				if (dataError)
				{
					break;
				}
			}

			buffer.Clear();
			if (dataError)
			{
				return null;
			}
			// Force the ESPPacket checksum to zero if the V1 does not support checksums before returning the packet.
			if ((retPacket.m_valentineType == Devices.VALENTINE1_LEGACY) || (retPacket.m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
			{

				retPacket.checkSum = 0;
			}

			retPacket.packetChecksum = retPacket.makePacketChecksum();
			return retPacket;
		}

		protected static ESPPacket makeFromBufferSPP(List<Byte> buffer, Devices lastV1Type)
		{

			int startIdx = -1;
			int endIdx = -1;

			// Make a copy of the buffer as it was at the beginning of the method call
			List<Byte> curStartBuffer = new List<Byte>();
			mCopyBuffer(buffer, curStartBuffer);

			for (int index = 0; index < buffer.Count; index++)
			{
				byte test = buffer[index];//.ByteValue();
				if (test == frameDelimitedConstant)
				{
					if (startIdx == -1)
					{
						// We are looking for the start index
						startIdx = index;
					}
					else {
						// We are looking for the end index
						if (index == startIdx + 1)
						{
							// There are two delimiters together. This is expected to happen during startup when we can 
							// receive the end of one packet followed by a valid packet. In this instance, we want to 
							// move the start index to the beginning of the new packet
							startIdx = index;
						}
						else {
							// This should be the end of the packet so save the index and stop searching
							endIdx = index;
							break;
						}
					}
				}
			}

			if (startIdx == -1 || endIdx == -1)
			{
				// Copy start and end buffer in case the next call fails			
				mCopyBuffer(curStartBuffer, mLastStartBuffer);
				mCopyBuffer(buffer, mLastEndBuffer);
				// We did not receive a full packet
				return null;
			}

			if (startIdx != 0 && ESPLibraryLogController.LOG_WRITE_ERROR)
			{
				//Log.e(LOG_TAG, "Skipping " + startIdx + " bytes because there was no delimiter at index 0");
				//Log.e(LOG_TAG, "  Current buffer: " + getBufferLogString(buffer));
				//Log.e(LOG_TAG, "  Last Start buffer: " + getBufferLogString(mLastStartBuffer));
				//Log.e(LOG_TAG, "  Last End buffer: " + getBufferLogString(mLastEndBuffer));
			}

			// Process the buffer
			int i = startIdx;
			int payloadIdx = 0;
			ESPPacket retPacket = null;
			ProcessState processState = ProcessState.START_PACK_BYTE;
			bool dataError = false;

			// Store these values until we have a packet to put them into
			byte tempLength = 0;
			byte tempDest = 0;
			byte tempOrigin = 0;
			byte packetChecksum = 0;
			byte espChecksum = 0;

			while (i <= endIdx)
			{
				// Read the next byte from the buffer 
				byte curByte = buffer[i];//.get(i).byteValue();

				if (curByte == frameDataEscapeConstant)
				{
					// Check the next byte to see if it should be turned into an 0x7F or 0x7D
					i++;
					curByte = buffer[i];//.get(i).byteValue();
										// Skip the next byte after the delimiter
					if (curByte == (byte)0x5D)
					{
						// If we find 0x5D after the delimiter, turn it into an 0x7D
						curByte = (byte)0x7D;
					}
					else if (curByte == (byte)0x5F)
					{
						// If we find 0x5F after the delimiter, turn it into an 0x7F
						curByte = (byte)0x7F;
					}
				}

				switch (processState)
				{
					default:
					case ProcessState.START_PACK_BYTE:
						if (curByte != frameDelimitedConstant)
						{
							// How did THIS happen?!?
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Missing 0x7F at startIdx: " + startIdx);
							}
						}

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.PACKET_LENGTH;
						break;

					case ProcessState.PACKET_LENGTH:
						tempLength = curByte;

						// Update the checksum
						packetChecksum += curByte;

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.SOF;
						break;

					case ProcessState.SOF:
						if (curByte != startOfFrameConstant)
						{
							// Bad data so let's bail
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Missing SOF at the expected index: " + i);
							}
						}

						// Update the checksum
						packetChecksum += curByte;
						espChecksum += curByte;

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.DESTINATION;
						break;

					case ProcessState.DESTINATION:
						if ((curByte & destinationIdentifierBaseConstant) != destinationIdentifierBaseConstant)
						{
							// This is not a valid destination
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Invalid destination ID (" + String.format("%02X ", curByte) + ") at the expected index: " + i);
							}
						}

						tempDest = curByte;

						// Update the checksum
						packetChecksum += curByte;
						espChecksum += curByte;

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.ORIGINATOR;
						break;

					case ProcessState.ORIGINATOR:
						if ((curByte & originationIdentifierBaseConstant) != originationIdentifierBaseConstant)
						{
							// This is not a valid originator
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Invalid originator ID (" + String.format("%02X ", curByte) + ") at the expected index: " + i);
							}
						}

						tempOrigin = curByte;

						// Update the checksum
						packetChecksum += curByte;
						espChecksum += curByte;

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.PACKET_ID;
						break;

					case ProcessState.PACKET_ID:
						// Make the packet
						retPacket = PacketFactory.getPacket(PacketIdLookup.getConstant(curByte));
						if (retPacket == null)
						{
							// We couldn't build the packet so stop trying
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Unable to generate packet for packet id (" + String.format("%02X ", curByte) + ")");
							}
						}
						else {
							// We have a good packet so fill it up
							retPacket.headerDelimter = frameDelimitedConstant;  //<- We can't get here if this wasn't true
							retPacket.packetLength = tempLength;

							retPacket.startOfFrame = startOfFrameConstant;   //<- We can't get here if this wasn't true
							retPacket.destinationIdentifier = tempDest;
							// Don't store the upper nibble of the destinations
							retPacket.m_destination = (byte)(tempDest - destinationIdentifierBaseConstant);
							// Don't store the upper nibble of the origin
							retPacket.originatorIdentifier = (byte)(tempOrigin - originationIdentifierBaseConstant);
							retPacket.packetIdentifier = curByte;

							// If the packet is from a V1 set the ESPPacket V1 type to the appropriate Device type. 
							if (IsPacketFromV1(retPacket.originatorIdentifier))
							{
								retPacket.m_valentineType = Devices.FromByteValue(retPacket.originatorIdentifier);
							}
							else {
								retPacket.m_valentineType = lastV1Type;
							}
							// If the last known V1 type is unknown check to see if the ESPPacket is V1connection version response. 
							if (retPacket.m_valentineType == Devices.UNKNOWN)
							{
								if (retPacket.packetIdentifier != PacketId.respVersion.ToByteValue() || retPacket.originatorIdentifier != Devices.V1CONNECT.ToByteValue())
								{
									// Always allow the V1connection version responses to pass through
									// Don't process any other data until we know what type of V1 we are working with
									dataError = true;
									if (ESPLibraryLogController.LOG_WRITE_ERROR)
									{
										//Log.e(LOG_TAG, "Ignore packet id 0x" + String.Format("%02X ", curByte) + " because the V1 type is unknown");
									}
								}
							}
						}

						// Update the checksum
						packetChecksum += curByte;
						espChecksum += curByte;

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.PAYLOAD_LENGTH;
						break;

					case ProcessState.PAYLOAD_LENGTH:
						if (curByte != 0)
						{
							byte tmp;
							if ((retPacket.m_valentineType == Devices.VALENTINE1_LEGACY) || (retPacket.m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
							{
								tmp = curByte;
							}
							else
							{
								// If this packet is from a V1 that supports checksum, we want to decrement the payload length by 1
								// to make packet match packets from Legacy and non-checksum V1's
								tmp = (byte)(curByte - 1);
							}
							retPacket.payloadLength = tmp;
							// If payloadLength is zero, then the next byte in the buffer will be the packet checksum. For non-checksum V1 devices
							// the payloadLength is greater than zero so the next byte will be payload data.
							if (retPacket.payloadLength == 0)
							{
								processState = ProcessState.PACKET_CHEKSUM;
							}
							else {
								retPacket.payloadData = new byte[retPacket.payloadLength];
								processState = ProcessState.PAYLOAD;

							}
							// Always include the payload length in the packet data.
							espChecksum += curByte;// Update the PACKET checksum
							packetChecksum += curByte;
						}
						else {
							// There is no payload data so go to the end of frame.
							processState = ProcessState.EOF;
						}
						break;
					case ProcessState.PAYLOAD:
						retPacket.payloadData[payloadIdx] = curByte;
						payloadIdx++;
						// Update the ESP checksum.
						espChecksum += curByte;
						// Update the PACKET checksum
						packetChecksum += curByte;
						// If we have reached the end of the payload data, handle checking the checksum.
						if (payloadIdx == retPacket.payloadLength)
						{
							if ((retPacket.m_valentineType == Devices.VALENTINE1_LEGACY) || (retPacket.m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
							{
								// Get the EOF byte next
								processState = ProcessState.EOF;
							}
							else {
								processState = ProcessState.PACKET_CHEKSUM;
							}
						}
						break;
					case ProcessState.PACKET_CHEKSUM:
						// If the calculated checksum does not equal the checksum byte, an error has occurred.
						if (espChecksum != curByte)
						{
							// The checksum does not match
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Bad ESP checksum. Expected 0x" + String.format("%02X ", espChecksum) + " but found 0x" + String.format("%02X ", curByte));
							}
						}
						// Store the checksum
						packetChecksum += curByte;
						retPacket.checkSum = curByte;
						// Get the EOF byte next
						processState = ProcessState.EOF;
						break;
					case ProcessState.EOF:
						if (curByte != endOfFrameConstant)
						{
							// Bad data so let's bail
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Unable to find EOF at the expected index: " + i);
							}
						}
						retPacket.endOfFrame = curByte;
						// Update the packet checksum
						packetChecksum += curByte;
						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.BT_CHECKSUM;
						break;
					case ProcessState.BT_CHECKSUM:
						// Update the packet checksum
						if (packetChecksum != curByte)
						{
							// We are missing something
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Bad packet checksum. Expected 0x" + String.format("%02X ", packetChecksum) + " but found 0x" + String.format("%02X ", curByte));
							}
						}
						retPacket.packetChecksum = curByte;

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.END_PACK_BYTE;
						break;

					case ProcessState.END_PACK_BYTE:
						if (i != endIdx)
						{
							// We should be at the end of the data by now
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Excpected to be at index " + endIdx + " but we are at index " + i);
							}
						}
						else if (curByte != frameDelimitedConstant)
						{
							// How did THIS happen?!?
							dataError = true;
							if (ESPLibraryLogController.LOG_WRITE_ERROR)
							{
								//Log.e(LOG_TAG, "Missing 0x7F at endIdx: " + endIdx);
							}
						}

						retPacket.endDelimter = curByte;

						// Move to the next state. If there is a data error, the next state will never be used so
						// we don't need to check for that.
						processState = ProcessState.START_PACK_BYTE;
						break;
				}

				if (dataError)
				{
					// Stop processing the data
					break;
				}

				// Increment the index to the next byte
				i++;
			}

			// Remove all bytes up to and including the end index
			TrimBuffer(buffer, endIdx);

			// Copy start and end buffer in case the next call fails			
			mCopyBuffer(curStartBuffer, mLastStartBuffer);
			mCopyBuffer(buffer, mLastEndBuffer);

			if (dataError)
			{
				return null;
			}

			// Force the ESPPacket checksum to zero if the V1 does not support checksums before returning the packet.
			if ((retPacket.V1Type == Devices.VALENTINE1_LEGACY) || (retPacket.V1Type == Devices.VALENTINE1_WITHOUT_CHECKSUM))
			{
				retPacket.checkSum = 0;
			}
			return retPacket;
		}

		public static void TrimBuffer(List<Byte> buffer, int trimToPosition)
		{
			// A potentially faster implementation to clear the buffer
			//buffer.subList(0, trimToPosition + 1).Clear();
			buffer = buffer.GetRange(trimToPosition, buffer.Count - trimToPosition);
		}

		public static bool IsPacketFromV1(byte originatorIdentifier)
		{
			return (originatorIdentifier == Devices.VALENTINE1_WITH_CHECKSUM.ToByteValue()
					|| originatorIdentifier == Devices.VALENTINE1_WITHOUT_CHECKSUM.ToByteValue()
					|| originatorIdentifier == Devices.VALENTINE1_LEGACY.ToByteValue());
		}

		public static byte[] MakeByteStream(ESPPacket packet, ConnectionType connectionType)
		{
			if(connectionType == ConnectionType.V1Connection_LE)
					return makeByteStreamLE(packet);
			else
					return makeByteStreamSPP(packet);
		}

		protected static byte[] makeByteStreamLE(ESPPacket packet)
		{
			byte[] buffer = null;
			// Error prevention. If we are given a null packet return a null buffer.
			if (packet == null)
			{
				return buffer;
			}

			int size = packet.PacketLength;
			int payloadOffset;
			if ((packet.V1Type == Devices.VALENTINE1_LEGACY) || (packet.V1Type == Devices.VALENTINE1_WITHOUT_CHECKSUM))
			{
				// If the size is not at least 5 plus payload length, return null.
				if (size <= 5 + packet.payloadLength)
				{
					if (ESPLibraryLogController.LOG_WRITE_ERROR)
					{
						//Log.e(LOG_TAG, "Packet length does not meet the minimum required lenght of " + 5 + "bytes. Returning null.");
					}
					return buffer;
				}
				// Set the payload offset to 1 because the legacy and no checksum will not have any payload data so adjust the payload
				// offset to account for the missing byte.
				payloadOffset = 0;
			}
			else {
				// If the size is not at least 6 plus payload length, return null.
				if (size <= 6 + packet.payloadLength)
				{
					if (ESPLibraryLogController.LOG_WRITE_ERROR)
					{
						//Log.e(LOG_TAG, "Packet length does not meet the minimum required lenght of " + 6 + "bytes. Returning null.");
					}
					return buffer;
				}
				// Set the payload offset to 1 if the V1 supports checksum.
				payloadOffset = 1;
			}
			buffer = new byte[packet.packetLength];

			buffer[0] = (byte)(packet.startOfFrame & 0xff);
			buffer[1] = (byte)(packet.destinationIdentifier & 0xff);
			buffer[2] = buildDeviceIdentifier(originationIdentifierBaseConstant, packet.originatorIdentifier);
			buffer[3] = (byte)(packet.packetIdentifier & 0xff);
			buffer[4] = (byte)((packet.payloadLength + payloadOffset) & 0xff);

			if (packet.payloadData != null)
			{
				for (int i = 0; i < packet.payloadLength; i++)
				{
					buffer[5 + i] = (byte)packet.payloadData[i];
				}
			}

			if (packet.V1Type == Devices.VALENTINE1_WITH_CHECKSUM)
			{
				buffer[5 + packet.payloadLength] = (byte)packet.checkSum;
				buffer[6 + packet.payloadLength] = (byte)packet.endOfFrame;
			}
			else {
				buffer[5 + packet.payloadLength] = (byte)packet.endOfFrame;
			}
			return buffer;
		}

		protected static byte[] makeByteStreamSPP(ESPPacket packet)
		{
			/*
			 * The data format for a V1 With Checksums is
			 * 		Byte		Description
			 * 		0				Leading PACK byte (0x7F)
			 * 		1				The packet length
			 * 		2				ESP SOF
			 * 		3				ESP Destination
			 * 		4				ESP Originator
			 * 		5				ESP Packet ID
			 * 		6				ESP Payload Length
			 * 		7				First byte of payload data
			 *     		....
			 *     		....
			 *     		....
			 * 		7 + payloadSize	ESP Checksum
			 * 		8 + payloadSize	ESP EOF
			 * 		9 + payloadSize	BT Wrapper Checksum
			 * 		10				Trailing PACK byte (0x7F)
			 * 
			 * The data format for a V1 Without Checksums is
			 * 		Byte		Description
			 * 		0				Leading PACK byte (0x7F)
			 * 		1				The packet length
			 * 		2				ESP SOF
			 * 		3				ESP Destination
			 * 		4				ESP Originator
			 * 		5				ESP Packet ID
			 * 		6				ESP Payload Length
			 * 		7				First byte of payload data
			 *     		....
			 *     		....
			 *     		....
			 * 		7 + payloadSize	ESP EOF
			 * 		8 + payloadSize	BT Wrapper Checksum
			 * 		9				Trailing PACK byte (0x7F)
			 */


			byte[] buffer = null;
			// Error prevention. If we are given a null packet return a null buffer.
			if (packet == null)
			{
				return buffer;
			}

			int size = packet.packetLength + 4;

			int payloadOffset;
			if ((packet.V1Type == Devices.VALENTINE1_LEGACY) || (packet.V1Type == Devices.VALENTINE1_WITHOUT_CHECKSUM))
			{
				// If the size is not at least 9 plus payload length, return null.
				if (size <= 9 + packet.payloadLength)
				{
					//Log.e(LOG_TAG, "Packet length does not meet the minimum required lenght of " + 9 + "bytes. Returning null.");
					return buffer;
				}
				// Set the payload offset to 1 because the legacy and no checksum will not have any payload data so adjust the payload
				// offset to account for the missing byte.
				payloadOffset = 0;
			}
			else {
				// If the size is not at least 10 plus payload length, return null.
				if (size <= 10 + packet.payloadLength)
				{
					//Log.e(LOG_TAG, "Packet length does not meet the minimum required lenght of " + 10 + "bytes. Returning null.");
					return buffer;
				}
				// Set the payload offset to 1 if the V1 supports checksum.
				payloadOffset = 1;
			}

			buffer = new byte[size];

			buffer[0] = (byte)(packet.headerDelimter & 0xff);
			buffer[1] = (byte)(packet.packetLength & 0xff);

			buffer[2] = (byte)(packet.startOfFrame & 0xff);
			buffer[3] = (byte)(packet.destinationIdentifier & 0xff);
			buffer[4] = buildDeviceIdentifier(originationIdentifierBaseConstant, packet.originatorIdentifier);
			buffer[5] = (byte)(packet.packetIdentifier & 0xff);
			buffer[6] = (byte)((packet.payloadLength + payloadOffset) & 0xff);

			if (packet.payloadData != null)
			{
				for (int i = 0; i < packet.payloadLength; i++)
				{
					buffer[7 + i] = (byte)packet.payloadData[i];
				}
			}

			if (packet.V1Type == Devices.VALENTINE1_WITH_CHECKSUM)
			{
				buffer[7 + packet.payloadLength] = (byte)packet.checkSum;
				buffer[8 + packet.payloadLength] = (byte)packet.endOfFrame;
				buffer[9 + packet.payloadLength] = (byte)packet.packetChecksum;
				buffer[10 + packet.payloadLength] = (byte)packet.endDelimter;
			}
			else {
				buffer[7 + packet.payloadLength] = (byte)packet.endOfFrame;
				buffer[8 + packet.payloadLength] = (byte)packet.packetChecksum;
				buffer[9 + packet.payloadLength] = (byte)packet.endDelimter;
			}
			return buffer;
		}

		public override string ToString()
		{
			byte[] buffer = ESPPacket.MakeByteStream(this, mConnectionType);
			if (buffer == null)
			{
				return "Null buffer.";
			}
			StringBuilder b = new StringBuilder("Packet Buffer Values:\n");

			for (int i = 0; i < buffer.Length; i++)
			{
				b.Append(String.Format("Pos %d = %02X\n", i, buffer[i]));
			}

			return b.ToString();
		}

		protected byte makeMessageChecksum()
		{
			int payloadOffset;
			if ((m_valentineType == Devices.VALENTINE1_LEGACY) || (m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
			{
				payloadOffset = 0;
			}
			else {
				// Set the payload offset to 1 if the V1 supports checksum.
				payloadOffset = 1;
			}

			long temp =
					startOfFrame +
					destinationIdentifier +
					buildDeviceIdentifier(originationIdentifierBaseConstant, originatorIdentifier) +
					packetIdentifier +
					(payloadLength + payloadOffset);

			if (payloadData != null)
			{
				for (int i = 0; i < payloadLength; i++)
				{
					temp += payloadData[i];
				}
			}

			temp = temp & 0xff;

			return (byte)temp;
		}

		protected byte makePacketChecksum()
		{

			int payloadOffset;
			if ((m_valentineType == Devices.VALENTINE1_LEGACY) || (m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
			{
				payloadOffset = 0;
			}
			else {
				// Set the payload offset to 1 if the V1 supports checksum.
				payloadOffset = 1;
			}

			long temp =
				startOfFrame +
				destinationIdentifier +
				buildDeviceIdentifier(originationIdentifierBaseConstant, originatorIdentifier) +
				packetIdentifier +
				(payloadLength + payloadOffset) +
				checkSum +
				endOfFrame;

			if (payloadData != null)
			{
				for (int i = 0; i < payloadLength; i++)
				{
					temp += payloadData[i];
				}
			}
			temp += packetLength;

			return (byte)temp;
		}

		public long TimeStamp
		{
			get
			{
				return m_timeStamp;
			}
		}

		public bool ResentFlag
		{
			get
			{
				return m_resent;
			}
			set
			{
				m_resent = value;
			}
		}

		public void SetNewV1Type(Devices newV1Type)
		{
			if (newV1Type != null && newV1Type != m_valentineType)
			{
				Devices oldDest = Destination;

				if (oldDest == m_valentineType || oldDest == Devices.VALENTINE1_LEGACY || oldDest == Devices.VALENTINE1_WITH_CHECKSUM || oldDest == Devices.VALENTINE1_WITHOUT_CHECKSUM)
				{
					// Change the destination to the current V1 type
					m_destination = newV1Type.ToByteValue();
				}
				m_valentineType = newV1Type;
				buildPacket();
			}
		}

		protected virtual void buildPacket()
		{
			headerDelimter = (byte)frameDelimitedConstant;

			startOfFrame = startOfFrameConstant;

			if (m_destination == Devices.VALENTINE1_LEGACY.ToByteValue())
			{
				destinationIdentifier = buildDeviceIdentifier(destinationIdentifierBaseConstant, Devices.VALENTINE1_WITHOUT_CHECKSUM.ToByteValue());
			}
			else if (m_destination == Devices.UNKNOWN.ToByteValue())
			{

				destinationIdentifier = buildDeviceIdentifier(destinationIdentifierBaseConstant, Devices.VALENTINE1_WITH_CHECKSUM.ToByteValue());
			}
			else
			{
				destinationIdentifier = buildDeviceIdentifier(destinationIdentifierBaseConstant, m_destination);
			}

			originatorIdentifier = Devices.V1CONNECT.ToByteValue();

			endOfFrame = endOfFrameConstant;

			endDelimter = (byte)frameDelimitedConstant;

		}

		protected void setPacketInfo()
		{

			if ((m_valentineType == Devices.VALENTINE1_LEGACY) || (m_valentineType == Devices.VALENTINE1_WITHOUT_CHECKSUM))
			{
				packetLength = (byte)(6 + payloadLength);
			}
			else {
				packetLength = (byte)(7 + payloadLength);
			}

			packetChecksum = makePacketChecksum();
		}

		protected static byte buildDeviceIdentifier(byte baseConstant, byte deviceByteValue)
		{
			return (byte)((deviceByteValue & 0x0f) + baseConstant);
		}


		public abstract Object getResponseData();

	}
}
