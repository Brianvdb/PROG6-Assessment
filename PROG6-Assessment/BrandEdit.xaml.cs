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
    /// Interaction logic for BrandEdit.xaml
    /// </summary>
    public partial class BrandEdit : Window
    {
        public static Window instance { get; set; }
        public BrandEdit()
        {
            InitializeComponent();
        }

        public static Window Instance
        {
            get
            {
                if (instance == null || !instance.IsVisible)
                {
                    instance = new BrandEdit();
                }
                return instance;
            }
        }
    }
}
