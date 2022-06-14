using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArabTradersGroup.Models
{
    public class HomeViewModels
    {
    }

    public class HomeViewModel {

        public IEnumerable<AspNetProduct> LatestProducts  { get; set; }
        public IEnumerable<AspNetGallery> LatestGalleries { get; set; }
        public IEnumerable<AspNetManager> HeadManagers { get; set; }



    }

    public class ProducstViewModel
    {

       
         public IEnumerable<AspNetProduct> Products { get; set; }
        public AspNetProduct Product { get; set; }
        public IEnumerable<AspNetGallery> Galleries { get; set; }

        public IEnumerable<AspNetProduct> RelatedProducts { get; set; }



    }


    public class FaqstViewModel
    {


        public IEnumerable<AspNetFaq> Faqs{ get; set; }
        


    }






}