﻿#pragma checksum "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B69DE3C6C4748B7D8E7B41FBC2C9C6F9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
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
using TestProject.Converters;
using TestProject.UserControls;


namespace TestProject.Tools {
    
    
    /// <summary>
    /// Colors_ValuesDisplay
    /// </summary>
    public partial class Colors_ValuesDisplay : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TestProject.Tools.Colors_ValuesDisplay ucval;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TestProject.UserControls.BaseTool_Expandable expandable;
        
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
            System.Uri resourceLocater = new System.Uri("/TestProject;component/tools/colors/colors_valuesdisplay.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
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
            this.ucval = ((TestProject.Tools.Colors_ValuesDisplay)(target));
            return;
            case 2:
            this.expandable = ((TestProject.UserControls.BaseTool_Expandable)(target));
            return;
            case 3:
            
            #line 38 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.previewTextInput_NumsOnlyInt);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.R_TextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 43 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.previewTextInput_NumsOnlyInt);
            
            #line default
            #line hidden
            
            #line 44 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.G_TextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 48 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.previewTextInput_NumsOnlyInt);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.B_TextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 53 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.previewTextInput_NumsOnlyInt);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.A_TextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 58 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.previewTextInput_NumsOnlyDec);
            
            #line default
            #line hidden
            
            #line 59 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.H_TextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 63 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.previewTextInput_NumsOnlyDec);
            
            #line default
            #line hidden
            
            #line 64 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.S_TextInput);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 68 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.previewTextInput_NumsOnlyDec);
            
            #line default
            #line hidden
            
            #line 69 "..\..\..\..\Tools\Colors\Colors_ValuesDisplay.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.B1_TextInput);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

