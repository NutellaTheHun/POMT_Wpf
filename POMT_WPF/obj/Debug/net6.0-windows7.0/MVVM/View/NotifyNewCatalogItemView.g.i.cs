﻿#pragma checksum "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "469075C233780D16F0BA3F5328C7586E98A3F554"
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
    /// NotifyNewCatalogItemView
    /// </summary>
    public partial class NotifyNewCatalogItemView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeBtn;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button okBtn;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button viewItemBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/POMT_WPF;V1.0.0.0;component/mvvm/view/notifynewcatalogitemview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml"
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
            this.closeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml"
            this.closeBtn.Click += new System.Windows.RoutedEventHandler(this.closeBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.okBtn = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml"
            this.okBtn.Click += new System.Windows.RoutedEventHandler(this.okBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.viewItemBtn = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\..\..\..\MVVM\View\NotifyNewCatalogItemView.xaml"
            this.viewItemBtn.Click += new System.Windows.RoutedEventHandler(this.viewItemBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

