using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EcommerceBookStore.Web.ViewModel;
using EcommerceBookStore.Model;
using EcommerceBookStore.Server;
using EcommerceBookStore.Data;

using Newtonsoft.Json;
using System.Data.Entity;

namespace EcommerceBookStore.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {

        private CartService _cartService = new CartService();
        private EBookStoreContext _db = new EBookStoreContext();
        // GET: ShoppingCarts
        public ActionResult Index(List<getAllCartItems> cartItems)
        {
  
            List<CartItem> AllCartItems = new List<CartItem>();
            if (User.Identity.IsAuthenticated)
            {
                string UserId = User.Identity.GetUserId();
                Cart cartId = _db.Carts.Where(u => u.BookStoreUserId == UserId).FirstOrDefault();
                Response.Cookies["Cart"].Value = "{}";

               
                if (Request.Cookies["Cart"].Value != "{}" )
                {
                     bool Result = CookieItemsByDbItems(cartId.Id);
                    if (Result)
                    {
                        AllCartItems = _db.CartItems.Include(model => model.proudct).Where(c => c.CartId == cartId.Id).ToList();
                    } 

                    
                }
                else 
                {

                    AllCartItems = _db.CartItems.Include(p => p.proudct).Where(c => c.CartId == cartId.Id).ToList();


                }
            }
            else
            {
                var CartItems = GetAllCartCookie();
                CartItem cartItem = new CartItem();
                if (CartItems != null)
                {
                    foreach (var item in CartItems)
                    {
                        cartItem.proudct = _db.proudcts.Find(item.ProudctId);
                        cartItem.proudct.discount = _db.discounts.Find(cartItem.proudct.DiscountId);

                        AllCartItems.Add(new CartItem()
                        {
                            ProudctId = item.ProudctId,
                            quantity = item.quantity,
                            proudct = _db.proudcts.Find(item.ProudctId),

                        });
                    }
                }
                else
                {
                    AllCartItems = null;
                }
               
                
                
            }
       


           

            return View(AllCartItems);
        }

       


        [HttpPost]
        public JsonResult AddToCart(CartItem cartItem)
        {
            JsonResult json = new JsonResult();
            string UserId = User.Identity.GetUserId();
           
            if(cartItem.quantity == 0)
            {
                cartItem.quantity += 1;
            }

            bool result = false;
            if(User.Identity.IsAuthenticated)
            {

                result = AddToDbCart(cartItem);
            }
            else
            {
                result = addToCookieCart(cartItem);

            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }


           //(會員)如果沒有購物車的話就要新增一個新的購物車放入購物的項目,如果有購物車就保持
           //(非會員)使用Cookies來記住購物車項目。


            return json;
        }


        [HttpPost]
        public JsonResult UpdateCart(CartItem cartItem,int Id)
        {
            JsonResult json = new JsonResult();
            string UserId = User.Identity.GetUserId();
            bool result = false;
            
            result = UpdateDbByCart(cartItem, Id);


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }
            return json;

        }



