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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Runtime.InteropServices;

namespace KeyboardMouseActivity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyboardMouseControl keyMouse = new KeyboardMouseControl();


        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            keyMouse.keyDisable();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            keyMouse.mouseDisable();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            keyMouse.keyEnable();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            keyMouse.mouseEnable();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            keyMouse.lockDesktop();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            keyMouse.shutDownDesktop();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            keyMouse.restartDesktop();
        }

        

        
    }

       
}
