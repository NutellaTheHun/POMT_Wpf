﻿#pragma checksum "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FEC9D046D3C448C285704181B51C5B911E2925CC"
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
using POMT_WPF.Converters;
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
    /// CatalogItemView
    /// </summary>
    public partial class CatalogItemView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ItemNameErrBdr;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ItemNameComboBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ItemNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox AltNameListBox;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ItemSizesErrBdr;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border CategoryNameErrBdr;
        
        #line default
        #line hidden
        
        
        #line 290 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveOrderButton;
        
        #line default
        #line hidden
        
        
        #line 296 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.IconPacks.PackIconBoxIcons SaveCheckMark;
        
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
            System.Uri resourceLocater = new System.Uri("/POMT_WPF;component/mvvm/view/catalogitemview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
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
            
            #line 75 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
            this.ItemNameTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AltNameListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.ItemSizesErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            
            #line 131 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
            ((System.Windows.Controls.StackPanel)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.StackPanel_GotFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CategoryNameErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            
            #line 203 "..\..\..\..\..\..\..\MVVM\View\CatalogItemView.xaml"
            ((System.Windows.Controls.ComboBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.ComboBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SaveOrderButton = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.SaveCheckMark = ((MahApps.Metro.IconPacks.PackIconBoxIcons)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

