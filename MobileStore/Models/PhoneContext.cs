using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MobileStore.Models
{
    public class PhoneContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }

    public class PhoneDbInitializer : DropCreateDatabaseAlways<PhoneContext>
    {
        protected override void Seed(PhoneContext db)
        {
            db.Phones.Add(new Phone { Name = "Samsung Note 9", Price = 45000,
                PathToImage = "/Content/PhoneImage/SamsungNote9.jpg"
            });
            db.Phones.Add(new Phone { Name = "Samsung Galaxy 9", Price = 40000,
            PathToImage = "/Content/PhoneImage/SamsungGalaxy9.jpeg"
            });
            db.Phones.Add(new Phone { Name = "IPhone X", Price = 125000,
            PathToImage = "/Content/PhoneImage/IPhoneX.jpg"
            });
            db.Phones.Add(new Phone { Name = "IPhone 9", Price = 55000,
            PathToImage = "/Content/PhoneImage/IPhone9.jpg"
            });
            db.Phones.Add(new Phone { Name = "IPhone 10", Price = 135000,
            PathToImage = "/Content/PhoneImage/IPhoneX.jpg"
            });
            base.Seed(db);
        }
    }
}