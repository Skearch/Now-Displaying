﻿#pragma checksum "..\..\TrackDisplayWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C1A84FB2B2CFC1F2D261844203E3F026E67658474E2F6E3418F65B7F52A6B5CE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    /// TrackDisplayWindow
    /// </summary>
    public partial class TrackDisplayWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image pbAlbumCoverBackground;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform backgroundImageScaleTransform;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TranslateTransform backgroundImageTransform;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel trackInfoPanel1;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image pbAlbumCover;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel trackInfoPanel2;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblSongName;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\TrackDisplayWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblSongArtists;
        
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
            System.Uri resourceLocater = new System.Uri("/NowDisplaying;component/trackdisplaywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TrackDisplayWindow.xaml"
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
            this.pbAlbumCoverBackground = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.backgroundImageScaleTransform = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 3:
            this.backgroundImageTransform = ((System.Windows.Media.TranslateTransform)(target));
            return;
            case 4:
            this.trackInfoPanel1 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.pbAlbumCover = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.trackInfoPanel2 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.lblSongName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.lblSongArtists = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
