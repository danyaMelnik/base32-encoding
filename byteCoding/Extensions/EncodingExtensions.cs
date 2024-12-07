using System.Text;

namespace byteCoding.Extensions
{
    public static class EncodingExtensions
    {
        //private static string Base64CodingSpace = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        private static string Base32CodingSpace = "0123456789abcdefghijklmnopqrstuv";
        private static char additionalSymbol = '=';
        public static string ToShortGuid(this Guid guid)
        {
            return ToBase32(guid.ToByteArray());
        }

        /// <summary>
        /// Converts bytes to base32 string (5 bit)
        /// </summary>
        /// <param name="bytes">input</param>
        /// <returns>encoded string</returns>
        public static string ToBase32(this byte[] bytes)
        {
            const int numericSystem = 5;

            var encodedResult = new StringBuilder((int)Math.Ceiling(bytes.Length * 8.0 / 5.0));

            for (var i = 0; i < bytes.Length; i += numericSystem)
            {
                var byteCount = Math.Min(numericSystem, bytes.Length - i);
                ulong buffer = 0;

                for (var j = 0; j < byteCount; ++j)
                {
                    buffer = (buffer << 8) | bytes[i + j];
                }

                var bitCount = byteCount * 8;

                while (bitCount > 0)
                {
                    byte oneByte = 0x1f;

                    var index = bitCount >= numericSystem
                                        ? (int)(buffer >> (bitCount - numericSystem)) & oneByte
                                         : (int)(buffer & (ulong)(oneByte >> (numericSystem - bitCount))) << (numericSystem - bitCount);

                    encodedResult.Append(Base32CodingSpace[index]);
                    bitCount -= numericSystem;
                }
            }

            int remainder = encodedResult.Length % 8;

            if (remainder > 0)
            {
                int paddingCount = 8 - remainder;
                encodedResult.Append(additionalSymbol, paddingCount);
            }

            return encodedResult.ToString();
        }
    }
}
