# AttributeTanimlamak

Öznitelikler (Attributes)
Eyl 06, 2013
Conditional Attribute (Koşullu Öznitelik)

Karar baklavaları olarak tanımlanan, program içerisinde kullanılan "if" bloklarına benzer. Yazılan programlarda tanımlanan metod ya da sınıflardan önce[Conditional(değişken)] (Koşullu) etiketi ile belirlenir. Değişken olarak kullanılan veri, program çalıştırılırken tanımlanırsa o bölüm ile ilgili olan kısım kod içerisine yerleştirilir. Diğer durumda bu bölüm, kod içerisinde bulunmaz. Böylece kod yazılmış olsa da bu blok ile ilgili kısım çalışma sırasında görünmeyecektir. Koşullu Özniteliği uygulamanın diğer bir yolu ise "#if ", "#endif" blokları kullanmaktır. Bununla ilgili örnek basit programlar incelenmek üzere aşağıda yer alacaktır.
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace AttributeUygulamasi
{
    class TestClass
    {
        public TestClass() { }
        public void MethodA()
        {
            Console.WriteLine("A metodu cagirildi !!!");
        }
       
        [Conditional("TEST")]
        public void MethodTest()
        {
            Console.WriteLine("Test metodu cagirildi !!!");
        }
        public void MethodB()
        {
            Console.WriteLine("B metodu cagirildi !!!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TestClass deneme = new TestClass();
            deneme.MethodA();
            deneme.MethodB();
            //#if(TEST)
            deneme.MethodTest();
            //#endif
            Console.ReadLine();
        }
    }
}
```

**TestClass metodu için yazılan MethodTest metodunun koda eklenmesi TEST' in tanımlanmış olması koşuluna bağlıdır. Eğer TEST tanımlanır ise "deneme.MethodTest()" kodu çalışarak diğer koşullar atlanacaktır.

"TEST" tanımlanmadığında programın yarattığı çıktı aşağıdadır: 



"TEST" tanımlandığında programın yarattığı çıktı aşağıdadır: ** 



Obsolute Attribute (Saf Öznitelik)

Yazılan programla içerisinde yer alan metodların zaman içerisinde geliştirilmesi ya da terk edilmesi mümkündür. Terk edilen modüllerin daha sonra başka programlar içerisinde kullanılma ihtimali ya da bu program içerisinde geçmişe dönülme ihtimali düşünülerek bu metodlar obsolute(saf) olarak tanımlanabilir. "Obsolute" olarak tanımlanan metodların kullanılması derlenme sırasında bir hata vermemesine karşın, çıktı ekranı incelendiğinde konuyla ilgili bir uyarı yer aldığı görülecektir. [Obsolute()] biçiminde tanımlanan özellik daha açıklayıcı olması açısından [Obsolute('Uyarı Mesajı')] biçiminde tanımlanmalıdır.
```C#
using System;
namespace SystemAttribute
{
       class DiziToplami
    {
        [Obsolete("Bu metod artık kullanılmıyor DiziToplamiFormula()
                   metodunu kullan!!!")]
        public static int DiziToplamiBruteForce(int N)
        {
            int sum = 0;
            for (int i = 1; i <= N; i++)
            {
                sum += i;
            }
            return sum;
        }
        public static int DiziToplamiFormula(int N)
        {
            int sum = 0;
            sum = (N * (N + 1)) / 2;
            return sum;
        }
        static void Main(string[] args)
        {
            int N2sum = 1;
            try
            {
                N2sum = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.ToString());
            }
            Console.WriteLine("" + DiziToplamiBruteForce(N2sum));
            Console.WriteLine("" + DiziToplamiFormula(N2sum));
            Console.ReadLine();
       }
    }
} 
```

**Bu program içerisinde 1'den N'e kadar olan sayıların toplamı iki farklı metodla hesaplanacak bu iki metoddan bir tanesi obsolute olarak tanımlanacaktır.**

**"Obsolete" olarak tanımlanan metod kullanılmaya çalışıldığında çıkan uyarı aşağıda belirtilmiştir:**


 

Kullanıcı Tanımlı Öznitelikler
Kullanıcı tanımlı öznitelikler, kullanıcının C# elemanlarının metadatasına ek özellikler eklemesini sağlar. Örneğin bir iletişim(COM) bağlantısı gerektiren tüm sınıflar için bir iletişim özelliği yaratılıp eklenebilir. Bu sınıflar Attribute sınıfından türetilmelidir. Türetilen sınıfın etki alanı belirlenmelidir. Örneğin sınıfın etki alanına Method yazılırsa, yaratılan öznitelik yalnızca fonksiyonlar için kullanılabilir. Bütün uygulama elemanlarına etki edecek bir öznitelik yazılıyor ise etki alanıAll(Hepsi) olarak belirtilmelidir. Etki alanı tanımlandıktan sonra sınıfın özellikleri ve fonksiyonları tanımlanır. Aşağıdaki uygulamada "Validation Key"(Onaylama Anahtarı) ile tanımlanmış metodlardan yola çıkılarak, anahtarı 23 olanlarda durumu başarılı yapan bir özellik tanımlanmıştır.
```C#
using System;
using System.Reflection;
using System.Diagnostics;
namespace CustomAttribute

{
    [AttributeUsage(AttributeTargets.Method)]
    class MyAttribute:Attribute
    {
        int Validator;
        bool status;
        public MyAttribute() {
            this.Validator = 23;
            status = true;
        }
        public MyAttribute(int val)
        {
            this.Validator = val;
            if (Validator == 23)
                status = true;
            else
                status = false;
        }
        public int ValKey
        {
            get
            {
                return Validator;
            }
        }
        public bool rtn_status
        {
            get
            {
                return status;
            }
        }
    }
    public class Test
    {
        [MyAttribute(23)]
        public void MyMethod()
        {
            Console.WriteLine("My method is invoked!!!");
        }
        public static void Main()
        {
            MyAttribute MyAttr;
            Type t = typeof(Test);
            foreach (MethodInfo method in t.GetMethods())
            {
                foreach (Attribute attr 
                         in method.GetCustomAttributes(true))
                {
                    MyAttr = attr as MyAttribute;
                    if (null != MyAttr)
                    {
                        Console.WriteLine(method.Name + " "
                                          + MyAttr.ValKey);
                        if (MyAttr.rtn_status)
                            Console.WriteLine("Validation
                                               Completed");
                        else
                            Console.WriteLine("Validation 
                                               Failed");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
```
 

*"MyMethod" tanımlanırken 23 ile tanımlandığında program çalışınca gelen ekran çıktısı aşağıdaki gibidir:*
**23**


*"MyMethod" tanımlanırken 23 dışında bir sayı girildiğinde gelecek olan ekran çıktısı aşağıdaki gibidir:*
**38**
