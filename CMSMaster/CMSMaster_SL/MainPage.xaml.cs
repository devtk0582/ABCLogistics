using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace CMSMaster_SL
{
    public partial class MainPage : UserControl
    {
        MyColorPicker colorPicker;

        public MainPage()
        {
            InitializeComponent();
            colorPicker = new MyColorPicker();
            colorPicker.Closed += new EventHandler(colorPicker_Closed);
        }

        void colorPicker_Closed(object sender, EventArgs e)
        {
            //Application.Current.Host.Content.IsFullScreen = false;
        }

        private void btnGetColor_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Host.Content.IsFullScreen = true;
            colorPicker.Show();
        }
    }
}
