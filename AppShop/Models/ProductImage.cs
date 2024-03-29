﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShop.Models
{
    public class ProductImage : BaseModel
    {
        [ForeignKey("FK_Product_12")]
        public int ProductId { get; set; }
        public byte[] Picture { get; set; }
        //virtual properties
        public Product Product { get; set; }


    }
}
