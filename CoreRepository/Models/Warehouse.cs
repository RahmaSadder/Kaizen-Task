
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace RepositoryCore.Models
{
    public class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Country { get; set; }

        [JsonIgnore]
        public virtual List<Item>? Items { get; set; }

    }

    public class WarehousStatistics
    {
        public Int64 Id { get; set; }
        public int count { get; set; }
        public string warehouse { get; set; }
    }
}
