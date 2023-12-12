﻿using Data.Entities;

namespace Data.Models.View
{
    public class FeverousProductViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public ProductImageViewModel ProductImage { get; set; } = null!;
    }
}
