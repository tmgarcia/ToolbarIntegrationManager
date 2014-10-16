﻿#pragma checksum "..\..\Sliders.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F0EABBF369DB0CA1515477F05D7FA0DF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ScrapProject.UserControls;
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


namespace ScrapProject {
    
    
    /// <summary>
    /// Sliders
    /// </summary>
    public partial class Sliders : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\Sliders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle ColorDisplay;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Sliders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.SolidColorBrush ColorDisplayBrush;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Sliders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SliderSelector;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Sliders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ScrapProject.UserControls.RGBASliders rgbaSliders;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Sliders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ScrapProject.UserControls.HSLASliders hslaSliders;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Sliders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ScrapProject.UserControls.HSBASliders hsbaSliders;
        
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
            System.Uri resourceLocater = new System.Uri("/ScrapProject;component/sliders.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Sliders.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.ColorDisplay = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 2:
            this.ColorDisplayBrush = ((System.Windows.Media.SolidColorBrush)(target));
            return;
            case 3:
            this.SliderSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 18 "..\..\Sliders.xaml"
            this.SliderSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SliderSelector_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.rgbaSliders = ((ScrapProject.UserControls.RGBASliders)(target));
            return;
            case 5:
            this.hslaSliders = ((ScrapProject.UserControls.HSLASliders)(target));
            return;
            case 6:
            this.hsbaSliders = ((ScrapProject.UserControls.HSBASliders)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

