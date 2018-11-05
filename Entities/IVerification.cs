namespace NetCore.Common.Entities
{
    public interface IVerification
    {
        string Series { get; set; }

        string Token { get; set; }

        string HashIdentity { get; set; }

        long? ExpiresAtUtc { get; set; }

        long? IssuedAtUtc { get; set; }
    }
}
