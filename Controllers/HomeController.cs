using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ArabTradersGroup.Models;
using PagedList;
using PagedList.Mvc;

namespace ArabTradersGroup.Controllers
{
    public class HomeController : Controller
    {
        private ATGEntities Context = new ATGEntities();
        public ActionResult Index(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            HomeViewModel Model = new HomeViewModel();
            Model.LatestProducts = Context.AspNetProducts.OrderByDescending(m => m.Product_DateTime).Take(6);
            Model.LatestGalleries = Context.AspNetGalleries.OrderByDescending(m => m.Photo_DateTime).Take(8);

            Model.HeadManagers = Context.AspNetManagers.OrderByDescending(m => m.Manager_DateTime).Take(3);


            return View(Model);
        }

        public ActionResult About(string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            HomeViewModel Model = new HomeViewModel();
            Model.LatestProducts = Context.AspNetProducts.OrderByDescending(m => m.Product_DateTime).Take(6);
            Model.LatestGalleries = Context.AspNetGalleries.OrderByDescending(m => m.Photo_DateTime).Take(8);

            Model.HeadManagers = Context.AspNetManagers.OrderByDescending(m => m.Manager_DateTime).Take(3);


            return View(Model);
        }

        public ActionResult Contact(string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            FaqstViewModel Model = new FaqstViewModel();
            Model.Faqs = Context.AspNetFaqs.OrderByDescending(m => m.Faq_DateTime).ToList();
            return View(Model);
        }


        public ActionResult Faq(string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            FaqstViewModel Model = new FaqstViewModel();
            Model.Faqs = Context.AspNetFaqs.OrderByDescending(m => m.Faq_DateTime).ToList();               
            return View(Model);
        }

        public ActionResult Gallery(string language,int?page)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            ProducstViewModel Model = new ProducstViewModel();
            Model.Galleries = Context.AspNetGalleries.OrderByDescending(m => m.Photo_DateTime).ToPagedList(page ?? 1, 12);

            return View(Model.Galleries);
        }


        public ActionResult Products(string language,int?page)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            ProducstViewModel Model = new ProducstViewModel();
            Model.Products = Context.AspNetProducts.OrderByDescending(m=>m.Product_DateTime).ToPagedList(page??1,12);

            return View(Model.Products);
        }

        public ActionResult ProductDetails(string language,string id)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            ProducstViewModel Model = new ProducstViewModel();
             Model.Product = Context.AspNetProducts.Find(id);
            Model.RelatedProducts = Context.AspNetProducts.OrderByDescending(m => m.Product_DateTime).Take(4);
            return View(Model);
        }




    }
}