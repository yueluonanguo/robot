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
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.IO;
using System.ComponentModel;
using System.Net;

namespace Robot
{
    /// <summary>
    /// Model.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private const string MODEL_PATH1_1 = @"C:\Users\gu\Documents\3dsMax\export\m3.obj";
        private const string MODEL_PATH1_2 = @"C:\Users\gu\Documents\3dsMax\export\m4.obj";
        private const string MODEL_PATH1_3 = @"C:\Users\gu\Documents\3dsMax\export\m5.obj";
        private const string MODEL_PATH1_4 = @"C:\Users\gu\Documents\3dsMax\export\m6.obj";
        private const string MODEL_PATH1_5 = @"C:\Users\gu\Documents\3dsMax\export\m7.obj";
        private const string MODEL_PATH1_6 = @"C:\Users\gu\Documents\3dsMax\export\m8.obj";
        private const string MODEL_PATH1_7 = @"C:\Users\gu\Documents\3dsMax\export\m9.obj";
        private const string MODEL_PATH1_8 = @"C:\Users\gu\Documents\3dsMax\export\m10.obj";


        public UserControl1()
        {
            InitializeComponent();
            ModelVisual3D device3D = new ModelVisual3D();
            ModelVisual3D device3D1 = new ModelVisual3D();
            ModelVisual3D device3D2 = new ModelVisual3D();
            ModelVisual3D device3D3 = new ModelVisual3D();
            ModelVisual3D device3D4 = new ModelVisual3D();
            ModelVisual3D device3D5 = new ModelVisual3D();
            ModelVisual3D device3D6 = new ModelVisual3D();
            ModelVisual3D device3D7 = new ModelVisual3D();


            this.device3D.Content = Display3d(MODEL_PATH1_8);
            this.device3D1.Content = Display3d(MODEL_PATH1_1);
            this.device3D2.Content = Display3d(MODEL_PATH1_2);
            this.device3D3.Content = Display3d(MODEL_PATH1_3);
            this.device3D4.Content = Display3d(MODEL_PATH1_4);
            this.device3D5.Content = Display3d(MODEL_PATH1_5);
            this.device3D6.Content = Display3d(MODEL_PATH1_6);
            this.device3D7.Content = Display3d(MODEL_PATH1_7);
            // Add to view port

            viewPort3d.Children.Add(device3D);
            viewPort3d.Children.Add(device3D1);
            viewPort3d.Children.Add(device3D2);
            viewPort3d.Children.Add(device3D3);
            viewPort3d.Children.Add(device3D4);
            viewPort3d.Children.Add(device3D5);
            viewPort3d.Children.Add(device3D6);
            viewPort3d.Children.Add(device3D7);

        }
        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                //Adding a gesture here
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file
                ModelImporter import = new ModelImporter();

                //Load the 3D model file
                device = import.Load(model);


            }
            catch (Exception e)
            {
                // Handle exception in case can not file 3D model
                MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }

       
    }
}
