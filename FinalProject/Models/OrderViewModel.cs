using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class OrderViewModel
    {
        public string code { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không để trống")]
        public string customname { get; set; }
        [Required(ErrorMessage = "Số điện thoại không để trống")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string address { get; set; }
        public Nullable<int> typepayment { get; set; }
        public string description { get; set; }
    }
}