using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDWITHModelViewController.Models
{
    public class CollageClass
    {
        public int Id { get; set; }
        public string Name { get; set; }//get mens data get bhi aur data set donokarshakte hai
        public int Countries { get; set; }
        public int States { get; set; }
    }
}