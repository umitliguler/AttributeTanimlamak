using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTanimlamak
{
    public static class KullaniciKontrol
    {
        public static bool Kontrol(Kullanici kullanici)
        {
            var retVal = true;

            //validasyon

            //Kullanıcı sınıfının tipi üzerinde bulunan tüm property'ler
            var props = kullanici.GetType().GetProperties();

            foreach (var propertyInfo in props)
            {
                if (propertyInfo.GetCustomAttributes(typeof(BosOlamazAttribute), true).Any())
                {
                    //custom attr. uygulamış propery

                    var val = propertyInfo.GetValue(kullanici) as string;
                    if (string.IsNullOrEmpty(val))
                    {
                        retVal = false;
                    }

                }
            }

            return retVal;
        }
    }
}
