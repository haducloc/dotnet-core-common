using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCore.Common.Entities
{

    public class RemMeToken : IVerification
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Series { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string HashIdentity { get; set; }

        [Required]
        public long? ExpiresAtUtc { get; set; }

        [Required]
        public long? IssuedAtUtc { get; set; }
    }
}
