﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BBCE8482D72176073A80FD68F30BE9C3F1028027"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PZ1;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PZ1 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid buttonGrid;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoadModel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEllipse;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPolygon;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnText;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUndo;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRedo;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClear;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollViewer;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform SkaliranjeTransform;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid networkGraph;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PZ1;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.buttonGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.btnLoadModel = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\MainWindow.xaml"
            this.btnLoadModel.Click += new System.Windows.RoutedEventHandler(this.LoadModel_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnEllipse = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\MainWindow.xaml"
            this.btnEllipse.Click += new System.Windows.RoutedEventHandler(this.Shape_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnPolygon = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\MainWindow.xaml"
            this.btnPolygon.Click += new System.Windows.RoutedEventHandler(this.Shape_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnText = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\MainWindow.xaml"
            this.btnText.Click += new System.Windows.RoutedEventHandler(this.Shape_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnUndo = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\MainWindow.xaml"
            this.btnUndo.Click += new System.Windows.RoutedEventHandler(this.Undo_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnRedo = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\MainWindow.xaml"
            this.btnRedo.Click += new System.Windows.RoutedEventHandler(this.Redo_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnClear = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\MainWindow.xaml"
            this.btnClear.Click += new System.Windows.RoutedEventHandler(this.Clear_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.slider = ((System.Windows.Controls.Slider)(target));
            return;
            case 10:
            this.scrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 96 "..\..\MainWindow.xaml"
            this.scrollViewer.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 97 "..\..\MainWindow.xaml"
            this.scrollViewer.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 101 "..\..\MainWindow.xaml"
            this.canvas.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 102 "..\..\MainWindow.xaml"
            this.canvas.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 12:
            this.SkaliranjeTransform = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 13:
            this.networkGraph = ((System.Windows.Controls.Grid)(target));
            
            #line 110 "..\..\MainWindow.xaml"
            this.networkGraph.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 110 "..\..\MainWindow.xaml"
            this.networkGraph.MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
