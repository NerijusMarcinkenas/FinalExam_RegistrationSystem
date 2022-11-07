using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Core.Models
{
    public class PersonImage : BaseModel
    {
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [MaxLength(255)]
        public string ContentType { get; set; } = null!;
        public byte[] ImageBytes { get; set; } = null!;

        [ForeignKey("Person")]
        public string PersonId { get; set; } = null!;

        public Person Person { get; set; } = null!;
    }
}