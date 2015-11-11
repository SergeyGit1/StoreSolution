﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreSolution.DatabaseProject.Model
{
    [Table("OrdersHistoryTable")]
    public class OrderHistory
    {
        [Key]
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }
        public string Order { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Culture { get; set; }
    }
}