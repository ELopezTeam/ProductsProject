using System;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.DTOs
{
	public class CUProductDto
	{
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 1)]
        public int Status { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}

