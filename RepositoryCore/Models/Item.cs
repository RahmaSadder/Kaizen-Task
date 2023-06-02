
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoreRepository.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? SKU_Code { get; set; }
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        [Required]
        [Column(TypeName = "decimal")]
        public decimal Cost_Price { get; set; }
        [Column(TypeName = "decimal")]
        public decimal? MSRP_Price { get; set; }

        [Required]
        public Int64 WarehouseId { get; set; }
        [JsonIgnore]
        public virtual Warehouse? Warehouse { get; set; }
    }


    public class ItemDTO
    {
        public int count { set; get; }
        public List<Item>? Items { set; get; }
    }

}
