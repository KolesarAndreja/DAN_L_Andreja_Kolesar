using DAN_L.Command;
using DAN_L.Service;
using DAN_L.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DAN_L.ViewModel
{
    class AudioPlayerViewModel:ViewModelBase
    {
        AudioPlayer player;
        #region Property
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
                OnPropertyChanged("user");
            }
        }

        private List<tblSong> _playList;
        public List<tblSong> playList
        {
            get
            {
                return _playList;
            }
            set
            {
                _playList = value;
                OnPropertyChanged("playList");
            }
        }

        private tblSong _viewSong;
        public tblSong viewSong
        {
            get
            {
                return _viewSong;
            }
            set
            {
                _viewSong = value;
                OnPropertyChanged("viewSong");
            }
        }
        private bool _isDeletedSong;
        public bool isDeletedSong
        {
            get
            {
                return _isDeletedSong;
            }
            set
            {
                _isDeletedSong = value;
            }
        }
        #endregion
        public AudioPlayerViewModel(AudioPlayer openPlayer, tblUser user)
        {
            player = openPlayer;
            this.user = user;
            playList = Service.Service.GetSongs(user.userId);
        }

        #region LOGOUT
        private ICommand _logOut;
        public ICommand logOut
        {
            get
            {
                if (_logOut == null)
                {
                    _logOut = new Command.RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return _logOut;
            }
        }

        private void LogOutExecute()
        {
            try
            {
                Login log = new Login();
                player.Close();
                log.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogOutExecute()
        {
            return true;
        }

        #endregion

        #region ADD SONG
        private ICommand _addSong;
        public ICommand addSong
        {
            get
            {
                if (_addSong == null)
                {
                    _addSong = new RelayCommand(param => AddNewEmployeeExecute(), param => CanAddNewEmployeeExecute());
                }
                return _addSong;
            }
        }

        private void AddNewEmployeeExecute()
        {
            try
            {
                AddSong adding = new AddSong(user);
                adding.ShowDialog();
                if ((adding.DataContext as AddSongViewModel).isUpdated == true)
                {
                    playList = Service.Service.GetSongs(user.userId);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewEmployeeExecute()
        {
            return true;
        }
        #endregion

        #region delete
        //Open messagebox where user can confirm deleting data
        private ICommand _deleteThisSong;
        public ICommand deleteThisSong
        {
            get
            {
                if (_deleteThisSong == null)
                {
                    _deleteThisSong = new RelayCommand(param => DeleteThisUserExecute(), param => CanDeleteThisUserExecute());

                }
                return _deleteThisSong;
            }
        }

        private void DeleteThisUserExecute()
        {
            //print confirm message
            MessageBoxResult result = MessageBox.Show("Do you realy want to delete this song?", "Delete Song", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Service.Service.DeleteSong(viewSong);
                isDeletedSong = true;
                playList = Service.Service.GetSongs(user.userId);

            }
        }
        private bool CanDeleteThisUserExecute()
        {
            return true;
        }

        #endregion
    }
}
