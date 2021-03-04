using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KS.FiksProtokollValidator.WebAPI.Models
{
    public class TestCase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TestName { get; set; }

        [Required]
        public string MessageType { get; set; }

        [Required]
        public string PayloadFileName { get; set; }

        public string PayloadAttachmentFileNames { get; set; }

        public List<FiksResponseTest> FiksResponseTests { get; set; }
    }
}
