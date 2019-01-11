using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileStore.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Модель")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Цена")]
        public int Price { get; set; }
        [Display(Name = "Путь к фото")]
        public string PathToImage { get; set; }
    }
}