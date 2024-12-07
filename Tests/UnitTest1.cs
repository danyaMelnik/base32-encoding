using byteCoding.Extensions;

namespace Tests
{
    public class EncodingExtensionsTests
    {
        [Fact]
        public void ToBase32_ShouldEncodeBytesCorrectly()
        {
            // Arrange
            byte[] inputBytes = new byte[] { 0b00001000, 0b01000010, 0b00000001 };
            string expectedBase32 = "11102===";

            // Act
            string actualBase32 = inputBytes.ToBase32();

            // Assert
            Assert.Equal(expectedBase32, actualBase32);
        }

        [Fact]
        public void FromBase32_ShouldDecodeBase32StringCorrectly()
        {
            // Arrange
            string base32String = "11102===";
            byte[] expectedBytes = new byte[] { 0b00001000, 0b01000010, 0b00000001 };

            // Act
            byte[] actualBytes = base32String.FromBase32();

            // Assert
            Assert.True(expectedBytes.SequenceEqual(actualBytes));
        }

        [Fact]
        public void FromBase32_ToBase32_ShouldBeReversible()
        {
            // Arrange
            byte[] originalBytes = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC };

            // Act
            string base32 = originalBytes.ToBase32();
            byte[] decodedBytes = base32.FromBase32();

            // Assert
            Assert.Equal(originalBytes, decodedBytes);
        }

        [Fact]
        public void ToBase32_WithEmptyArray_ShouldReturnEmptyString()
        {
            // Arrange
            byte[] emptyBytes = new byte[0];
            string expectedBase32 = "";

            // Act
            string actualBase32 = emptyBytes.ToBase32();

            // Assert
            Assert.Equal(expectedBase32, actualBase32);
        }

        [Fact]
        public void FromBase32_WithEmptyString_ShouldReturnEmptyArray()
        {
            // Arrange
            string emptyBase32 = "";
            byte[] expectedBytes = new byte[0];

            // Act
            byte[] actualBytes = emptyBase32.FromBase32();

            // Assert
            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
