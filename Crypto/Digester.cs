namespace NetCore.Common.Crypto
{
    public interface Digester
    {
        byte[] Digest(byte[] message);

        bool Verify(byte[] message, byte[] digested);

        int GetDigestSize();
    }
}
