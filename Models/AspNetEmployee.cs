//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArabTradersGroup.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetEmployee
    {
        public string Emp_Id { get; set; }
        public string Emp_Fname { get; set; }
        public string Emp_Lname { get; set; }
        public string Emp_Mobile { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Country { get; set; }
        public System.DateTime Emp_StarteDateTime { get; set; }
        public string Emp_Publisher { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
