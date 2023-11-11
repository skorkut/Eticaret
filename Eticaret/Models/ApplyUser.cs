using System.ComponentModel.DataAnnotations;

namespace Eticaret.Models
{
	/// <summary>
	/// Başvuru ekranındaki bilgileri almak ve db tarafına eklemek için kullanılır.
	/// </summary>
	public class ApplyUser
	{
		public int Id { get; set; }
		
		[StringLength(30)]
		[Required]
		public string Name {  get; set; }
		
		[StringLength(30)]
		[Required]
		public string Surname { get; set; }
		
		[StringLength(50)]
		[Required]
		public string Email { get; set; }
		
		[StringLength(11)]
		[Required]
		public string Telephone { get; set; }
		
		[StringLength(300)]
		[Required]
		public string Description { get; set; }

	}
}
