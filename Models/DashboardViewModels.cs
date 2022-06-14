using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArabTradersGroup.Models
{
    public class DashboardViewModels
    {

    }

    public class ProductViewModel
    {
        [DataType(DataType.Text)]
        [Key]
        [Display(Name="ProductIdModel",ResourceType =typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Product_Id { get; set; }
        [Required]
        [Display(Name = "ProductNameEnModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Product_Name_En { get; set; }
        [Required]
        [Display(Name = "ProductNameArModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Product_Name_Ar { get; set; }
        [Required]
        [Display(Name = "ProductDescriptionEnModel", ResourceType =typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Product_Description_En { get; set; }

        [Required]
        [Display(Name = "ProductDescriptionArModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Product_Description_Ar { get; set; }

        [Required]
        [Display(Name = "ProductImageModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Product_Image { get; set; }


        
        [Display(Name = "ProductDateTimeModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.DateTime)]
        public System.DateTime Product_DateTime { get; set; }


        [Required]
        [Display(Name = "ProductQuantityModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public decimal Product_Quantity { get; set; }


      
        [Display(Name = "ProductPublisherModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Product_Publisher { get; set; }




        [Display(Name = "ProductErrorModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Product_Error { get; set; }



        public IEnumerable<AspNetProduct> Products { get; set; }


    }

    public class GalleryViewModel
    {

        [Key]
        [DataType(DataType.Text)]
        [Display(Name ="PhotoIdModel",ResourceType =typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Photo_Id { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotoCaptionEnModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Photo_Caption_En { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotoCaptionArModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Photo_Caption_Ar { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotoPathModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]   
        public string Photo_Path { get; set; }

        
        [DataType(DataType.DateTime)]
        [Display(Name = "PhotoDateTimeModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public System.DateTime Photo_DateTime { get; set; }

        
        [DataType(DataType.Text)]
        [Display(Name = "PhotoPublisherModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Photo_Publisher { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "PhotoErrorModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Photo_Error { get; set; }



        public IEnumerable<AspNetGallery> Photos { get; set; }



    }

    public class VendorViewModel
    {

         [Key]
         [DataType(DataType.Text)]
         [Display(Name ="VendorIdModel",ResourceType =typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "VendorManagerNameModel",ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Manager { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "VendorCompanyNameModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Company { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "VendorCompanyEmailModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "VendorPhoneNumberModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Phone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "VendorCountryModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Country { get; set; }

        
        [DataType(DataType.Text)]
        [Display(Name = "VendorDateTimeModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public System.DateTime Vendor_StartDateTime { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "VendorPublisherModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Publisher { get; set; }



        [DataType(DataType.Text)]
        [Display(Name = "VendorErrorModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Vendor_Error { get; set; }


        public IEnumerable<AspNetVendor> Vendors { get; set; }




    }

    public class TeamViewModel {
         [Key]
         [DataType(DataType.Text)]
         [Display(Name ="EmployeeIdModel",ResourceType =typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Emp_Id { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "EmployeeFirstNameModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Emp_Fname { get; set; }



        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "EmployeeLastNameModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Emp_Lname { get; set; }



        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "EmployeePhoneNumberModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Emp_Mobile { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EmployeeEmailModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]                                                                    
        public string Emp_Email { get; set; }



        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "EmployeeCountryModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Emp_Country { get; set; }



        
        [DataType(DataType.DateTime)]
        [Display(Name = "EmployeeDateTimeModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public System.DateTime Emp_StarteDateTime { get; set; }


        
        [DataType(DataType.Text)]
        [Display(Name = "EmployeePublisherModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Emp_Publisher { get; set; }




        [DataType(DataType.Text)]
        [Display(Name = "EmployeeEmpErrorModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        public string Emp_Error { get; set; }


        public IEnumerable<AspNetEmployee> Teams { get; set; }



    }

    public class ManagerViewModel
    {
        [Key]
        [Display(Name ="ManagerIdModel",ResourceType =typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Id { get; set; }

        [Required]
        [Display(Name = "FirstNameModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Fname { get; set; }


        [Required]
        [Display(Name = "LastNameModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Lname { get; set; }


        [Required]
        [Display(Name = "JobTitleModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Position { get; set; }


        [Required]
        [Display(Name = "DescriptionModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Description { get; set; }

     
        [Display(Name = "ImageModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Image { get; set; }


        [Display(Name = "PublisherModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Publisher { get; set; }


        [Display(Name = "DateTimeModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.DateTime)]
        public System.DateTime Manager_DateTime { get; set; }


        [Display(Name = "ManagerErrorModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Manager_Error { get; set; }


        public IEnumerable<AspNetManager> Managers { get; set; }


    }

    public class FaqViewModel
    {
        [Key]
        [Display(Name ="FaqIdModel",ResourceType =typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Faq_Id { get; set; }


        [Required]
        [Display(Name = "FaqQuestionEnModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Faq_Question_En { get; set; }

        [Required]
        [Display(Name = "FaqQuestionArModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Faq_Question_Ar { get; set; }


        [Required]
        [Display(Name = "FaqAnswerEnModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Faq_Answer_En { get; set; }

        [Required]
        [Display(Name = "FaqAnswerArModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Faq_Answer_Ar { get; set; }



        [Display(Name = "FaqDateTimeModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.DateTime)]
        public System.DateTime Faq_DateTime { get; set; }



        [Display(Name = "FaqPublisherModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Faq_Publisher { get; set; }

        [Display(Name = "FaqErrorModel", ResourceType = typeof(ArabTradersGroup.App_LocalResources.ATG))]
        [DataType(DataType.Text)]
        public string Faq_Error { get; set; }


        public IEnumerable<AspNetFaq> Faqs { get; set; }



    }


    public class AboutViewModel
    {
         [Key]
         [Display(Name ="About Id")]
         [DataType(DataType.Text)]
        public string About_Id { get; set; }


        [Required]
        [Display(Name = "About Category")]
        [DataType(DataType.Text)]
        public string About_Who { get; set; }

        [Required]
        [Display(Name = "About Content")]
        [DataType(DataType.Text)]
        public string About_How { get; set; }

        [Required]
        [Display(Name = "About Content")]
        [DataType(DataType.Text)]
        public string About_Us { get; set; }


        [Display(Name = "About Publisher")]
        [DataType(DataType.Text)]
        public string About_Publisher { get; set; }

      
        [Display(Name = "About DateTime")]
        [DataType(DataType.DateTime)]
        public System.DateTime About_DateTime { get; set; }

        [Display(Name = "About Error")]
        [DataType(DataType.Text)]
        public string About_Error { get; set; }


        public AspNetAbout Abouts { get; set; }



    }


    public class ContactViewModel {

        [Key]
        [Display(Name ="Contact Id")]
        [DataType(DataType.Text)]
        public string Contact_Id { get; set; }


        
        [Display(Name = "Contact DateTime")]
        [DataType(DataType.DateTime)]
        public System.DateTime Contact_DateTime { get; set; }



        [Display(Name = "Contact Publisher")]
        [DataType(DataType.Text)]
        public string Contact_Publisher { get; set; }



        [Display(Name = "Contact Error")]
        [DataType(DataType.Text)]
        public string Contact_Error { get; set; }



        [Required]
        [Display(Name = "Contact Phone1")]
        [DataType(DataType.Text)]
        public string Contact_Phone1 { get; set; }


        [Required]
        [Display(Name = "Contact Phone2")]
        [DataType(DataType.Text)]
        public string Contact_Phone2 { get; set; }


        [Required]
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        public string Contact_Email { get; set; }


        [Required]
        [Display(Name = "Contact Address")]
        [DataType(DataType.Text)]
        public string Contact_Address { get; set; }

        public  AspNetContact Contacts { get; set; }


    }

    public class DashboardViewModel {


        public IEnumerable<AspNetVendor> LatestVendors { get; set; }

        public int SystemVendors { get; set; }
        public int SystemProducts { get; set; }
        public int SystemEmployees { get; set; }
        public int SystemManagers { get; set; }

      public IEnumerable<AspNetManager> TopManagers { get; set; }

    }
}