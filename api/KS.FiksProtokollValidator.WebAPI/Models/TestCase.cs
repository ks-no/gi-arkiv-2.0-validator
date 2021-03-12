using System;
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

        [Required]
        public string Description { get; set; }
        [Required]
        public string TestStep { get; set; }
        [Required]
        public string Operation { get; set; }
        [Required]
        public string Situation { get; set; }
        [Required]
        public string ExpectedResult { get; set; }
        [Required]
        public Boolean Supported { get; set; }
    }
}
