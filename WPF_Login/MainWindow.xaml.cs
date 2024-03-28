using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Login
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 旧代码
        //LoginModel loginModel;
        //LoginVM loginVM;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region 旧代码
            //loginModel=new LoginModel();    
            //this.DataContext = loginModel;
            //loginVM = new LoginVM(this);
            //this.DataContext = loginVM;
            #endregion


            this.DataContext = new LoginVM(this);
        }



        #region 旧代码
        //    private void Button_Click(object sender, RoutedEventArgs e)
        //    {
        //        //if (loginVM.UserName == "wpf" && loginVM.Password == "123")
        //        //{
        //        //    Index index = new Index();
        //        //    index.Show();

        //        //    this.Hide();
        //        //}
        //        //else
        //        //{
        //        //    MessageBox.Show("账号或密码错误，请重新输入");
        //        //    loginVM.UserName = "";
        //        //    loginVM.Password = "";
        //        //    #region 旧代码
        //        //    //loginVM.LoginM = loginVM.LoginM;
        //        //    #endregion
        //        //}
        //    }
        //public class LoginModel: INotifyPropertyChanged
        //{
        //    public event PropertyChangedEventHandler PropertyChanged;
        //    private void RaisePropertyChanged(string propertyName)
        //    {
        //        PropertyChangedEventHandler handler = PropertyChanged;
        //        if (handler != null)
        //            handler(this, new PropertyChangedEventArgs(propertyName));
        //    }

        //    private string _UserName;

        //    public string UserName
        //    {
        //        get { return _UserName; }
        //        set
        //        {
        //            _UserName = value;
        //            RaisePropertyyChanged("UserName");
        //        }
        //    }
        //    private string _Password;

        //    public string Password
        //    {
        //        get { return _Password; }
        //        set
        //        {
        //            _Password = value;
        //            RaisePropertyyChanged("Password");
        //        }
        //    }
        //}
        #endregion


    }
}