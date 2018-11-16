using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WpfDataBaseProject_BlancoF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        BlancoDBEntities dataEntities = new BlancoDBEntities();

        ObservableCollection<User> users;


        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            users = new ObservableCollection<User>(dataEntities.Users);
            foreach (User user in users)
            {
                user.Name = user.Name.Trim();
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            this.UserName = NameBox.Text;
            this.Password = PasswordBox.Text;

            var query = from user in users
                        where UserName == user.Name
                        where Password == user.Password
                        select user;

            if (query.Count() > 0)
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: User not found. Try again.");
            }

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.UserName = NameBox.Text;
            this.Password = PasswordBox.Text;
            User newUser = new User() {Name = this.UserName, Password = this.Password };
            dataEntities.Users.Add(newUser);
            dataEntities.SaveChanges();
            MessageBox.Show("User successfully added.");
            users = new ObservableCollection<User>(dataEntities.Users);
            foreach (User user in users)
            {
                user.Name = user.Name.Trim();
            }
        }
    }
}
