using Microsoft.AspNetCore.Mvc;
using BitirmeProjesiLiman.Core.DTOs;

namespace BitirmeProjesiLiman.Dapper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        [HttpPost("chat")]
        public IActionResult Chat([FromBody] ChatRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { answer = "Lütfen geçerli bir mesaj gönderin." });
            }

            string userMsg = request.Message.ToLowerInvariant();
            string answer;

            if (userMsg.Contains("doluluk") || userMsg.Contains("kapasite") || userMsg.Contains("saha"))
            {
                answer = "**Saha Doluluk Analizi:**\n\nŞu an liman depolama sahası doluluk oranımız yaklaşık **%45** seviyesindedir. En yoğun alan **A Blok** olup, Reefer (soğutmalı) konteyner kapasitesi %80 doluluğa ulaşmıştır. B Blok ve C Blok yük kabulüne uygundur.";
            }
            else if (userMsg.Contains("gemi") || userMsg.Contains("rıhtım") || userMsg.Contains("vessel"))
            {
                answer = "**Rıhtım ve Gemi Durumu:**\n\n* **Rıhtım 1 (Berth 1):** *MSC Isabella* gemisi yanaşık durumda (Yükleme İlerlemesi: %65). STS vinci aktif çalışıyor.\n* **Rıhtım 2 (Berth 2):** *Maersk Mc-Kinney* yanaşma izni bekliyor.\n* **Yolda Olanlar:** *CMA CGM Antoine* gemisinin yarın sabah liman sahasına girmesi planlanmaktadır.";
            }
            else if (userMsg.Contains("hava") || userMsg.Contains("rüzgar") || userMsg.Contains("fırtına"))
            {
                answer = "**Meteoroloji & İş Güvenliği:**\n\nGüncel rüzgar hızı **14 Knot** seviyesindedir. Hava şartları operasyon için tamamen elverişlidir. Rüzgar hızı 30 Knot limitini aşarsa vinç operasyonları otomatik olarak askıya alınacaktır.";
            }
            else if (userMsg.Contains("tır") || userMsg.Contains("kapı") || userMsg.Contains("randevu") || userMsg.Contains("gate"))
            {
                answer = "**Tır Randevu & Kapı Süreçleri:**\n\nSistemde onaylı **2 aktif tır randevusu** bulunmaktadır. Tır girişlerinde plaka tanıma sistemi aktiftir. Lütfen şoförlerin gümrük onay belgelerini yanlarında bulundurduğundan emin olun.";
            }
            else if (userMsg.Contains("gümrük") || userMsg.Contains("muayene"))
            {
                answer = "**Gümrük Denetleme Raporu:**\n\nBugün kontrol edilen 3 konteynerden 1 tanesi (*MSKU9081234*) başarıyla gümrükten geçti. *HLXU7890011* numaralı tehlikeli madde sınıfındaki konteyner ise etiket eksikliği sebebiyle **Reddedildi** olarak işaretlenmiştir.";
            }
            else if (userMsg.Contains("merhaba") || userMsg.Contains("selam"))
            {
                answer = "Merhaba! Ben **PortMaster Yapay Zeka Asistanı**. Liman doluluk oranları, gemi yanaşma planları, tır kapı hareketleri veya iş emirleri hakkında size bilgi verebilirim. Nasıl yardımcı olabilirim?";
            }
            else
            {
                answer = "**PortMaster Destek:**\n\nSorunuzu tam olarak anlayamadım. Liman doluluk durumu için *'doluluk'*, rıhtım bilgileri için *'gemi'*, hava durumu için *'fırtına'*, tır geçişleri için *'randevu'* kelimelerini içeren sorular sorabilirsiniz.";
            }

            return Ok(new { answer = answer });
        }
    }
}
