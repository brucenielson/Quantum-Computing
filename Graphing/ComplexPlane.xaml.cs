using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphing
{
    /// <summary>
    /// Interaction logic for ComplexPlane.xaml
    /// </summary>
    public partial class ComplexPlane : Window
    {
        public ComplexPlane()
        {
            InitializeComponent();

            // draw the graph markers
            // 250, 250 = 0, 0
            for (int i = 0; i * 10 < 500; i++)
            {
                // horizontal
                Line graphMarker = new Line();
                graphMarker.Stroke = System.Windows.Media.Brushes.Black;
                graphMarker.X1 = i * 10;
                graphMarker.Y1 = 245;
                graphMarker.X2 = i * 10;
                graphMarker.Y2 = 255;
                graphMarker.StrokeThickness = 0.5;
                ComplexPlaneCanvas.Children.Add(graphMarker);

                //verticle
                graphMarker = new Line();
                graphMarker.Stroke = System.Windows.Media.Brushes.Black;
                graphMarker.Y1 = i * 10;
                graphMarker.X1 = 245;
                graphMarker.Y2 = i * 10;
                graphMarker.X2 = 255;
                graphMarker.StrokeThickness = 0.5;
                ComplexPlaneCanvas.Children.Add(graphMarker);

            }
        }
    }
}
