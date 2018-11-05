namespace NetCore.Common.Crypto
{
    public interface Encryptor
    {
        byte[] Encrypt(byte[] message);

        byte[] Decrypt(byte[] message);
    }
}
