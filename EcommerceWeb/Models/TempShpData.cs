﻿using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EcommerceWeb.Models
{
    public class TempShpData
    {
        public static int UserID { get; set; }
        public static List<OrderDetail> items { get; set; }
    }
}