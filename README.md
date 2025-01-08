
# VatanSMS.Net VB.NET SDK

VatanSMS API'sini VB.NET projelerinizde kolayca kullanmak için geliştirilmiş bir SDK.

## Kurulum

### NuGet ile Kurulum

Kütüphaneyi NuGet ile yükleyin:

```bash
dotnet add package VatanSoft.VatanSmsVb.Net
```

---

## Kullanım

Aşağıda, VB.NET ile **VatanSMS.Net VB.NET SDK** kullanılarak API isteklerinin nasıl yapıldığını gösteren örnekler yer almaktadır:

---

### 1. 1-to-N SMS Gönderimi

**Açıklama:**

Birden fazla numaraya aynı mesajı göndermek için kullanılır.

**Parametreler:**

- `phones As List(Of String)`: Mesaj gönderilecek telefon numaralarının listesi.
- `message As String`: Gönderilecek mesaj içeriği.
- `sender As String`: Gönderici adı (örneğin, "FIRMA").
- `messageType As String`: Mesaj türü, varsayılan olarak `"normal"`.
- `messageContentType As String`: Mesaj içerik türü, örneğin `"bilgi"` veya `"ticari"`.
- `sendTime As DateTime?`: Mesajın gönderileceği tarih ve saat. Varsayılan olarak hemen gönderilir.

**Örnek Kullanım:**

```vb
Imports VatanSms

Dim client As New VatanSmsClient("API_ID", "API_KEY")
Dim phones As New List(Of String) From {"5xxxxxxxxx", "5xxxxxxxxx"}
Dim response As String = Await client.SendSmsAsync(phones, "Bu bir test mesajıdır.", "SMSBASLIGINIZ")
Console.WriteLine(response)
```

---

### 2. N-to-N SMS Gönderimi

**Açıklama:**

Her telefon numarasına farklı mesajlar göndermek için kullanılır.

**Parametreler:**

- `phones As List(Of Dictionary(Of String, String))`: Telefon numaralarını ve mesajlarını içeren bir liste. Her eleman `{phone: '...', message: '...'}` şeklinde olmalıdır.
- `sender As String`: Gönderici adı.
- `messageType As String`: Mesaj türü, varsayılan olarak `"turkce"`.
- `messageContentType As String`: Mesaj içerik türü, örneğin `"bilgi"` veya `"ticari"`.
- `sendTime As DateTime?`: Mesajın gönderileceği tarih ve saat. Varsayılan olarak hemen gönderilir.

**Örnek Kullanım:**

```vb
Dim phones As New List(Of Dictionary(Of String, String)) From {
    New Dictionary(Of String, String) From {{"phone", "5xxxxxxxxx"}, {"message", "Mesaj 1"}},
    New Dictionary(Of String, String) From {{"phone", "5xxxxxxxxx"}, {"message", "Mesaj 2"}}
}
Dim response As String = Await client.SendNtoNSmsAsync(phones, "SMSBASLIGINIZ")
Console.WriteLine(response)
```

---

### 3. Gönderici Adlarını Sorgulama

**Açıklama:**

Hesabınıza tanımlı gönderici adlarını sorgulamak için kullanılır.

**Parametreler:**

Hiçbir parametre almaz.

**Örnek Kullanım:**

```vb
Dim response As String = Await client.GetSenderNamesAsync()
Console.WriteLine(response)
```

---

### 4. Kullanıcı Bilgilerini Sorgulama

**Açıklama:**

Hesap bilgilerinizi sorgulamak için kullanılır.

**Parametreler:**

Hiçbir parametre almaz.

**Örnek Kullanım:**

```vb
Dim response As String = Await client.GetUserInformationAsync()
Console.WriteLine(response)
```

---

### 5. Rapor Detayı Sorgulama

**Açıklama:**

Belirli bir raporun detaylarını sorgulamak için kullanılır.

**Parametreler:**

- `reportId As Integer`: Sorgulanacak raporun ID'si.
- `page As Integer`: Sayfa numarası, varsayılan olarak `1`.
- `pageSize As Integer`: Bir sayfada gösterilecek rapor sayısı, varsayılan olarak `20`.

**Örnek Kullanım:**

```vb
Dim response As String = Await client.GetReportDetailAsync(123456, 1, 20)
Console.WriteLine(response)
```

---

### 6. Tarih Bazlı Rapor Sorgulama

**Açıklama:**

Belirli bir tarih aralığındaki raporları sorgulamak için kullanılır.

**Parametreler:**

- `startDate As String`: Başlangıç tarihi (örneğin, `"2023-01-01"`).
- `endDate As String`: Bitiş tarihi (örneğin, `"2023-12-31"`).

**Örnek Kullanım:**

```vb
Dim response As String = Await client.GetReportsByDateAsync("2023-01-01", "2023-12-31")
Console.WriteLine(response)
```

---

### 7. Sonuç Sorgusu

**Açıklama:**

Gönderilen bir raporun durumunu sorgulamak için kullanılır.

**Parametreler:**

- `reportId As Integer`: Sorgulanacak raporun ID'si.

**Örnek Kullanım:**

```vb
Dim response As String = Await client.GetReportStatusAsync(123456)
Console.WriteLine(response)
```

---

### 8. İleri Tarihli SMS İptali

**Açıklama:**

Zamanlanmış bir SMS gönderimini iptal etmek için kullanılır.

**Parametreler:**

- `id As Integer`: İptal edilecek SMS'in ID'si.

**Örnek Kullanım:**

```vb
Dim response As String = Await client.CancelFutureSmsAsync(123)
Console.WriteLine(response)
```

---

## Testler

Testleri çalıştırmak için şu adımları izleyin:

1. `VatanSmsClientTests.vb` dosyasını çalıştırın.
2. Tüm testler `RunTests()` fonksiyonu üzerinden çağrılır ve konsolda sonuçlar gösterilir.

---

## Lisans

Bu SDK, MIT lisansı ile lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasına göz atabilirsiniz.
