﻿#pragma checksum "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "328C7D21EFAB001732B6B3F63AEB435E8D8F1AEB"
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
using POMT_WPF.MVVM.Other;
using POMT_WPF.MVVM.View.Controls;
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
    /// TemplateViewWindow
    /// </summary>
    public partial class TemplateViewWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 67 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox templateNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MoveItemUpBtn;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MoveItemDownBtn;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid templateViewDataGrid;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn orderFormTemplate;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteItemButton;
        
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
            System.Uri resourceLocater = new System.Uri("/POMT_WPF;V1.0.0.0;component/mvvm/view/templateviewwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            case 2:
            
            #line 58 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindow_ButtonClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.templateNameTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 4:
            this.MoveItemUpBtn = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            this.MoveItemUpBtn.Click += new System.Windows.RoutedEventHandler(this.MoveItemUpBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MoveItemDownBtn = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            this.MoveItemDownBtn.Click += new System.Windows.RoutedEventHandler(this.MoveItemDownBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.templateViewDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.orderFormTemplate = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 8:
            
            #line 146 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLineItem_BtnClk);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DeleteItemButton = ((System.Windows.Controls.Button)(target));
            
            #line 158 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            this.DeleteItemButton.Click += new System.Windows.RoutedEventHandler(this.DeleteItemButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 177 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfirmCloseWin_BtnClk);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 182 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindow_ButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 30 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ItemNameTextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 31 "..\..\..\..\..\MVVM\View\TemplateViewWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.itemNameComboBox_SelectionChanged);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

