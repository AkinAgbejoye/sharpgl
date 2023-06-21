using SharpGL;
using System.Windows;
using SharpGL.WPF;
using Autodesk.AutoCAD;
using System.IO;
using SharpGL.SceneGraph;
using SharpGL.Version;
using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.Serialization;
using SharpGL.SceneGraph.Transformations;
using SharpGL.SceneGraph.Assets;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using ACadSharp.IO;
using ACadSharp;
using System;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        float rotatePyramid = 0;
        float rquad = 0;
   
    public MainWindow()
        {
            InitializeComponent();

        }

        private void openGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Enable depth testing
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            // Set up lighting
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, new float[] { 0, 0, 1, 0 });

            // Set up material properties
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_DIFFUSE, new float[] { 0.8f, 0.8f, 0.8f, 1.0f });
        }
        private void OpenGLControl_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear the color and depth buffers
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Set the projection matrix
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(45.0f, (double)Width / (double)Height, 0.1, 100.0);

            // Set the modelview matrix
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.LookAt(0, 0, -5, 0, 0, 0, 0, 1, 0);

            // Render the AutoCAD file
            // Replace "path/to/autocad/file.dwg" with the actual path to your AutoCAD file
            if (File.Exists("Airport.dwg"))
            {
                string path = "Airport.dwg";
                CadDocument doc = DwgReader.Read(path, onNotification);

                var layers = doc.Layers;

                // Iterate through layers and extract data
                foreach (var layer in layers)
                {
                    Console.WriteLine("Layer Name: " + layer.Name);
                    Console.WriteLine("Layer Color: " + layer.Color);
                    // ... Extract other layer properties
                }

            }

        }
        private static void onNotification(object sender, NotificationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
