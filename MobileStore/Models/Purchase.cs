using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        [Required]
        [Display(Name = "Емейл")]
        public string PersonEmail { get; set; }
        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Город")]
        public string City { get; set; }
        public int PhoneId { get; set; }
        public DateTime Date { get; set; }
    }
}