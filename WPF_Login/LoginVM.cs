using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Login
{
    internal class LoginVM: DependencyObject,INotifyPropertyChanged
    {
        private Window _main;
        public LoginVM(Window main)
        {
            _main=main;
        }

		private LoginModel _LoginM=new LoginModel();
        #region 旧代码
        //public LoginModel LoginM
        //      {
        //	get { 
        //		if(_LoginM == null)
        //			_LoginM = new LoginModel();
        //		return _LoginM; 
        //	}
        //	set { 
        //		_LoginM = value;
        //		RaisePropertyyChanged("LoginM");
        //          }
        //}

        //      public string UserName
        //      {
        //	get { return _LoginM.UserName; }
        //	set { 
        //		_LoginM.UserName = value;
        //		RaisePropertyChanged("UserName");
        //          }
        //}
        #endregion



        //在这里修改依赖属性,界面绑定的变量是赋值是从依赖属性中的get中的GetValue获得的,
        //变量的改变是从set中的依赖属性中的set中的SetValue同步到界面的,和正常属性的get set的赋值操作是相反的
        //

        public string UserName
        {
            get {

                _LoginM.UserName = (string)GetValue(UserNameProperty);
                return _LoginM.UserName;

            }
            set {
                _LoginM.UserName = value;
                SetValue(UserNameProperty, _LoginM.UserName);
            }
        }

        // Using a DependencyProperty as the backing store for UserName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(LoginVM));




        public string Password
        {
            get
            {
                _LoginM.Password = (string)GetValue(PasswordProperty);
                return _LoginM.Password;
            }
            set
            {
                _LoginM.Password = value;
                SetValue(PasswordProperty, _LoginM.Password);
            }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(LoginVM));


        #region 旧代码
        //public string Password
        //{
        //    get { return _LoginM.Password; }
        //    set
        //    {
        //        _LoginM.Password = value;
        //        RaisePropertyChanged("Password");
        //    }
        //}
        #endregion




        public void LoginFunc()
        {
            if (UserName == "wpf" && Password == "123")
            {
                Index index = new Index();
                index.Show();

                _main.Hide();
            }
            else
            {
                MessageBox.Show("账号或密码错误，请重新输入");
                UserName = "";
                Password = "";
            }
        }

        bool CanLoginExeture()
        {
            return true;
        }

        public ICommand LoginAction
        {
            get
            {
                return new RelayCommand(LoginFunc, CanLoginExeture);
            }
        }


        #region 功能代码
        /// <summary>
        /// 变量通知页面接口
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion




    }
}
