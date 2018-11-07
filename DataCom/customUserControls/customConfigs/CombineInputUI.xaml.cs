﻿using DataCom.modals;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataCom.customUserControls.customConfigs
{
    /// <summary>
    /// Interaction logic for CombineInputUI.xaml
    /// </summary>
    public partial class CombineInputUI : UserControl
    {
        private CombineInputs CombineInput;
        public CombineInputUI(CombineInputs CombineInput)
        {
            this.CombineInput = CombineInput;
            InitializeComponent();
        }
    }
}