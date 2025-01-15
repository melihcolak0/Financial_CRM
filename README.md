# :gem:C# Eğitim Kampı (#701)
Bu repository, Murat Yücedağ'ın C# Eğitim Kampı isimli eğitiminin #701 modülünü içermektedir.

## :pushpin: Financial CRM Projesi
Eğitimin son modülünde bir proje yaptım. Financial CRM (Müşteri İlişkileri Yönetimi), finans sektöründe faaliyet gösteren işletmeler için müşteri ilişkilerini ve etkileşimlerini yönetmek amacıyla kullanılan özel bir yazılım türüdür. Bu tür CRM sistemleri, finansal kurumlar, bankalar, sigorta şirketleri ve yatırım yönetim şirketleri gibi kuruluşların ihtiyaçlarına göre tasarlanmıştır. Proje için öncelikle veri tabanlarını ve tabloları oluşturdum. Kullanıcılar, Kategoriler, Harcamalar, Faturalar, Bankalar ve Banka İşlemleri tabloları oluşturdum. Veri tabanını koduma ekleyebilmek için bir ADO.NET Entity Data Model oluşturdum. Bu sayede oluşturduğum tabloları ve verileri kullanabileceğim.

### :arrow_forward: Login ve Register Form
Projemde birden fazla kullanıcının aynı anda kullanmasını sağlamak için oluşturmuş olduğum Tbl_Users tablosuna birkaç kişi ekledim. Bu kişiler kullanıcı adlarını ve şifrelerini LoginForm'a girerek sisteme giriş yapabilir durumdalar. Entity Framework ile oluşturmuş olduğum model üzerinde kullanıcı adını ve şifreyi tablo içerisinde sorgulayarak kullanıcı kontrolü yaptım. Eğer şifre veya kullanıcı adı yanlış girilirse bir hata mesajı gösterilecektir. Bir kullanıcı daha önce kayıt olmadıysa "Kayıt Ol" linkLabel aracına tıklayarak RegisterForm'a gidebilir. Register formda kullanıcı, Kullanıcı Adı, Adı ve Soyadı, E-Mail, Şifre ve Şifre Tekrar textBox'larını doldurur ve "Kayıt Ol" butonuna basarsa Tbl_Users tablosuna kullanıcı kaydedilir. Sonrasında uygulamayı yeniden başlatıp LoginForm üzerinden uygulamaya giriş yapabilir. Her kullanıcının kendi banka hesapları ve hesap hareketleri bulunmaktadır.

