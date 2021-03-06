﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApplication1.ViewModels;

namespace WpfApplication1.Views
{
    /// <summary>
    /// Interaction logic for VisionWindow.xaml
    /// </summary>
    public partial class VisionWindow : Window
    {
        public VisionWindow()
        {
            InitializeComponent();
            var vm = new VisionViewModel {VideoCapture = VideoCaptureElement};
            DataContext = vm;
        }
    }
}
