﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public byte Gender { get; set; }
        public string? Pictures { get; set; }
    }
}
