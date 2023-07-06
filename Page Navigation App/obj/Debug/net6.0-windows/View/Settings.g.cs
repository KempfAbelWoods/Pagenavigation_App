﻿#pragma checksum "..\..\..\..\View\Settings.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4142CE05AA3D3F5AD5039658760E11CD452DC09C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Page_Navigation_App.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Page_Navigation_App.View {
    
    
    /// <summary>
    /// Settings
    /// </summary>
    public partial class Settings : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\..\View\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PDFPaths;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\View\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Vorlagepath;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\View\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SocketIpAddress;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\View\Settings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Code;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Page Navigation App;component/view/settings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Settings.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 39 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Set_BillPath);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PDFPaths = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 41 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Set_ModelPdf);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Vorlagepath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 43 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PreviewPDF);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 44 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Set_SocketIp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SocketIpAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 46 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Get_Code);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Code = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 62 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Table_Settings);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 63 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Table_Customer);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 64 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Table_Order);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 65 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Table_Ressource);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 66 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Table_Users);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 67 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Table_Tasks);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 68 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Finish_order);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 69 "..\..\..\..\View\Settings.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Start_Server);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

