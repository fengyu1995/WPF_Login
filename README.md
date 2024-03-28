这是一个Wpf登录程序的跟学习案例

根据MvvM编写





M:LoginModel.cs

V：MainWindown.cs

VM:LoginVM.cs

## 一、绑定变量

xaml文件

绑定 UserName变量

```xaml
 <TextBox Text="{Binding UserName}" Grid.Column="1"  Margin="2"/>
```



LoginVM.cs中提供变量并实现接口调用

```c#
  internal class LoginVM:INotifyPropertyChanged{
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
        
        
        ///变量调用
        public string UserName
        {
			get { return _LoginM.UserName; }
			set { 
				_LoginM.UserName = value;
				RaisePropertyChanged("UserName");
            }
		}
  }
```

## 二、绑定事件

xaml文件Command绑定事件

```xaml
            <local:CustomButton ButtonCornerRadius="5" Content="登录" BackgroundHover="#0078d4" BackgroundPressed="Gray" Background="CadetBlue" Foreground="#ffffff" Grid.Row="3" Grid.ColumnSpan="2" Command="{Binding LoginAction}" />
```



LoginVM.cs中提供事件并实现接口调用，需要创建依赖文件RelayCommand.cs

```c#
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
		///调用
        public ICommand LoginAction
        {
            get
            {
                return new RelayCommand(LoginFunc, CanLoginExeture);
            }
        }
```



依赖文件RelayCommand.cs

```c#
    internal class RelayCommand:ICommand
    {

        /// <summary>
        /// 命令是否能够执行
        /// </summary>
        readonly Func<bool> _canExecute;

        /// <summary>
        /// 命令需要执行的方法
        /// </summary>
        readonly Action _execute;

        public RelayCommand(Action action, Func<bool> canExecute)
        {          
            _execute = action;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute();
        }



        public void Execute(object parameter)
        {
            _execute();
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }
    }
```

## 三、自定义Button

创建文件CustomButton.cs

```c#
    public class CustomButton:Button 
    {
        //依赖属性：使用时加载

		///圆角属性
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(CustomButton));


		//鼠标悬停的颜色
        public Brush BackgroundHover
        {
            get { return (Brush)GetValue(BackgroundHoverProperty); }
            set { SetValue(BackgroundHoverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundHover.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundHoverProperty =
            DependencyProperty.Register("BackgroundHover", typeof(Brush), typeof(CustomButton));



		//鼠标按下的颜色
        public Brush BackgroundPressed
        {
            get { return (Brush)GetValue(BackgroundPressedProperty); }
            set { SetValue(BackgroundPressedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundPressed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundPressedProperty =
            DependencyProperty.Register("BackgroundPressed", typeof(Brush), typeof(CustomButton));


    }
```



创建资源文件CustomButtonStyles.xaml

```xaml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:bb="clr-namespace:WPF_Login">
    <Style TargetType="{x:Type bb:CustomButton}">
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type bb:CustomButton}">
                    <Border x:Name="buttonBorder" Background="{TemplateBinding Background}"  CornerRadius="{TemplateBinding ButtonCornerRadius}" >
                        <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
                    </Border>   
                    
                    <!--触发器-->
                    <ControlTemplate.Triggers>
                        <Trigger Property="IsMouseOver" Value="True">
                            <Setter TargetName="buttonBorder" Property="Background" Value="{Binding BackgroundHover,RelativeSource={RelativeSource TemplatedParent}}"/>
                        </Trigger>
                        <Trigger Property="IsPressed" Value="True">
                            <Setter TargetName="buttonBorder" Property="Background" Value="{Binding BackgroundPressed,RelativeSource={RelativeSource TemplatedParent}}"/>
                        </Trigger>
                    </ControlTemplate.Triggers>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>            
</ResourceDictionary>
```

全局应用CustomButtonStyles.xaml文件在App.xaml添加

```xaml
<Application x:Class="WPF_Login.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WPF_Login"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="CustomButtonStyles.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```



在页面使用ButtonCornerRadius、BackgroundHover和BackgroundPressed自定义属性

```xaml
 <local:CustomButton ButtonCornerRadius="5" Content="登录" BackgroundHover="#0078d4" BackgroundPressed="Gray" Background="CadetBlue" Foreground="#ffffff" Grid.Row="3" Grid.ColumnSpan="2" Command="{Binding LoginAction}" />
```






来源：https://www.bilibili.com/video/BV13D4y1u7XX/?p=24&spm_id_from=pageDriver&vd_source=7d45fb6ad2fc4325ad917878f44118e8

