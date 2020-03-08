using ReadLater.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class BookmarkCreateViewModel
    {
        public Bookmark bookmark { get; set; }
        public IList<Category> categories { get; set; }
    }
}