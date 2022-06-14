using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArabTradersGroup.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Globalization;

namespace ArabTradersGroup.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private ATGEntities Context = new ATGEntities();


        //------------------------- Start Home Region----------------
        public ActionResult Index(string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            DashboardViewModel Model = new DashboardViewModel();
            Model.LatestVendors = Context.AspNetVendors.OrderByDescending(m => m.Vendor_StartDateTime).Take(5).ToList();
            Model.SystemEmployees = Context.AspNetEmployees.Count();
            Model.SystemManagers = Context.AspNetManagers.Count();
            Model.SystemProducts = Context.AspNetProducts.Count();
            Model.SystemVendors = Context.AspNetVendors.Count();
            Model.TopManagers = Context.AspNetManagers.OrderByDescending(m => m.Manager_DateTime).Take(3);


            return View(Model);
        }
        //------------------------- End Home Region----------------

        //------------------------- Start Product Region----------------

        #region Products

        [HttpGet]
        public ActionResult Product(string Product_Error,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            ProductViewModel Model = new ProductViewModel();

            Model.Products = Context.AspNetProducts.ToList().OrderByDescending(m => m.Product_DateTime);
            if (!string.IsNullOrEmpty(Product_Error))
            {
                Model.Product_Error = Product_Error;
            }


            return View(Model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product(ProductViewModel model,HttpPostedFileBase Product_Image ,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {
          
            //.........check file length
            if (Product_Image != null && Product_Image.ContentLength > 0)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };

                var fileextention = Path.GetExtension(Product_Image.FileName);

                //.........check file Extention

                if (!supportedTypes.Contains(fileextention))
                {
                    model.Products = Context.AspNetProducts.ToList().OrderByDescending(m => m.Product_DateTime);
                    model.Product_Error = "File Extension Is InValid - Only Upload (.jpg , .jpeg, .png)";
                    return View(model);
                }
                //.........Add product successfuly message

                else
                {
                    model.Product_Error = "Product Added Successfuly";

                    string fileName = Guid.NewGuid() + Path.GetExtension(Product_Image.FileName);
                    string pathoriginal = Path.Combine(Server.MapPath("~/Images/imagesadmin/products"), fileName).ToString();

                    string pathresized = Path.Combine(Server.MapPath("~/Images/imagesadmin/productsresized"), fileName).ToString();
                    Product_Image.SaveAs(pathoriginal);
                    ResizeImage(250, pathoriginal, pathresized);

                    using (Context)
                    {
                        AspNetProduct Product = new AspNetProduct();

                        Product.Product_Id = Guid.NewGuid().ToString();
                        Product.Product_Name_En = model.Product_Name_En;
                        Product.Product_Name_Ar = model.Product_Name_Ar;
                            Product.Product_Description_En = model.Product_Description_En;
                            Product.Product_Description_Ar = model.Product_Description_Ar;
                            Product.Product_Quantity = model.Product_Quantity;
                        Product.Product_DateTime = DateTime.Now;
                        Product.Product_Image = fileName;
                        Product.Product_Publisher = HttpContext.User.Identity.GetUserId();


                        Context.AspNetProducts.Add(Product);

                        Context.SaveChanges();

                         model.Products = Context.AspNetProducts.ToList().OrderByDescending(m => m.Product_DateTime);
                    }
                  
                    return View(model);

                }

            }

            }
            return View(model);
           
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(string Product_Id,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            AspNetProduct ProductFound = Context.AspNetProducts.Find(Product_Id);
            ProductViewModel Model = new ProductViewModel();

            // Check Product Existence
            if (ProductFound!=null) {

                System.IO.File.Delete(Server.MapPath("~/Images/imagesadmin/products/" + ProductFound.Product_Image));
                System.IO.File.Delete(Server.MapPath("~/Images/imagesadmin/productsresized/"+ ProductFound.Product_Image));

                Context.AspNetProducts.Remove(ProductFound);
                Context.SaveChanges();

              
              
                Model.Product_Error = "Product Deleted Successfuly";


                return RedirectToAction("Product", "Dashboard",new { Product_Error= Model.Product_Error });

            }
            else
            {
                Model.Product_Error = "Something worng happened please try again later ";
                Model.Products = Context.AspNetProducts.ToList();
                return RedirectToAction("Product", "Dashboard", new { Product_Error = Model.Product_Error });


            }
                        

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductViewModel model, HttpPostedFileBase Product_Image,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);


            if (ModelState.IsValid)
            {




                //.........check file length
                if (Product_Image != null && Product_Image.ContentLength > 0)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };

                    var fileextention = Path.GetExtension(Product_Image.FileName);

                    //.........check file Extention

                    if (!supportedTypes.Contains(fileextention))
                    {
                        model.Products = Context.AspNetProducts.ToList().OrderByDescending(m => m.Product_DateTime);
                        model.Product_Error = "File Extension Is InValid - Only Upload (.jpg , .jpeg, .png)";
                        return RedirectToAction("Product", "Dashboard", new { Product_Error = model.Product_Error });
                    }
                   
                    //.........Add product successfuly message

                    else
                    {
                       

                        string fileName = Guid.NewGuid() + Path.GetExtension(Product_Image.FileName);
                        string pathoriginal = Path.Combine(Server.MapPath("~/Images/imagesadmin/products"), fileName).ToString();
                        string pathresized = Path.Combine(Server.MapPath("~/Images/imagesadmin/productsresized"), fileName).ToString();
                        Product_Image.SaveAs(pathoriginal);
                        ResizeImage(250, pathoriginal, pathresized);

                       

                        using (Context)
                        {
                           var ProductFound= Context.AspNetProducts.Find(model.Product_Id);

                            if (ProductFound!=null)
                            {

                               

                                ProductFound.Product_Description_Ar = model.Product_Description_Ar;
                                ProductFound.Product_Name_Ar = model.Product_Name_Ar;
                                ProductFound.Product_Name_En = model.Product_Name_En;
                                ProductFound.Product_Description_En = model.Product_Description_En;
                                ProductFound.Product_Quantity = model.Product_Quantity;
                                ProductFound.Product_DateTime = DateTime.Now;
                                ProductFound.Product_Publisher = HttpContext.User.Identity.GetUserId();
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/imagesadmin/productsresized"), ProductFound.Product_Image).ToString());
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/imagesadmin/productsresized"), ProductFound.Product_Image).ToString());
                                ProductFound.Product_Image = fileName;
                               



                                Context.SaveChanges();

                            }

     
                            model.Products = Context.AspNetProducts.ToList().OrderByDescending(m => m.Product_DateTime);

                            model.Product_Error = "Product Edited Successfuly";
                            return RedirectToAction("Product", "Dashboard", new { Product_Error = model.Product_Error });
                        }

                      

                    }

                }

            }






            return RedirectToAction("Product", "Dashboard", new { Product_Error = model.Product_Error });

        }

        #endregion

        //------------------------- End Product Region----------------

        //------------------------- Start Gallery Region----------------


        #region Gallery
        public ActionResult Gallery(string Photo_Error,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            GalleryViewModel Model = new GalleryViewModel();

            Model.Photo_Error = Photo_Error;
            Model.Photos = Context.AspNetGalleries.ToList().OrderByDescending(m => m.Photo_DateTime);


            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Gallery(GalleryViewModel model, HttpPostedFileBase Photo_Path,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {




                //.........check file length
                if (Photo_Path != null && Photo_Path.ContentLength > 0)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };

                    var fileextention = Path.GetExtension(Photo_Path.FileName);

                    //.........check file Extention

                    if (!supportedTypes.Contains(fileextention))
                    {
                        model.Photos = Context.AspNetGalleries.ToList().OrderByDescending(m => m.Photo_DateTime);
                        model.Photo_Error = "File Extension Is InValid - Only Upload (.jpg , .jpeg, .png)";
                        return View(model);
                    }

                    //.........check file size

                   
                    //.........Add product successfuly message

                    else
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(Photo_Path.FileName);
                        string pathoriginal = Path.Combine(Server.MapPath("~/Images/imagesadmin/galleries"), fileName).ToString();
                        string pathresized = Path.Combine(Server.MapPath("~/Images/imagesadmin/galleriesresized"), fileName).ToString();
                        Photo_Path.SaveAs(pathoriginal);
                        ResizeImage(400, pathoriginal, pathresized);

                        using (Context)
                        {
                            AspNetGallery Gallery = new AspNetGallery();

                            Gallery.Photo_Id = Guid.NewGuid().ToString();

                            Gallery.Photo_Caption_En = model.Photo_Caption_En;
                            Gallery.Photo_Caption_Ar = model.Photo_Caption_Ar;
                            Gallery.Photo_Path = fileName;
                            
                            Gallery.Photo_DateTime = DateTime.Now;
                         
                            Gallery.Photo_Publisher = HttpContext.User.Identity.GetUserId();


                            Context.AspNetGalleries.Add(Gallery);

                            Context.SaveChanges();

                            model.Photos = Context.AspNetGalleries.ToList().OrderByDescending(m => m.Photo_DateTime);
                        }


                        model.Photo_Error = "Product Added Successfuly";


                        return View(model);

                    }

                }

            }
            return View(model);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGallery(GalleryViewModel model, HttpPostedFileBase Photo_Path,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            if (ModelState.IsValid)
            {




                //.........check file length
                if (Photo_Path != null && Photo_Path.ContentLength > 0)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };

                    var fileextention = Path.GetExtension(Photo_Path.FileName);

                    //.........check file Extention

                    if (!supportedTypes.Contains(fileextention))
                    {
                        model.Photos = Context.AspNetGalleries.ToList().OrderByDescending(m => m.Photo_DateTime);
                        model.Photo_Error = "File Extension Is InValid - Only Upload (.jpg , .jpeg, .png)";
                        return RedirectToAction("Gallery", "Dashboard", new { Photo_Error = model.Photo_Error });
                    }

                  

                    //.........Add Photo successfuly message

                    else
                    {


                        string fileName = Guid.NewGuid() + Path.GetExtension(Photo_Path.FileName);
                        string pathoriginal = Path.Combine(Server.MapPath("~/Images/imagesadmin/galleries"), fileName).ToString();
                        string pathresized = Path.Combine(Server.MapPath("~/Images/imagesadmin/galleriesresized"), fileName).ToString();
                        Photo_Path.SaveAs(pathoriginal);
                        ResizeImage(250, pathoriginal, pathresized);

                        using (Context)
                        {
                            var PhotoFound = Context.AspNetGalleries.Find(model.Photo_Id);

                            if (PhotoFound != null)
                            {

                               
                                PhotoFound.Photo_DateTime = DateTime.Now;
                                
                                PhotoFound.Photo_Publisher = HttpContext.User.Identity.GetUserId();
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/imagesadmin/galleries"), PhotoFound.Photo_Path).ToString());
                                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images/imagesadmin/galleriesresized"), PhotoFound.Photo_Path).ToString());

                                PhotoFound.Photo_Path = fileName;


                                Context.SaveChanges();

                            }


                            model.Photos = Context.AspNetGalleries.ToList().OrderByDescending(m => m.Photo_DateTime);

                            model.Photo_Error = "Photo Edited Successfuly";
                            return RedirectToAction("Gallery", "Dashboard", new { Photo_Error = model.Photo_Error });
                        }



                    }

                }

            }






            return RedirectToAction("Photo", "Dashboard", new { Photo_Error = model.Photo_Error });

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGallery(string Photo_Id,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            AspNetGallery GalleryFound = Context.AspNetGalleries.Find(Photo_Id);
            GalleryViewModel Model = new GalleryViewModel();

            // Check Product Existence
            if (GalleryFound != null)
            {
                System.IO.File.Delete(Server.MapPath("~/Images/imagesadmin/galleries/" + GalleryFound.Photo_Path));
                System.IO.File.Delete(Server.MapPath("~/Images/imagesadmin/galleriesresized/" + GalleryFound.Photo_Path));

                Context.AspNetGalleries.Remove(GalleryFound);
                Context.SaveChanges();


                Model.Photo_Error = "Photo Deleted Successfuly";


                return RedirectToAction("Gallery", "Dashboard", new { Photo_Error  = Model.Photo_Error });

            }
            else
            {
                Model.Photo_Error = "Something worng happened please try again later ";
                Model.Photos = Context.AspNetGalleries.ToList().OrderByDescending(m=>m.Photo_DateTime);
                return RedirectToAction("Gallery", "Dashboard", new { Photo_Error = Model.Photo_Error });


            }


        }

        #endregion

        //------------------------- End Gallery Region----------------


        //------------------------- Start Vendor Region----------------
        #region Vendor
        [HttpGet]
        public ActionResult Vendor(string Vendor_Error,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);


            VendorViewModel Model = new VendorViewModel();
            Model.Vendor_Error = Vendor_Error;
            Model.Vendors = Context.AspNetVendors.OrderByDescending(m => m.Vendor_StartDateTime).ToList();

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vendor(VendorViewModel model,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {              
                       

                        using (Context)
                        {
                            AspNetVendor Vendor = new AspNetVendor();

                            Vendor.Vendor_Id = Guid.NewGuid().ToString();
                            Vendor.Vendor_Company = model.Vendor_Company;
                            Vendor.Vendor_Country = model.Vendor_Country;
                            Vendor.Vendor_Email = model.Vendor_Email;
                            Vendor.Vendor_StartDateTime = DateTime.Now;
                            Vendor.Vendor_Phone = model.Vendor_Phone;
                            Vendor.Vendor_Manager = model.Vendor_Manager;
                            Vendor.Vendor_Publisher = HttpContext.User.Identity.GetUserId();


                            Context.AspNetVendors.Add(Vendor);

                            Context.SaveChanges();
                             
                            model.Vendors = Context.AspNetVendors.ToList().OrderByDescending(m => m.Vendor_StartDateTime);
                            model.Vendor_Error = "Vendor Added Successfuly";
                           
                          }
                          return View(model);

                
              
            }

            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVendor(VendorViewModel model,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            if (ModelState.IsValid)
            {
                 
                        using (Context)
                        {
                            var VendorFound = Context.AspNetVendors.Find(model.Vendor_Id);

                            if (VendorFound != null)
                            {

                                VendorFound.Vendor_Country = model.Vendor_Country;
                                VendorFound.Vendor_StartDateTime = DateTime.Now;
                                VendorFound.Vendor_Company = model.Vendor_Company;
                                VendorFound.Vendor_Publisher = HttpContext.User.Identity.GetUserId();
                                VendorFound.Vendor_Email = model.Vendor_Email;
                                VendorFound.Vendor_Phone = model.Vendor_Phone;
                                VendorFound.Vendor_Manager = model.Vendor_Manager;


                                 Context.SaveChanges();


                                model.Vendors = Context.AspNetVendors.ToList().OrderByDescending(m => m.Vendor_StartDateTime);

                                model.Vendor_Error = "Vendor Edited Successfuly";
                               return RedirectToAction("Vendor", "Dashboard", new { Vendor_Error = model.Vendor_Error });
                    }

                        }

                         

                    }

              
              return RedirectToAction("Vendor", "Dashboard", new { Vendor_Error = model.Vendor_Error });
            }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVendor(string Vendor_Id,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            AspNetVendor VendorFound = Context.AspNetVendors.Find(Vendor_Id);
            VendorViewModel Model = new VendorViewModel();

            // Check Product Existence
            if (VendorFound != null)
            {
                

                Context.AspNetVendors.Remove(VendorFound);
                Context.SaveChanges();


                Model.Vendor_Error = "Vendor Deleted Successfuly";


                return RedirectToAction("Vendor", "Dashboard", new { Vendor_Error = Model.Vendor_Error });

            }
            else
            {
                Model.Vendor_Error = "Something worng happened please try again later ";
                Model.Vendors = Context.AspNetVendors.ToList().OrderByDescending(m => m.Vendor_StartDateTime);
                return RedirectToAction("Vendor", "Dashboard", new { Vendor_Error = Model.Vendor_Error });


            }


        }
        #endregion

        //------------------------- End Vendor Region----------------

        //------------------------- Start Team Region----------------

        #region Team
        [HttpGet]
        public ActionResult Team(string Emp_Error,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);



            TeamViewModel Model = new TeamViewModel();

            Model.Teams = Context.AspNetEmployees.OrderByDescending(m => m.Emp_StarteDateTime).ToList();

            Model.Emp_Error = Emp_Error;


            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Team(TeamViewModel model,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {


                using (Context)
                {
                    AspNetEmployee Employee = new AspNetEmployee();

                    Employee.Emp_Id = Guid.NewGuid().ToString();
                    Employee.Emp_Country = model.Emp_Country;
                    Employee.Emp_Email = model.Emp_Email;
                    Employee.Emp_Fname = model.Emp_Fname;
                    Employee.Emp_Lname = model.Emp_Lname;
                    Employee.Emp_StarteDateTime = DateTime.Now;
                    Employee.Emp_Mobile = model.Emp_Mobile;
                    Employee.Emp_Publisher = HttpContext.User.Identity.GetUserId();


                    Context.AspNetEmployees.Add(Employee);

                    Context.SaveChanges();

                    model.Teams = Context.AspNetEmployees.ToList().OrderByDescending(m => m.Emp_StarteDateTime);
                    model.Emp_Error = "Employee Added Successfuly";
                    return View(model);
                }
               



            }

            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTeam(TeamViewModel model,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {

                using (Context)
                {
                    var TeamFound = Context.AspNetEmployees.Find(model.Emp_Id);

                    if (TeamFound != null)
                    {

                        TeamFound.Emp_Country = model.Emp_Country;
                        TeamFound.Emp_StarteDateTime = DateTime.Now;
                        TeamFound.Emp_Email = model.Emp_Email;
                        TeamFound.Emp_Publisher = HttpContext.User.Identity.GetUserId();
                        TeamFound.Emp_Mobile = model.Emp_Mobile;
                        TeamFound.Emp_Fname = model.Emp_Fname;
                        TeamFound.Emp_Lname = model.Emp_Lname;


                        Context.SaveChanges();


                        model.Teams = Context.AspNetEmployees.ToList().OrderByDescending(m => m.Emp_StarteDateTime);

                        model.Emp_Error = "Employee Edited Successfuly";
                        return RedirectToAction("Team", "Dashboard", new { Emp_Error = model.Emp_Error });
                    }

                }



            }


            return RedirectToAction("Team", "Dashboard", new { Emp_Error = model.Emp_Error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTeam(string Emp_Id,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            AspNetEmployee TeamFound = Context.AspNetEmployees.Find(Emp_Id);
            TeamViewModel Model = new TeamViewModel();

            // Check Employee Existence
            if (TeamFound != null)
            {


                Context.AspNetEmployees.Remove(TeamFound);
                Context.SaveChanges();


                Model.Emp_Error = "Employee Deleted Successfuly";


                return RedirectToAction("Team", "Dashboard", new { Emp_Error = Model.Emp_Error });

            }
            else
            {
                Model.Emp_Error = "Something worng happened please try again later ";
                Model.Teams = Context.AspNetEmployees.OrderByDescending(m => m.Emp_StarteDateTime);
                return RedirectToAction("Team", "Dashboard", new { Emp_Error = Model.Emp_Error });


            }


        }

        #endregion
        //------------------------- End Team Region----------------

        //------------------------- Start Manager Region----------------

        #region Manager

        [HttpGet]
        public ActionResult Manager(string Manager_Error,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);




            ManagerViewModel Model = new ManagerViewModel();

            Model.Managers = Context.AspNetManagers.OrderByDescending(m => m.Manager_DateTime).ToList();

            Model.Manager_Error = Manager_Error;


            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteManager(string Manager_Id,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);


            AspNetUser ManagerFound = Context.AspNetUsers.Find(Manager_Id);

            AspNetManager ManagerDetails = Context.AspNetManagers.Find(Manager_Id);


            ManagerViewModel Model = new ManagerViewModel();

            // Check Employee Existence
            if (ManagerFound != null&& ManagerDetails!=null)
            {

                if (HttpContext.User.Identity.IsAuthenticated)
                {

                    Model.Manager_Error = "Sorry You Can't Delete Because it's you !";
                    return RedirectToAction("Manager", "Dashboard", new { Manager_Error = Model.Manager_Error });
                }

                System.IO.File.Delete(Server.MapPath("~/Images/imagesadmin/managersresized/" + ManagerDetails.Manager_Image));

                Context.AspNetManagers.Remove(ManagerDetails);
            
                Context.AspNetUsers.Remove(ManagerFound);

                Context.SaveChanges();




                Model.Manager_Error = "Manager Deleted Successfuly";


                return RedirectToAction("Manager", "Dashboard", new { Manager_Error = Model.Manager_Error });

            }
            else
            {
                Model.Manager_Error = "Something worng happened please try again later ";
                Model.Managers = Context.AspNetManagers.OrderByDescending(m => m.Manager_DateTime);
                return RedirectToAction("Manager", "Dashboard", new { Manager_Error = Model.Manager_Error });


            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManager(ManagerViewModel model, HttpPostedFileBase Manager_Image,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {
                //.........check file length
                if (Manager_Image != null && Manager_Image.ContentLength > 0)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };

                    var fileextention = Path.GetExtension(Manager_Image.FileName);

                    //.........check file Extention

                    if (!supportedTypes.Contains(fileextention))
                    {
                        model.Managers = Context.AspNetManagers.ToList().OrderByDescending(m => m.Manager_DateTime);
                        model.Manager_Error = "File Extension Is InValid - Only Upload (.jpg , .jpeg, .png)";
                        return RedirectToAction("Manager", "Dashboard", new { Manager_Error = model.Manager_Error });
                    }


                    string fileName = Guid.NewGuid() + Path.GetExtension(Manager_Image.FileName);
                    string pathoriginal = Path.Combine(Server.MapPath("~/Images/imagesadmin/managers"), fileName).ToString();
                    string pathresized = Path.Combine(Server.MapPath("~/Images/imagesadmin/managersresized"), fileName).ToString();
                    Manager_Image.SaveAs(pathoriginal);
                    ResizeImage(250, pathoriginal, pathresized);

                    using (Context)
                    {
                        var ManagerFound = Context.AspNetManagers.Find(model.Manager_Id);

                        if (ManagerFound != null)
                        {

                            ManagerFound.Manager_Fname = model.Manager_Fname;
                            ManagerFound.Manager_Description = model.Manager_Description;
                            ManagerFound.Manager_Lname = model.Manager_Lname;
                            ManagerFound.Manager_DateTime = DateTime.Now;
                           
                            ManagerFound.Manager_Publisher = HttpContext.User.Identity.GetUserId();

                            ManagerFound.Manager_Image = fileName;

                            Context.SaveChanges();

                        }


                        model.Managers = Context.AspNetManagers.ToList().OrderByDescending(m => m.Manager_DateTime);

                        model.Manager_Error = "Manager Edited Successfuly";
                        return RedirectToAction("Manager", "Dashboard", new { Manager_Error = model.Manager_Error });





                    }


                }
                else
                {

                    using (Context)
                    {
                        var ManagerFound = Context.AspNetManagers.Find(model.Manager_Id);

                        if (ManagerFound != null)
                        {

                            ManagerFound.Manager_Fname = model.Manager_Fname;
                            ManagerFound.Manager_Description = model.Manager_Description;
                            ManagerFound.Manager_Lname = model.Manager_Lname;
                            ManagerFound.Manager_DateTime = DateTime.Now;
                          
                            ManagerFound.Manager_Publisher = HttpContext.User.Identity.GetUserId();



                            Context.SaveChanges();

                        }


                        model.Managers = Context.AspNetManagers.ToList().OrderByDescending(m => m.Manager_DateTime);

                        model.Manager_Error = "Manager Edited Successfuly";
                        return RedirectToAction("Manager", "Dashboard", new { Manager_Error = model.Manager_Error });



                    }


                }



                }
            return RedirectToAction("Manager", "Dashboard", new { Manager_Error = model.Manager_Error });
        }

        #endregion

        //------------------------- End Manager Region----------------

        //------------------------- Start Faq Region----------------
        #region Faq

        [HttpGet]
        public ActionResult Faq(string Faq_Error,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);



            FaqViewModel Model = new FaqViewModel();

            Model.Faqs = Context.AspNetFaqs.OrderByDescending(m => m.Faq_DateTime).ToList();

            Model.Faq_Error = Faq_Error;


            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Faq(FaqViewModel model,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            if (ModelState.IsValid)
            {


                using (Context)
                {
                    AspNetFaq Faq = new AspNetFaq();

                    Faq.Faq_Id = Guid.NewGuid().ToString();
                    Faq.Faq_Question_En = model.Faq_Question_En;
                    Faq.Faq_Answer_En = model.Faq_Answer_En;
                    Faq.Faq_Question_Ar = model.Faq_Question_Ar;
                    Faq.Faq_Answer_Ar = model.Faq_Answer_Ar;

                    Faq.Faq_DateTime = DateTime.Now;
                    
                    Faq.Faq_Publisher = HttpContext.User.Identity.GetUserId();


                    Context.AspNetFaqs.Add(Faq);

                    Context.SaveChanges();

                    model.Faqs = Context.AspNetFaqs.ToList().OrderByDescending(m => m.Faq_DateTime);
                    model.Faq_Error = "Faq Added Successfuly";
                    return View(model);
                }




            }

            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFaq(FaqViewModel model,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {

                using (Context)
                {
                    var FaqFound = Context.AspNetFaqs.Find(model.Faq_Id);

                    if (FaqFound != null)
                    {
                        FaqFound.Faq_Question_En = model.Faq_Question_En;
                        FaqFound.Faq_Answer_En = model.Faq_Answer_En;
                        FaqFound.Faq_Question_Ar = model.Faq_Question_Ar;
                        FaqFound.Faq_Answer_Ar = model.Faq_Answer_Ar;

                        FaqFound.Faq_DateTime = DateTime.Now;
                       
                        FaqFound.Faq_Publisher = HttpContext.User.Identity.GetUserId();
                        

                        Context.SaveChanges();


                        model.Faqs = Context.AspNetFaqs.ToList().OrderByDescending(m =>m.Faq_DateTime);

                        model.Faq_Error = "Employee Edited Successfuly";
                        return RedirectToAction("Faq", "Dashboard", new { Faq_Error = model.Faq_Error });
                    }

                }



            }


            return RedirectToAction("Faq", "Dashboard", new { Faq_Error = model.Faq_Error });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFaq(string Faq_Id,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            AspNetFaq  FaqFound = Context.AspNetFaqs.Find(Faq_Id);
            FaqViewModel Model = new FaqViewModel();

            // Check Employee Existence
            if (FaqFound != null)
            {


                Context.AspNetFaqs.Remove(FaqFound);
                Context.SaveChanges();


                Model.Faq_Error = "Faq Deleted Successfuly";


                return RedirectToAction("Faq", "Dashboard", new { Faq_Error = Model.Faq_Error });

            }
            else
            {
                Model.Faq_Error = "Something worng happened please try again later ";
                Model.Faqs = Context.AspNetFaqs.OrderByDescending(m => m.Faq_DateTime);
                return RedirectToAction("Faq", "Dashboard", new { Emp_Error = Model.Faq_Error });


            }


        }

        #endregion

        //------------------------- End Faq Region----------------

        //------------------------- Start About Region----------------

        #region About

        public ActionResult About(string About_Error,string language)
        {


            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);




            AboutViewModel Model = new AboutViewModel();
            Model.Abouts = Context.AspNetAbouts.ToList().OrderByDescending(m => m.About_DateTime).SingleOrDefault();
            Model.About_Error = About_Error;
            Model.About_Who = Model.Abouts.About_Who;
            Model.About_How = Model.Abouts.About_How;
            Model.About_Us = Model.Abouts.About_Us;

            return View(Model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult About(AboutViewModel model,string language)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {


                using (Context)
                {
                    AspNetAbout About = new AspNetAbout();

                    About.About_Id = Guid.NewGuid().ToString();
                    About.About_Who = model.About_Who;
                    About.About_How = model.About_How;
                    About.About_DateTime = DateTime.Now;
                    About.About_Publisher = HttpContext.User.Identity.GetUserId();
                    About.About_Us = model.About_Us;

                    AspNetAbout result = Context.AspNetAbouts.SingleOrDefault();
                    if (result!=null)
                    {
                        Context.AspNetAbouts.Remove(result);
                        Context.SaveChanges();
                      
                       
                    }

                    Context.AspNetAbouts.Add(About);

                    Context.SaveChanges();


                    model.Abouts = Context.AspNetAbouts.ToList().OrderByDescending(m => m.About_DateTime).SingleOrDefault();
                    model.About_Error = "About Added Successfuly";
                    return RedirectToAction("About","Dashboard",new {About_Error=model.About_Error });
                }




            }

            return RedirectToAction("About", "Dashboard", new { About_Error = model.About_Error });

        }

        #endregion

        //------------------------- End About Region----------------
        //------------------------- Start Contact Region----------------
        #region Contact

        public ActionResult Contact(string Contact_Error,string language)
        {


            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);




            ContactViewModel Model = new ContactViewModel();
            Model.Contacts = Context.AspNetContacts.ToList().OrderByDescending(m => m.Contact_DateTime).SingleOrDefault();
            if (Model.Contacts!=null)
            {
                Model.Contact_Error = Contact_Error;
                Model.Contact_Address = Model.Contacts.Contact_Address;
                Model.Contact_Phone1 = Model.Contacts.Contact_Phone2;
                Model.Contact_Phone2 = Model.Contacts.Contact_Phone2;
                Model.Contact_Email = Model.Contacts.Contact_Email;

            }

            return View(Model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model,string language)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (ModelState.IsValid)
            {


                using (Context)
                {
                    AspNetContact Contact = new AspNetContact();

                    Contact.Contact_Id = Guid.NewGuid().ToString();
                    Contact.Contact_Email = model.Contact_Email;
                    Contact.Contact_Phone1 = model.Contact_Phone1;
                    Contact.Contact_Phone2 = model.Contact_Phone2;
                    Contact.Contact_Address = model.Contact_Address;
                    Contact.Contact_DateTime = DateTime.Now;
                    Contact.Contact_Publisher = HttpContext.User.Identity.GetUserId();
                    

                    AspNetContact result = Context.AspNetContacts.SingleOrDefault();
                    if (result != null)
                    {
                        Context.AspNetContacts.Remove(result);
                        Context.SaveChanges();


                    }

                    Context.AspNetContacts.Add(Contact);

                    Context.SaveChanges();


                    model.Contacts = Context.AspNetContacts.ToList().OrderByDescending(m => m.Contact_DateTime).SingleOrDefault();
                    model.Contact_Error = "Contact Added Successfuly";
                    return RedirectToAction("Contact", "Dashboard", new { Contact_Error = model.Contact_Error });
                }




            }

            return RedirectToAction("Contact", "Dashboard", new { Contact_Error = model.Contact_Error });

        }

        #endregion
        //------------------------- End Contact Region----------------

        #region Helpers

        public static void ResizeImage(int size, string filePath, string saveFilePath)
        {
            //variables for image dimension/scale

            double newHeight = 0;
            double newWidth = 0;
            double scale = 0;

            //create new image object
            Bitmap curImage = new Bitmap(filePath);

            //Determine image scaling
            if (curImage.Height > curImage.Width)
            {
                scale = Convert.ToSingle(size) / curImage.Height;
            }
            else
            {
                scale = Convert.ToSingle(size) / curImage.Width;
            }
            if (scale < 0 || scale > 1) { scale = 1; }

            //New image dimension
            newHeight = Math.Floor(Convert.ToSingle(curImage.Height) * scale);
            newWidth = Math.Floor(Convert.ToSingle(curImage.Width) * scale);

            //Create new object image
            Bitmap newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
            Graphics imgDest = Graphics.FromImage(newImage);
            imgDest.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            imgDest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            imgDest.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            imgDest.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters param = new EncoderParameters(1);
            param.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            //Draw the object image
            imgDest.DrawImage(curImage, 0, 0, newImage.Width, newImage.Height);

            //Save image file
            newImage.Save(saveFilePath, info[1], param);

            //Dispose the image objects
            curImage.Dispose();
            newImage.Dispose();
            imgDest.Dispose();

         
        }


        #endregion



    }
}
