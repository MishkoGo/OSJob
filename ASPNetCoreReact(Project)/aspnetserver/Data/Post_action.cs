using System.ComponentModel.DataAnnotations;

namespace aspnetserver.Data
{
    internal sealed class Posts_action
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public int id_task { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(100)]
        public string whom { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; } = string.Empty;
    }
}
