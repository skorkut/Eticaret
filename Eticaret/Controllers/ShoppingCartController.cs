using Eticaret.Data;
using Eticaret.Models;
using Eticaret.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

namespace Eticaret.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly EticaretContext _context;
        private ShoppingCart shoppingCart;
        public ShoppingCartController(EticaretContext context)
        {
            _context = context;
            shoppingCart = new ShoppingCart();
            shoppingCart._context = context;
        }



        public IActionResult SepetiBosalt(string shoppingcartid)
        {
            HttpContext.Session.Clear();        //logout iken çalıştı
            shoppingCart.EmptyCart(shoppingcartid);
            return RedirectToAction("Index", "Home");
        }


        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            ShoppingCart bus = new ShoppingCart();
            if (HttpContext.Session.GetString("adet") == null || HttpContext.Session.GetString("adet") == "0")
            {
                var viewModel = new ShoppingCartViewModel();
                return View(viewModel);
            }

            else
            {
                //shoppingCart._context = _context;
                var cart = shoppingCart.GetCart(this.HttpContext); //maili ya da Guid bilgisini okuduk
                bus.ShoppingCartId = cart.ShoppingCartId;
                // Set up our ViewModel
                var viewModel = new ShoppingCartViewModel
                {
                    CartItems = cart.GetCartItems(), //sepete eklediğin ürünler
                    CartTotal = cart.GetTotal(),      //toplam tutar.
                    ShoppingCartId = cart.ShoppingCartId
                };


                return View(viewModel);
            }

        }


        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            //var addedAlbum = _context.Albums.Single(album => album.Id == id);

            var addedAlbum = _context.Albums.Where(album => album.Id == id).FirstOrDefault();

            // Add it to the shopping cart
            var cart = shoppingCart.GetCart(this.HttpContext); //alışveriş yapanın mailini ya da guid bilgisini alırız.


            cart.AddToCart(addedAlbum);
            HttpContext.Session.SetString("adet", cart.GetCount().ToString());

            var adett = HttpContext.Session.GetString("adet");
            ViewData["sepetAdedi"] = adett;

            // Go back to the main store page for more shopping
            return RedirectToAction("AlbumlerHepsi", "Home");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = shoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string albumName = _context.Carts.Include(p => p.Album).Single(item => item.RecordId == id).Album.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);
            HttpContext.Session.SetString("adet", cart.GetCount().ToString());

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = HtmlEncoder.Default.Encode(albumName) + " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }


        //
        // GET: /ShoppingCart/CartSummary     
        public IActionResult CartSummary()
        {
            HttpContext.Session.Clear();
            var cart = shoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary", ViewData["CartCount"]);
        }
    }
}