![image_alt](https://github.com/melihcolak0/Financial_CRM/blob/d156ddbbeb83ab9cea4b83310505ee4e83444af7/Financial%20CRM%20SS/loginForm.jpg)
![image_alt](https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/eec02ad7af3ba54a6cfd832171bc4298952b5281/Financial%20CRM%20SS/loginForm2.jpg)
![image_alt](https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/15b5ac6ded01bdee32610132dc1b1edcd97b2632/Financial%20CRM%20SS/registerForm.jpg)

### :arrow_forward: Dashboard Form
Bir kullanıcı uygulamaya giriş yaptığında ilk karşılaşacağı form DashboardForm'dur. Dashboard aslında ana formumuzdur. Diğer formlardaki Geri butonu ile tekrar Dashboard formuna dönebiliriz. Dashboard da dahil tüm formlarda kullanıcı güncel tarih ve saati görüntüleyebilmektedir. Saat için SharedTime isminde bir sınıf oluşturup bu oluşturduğum sınıftaki Timer'ın Tick olayı ile tüm formlardaki saati kontrol edebiliyorum. Dashboard formunu inceleyecek olursak, formda kullanıcı, kendi adını ve soyadını görebilir. Bu her formda olan bir özelliktir. Devamında bankalardaki toplam bakiyesini görebilir. Tbl_Bills tablosundaki faturalarının isimlerini ve miktarlarını 1 saniyelik geçişlerle görebilir. Tbl_BankProcesses tablosundaki banka hesap hareketlerinden son hareketin detaylarını da görebilir. Formun alt kısmındaki grafiklerde ise kullanıcı hangi bankada ne kadar bakiyesinin bulunduğunu görebilir. Bir diğer grafikte de faturaların tutarlarını görebilir. Kullanıcı form değiştirme esnasında zaten o an bulunduğu formun butonuna basacak olursa bir uyarı mesajı ile karşılaşır.

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/26c71d119189b689a83beb92c7d3d8c0fb2755f1/Financial%20CRM%20SS/dashboardForm.jpg" alt="image alt">
</div>

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/8571b7920f4a520abe8f49eae8022151b1d372da/Financial%20CRM%20SS/warningForm.jpg" alt="image alt">
</div>

### :arrow_forward: Kategoriler Formu
Kategoriler formunu inceleyecek olursak kullanıcı, bu formda Tbl_Categories tablosundaki kendi kategorilerini düzenleyebilir. Kategoriler, Tbl_Spendings tablosunda sadece Id'si ile birlikte tutulur. Bu şekilde giderleri kategorilerine ayırabiliriz. Formun üst kısmında kategorileri Listeleme, Ekleme, Silme ve Güncelleme için butonlar ve textBox araçları bulunuyor. Bu şekilde veri tabanı üzerinde değişikliler yapabiliriz. Formun alt kısmında ise Tbl_Categories tablosunu liste şeklinde görüntüleyebiliriz. 

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/2d583759f3e72d53d3ddafff0feee7a8fb994af1/Financial%20CRM%20SS/categoryForm.jpg" alt="image alt">
</div>

### :arrow_forward: Bankalar Formu
Bankalar formunu inceleyecek olursak kullanıcı, bu formda her bankadaki bakiyesini ayrı ayrı görebilir. Devamında ise formun altında banka farketmeksizin Tbl_BankProcesses tablosundaki hesap hareketlerinden son beşini görebilir. 

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/d13acffef5e0ce48025ba4890fbce3b6ba92f887/Financial%20CRM%20SS/banksForm.jpg" alt="image alt">
</div>

### :arrow_forward: Faturalar Formu
Faturalar formunu inceleyecek olursak kullanıcı, bu formda Tbl_Bills tablosundaki faturaları ile ilgili işlemler yapabilir. Formun üst kısmında faturaları Listeleme, Ekleme, Silme ve Güncelleme için butonlar ve textBox araçları bulunuyor. Bu şekilde veri tabanı üzerinde değişikliler yapabiliriz. Formun alt kısmında ise Tbl_Bills tablosunu liste şeklinde görüntüleyebiliriz. Formun tam ortasında kalan kısımda ise faturaların toplam miktarı kullanıcı tarafından görülebilir.

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/a68d2da54f254fc79d06f754c2211e54dff70e9a/Financial%20CRM%20SS/billsForm.jpg" alt="image alt">
</div>

### :arrow_forward: Giderler Formu
Giderler formunu inceleyecek olursak kullanıcı, bu formda Tbl_Spendings tablosundaki giderler ile ilgili işlemler yapabilir. Formun üst kısmında giderleri Listeleme, Ekleme, Silme ve Güncelleme için butonlar ve textBox araçları bulunuyor. Bu şekilde veri tabanı üzerinde değişikliler yapabiliriz. Formun alt kısmında ise Tbl_Spendings tablosunu liste şeklinde görüntüleyebiliriz. Formun tam ortasında kalan kısımda ise giderlerin toplam miktarı kullanıcı tarafından görülebilir.

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/e3a2ca055c2a36334dd30de338afe5ebca65d3bf/Financial%20CRM%20SS/spendingsForm.jpg" alt="image alt">
</div>

### :arrow_forward: Hesap Hareketleri Formu
Hesap hareketleri formunu inceleyecek olursak kullanıcı, bu formda Tbl_BankProcesses tablosundaki hareketler ile ilgili işlemler yapabilir. Formun üst kısmında hareketleri Listeleme, Ekleme, Silme ve Güncelleme için butonlar ve textBox araçları bulunuyor. Bu şekilde veri tabanı üzerinde değişikliler yapabiliriz. Formun alt kısmında ise Tbl_BankProcesses tablosunu liste şeklinde görüntüleyebiliriz. Formun tam ortasında kalan kısımda ise hareketlerin toplam miktarı kullanıcı tarafından görülebilir.

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/106e2f00e30d0e53f7649317b6f3611415900a12/Financial%20CRM%20SS/bankProcessesForm.jpg" alt="image alt">
</div>

### :arrow_forward: Ayarlar Formu
Ayarlar formunu inceleyecek olursak kullanıcı, bu formda sisteme kaydetmiş olduğu kullanıcı adını, kendi adı ve soyadını, şifresini ve mail adresini görebilir. Şifre değişikliği bölümünden şifresini güncelleyebilir. Şifre değişikliği sonrası uygulamayı yeniden başlatması gerekmektedir. Bunun için de formun sağ alt köşesinde "Yeniden Başlat" butonu kullanılmıştır.

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/1da6aad46d9abfd9943bd48afd8cb7a138720c23/Financial%20CRM%20SS/settingsForm.jpg" alt="image alt">
</div>

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/f79381d585f4d0710ecf840dc7d0022db9d71178/Financial%20CRM%20SS/settingsForm2.jpg" alt="image alt">
</div>

<div align="left">
  <img src="https://github.com/melihcolak0/CSharpEgitimKampiMY701/blob/1efae1a77bd24ec4fd40b69d5cd7d03fd01a37eb/Financial%20CRM%20SS/settingsForm3.jpg" alt="image alt">
</div>

Bu şekilde projeyi sonlandırmış oldum. Bu eğitimde bana yol gösteren Murat Yücedağ'a çok teşekkür ederim.
