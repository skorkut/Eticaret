using Eticaret.Models;

namespace Eticaret.ViewModels
{
	//view model yapısı view için gerekli olan alanları tutsun diye oluşturduğumuz class
	public class ShoppingCartViewModel
	{
        public List <Cart> CartItems { get; set; }
		public decimal CartTotal { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
