using byteCoding.Extensions;

public static class Porgram {
    public static void Main(String[] args)
    {

        byte[] inputBytes = new byte[3];

        inputBytes[0] = 0b00001000;
        inputBytes[1] = 0b01000010;
        inputBytes[2] = 0b00000001;

        string base32 = inputBytes.ToBase32();

        byte[] fromBase32 = base32.FromBase32();
    }

}