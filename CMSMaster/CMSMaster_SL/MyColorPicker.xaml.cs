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

namespace CMSMaster_SL
{
    public partial class MyColorPicker : ChildWindow
    {
        public MyColorPicker()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void UpdateColor(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color color = Color.FromArgb((byte)AlphaBar.Value, (byte)RedBar.Value, (byte)GreenBar.Value, (byte)BlueBar.Value);
            previewRec.Fill = new SolidColorBrush(color);
            txtColor.Text = color.ToString();
        }
    }
}

