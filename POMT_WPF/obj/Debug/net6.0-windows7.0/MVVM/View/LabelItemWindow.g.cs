﻿#pragma checksum "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "50AA863BA5C51C64305E557D4079B3BEBD94C4FB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using POMT_WPF.MVVM.View;
using POMT_WPF.Validation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace POMT_WPF.MVVM.View {
    
    
    /// <summary>
    /// LabelItemWindow
    /// </summary>
    public partial class LabelItemWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ItemNameErrBdr;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ItemNameComboBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteStandardLabel;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteCutieLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/POMT_WPF;component/mvvm/view/labelitemwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ItemNameErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.ItemNameComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.ItemNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\..\..\MVVM\View\LabelItemWindow.xaml"
            this.ItemNameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ItemNameTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.deleteStandardLabel = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.deleteCutieLabel = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