//Cookie
        [HttpPost]
        public JsonResult UpdateCookieCart(List<ShoppingCartViewModel> CookiesCartItem)
        {
            bool Result = false;
            JsonResult json = new JsonResult();


            Result = SaveCookiesItems(CookiesCartItem);


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }

            return json;
        }

        
        [HttpPost]
        public JsonResult DeleteCookieCart(List<ShoppingCartViewModel> CookCartItem)
        {
            bool Result = false;
            JsonResult json = new JsonResult();

            Result = SaveCookiesItems(CookCartItem);


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }

            return json;
        }

        private bool AddToDbCart(CartItem cartItem)
        {

            string UserId = User.Identity.GetUserId();
            bool Result = false;
            Cart UserCart = _db.Carts.Where(u => u.BookStoreUserId == UserId).FirstOrDefault();
            if (UserCart != null)
            {
                cartItem.CartId = UserCart.Id;
                if(cartItem.quantity == null)
                {
                    cartItem.quantity++;
                }
                
                _db.CartItems.Add(cartItem);
                Result = _db.SaveChanges() > 0;

            }
            else
            {
                Cart SaveUserCart = new Cart();
                SaveUserCart.BookStoreUserId = UserId;
                _db.Carts.Add(SaveUserCart);
                _db.SaveChanges();
                AddToDbCart(cartItem);
            }

            return Result;
        }


        private bool addToCookieCart(CartItem cartItem)
        {
            CookieCartViewModel CookiesCart = getCartFormCookie();
           
            bool Result = false;
            if (cartItem != null)
            {
                Proudct FindProudct = _db.proudcts.Find(cartItem.ProudctId);
                if (FindProudct != null)
                {
                    ShoppingCartViewModel cookieCartViewModel = new ShoppingCartViewModel();
                    cookieCartViewModel.ProudctId = cartItem.ProudctId;
                    cookieCartViewModel.quantity = cartItem.quantity;
                    CookiesCart.shoppingCartViewModels.Add(cookieCartViewModel);


                }

            }
            

            if (SaveCookieCart(CookiesCart))
            {
                Result = true;
                return Result;
            }
            else
            {
                return Result;
            }
        }

        private bool SaveCookieCart(CookieCartViewModel CookieCart)
        {
           
            bool Result = false;


            Response.Cookies["Cart"].Value = JsonConvert.SerializeObject(CookieCart);


            if (Request.Cookies["Cart"] != null)
            {
                Result = true;
            }

            return Result;
        }

        private bool SaveCookiesItems (List<ShoppingCartViewModel> shoppingCartViewModels)
        {
            bool Result = false;
            CookieCartViewModel CookiesItems = new CookieCartViewModel();
            CookiesItems.shoppingCartViewModels = shoppingCartViewModels;
            Response.Cookies["Cart"].Value = JsonConvert.SerializeObject(CookiesItems);

            if (Request.Cookies["Cart"] != null)
            {
                Result = true;
            }


            return Result;
        }



        private bool UpdateDbByCart(CartItem cartItem , int Id)
        {
            bool Result = false;
           
            if (_db.proudcts.Find(cartItem.ProudctId) != null  && _cartService.GetCartItemById(Id) != null)
            {
                Result = _cartService.UpdateCartItem(cartItem);

            }


            return Result;
        }




        private CookieCartViewModel getCartFormCookie()
        {
            var getCookie = Request.Cookies["Cart"];
            

            CookieCartViewModel CookieCart = new CookieCartViewModel();
            if (getCookie != null)
            {
                string getAllCookie = getCookie.Value;
                CookieCart = JsonConvert.DeserializeObject<CookieCartViewModel>(getAllCookie);
       
            }      

            return CookieCart; 
        }


        private List<ShoppingCartViewModel> GetAllCartCookie()
        {
            var getCookie = Request.Cookies["Cart"];
            List<ShoppingCartViewModel> AllCart = new List<ShoppingCartViewModel>();
            if (getCookie != null)
            {
               var CookieCart = JsonConvert.DeserializeObject<CookieCartViewModel>(getCookie.Value);
               AllCart = CookieCart.shoppingCartViewModels.ToList();
            }
            else
            {
                AllCart = null;
            }
         
            

            return AllCart;
        }


        private bool AddCart(string UserID)
        {
        
            bool Result= false;
            Cart cart = new Cart();
            cart.BookStoreUserId = UserID;
            Result = _cartService.SaveCart(cart);

            return Result;
        }


        private bool CookieItemsByDbItems(int? CartId)
        {
            //If is have Cart CartItem add CartId Clounm
           
            bool Result = false;
           
            if (CartId.HasValue)
            {
                var CookieCartItems = GetAllCartCookie();
               
                foreach(var CartItem in CookieCartItems)
                {
                    CartItem cartItem = new CartItem()
                    {
                        CartId = CartId.Value,
                        ProudctId = CartItem.ProudctId,
                        quantity = CartItem.quantity,
                    };
                    _db.CartItems.Add(cartItem);
                }
                Response.Cookies["Cart"].Value = "{}";
                Result= _db.SaveChanges() > 0;
            }
            else
            {
                Result = AddCart(User.Identity.Name);
                Cart cart = _db.Carts.Find(CartId);
                if(cart != null)
                {
                    CookieItemsByDbItems(cart.Id);
                }
            }


            return Result;
        }

    }
}