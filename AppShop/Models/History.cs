﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShop.Models
{
    public class History : BaseModel
    {
        [ForeignKey("FK_Product_1234")]
        public int ProductId { get; set; }
        [ForeignKey("FK_User_1234")]
        public int UserId { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

    }
}
