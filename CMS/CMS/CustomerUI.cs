using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS
{
    public class CustomerUI
    {
        public string HeaderMessage { get; set;}
        public string FooterMessage { get; set;}
        public string SideBarMessage { get; set;}
        public string WelcomeMessage { get; set;}

        public string HeaderBGColor { get; set; }
        public string HeaderFontName { get; set; }
        public string HeaderFontSize { get; set; }
        public string HeaderForeColor { get; set; }
        public string FooterBGColor { get; set; }
        public string FooterFontName { get; set; }
        public string FooterFontSize { get; set; }
        public string FooterForeColor { get; set; }
        public string SideBarBGColor { get; set; }
        public string SideBarFontName { get; set; }
        public string SideBarFontSize { get; set; }
        public string SideBarForeColor { get; set; }

        public bool HasHeaderImage { get; set; }
        public bool HasFooterImage { get; set; }
        public bool HasSideBarImage { get; set; }


    }
}