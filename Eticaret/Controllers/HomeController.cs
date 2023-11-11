using Eticaret.Data;
using Eticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Eticaret.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly EticaretContext eticaretContext;

		public HomeController(ILogger<HomeController> logger, EticaretContext context)
		{
			_logger = logger;
			eticaretContext = context;
		}

		public IActionResult Index()
		{
			//Sessions yapısı: Oturum anlamına gelir,veriyi server tarafında tutmaya yarar.
			//SessionsTimeout ile serverda tutulacak data süresini ayarlarız.
			//Facebookda Sessions süresi sınırsız, logout olmadıkça düşmüyor.
			//Controllerdan başka bir controllera veri taşımak için Sessions yapısı kullanılır.
			//Banka uygulamaları, bilet satış uygulamaları timeout süresi oluyor.

			//Session içerisine değer atamak için
			//HttpContext.Session.SetInt32("sepetAdedi", 3);

			SampleData sampleData = new SampleData(eticaretContext);
			sampleData.AddGenres();
			sampleData.AddArtist();
			sampleData.AddAlbums();
			return View();
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		//takım sayfasını getirip gösterecek metotu yazalım.
		public IActionResult Team()
		{
			//1. yol
			//int? adett = HttpContext.Session.GetInt32("sepetAdedi"); //int ile int?(boş geçilebilir.)
			//ViewData["sepet"] = adett; //View tarafında sepet verisini görebilmek için ekledik

			//2. yol
			//ViewData["sepet"] = HttpContext.Session.GetInt32("sepetAdedi");


			return View();
		}
		//bize ulaşın sayfasını getirip gösterecek metot
		public IActionResult ContactUs()
		{
			return View();
		}

		//bize katılın sayfasını getirip göstericek
		public IActionResult JoinUs()
		{
			return View();
		}

		// joinus sayfasındaki viewında butona tıklanınca form içerisindeki veriler asp-actionda belirtiğimiz yere yani buraya post yöntemi ile gönderilir, burada o veriler veritabanındaki tabloya eklenir.
		[HttpPost]
		public IActionResult UserSave(string name, string surname, string email, string phone, string message)
		{
			ApplyUser user = new ApplyUser() { Name = name, Surname = surname, Email = email, Telephone = phone, Description = message };
			eticaretContext.ApplyUsers.Add(user);
			eticaretContext.SaveChanges();
			return View();
		}

		//müzik kategorilerini getirecek sayfayı oluşturalım.
		public IActionResult Kategoriler()
		{
			List<Genre> turler = eticaretContext.Genres.ToList();
			return View(turler);
		}

		//sayfayı açmaya yarayan metot
		public IActionResult KategoriEkle()
		{

			return View();
		}

		//butona tıkladığında form üzerindeki verileri backende göndermeye ve kaydetmeye yarayan metot
		[HttpPost]
		public IActionResult KategoriEkle(Genre genre)
		{
			string duzenlenenVeri = MetniDuzenle(genre.Name);
			if (!KategoriVarmi(duzenlenenVeri))
			{
				genre.Name = duzenlenenVeri;
				eticaretContext.Genres.Add(genre);
				eticaretContext.SaveChanges();
				return View();
			}
			else
			{
				return View("Error");
			}
		}

		//girilen tür adı eğer tabloda var ise bu durumda o kaydı ekletmemek gerekir.

		public bool KategoriVarmi(string kategoriAdi)
		{
			Genre dbdenGelen = eticaretContext.Genres.Where(satir => satir.Name == kategoriAdi).FirstOrDefault();
			if (dbdenGelen != null)
				return true;
			else
				return false; ;
		}

		//girilmiş olan bilgiyi ilk harfi büyük diğerleri küçük olacak şekilde düzenliyor
		//rock...Rock
		//POP....Pop
		public string MetniDuzenle(string tur)
		{
			string result = "";
			result += tur.Substring(0, 1).ToUpper();
			for (int i = 1; i < tur.Length; i++)
			{
				result += tur[i].ToString().ToLower();
			}
			return result;
		}

		//Kategori eklede edit sayfasını açacak metot

		public IActionResult Edit(int id)
		{
			Genre editYapılacakTur = eticaretContext.Genres.Where(satir => satir.Id == id).FirstOrDefault();
			return View(editYapılacakTur);
		}

		//HTTPPOST yani güncelle butonuna tıklayınca backende gidip güncelleme işlemini yapıcak.
		[HttpPost]
		public IActionResult Edit(Genre genre)
		{
			eticaretContext.Genres.Update(genre);
			eticaretContext.SaveChanges();
			return View("Kategoriler",eticaretContext.Genres.ToList());
		}

		public IActionResult Details(int id)
		{
			Genre genre= eticaretContext.Genres.Where(satir=>satir.Id==id).FirstOrDefault();
			return View(genre);
		}

		public IActionResult Delete(int id)
		{
			Genre genre = eticaretContext.Genres.Where(satir => satir.Id == id).FirstOrDefault();
			return View(genre);
		}

		[HttpPost]
		public IActionResult DeleteConfirmed(int id)
		{
			Genre genre = eticaretContext.Genres.Where(satir => satir.Id == id).FirstOrDefault();
			eticaretContext.Remove(genre);
			eticaretContext.SaveChanges();
			return View("Kategoriler", eticaretContext.Genres.ToList());
		}

		public IActionResult Sanatcilar()
		{
			List<Artist> sanatcilar = eticaretContext.Artists.ToList();
			return View(sanatcilar);
		}

		public IActionResult Detay(int id)
		{

			return View(eticaretContext.Artists.Where(satir => satir.ArtistId == id).FirstOrDefault());
        }

		//include tanıştırmak için kullanılır
		public IActionResult Albumler()
		{
			List<Album> albumler = eticaretContext.Albums.Include(satir=>satir.Genre).Include(satir=>satir.Artist).ToList();
			return View(albumler);
		}

        public IActionResult AlbumlerHepsi()
        {
            List<Album> albumler = eticaretContext.Albums.Include(satir => satir.Genre).Include(satir => satir.Artist).ToList();
            ViewData["CartCount"] = HttpContext.Session.GetString("adet");
            return View(albumler);
        }

    }
}