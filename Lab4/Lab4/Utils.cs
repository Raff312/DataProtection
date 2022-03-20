using System.Text;

namespace Lab4
{
    public static class Utils
    {
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static string ToBinary(byte[] data)
        {
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }

        public static string ToString(byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        public static byte[] BinaryToByte(string str)
        {
            int numOfBytes = str.Length / 8;
            List<byte> bytes = new();
            for (int i = 0; i < numOfBytes; i++)
            {
                byte b = Convert.ToByte(str.Substring(8 * i, i * 8 + 8 < str.Length ? 8 : str.Length - i * 8), 2);
                if (b != 0)
                {
                    bytes.Add(b);
                }
            }

            return bytes.ToArray();
        }

        public static string[] SplitIntoBlocks(string input, byte size)
        {
            List<string> result = new();

            if (input.Length < size)
            {
                result.Add(input.PadLeft(size, '0'));
                return result.ToArray();
            }

            for (int i = 0; i < Math.Ceiling((double)input.Length / size); i++)
            {
                string value = input.Substring(i * size, i * size + size < input.Length ? size : input.Length - i * size);
                if (value.Length < size)
                {
                    value = value.PadLeft(size, '0');
                }

                result.Add(value);
            }

            return result.ToArray();
        }
    }
}
