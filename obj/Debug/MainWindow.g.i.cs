﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "52CC2511BE744D411844A4EAD80F5235D755703AF984AC412C558F46D09741A7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NowDisplaying;
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


namespace NowDisplaying {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbMinimizeOnStart;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbStartOnWindows;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxOptions;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbRedirectUri;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbClientId;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbClientSecret;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConnectToSpotify;
        
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
            System.Uri resourceLocater = new System.Uri("/NowDisplaying;component/mainwindow.xaml", System.UriKind.Relative);
            
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
            this.cbMinimizeOnStart = ((System.Windows.Controls.CheckBox)(target));
            
            #line 40 "..\..\MainWindow.xaml"
            this.cbMinimizeOnStart.Checked += new System.Windows.RoutedEventHandler(this.cbMinimizeOnStart_Checked);
            
            #line default
            #line hidden
            
            #line 41 "..\..\MainWindow.xaml"
            this.cbMinimizeOnStart.Unchecked += new System.Windows.RoutedEventHandler(this.cbMinimizeOnStart_Unchecked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbStartOnWindows = ((System.Windows.Controls.CheckBox)(target));
            
            #line 47 "..\..\MainWindow.xaml"
            this.cbStartOnWindows.Checked += new System.Windows.RoutedEventHandler(this.cbStartOnWindows_Checked);
            
            #line default
            #line hidden
            
            #line 48 "..\..\MainWindow.xaml"
            this.cbStartOnWindows.Unchecked += new System.Windows.RoutedEventHandler(this.cbStartOnWindows_Unchecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.comboBoxOptions = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.tbRedirectUri = ((System.Windows.Controls.TextBox)(target));
            
            #line 79 "..\..\MainWindow.xaml"
            this.tbRedirectUri.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbRedirectUri_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbClientId = ((System.Windows.Controls.TextBox)(target));
            
            #line 84 "..\..\MainWindow.xaml"
            this.tbClientId.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbClientId_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbClientSecret = ((System.Windows.Controls.TextBox)(target));
            
            #line 89 "..\..\MainWindow.xaml"
            this.tbClientSecret.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbClientSecret_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnConnectToSpotify = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\MainWindow.xaml"
            this.btnConnectToSpotify.Click += new System.Windows.RoutedEventHandler(this.btnConnectToSpotify_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

