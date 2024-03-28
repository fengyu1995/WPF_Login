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
    internal class LoginVM:INotifyPropertyChanged
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
        #endregion


        public string UserName
        {
			get { return _LoginM.UserName; }
			set { 
				_LoginM.UserName = value;
				RaisePropertyChanged("UserName");
            }
		}

        public string Password
        {
            get { return _LoginM.Password; }
            set
            {
                _LoginM.Password = value;
                RaisePropertyChanged("Password");
            }
        }


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
