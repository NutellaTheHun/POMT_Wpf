﻿#pragma checksum "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C933A1EAF27CE111DD29349DAF7D1435D5160199"
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
using POMT_WPF.MVVM.Other;
using POMT_WPF.MVVM.View.Controls;
using POMT_WPF.MVVM.ViewModel;
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
    /// PetsiOrderWindow
    /// </summary>
    public partial class PetsiOrderWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 70 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox recipientTextBox;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton PickupRadioButton;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton DeliveryRadioButton;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox DeliveryAddressTextBox;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox OrderTypeTextBox;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton WeeklyRadioButton;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton OneTimeRadioButton;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox orderDateTextBox;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox orderTimeTextBox;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox phoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal POMT_WPF.MVVM.View.Controls.TextFillTextBox emailTextBox;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid orderFormDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/POMT_WPF;component/mvvm/view/petsiorderwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
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
            case 1:
            
            #line 45 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindow_ButtonClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.recipientTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 3:
            this.PickupRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.DeliveryRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.DeliveryAddressTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 6:
            this.OrderTypeTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 7:
            this.WeeklyRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.OneTimeRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 9:
            this.orderDateTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 10:
            this.orderTimeTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 11:
            this.phoneTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 12:
            this.emailTextBox = ((POMT_WPF.MVVM.View.Controls.TextFillTextBox)(target));
            return;
            case 13:
            this.orderFormDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 14:
            
            #line 239 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLineItem_BtnClk);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 266 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfirmCloseWin_BtnClk);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 272 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindow_ButtonClick);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 281 "..\..\..\..\..\MVVM\View\PetsiOrderWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_BtnClk);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

