using SiparisYonetimi.Entities;
using System.Data.Entity; // Bu kütüphane Entity framework paketinden geliyor 

namespace SiparisYonetimi.Data
{
    public class DatabaseContext : DbContext // Burada Entity frameworkün DbContext sınıfından miras alıyoruz DatabaseContext sınıfında dbcontext leri kullanabilmek için 
    {
        public DatabaseContext()
        {

        }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
/*
    Proje yaparken classları ve database context i kurduktan sonra veritabanını otomatik oluşturmak yerine migration la oluşturmamız gerekir.
    Migration u aktif etmek için Visual Studio da en üst  menüden Tools > Nuget Pack... Maneger > Package manage conole menüsünü aktif ediyoruz.
    Önce Default Project bölümünden database context inizin bulunduğu data katmanını seçiyoruz.
    Sonra aşağıdaki kod alanına Enable-migrations yazıp enter a basarak Initialcreate class ının oluşturulmasını sağlıyoruz.
    Oluşan Configuration sınıfının içerisinde başlangıç verisi oluşturabileceğimiz Seed metodu oluşuyor bunu kullanarak veritabanı oluştuktan sonra örnek data oluşturabiliz 
    Eğer enable-migrations tan sonra initial create classı oluşmazsa P.M.C komut ekranına add-migration InitialCreate yazıp enter a basaak kendimiz oluşturabiliriz
    
 */
