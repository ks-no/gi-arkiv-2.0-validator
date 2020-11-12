using System;
using System.ComponentModel.DataAnnotations;

namespace KS.GIArkivValidator.WebAPI.Models
{
    public class FiksResponse
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ReceivedAt { get; set; }

        [Required]
        public string Type { get; set; }

        public string Payload { get; set; }

        public string PayloadContent { get; set; }
    }
}
