using System.Text;

namespace byteCoding.Extensions
{
    public static class EncodingExtensions
    {
        private const string Base32CodingSpace = "0123456789abcdefghijklmnopqrstuv";
        private const char PaddingChar = '=';
        private const int BitsPerByte = 8;
        private const int BitsPerBase32Char = 5;
        private const int Base32BlockSize = 5;

        public static string ToShortGuid(this Guid guid)
        {
            return ToBase32(guid.ToByteArray());
        }

        public static string ToBase32(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            var encodedResult = new StringBuilder((int)Math.Ceiling(bytes.Length * BitsPerByte / (double)BitsPerBase32Char));

            for (var i = 0; i < bytes.Length; i += Base32BlockSize)
            {
                var byteCount = Math.Min(Base32BlockSize, bytes.Length - i);
                ulong buffer = 0;

                for (var j = 0; j < byteCount; ++j)
                {
                    buffer = (buffer << BitsPerByte) | bytes[i + j];
                }

                var bitCount = byteCount * BitsPerByte;
                const byte Base32Mask = 0x1F;

                while (bitCount > 0)
                {
                    var index = bitCount >= BitsPerBase32Char
                        ? (int)(buffer >> (bitCount - BitsPerBase32Char)) & Base32Mask
                        : (int)(buffer & (ulong)(Base32Mask >> (BitsPerBase32Char - bitCount))) << (BitsPerBase32Char - bitCount);

                    encodedResult.Append(Base32CodingSpace[index]);
                    bitCount -= BitsPerBase32Char;
                }
            }

            int remainder = encodedResult.Length % BitsPerByte;

            if (remainder > 0)
            {
                int paddingCount = BitsPerByte - remainder;
                encodedResult.Append(PaddingChar, paddingCount);
            }

            return encodedResult.ToString();
        }

        public static byte[] FromBase32(this string str)
        {
            str = str.TrimEnd(PaddingChar);

            var bytes = new List<byte>();
            int buffer = 0;
            int bitsInBuffer = 0;

            foreach (char charValue in str)
            {
                int index = Base32CodingSpace.IndexOf(charValue);

                buffer = (buffer << BitsPerBase32Char) | index;
                bitsInBuffer += BitsPerBase32Char;

                while (bitsInBuffer >= BitsPerByte)
                {
                    bitsInBuffer -= BitsPerByte;
                    bytes.Add((byte)((buffer >> bitsInBuffer) & 0xFF));
                }
            }

            return bytes.ToArray();
        }
    }
}
