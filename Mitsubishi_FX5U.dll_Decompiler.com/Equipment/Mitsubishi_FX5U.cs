using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Equipment;

public class Mitsubishi_FX5U
{
	private SerialPort comport;

	private string COM = string.Empty;

	public bool sending;

	private byte[] ThetaSpeed = new byte[2];

	private byte[] PhiSpeed = new byte[2];

	public Mitsubishi_FX5U(string Port)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		COM = Port;
		comport = new SerialPort(COM, 115200, (Parity)0, 8, (StopBits)1);
		ThetaSpeed[0] = 132;
		ThetaSpeed[1] = 3;
		PhiSpeed[0] = 132;
		PhiSpeed[1] = 3;
	}

	public void test()
	{
		string text = Convert.ToString(900, 16);
		string text2 = Convert.ToString(3600, 16);
		byte[] array = SetPhiAngleModeBusByteConvertoer(90.0);
		byte[] buf = new byte[6] { 1, 3, 0, 101, 0, 1 };
		byte[] array2 = ModRTUConverter(buf);
		byte[] buf2 = new byte[6] { 1, 3, 0, 103, 0, 1 };
		byte[] array3 = ModRTUConverter(buf2);
		byte[] array4 = new byte[6] { 1, 6, 0, 1, 3, 132 };
		byte[] array5 = ModRTUConverter(buf2);
		byte[] array6 = new byte[6] { 1, 6, 0, 0, 0, 8 };
		byte[] array7 = Calculate_CRC(array6);
		string text3 = ModRTU_CRC(array6, 6).ToString("X2");
	}

	public void TurnClockWise()
	{
		try
		{
			string empty = string.Empty;
			comport.Open();
			byte[] array = new byte[8] { 1, 6, 0, 1, 3, 132, 216, 153 };
			comport.Write(array, 0, array.Length);
			Thread.Sleep(500);
			string text = string.Empty;
			byte[] array2 = new byte[4096];
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array2, 0, array2.Length);
				string empty2 = string.Empty;
				empty2 = Encoding.ASCII.GetString(array2);
				empty2 = empty2.Trim(new char[1]);
				text += empty2;
			}
			text = text.Trim(new char[1]);
			Thread.Sleep(500);
			byte[] array3 = new byte[8] { 1, 6, 0, 3, 3, 132, 121, 89 };
			comport.Write(array3, 0, array3.Length);
			Thread.Sleep(500);
			while (comport.BytesToRead > 0)
			{
				int num2 = comport.Read(array2, 0, array2.Length);
				string empty3 = string.Empty;
				empty3 = Encoding.ASCII.GetString(array2);
				empty3 = empty3.Trim(new char[1]);
				text += empty3;
			}
			byte[] array4 = new byte[8] { 1, 6, 0, 0, 4, 0, 139, 10 };
			comport.Write(array4, 0, array4.Length);
			Thread.Sleep(500);
			while (comport.BytesToRead > 0)
			{
				int num3 = comport.Read(array2, 0, array2.Length);
				string empty4 = string.Empty;
				empty4 = Encoding.ASCII.GetString(array2);
				empty4 = empty4.Trim(new char[1]);
				text += empty4;
			}
			byte[] array5 = new byte[8] { 1, 6, 0, 0, 0, 16, 136, 6 };
			comport.Write(array5, 0, array5.Length);
			Thread.Sleep(500);
			while (comport.BytesToRead > 0)
			{
				int num4 = comport.Read(array2, 0, array2.Length);
				string empty5 = string.Empty;
				empty5 = Encoding.ASCII.GetString(array2);
				empty5 = empty5.Trim(new char[1]);
				text += empty5;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
		}
		finally
		{
			comport.Close();
			sending = false;
		}
	}

	public void SetThetaAngle(double Angle)
	{
		try
		{
			string empty = string.Empty;
			byte[] array = new byte[8];
			comport.Open();
			byte[] buf = new byte[6] { 1, 6, 0, 0, 0, 0 };
			byte[] array2 = ModRTUConverter(buf);
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array, 0, array.Length);
			}
			byte[] buf2 = SetThetaAngleModeBusByteConvertoer(Angle);
			byte[] array3 = ModRTUConverter(buf2);
			comport.Write(array3, 0, array3.Length);
			Thread.Sleep(100);
			string empty2 = string.Empty;
			while (comport.BytesToRead > 0)
			{
				int num2 = comport.Read(array, 0, array.Length);
			}
			byte[] array4 = new byte[6] { 1, 6, 0, 3, 3, 132 };
			array4[4] = ThetaSpeed[1];
			array4[5] = ThetaSpeed[0];
			byte[] array5 = ModRTUConverter(array4);
			comport.Write(array5, 0, array5.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num3 = comport.Read(array, 0, array.Length);
			}
			byte[] buf3 = new byte[6] { 1, 6, 0, 0, 4, 0 };
			byte[] array6 = ModRTUConverter(buf3);
			comport.Write(array6, 0, array6.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num4 = comport.Read(array, 0, array.Length);
			}
			byte[] buf4 = new byte[6] { 1, 6, 0, 0, 0, 16 };
			byte[] array7 = ModRTUConverter(buf4);
			comport.Write(array7, 0, array7.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num5 = comport.Read(array, 0, array.Length);
			}
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num6 = comport.Read(array, 0, array.Length);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
		}
		finally
		{
			comport.Close();
		}
	}

	public void SetPhiAngle(double Angle)
	{
		try
		{
			string empty = string.Empty;
			byte[] array = new byte[8];
			comport.Open();
			byte[] buf = new byte[6] { 1, 6, 0, 0, 0, 0 };
			byte[] array2 = ModRTUConverter(buf);
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array, 0, array.Length);
			}
			byte[] buf2 = SetPhiAngleModeBusByteConvertoer(Angle);
			byte[] array3 = ModRTUConverter(buf2);
			comport.Write(array3, 0, array3.Length);
			Thread.Sleep(100);
			string empty2 = string.Empty;
			while (comport.BytesToRead > 0)
			{
				int num2 = comport.Read(array, 0, array.Length);
			}
			byte[] array4 = new byte[6] { 1, 6, 0, 4, 3, 132 };
			array4[4] = PhiSpeed[1];
			array4[5] = PhiSpeed[0];
			byte[] array5 = ModRTUConverter(array4);
			comport.Write(array5, 0, array5.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num3 = comport.Read(array, 0, array.Length);
			}
			byte[] buf3 = new byte[6] { 1, 6, 0, 0, 4, 0 };
			byte[] array6 = ModRTUConverter(buf3);
			comport.Write(array6, 0, array6.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num4 = comport.Read(array, 0, array.Length);
			}
			byte[] buf4 = new byte[6] { 1, 6, 0, 0, 0, 32 };
			byte[] array7 = ModRTUConverter(buf4);
			comport.Write(array7, 0, array7.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num5 = comport.Read(array, 0, array.Length);
			}
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num6 = comport.Read(array, 0, array.Length);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
		}
		finally
		{
			comport.Close();
		}
	}

	public void SetThetaSpeed(int Speed)
	{
		string text = Speed.ToString("X2");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (text.Length == 2)
		{
			empty = text.Substring(0, 2);
			empty2 = "0";
		}
		else if (text.Length == 3)
		{
			empty = text.Substring(1, 2);
			empty2 = text.Substring(0, 1);
		}
		else
		{
			empty = text.Substring(2, 2);
			empty2 = text.Substring(0, 2);
		}
		ThetaSpeed[0] = Convert.ToByte(empty, 16);
		ThetaSpeed[1] = Convert.ToByte(empty2, 16);
	}

	public void SetPhiSpeed(int Speed)
	{
		string text = Speed.ToString("X2");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (text.Length == 2)
		{
			empty = text.Substring(0, 2);
			empty2 = "0";
		}
		else if (text.Length == 3)
		{
			empty = text.Substring(1, 2);
			empty2 = text.Substring(0, 1);
		}
		else
		{
			empty = text.Substring(2, 2);
			empty2 = text.Substring(0, 2);
		}
		PhiSpeed[0] = Convert.ToByte(empty, 16);
		PhiSpeed[1] = Convert.ToByte(empty2, 16);
	}

	public double ReadThetaPosition()
	{
		byte[] array = new byte[16];
		double result = 0.0;
		try
		{
			string empty = string.Empty;
			comport.Open();
			byte[] buf = new byte[6] { 1, 3, 0, 101, 0, 2 };
			byte[] array2 = ModRTUConverter(buf);
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			string empty2 = string.Empty;
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array, 0, array.Length);
			}
			empty += array[5].ToString("X2");
			empty += array[6].ToString("X2");
			empty += array[3].ToString("X2");
			empty += array[4].ToString("X2");
			int num2 = Convert.ToInt32(empty, 16);
			result = (double)num2 / 10000.0;
			return result;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
			return result;
		}
		finally
		{
			comport.Close();
			sending = false;
		}
	}

	public double ReadPhiPosition()
	{
		byte[] array = new byte[16];
		double result = 0.0;
		try
		{
			string empty = string.Empty;
			comport.Open();
			byte[] buf = new byte[6] { 1, 3, 0, 103, 0, 2 };
			byte[] array2 = ModRTUConverter(buf);
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			string text = string.Empty;
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array, 0, array.Length);
				string empty2 = string.Empty;
				empty2 = Encoding.ASCII.GetString(array);
				empty2 = empty2.Trim(new char[1]);
				text += empty2;
			}
			empty += array[5].ToString("X2");
			empty += array[6].ToString("X2");
			empty += array[3].ToString("X2");
			empty += array[4].ToString("X2");
			int num2 = Convert.ToInt32(empty, 16);
			result = (double)num2 / 10000.0;
			return result;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
			return result;
		}
		finally
		{
			comport.Close();
			sending = false;
		}
	}

	public byte[] ReadThetaState()
	{
		byte[] array = new byte[8];
		try
		{
			string empty = string.Empty;
			comport.Open();
			byte[] array2 = new byte[8] { 1, 3, 0, 103, 0, 1, 53, 213 };
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			string text = string.Empty;
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array, 0, array.Length);
				string empty2 = string.Empty;
				empty2 = Encoding.ASCII.GetString(array);
				empty2 = empty2.Trim(new char[1]);
				text += empty2;
			}
			return array;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
			return array;
		}
		finally
		{
			comport.Close();
			sending = false;
		}
	}

	public bool ReadState()
	{
		bool result = false;
		byte[] array = new byte[8];
		string empty = string.Empty;
		try
		{
			string empty2 = string.Empty;
			comport.Open();
			byte[] array2 = new byte[8] { 1, 3, 0, 100, 0, 1, 197, 213 };
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			string empty3 = string.Empty;
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array, 0, array.Length);
			}
			empty = Convert.ToString(array[3], 2).PadLeft(8, '0');
			empty += Convert.ToString(array[4], 2).PadLeft(8, '0');
			if (empty[12] == '0' && empty[13] == '0')
			{
				if (empty[10] == '1' && empty[11] == '1')
				{
					result = true;
				}
			}
			else
			{
				result = false;
			}
			return result;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
			return result;
		}
		finally
		{
			comport.Close();
			sending = false;
		}
	}

	public void Reset()
	{
		byte[] array = new byte[4096];
		try
		{
			string empty = string.Empty;
			comport.Open();
			byte[] buf = new byte[6] { 1, 6, 0, 0, 0, 8 };
			byte[] array2 = ModRTUConverter(buf);
			comport.Write(array2, 0, array2.Length);
			Thread.Sleep(100);
			while (comport.BytesToRead > 0)
			{
				int num = comport.Read(array, 0, array.Length);
			}
			byte[] array3 = new byte[8] { 1, 6, 0, 0, 4, 0, 139, 10 };
			comport.Write(array3, 0, array3.Length);
			Thread.Sleep(500);
			while (comport.BytesToRead > 0)
			{
				int num2 = comport.Read(array, 0, array.Length);
			}
			byte[] array4 = new byte[8] { 1, 6, 0, 0, 0, 16, 136, 6 };
			comport.Write(array4, 0, array4.Length);
			Thread.Sleep(500);
			while (comport.BytesToRead > 0)
			{
				int num3 = comport.Read(array, 0, array.Length);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error:{ex.ToString()}");
		}
		finally
		{
			comport.Close();
			sending = false;
		}
	}

	private byte[] SetThetaAngleModeBusByteConvertoer(double Angle)
	{
		byte[] array = new byte[6] { 1, 6, 0, 1, 0, 0 };
		string text = ((int)(Angle * 10.0)).ToString("X2");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (text.Length == 2)
		{
			empty = text.Substring(0, 2);
			empty2 = "0";
		}
		else if (text.Length == 3)
		{
			empty = text.Substring(1, 2);
			empty2 = text.Substring(0, 1);
		}
		else
		{
			empty = text.Substring(2, 2);
			empty2 = text.Substring(0, 2);
		}
		array[4] = Convert.ToByte(empty2, 16);
		array[5] = Convert.ToByte(empty, 16);
		return array;
	}

	private byte[] SetPhiAngleModeBusByteConvertoer(double Angle)
	{
		byte[] array = new byte[6] { 1, 6, 0, 2, 0, 0 };
		string text = ((int)(Angle * 10.0)).ToString("X2");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (text.Length == 2)
		{
			empty = text.Substring(0, 2);
			empty2 = "0";
		}
		else if (text.Length == 3)
		{
			empty = text.Substring(1, 2);
			empty2 = text.Substring(0, 1);
		}
		else
		{
			empty = text.Substring(2, 2);
			empty2 = text.Substring(0, 2);
		}
		array[4] = Convert.ToByte(empty2, 16);
		array[5] = Convert.ToByte(empty, 16);
		return array;
	}

	private byte[] SetThetaSpeedModeBusByteConvertoer(int Speed)
	{
		byte[] array = new byte[6] { 1, 6, 0, 3, 3, 132 };
		string text = Speed.ToString("X2");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (text.Length == 2)
		{
			empty = text.Substring(0, 2);
			empty2 = "0";
		}
		else if (text.Length == 3)
		{
			empty = text.Substring(1, 2);
			empty2 = text.Substring(0, 1);
		}
		else
		{
			empty = text.Substring(2, 2);
			empty2 = text.Substring(0, 2);
		}
		array[4] = Convert.ToByte(empty2, 16);
		array[5] = Convert.ToByte(empty, 16);
		return array;
	}

	private byte[] SetPhiSpeedModeBusByteConvertoer(int Speed)
	{
		byte[] array = new byte[6] { 1, 6, 0, 3, 3, 132 };
		string text = Speed.ToString("X2");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (text.Length == 2)
		{
			empty = text.Substring(0, 2);
			empty2 = "0";
		}
		else if (text.Length == 3)
		{
			empty = text.Substring(1, 2);
			empty2 = text.Substring(0, 1);
		}
		else
		{
			empty = text.Substring(2, 2);
			empty2 = text.Substring(0, 2);
		}
		array[4] = Convert.ToByte(empty2, 16);
		array[5] = Convert.ToByte(empty, 16);
		return array;
	}

	private byte[] ModRTUConverter(byte[] buf)
	{
		byte[] array = new byte[8];
		string text = ModRTU_CRC(buf, buf.Length).ToString("X2");
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (text.Length == 3)
		{
			empty = text.Substring(1, 2);
			empty2 = text.Substring(0, 1);
		}
		else
		{
			empty = text.Substring(2, 2);
			empty2 = text.Substring(0, 2);
		}
		for (int i = 0; i < 8; i++)
		{
			switch (i)
			{
			case 6:
				array[i] = Convert.ToByte(empty, 16);
				break;
			case 7:
				array[i] = Convert.ToByte(empty2, 16);
				break;
			default:
				array[i] = buf[i];
				break;
			}
		}
		return array;
	}

	private ushort ModRTU_CRC(byte[] buf, int len)
	{
		ushort num = ushort.MaxValue;
		for (int i = 0; i < len; i++)
		{
			num ^= buf[i];
			for (int num2 = 8; num2 != 0; num2--)
			{
				if (((uint)num & (true ? 1u : 0u)) != 0)
				{
					num >>= 1;
					num = (ushort)(num ^ 0xA001u);
				}
				else
				{
					num >>= 1;
				}
			}
		}
		return num;
	}

	public byte[] Calculate_CRC(byte[] message)
	{
		byte[] array = new byte[2];
		ushort num = ushort.MaxValue;
		byte b = byte.MaxValue;
		byte b2 = byte.MaxValue;
		for (int i = 0; i < message.Length - 2; i++)
		{
			num ^= message[i];
			for (int j = 0; j < 8; j++)
			{
				char c = (char)(num & 1u);
				num = (ushort)((uint)(num >> 1) & 0x7FFFu);
				if (c == '\u0001')
				{
					num = (ushort)(num ^ 0xA001u);
				}
			}
		}
		b = (array[1] = (byte)((uint)(num >> 8) & 0xFFu));
		b2 = (array[0] = (byte)(num & 0xFFu));
		return array;
	}

	public byte[] HexToByte(string hexString)
	{
		byte[] array = new byte[hexString.Length / 2];
		for (int i = 0; i < hexString.Length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
		}
		return array;
	}
}
