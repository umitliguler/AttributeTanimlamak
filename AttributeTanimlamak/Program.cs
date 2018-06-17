using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTanimlamak
{
    class Program
    {
        public static void Main(string[] args)
        {
            var kullanici = new Kullanici { Adi = "emre" };

            if (KullaniciKontrol.Kontrol(kullanici))
            {
                Console.WriteLine("Kullanıcı doğru oluşturuldu");
            }
            else
            {
                Console.WriteLine("Kullanıcı yanlış oluşturuldu");
            }

            Console.ReadKey();
        }
    }
}
