namespace Eticaret.Models
{
	public class Artist
	{
		public int ArtistId { get; set; }
		public string Name { get; set; }
		public List<Album> Albums { get; set; } //bir sanatçının birden fazla albümü olabilir (Bire çok ilişkini yapmış olduk)
	}
}
