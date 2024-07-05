﻿#pragma checksum "..\..\..\..\..\MVVM\View\OrderItemView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0A8422EEBE7861407EF9D5277998DF85DA020A68"
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
using POMT_WPF.MVVM.View.Controls;
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
    /// OrderItemView
    /// </summary>
    public partial class OrderItemView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 39 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackButton;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton FreezeToggleButton;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton EditToggleButton;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border RecipientErrBdr;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border PhoneErrBdr;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border DatePickerErrBdr;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker OrderDatePicker;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border TimeErrBdr;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border DelAddrErrBdr;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border OrderTypeErrBdr;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox OrderTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border FulfillmentErrBdr;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FulfillmentTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border FrequencyErrBdr;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox OrderFrequencyTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 239 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid dataGridGrid;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border LineItemsErrBdr;
        
        #line default
        #line hidden
        
        
        #line 245 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid orderFormDataGrid;
        
        #line default
        #line hidden
        
        
        #line 257 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn orderFormTemplate;
        
        #line default
        #line hidden
        
        
        #line 364 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddLineButton;
        
        #line default
        #line hidden
        
        
        #line 377 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteLineButton;
        
        #line default
        #line hidden
        
        
        #line 466 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveOrderButton;
        
        #line default
        #line hidden
        
        
        #line 473 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteOrderButton;
        
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
            System.Uri resourceLocater = new System.Uri("/POMT_WPF;V1.0.0.0;component/mvvm/view/orderitemview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
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
            this.BackButton = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.FreezeToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 3:
            this.EditToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 4:
            this.RecipientErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            
            #line 88 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            ((System.Windows.Controls.TextBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.Recipient_GotFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PhoneErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            
            #line 107 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            ((System.Windows.Controls.TextBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.Phone_GotFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DatePickerErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.OrderDatePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 129 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            this.OrderDatePicker.GotFocus += new System.Windows.RoutedEventHandler(this.OrderDatePicker_GotFocus);
            
            #line default
            #line hidden
            return;
            case 10:
            this.TimeErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 11:
            
            #line 141 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            ((System.Windows.Controls.TextBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.Phone_GotFocus);
            
            #line default
            #line hidden
            return;
            case 12:
            this.DelAddrErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 13:
            
            #line 153 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            ((System.Windows.Controls.TextBox)(target)).GotFocus += new System.Windows.RoutedEventHandler(this.Time_GotFocus);
            
            #line default
            #line hidden
            return;
            case 14:
            this.OrderTypeErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 15:
            this.OrderTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 174 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            this.OrderTypeComboBox.GotFocus += new System.Windows.RoutedEventHandler(this.OrderTypeComboBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 16:
            this.FulfillmentErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 17:
            this.FulfillmentTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 193 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            this.FulfillmentTypeComboBox.GotFocus += new System.Windows.RoutedEventHandler(this.FulfillmentTypeComboBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 18:
            this.FrequencyErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 19:
            this.OrderFrequencyTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 212 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            this.OrderFrequencyTypeComboBox.GotFocus += new System.Windows.RoutedEventHandler(this.OrderFrequencyTypeComboBox_GotFocus);
            
            #line default
            #line hidden
            return;
            case 20:
            this.dataGridGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 21:
            this.LineItemsErrBdr = ((System.Windows.Controls.Border)(target));
            return;
            case 22:
            this.orderFormDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 251 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            this.orderFormDataGrid.GotFocus += new System.Windows.RoutedEventHandler(this.orderFormDataGrid_GotFocus);
            
            #line default
            #line hidden
            return;
            case 23:
            this.orderFormTemplate = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 25:
            this.AddLineButton = ((System.Windows.Controls.Button)(target));
            return;
            case 26:
            this.DeleteLineButton = ((System.Windows.Controls.Button)(target));
            return;
            case 27:
            this.SaveOrderButton = ((System.Windows.Controls.Button)(target));
            return;
            case 28:
            this.DeleteOrderButton = ((System.Windows.Controls.Button)(target));
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
            case 24:
            
            #line 271 "..\..\..\..\..\MVVM\View\OrderItemView.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ItemNameTextBox_TextChanged);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

