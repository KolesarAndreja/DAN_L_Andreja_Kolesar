using DAN_L.Command;
using DAN_L.Service;
using DAN_L.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace DAN_L.ViewModel
{
    class AddSongViewModel:ViewModelBase
    {
        AddSong addSong;

        private tblSong _newSong;
        public tblSong newSong
        {
            get
            {
                return _newSong; ;
            }
            set
            {
                _newSong = value;
                OnPropertyChanged("newSong");
            }
        }
        private tblUser _user;
        public tblUser user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        private bool _isUpdated = false;
        public bool isUpdated
        {
            get
            {
                return _isUpdated;
            }
            set
            {
                _isUpdated = value;
            }
        }

        #region constructor
        public AddSongViewModel(AddSong addSongOpen, tblUser user)
        {
            addSong = addSongOpen;
            this.user = user;
            newSong = new tblSong();
        }

        #endregion
        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return _save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                newSong.userId = user.userId;
                tblSong s = Service.Service.AddNewSong(newSong);
                if (s != null)
                {
                    isUpdated = true;
                    MessageBox.Show("Song has been succesfully added!");
                    addSong.Close();
                    
                }
                else
                {
                    MessageBox.Show("Error.Try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (!String.IsNullOrEmpty(newSong.name) && !String.IsNullOrEmpty(newSong.author) && newSong.duration > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
