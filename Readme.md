## VakıfBank - Patika Bootcamp Final Work

Proje bir e-ticaret sitesi örneği gibidir. Senaryo ise toptancı ve parekendeci aktörlerini içermektedir. Toptancılar ürünlerini sisteme kaydedip parekendeciler tarafından satın almasını beklemektedir.

Parekendeciler;
- Sisteme üye olabilir veya toptancı tarafından sisteme kar payı ve açık hesap limiti belirlenerek eklenir.
- Sipariş oluşturur. Ödeme yapar.
- Yaptığı ödemelere ait faturalarını görüntüleyebilir.

Toptancılar;
- Sisteme parekendeci ekleyebilir. Açık hesap limiti ve kar paylarını düzenleyebilir.
- Parekendecilerin onlara verdiği siparişleri onaylar veya reddeder.
- Kendi satışlarına dair rapor oluşturabilir.
- Ürünlerini ekleyebilir, silebilir, güncelleyebilir.

#### Architecture - Design Patern
Clean Architecture ile inşa edilen bu proje veri tabanı modellerinin kullanımı için Repository Design Pattern'i UnitOfWork Design Pattern ile birlikte kullanılmıştır. Karşılanan request'lerin ilgili business logiclere yönlendirilimesi içinde Mediator Design Pattern'i kullanmıştır.

#### Kurulum
- Projeyi indirmek:
```Git I'm A tab
git clone https://github.com/omerbilalusta/VB-Bootcamp.git
```

- Veritabanını kurduktan sonra tabloları eklemek:
```Console I'm A tab
.\VB-Bootcamp\API> dotnet ef database update --project  "./Vb-Data" --startup-project "./Vb-Api"
```
- Projeyi çalıştırmak(Backend):
```Console I'm A tab
.\VB-Bootcamp\API\Vb-Api> dotnet run
```
- "dotnet restore" komutu ile paketlerin kurulumu garanti edilmelidir.
- 5082 portuyla http://localhost:5082 üzerinden API çalışmaktadır.
<br>
- Node.js kurulumu yapılmalı.
- Projeyi çalıştırmak(Frontend):
```Console I'm A tab
.\VB-Bootcamp\Angular\Presentation> npm install
.\VB-Bootcamp\Angular\Presentation> ng serve
```
- Varsayılan olarak 4200 portuyla http://localhost:4200 üzerinden Frontend çalışmaktadır.

#### Framework/Packages:
- Angular : 16
- CoreUIAngular Dashboard Admin Template
- Node.js : 18.18.2
- .Net : 7.0
- AutoMapper: 12.0.1
- AutoMapper.Extensions.Microsoft.DependencyInjection: 12.0.1
- Microsoft.AspNetCore.Authentication.JwtBearer: 6.0.5
- Microsoft.EntityFrameworkCore : 7.0.10
- Microsoft.EntityFrameworkCore.Relational: 7.0.11
- Microsoft.EntityFrameworkCore.SqlServer: 7.0.11
- Microsoft.EntityFrameworkCore.Tools: 7.0.11
- Microsoft.EntityFrameworkCore.Design : 7.0.10
- Microsoft.AspNetCore.OpenApi : 7.0.10
- Microsoft.AspNetCore.Cors : 2.2.0
- MediatR : 9.0.0
- MediatR.Extensions.Microsoft.DependencyInjection : 9.0.0
- FluentValidation.AspNetCore : 9.5.2
- FluentValidation.DependencyInjectionExtensions : 9.5.2
- Swashbuckle.AspNetCore : 6.5.0
- Newtonsoft.Json : 13.0.3
- Dapper : 2.1.15



#### Dosya yapısı (Backend)
- Vb-Api => MiddleWare, Controller'lar ve Program.cs'yi içermektedir. Proje buradan ayağa kaldırılır.
- Vb-Base => Base nesneleri; parolaların şifrelenmesi, domainler için base model, yanıtlar için response, token için ayrıca bir response içermektedir. 
- Vb-Data => Repository ve unitOfWork design pattern ile birlikte yazılan bu kısım projenin veri tabanı sorguları ve entity'leri içermektedir.
- Vb-DTO => DTO'ları içeren kısımdır.
- Vb-Operations => MediatR design pattern ile birlikte yazılan bu kısım business logic'leri, validation ve mapping kurallarını içermektedir.

![](https://github.com/omerbilalusta/VB-Bootcamp/blob/main/images/backend_folder_structure.png?raw=true)

#### Proje yapısı
- API'ın aldığı istekleri MediatR aracılığıyla işler.
- MediatR istekleri ilgili command ve query'lere iletir.
- Query'ler ve Command'lar UnitOfWork ile birlikte Repository pattern ile oluşturulan ilgili repository'ler aracılığıyla veri tabanına gerekli istekleri yapar.



#### Projeye ait veri tabanı şeması
- Code First yöntemiyle entity'ler ve onlara bağlı kurallar ile birlikte veri tabanı oluşturuldu.
- Entity'ler arası ilişkiler aşağıda görülebilir.
![](https://github.com/omerbilalusta/VB-Bootcamp/blob/main/images/database_structure.png?raw=true)


#### Frontend
![](https://github.com/omerbilalusta/VB-Bootcamp/blob/main/images/productList.png?raw=true)
![](https://github.com/omerbilalusta/VB-Bootcamp/blob/main/images/companyOrderList.png?raw=true)


