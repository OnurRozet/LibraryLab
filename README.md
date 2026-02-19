# 📚 LibraryLab: Advanced Specification Pattern Implementation

Bu proje, **.NET 9** ve **Entity Framework Core** kullanılarak, iş kurallarını (business rules) veritabanı sorgularından tamamen soyutlayan, yüksek düzeyde ölçeklenebilir ve test edilebilir bir **Specification Pattern** uygulamasıdır. 

Projenin temel amacı, karmaşık sorgu mantıklarını merkezi bir yapıda toplayarak kod tekrarını önlemek ve yönetilebilir bir mimari sunmaktır.

---

## 🚀 Öne Çıkan Özellikler

* **Advanced Specification Pattern:** İş kuralları tekil sınıflarda kapsüllenir (Encapsulation).
* **Composite Logic (And/Or):** Kurallar dinamik olarak birbirine bağlanabilir, böylece karmaşık filtreleme senaryoları kolayca yönetilir.
* **Expression Trees:** Sorgular RAM'e çekilmeden SQL düzeyinde (IQueryable) filtreleme yapılır, böylece yüksek performans sağlanır.
* **Vertical Slice Architecture:** Klasik Controller yapısı yerine özellik bazlı (feature-based) endpoint yönetimi kullanılmıştır.
* **Bogus Integration:** Geliştirme sürecinde sistemi test etmek amacıyla **Bogus** kütüphanesi kullanılarak gerçekçi sahte veriler (Fake Data) üretilmiştir.

---

## 🛠️ Teknik Yığın (Tech Stack)

* **Backend:** .NET 9 (C#)
* **ORM:** Entity Framework Core
* **Data Seeding:** Bogus (Fake Data Generation)
* **Architecture:** Vertical Slice Architecture & Specification Pattern
* **API:** Minimal APIs with IEndpoint Definition

---

## 📐 Mimari Bakış

### Specification Örneği
İş kuralları aşağıdaki gibi temiz, anlaşılır ve tekrar kullanılabilir şekilde tanımlanır:

```csharp
public class BigBooksWithAuthorSpecification : Specification<Book>
{
    public BigBooksWithAuthorSpecification(int minPageCount = 500)
    {
        AddFilterQuering(b => b.PageCount > minPageCount);
        AddIncludeQuery(b => b.Author);
        AddOrderByQuery(b => b.Title);
    }
}
```
## Dinamik Sorgu Birleştirme
Birden fazla kuralı tek bir sorguda birleştirmek oldukça basittir:
```csharp
var combinedSpec = bigBooksSpec.And(turkishLanguageSpec);
var result = await dbContext.ApplySpecification(combinedSpec).ToListAsync();
```