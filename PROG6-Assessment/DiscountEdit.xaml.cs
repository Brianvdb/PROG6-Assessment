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

namespace PROG6_Assessment
{
    /// <summary>
    /// Interaction logic for DiscountEdit.xaml
    /// </summary>
    public partial class DiscountEdit : Window
    {
        private static Window _instance;
        public DiscountEdit()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public static Window Instance
        {
            get
            {
                if (_instance == null || !_instance.IsVisible)
                {
                    _instance = new DiscountEdit();
                }
                return _instance;
            }
        }
    }
}
