using DAN_L.Command;
using DAN_L.Model;
using DAN_L.Service;
using DAN_L.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_L.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        Login login;
        private User _currentUser;
        public User currentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged("currentUser");
            }
        }

        public LoginViewModel(Login openLogin)
        {
            login = openLogin;
            currentUser = new User();
        }

        #region Commands
        private ICommand _loginBtn;
        public ICommand loginBtn
        {
            get
            {
                if (_loginBtn == null)
                {
                    _loginBtn = new RelayCommand(LoginExecute, CanLoginExecute);
                }
                return _loginBtn;
            }
        }

        public bool ValidPassword(string psw)
        {
            if (psw.Length < 6)
            {
                return false;
            }
            else
            {
                int counter = 0;

                char[] arr = psw.ToCharArray();
                foreach(char c in arr)
                {
                    if (c>=65 && c <= 90)
                    {
                        counter++;
                    }
                }
                if (counter < 2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void LoginExecute(object obj)
        {
            currentUser.password = (obj as PasswordBox).Password;
            try
            {
                bool validPsw = ValidPassword(currentUser.password);
                if (!validPsw)
                {
                    MessageBox.Show("Password must contain at least 6 characters including 2 uppercase. Try again");
                }
                else
                {
                    bool registeredBefore = Service.Service.IsRegisteredUser(currentUser.username);
                    if (registeredBefore)
                    {
                        tblUser u = Service.Service.GetUserByUsernameAndPsw(currentUser.username, currentUser.password);
                        if (u == null)
                        {
                            MessageBox.Show("Invalid password. Try again");
                        }
                        else
                        {
                            AudioPlayer player = new AudioPlayer(u);
                            login.Close();
                            player.ShowDialog();
                        }
                    }
                    else
                    {
                        tblUser newUser = Service.Service.AddUser(currentUser.username, currentUser.password);
                        AudioPlayer player = new AudioPlayer(newUser);
                        login.Close();
                        player.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLoginExecute(object obj)
        {
            return true;
        }
        #endregion

    }
}
