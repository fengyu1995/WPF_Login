using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Login
{
    public class PasswordHelpr
    {



        public static string GetMyPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(MyPasswordProperty);
        }

        public static void SetMyPassword(DependencyObject obj, string value)
        {
            obj.SetValue(MyPasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPasswordProperty =
            DependencyProperty.RegisterAttached("MyPassword", typeof(string), typeof(PasswordHelpr),new PropertyMetadata(string.Empty,OnMyPwdChanged));

        private static void OnMyPwdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;
            if (box == null)
                return;
                box.Password = (string)e.NewValue;

            SetSelection(box, box.Password.Length,0);
        }

        /// <summary>
        /// 控制光标位置
        /// </summary>
        /// <param name="passwordBox"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public static void SetSelection(PasswordBox passwordBox,int start,int length)
        {
            passwordBox.GetType()
                       .GetMethod("Select",BindingFlags.Instance|BindingFlags.NonPublic)
                       .Invoke(passwordBox, new object[] { start, length });
        }


        public static bool GetIsEnablePasswordPropertyChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnablePasswordPropertyChangedProperty);
        }

        public static void SetIsEnablePasswordPropertyChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnablePasswordPropertyChangedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnablePasswordPropertyChanged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnablePasswordPropertyChangedProperty =
            DependencyProperty.RegisterAttached("IsEnablePasswordPropertyChanged", typeof(bool), typeof(PasswordHelpr), new PropertyMetadata(false, OnPasswordPropertyChanged));



        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox pwd = d as PasswordBox;
            if(pwd ==null)
                return;
            if((bool)e.NewValue)
                pwd.PasswordChanged += MyPasswordPropertyChanged;
            else 
                pwd.PasswordChanged -= MyPasswordPropertyChanged;
        }

        private static void MyPasswordPropertyChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = (PasswordBox)sender;

            SetMyPassword(box, box.Password);
        }
    }
}
