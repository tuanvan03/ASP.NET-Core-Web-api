using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class StockUpdateModel
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(10, ErrorMessage = "CompanyName must be at most 10 characters long")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, 1000000, ErrorMessage = "Purchase must be between 0 and 1000000")]
        public decimal Purchase { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, 1000000, ErrorMessage = "Purchase must be between 0 and 1000000")]
        public decimal LastDividend { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Industry must be at most 10 characters long")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000000, ErrorMessage = "MarketCap must be between 0 and 1000000")]
        public long MarketCap { get; set; }
    }
}