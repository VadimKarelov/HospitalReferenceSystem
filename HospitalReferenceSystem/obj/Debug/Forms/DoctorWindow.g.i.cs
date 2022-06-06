﻿#pragma checksum "..\..\..\Forms\DoctorWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "43DDD2C9B6861A8545D8A2CCEB06B185DE8AF1BF411A7900EBE2BBCD49B4736F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using HospitalReferenceSystem;
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


namespace HospitalReferenceSystem {
    
    
    /// <summary>
    /// DoctorWindow
    /// </summary>
    public partial class DoctorWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Forms\DoctorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_FIO;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Forms\DoctorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox_PatientChoose;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Forms\DoctorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_FIO;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Forms\DoctorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_LoginPassword;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Forms\DoctorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_WardNumber;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Forms\DoctorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox_Journal;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Forms\DoctorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox_Procedures;
        
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
            System.Uri resourceLocater = new System.Uri("/HospitalReferenceSystem;component/forms/doctorwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Forms\DoctorWindow.xaml"
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
            
            #line 18 "..\..\..\Forms\DoctorWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Logout_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textBlock_FIO = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.comboBox_PatientChoose = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\..\Forms\DoctorWindow.xaml"
            this.comboBox_PatientChoose.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LoadPatientInfo_IndexChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 34 "..\..\..\Forms\DoctorWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPatient_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.textBox_FIO = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.textBlock_LoginPassword = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.textBox_WardNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 72 "..\..\..\Forms\DoctorWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 73 "..\..\..\Forms\DoctorWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.WriteOut_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.listBox_Journal = ((System.Windows.Controls.ListBox)(target));
            return;
            case 11:
            this.listBox_Procedures = ((System.Windows.Controls.ListBox)(target));
            return;
            case 12:
            
            #line 107 "..\..\..\Forms\DoctorWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MarkProcedure);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 113 "..\..\..\Forms\DoctorWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddProcedure_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

