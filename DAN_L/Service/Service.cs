using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_L.Service
{
    class Service
    {
        #region READ SONGS
        public static List<tblSong> GetSongs(int id)
        {
            try
            {
                using (dbPlayerEntities context = new dbPlayerEntities())
                {
                    List<tblSong> list = new List<tblSong>();
                    list = (from x in context.tblSongs where x.userId ==id select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        #endregion
       
        #region DELETE SONG
        public static void DeleteReport(tblSong song)
        {
            try
            {
                using (dbPlayerEntities context = new dbPlayerEntities())
                {
                    tblSong songToDelete
 = (from u in context.tblSongs where u.songId == song.songId select u).First();
                    context.tblSongs.Remove(songToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        #endregion

        #region GET USER ID
        public static tblUser GetUserByUsernameAndPsw(string username, string password)
        {
            try
            {
                using (dbPlayerEntities context = new dbPlayerEntities())
                {
                    tblUser result = (from x in context.tblUsers where x.username == username && x.password == password select x).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region CHECKING
        public static bool IsRegisteredUser(string username)
        {
            try
            {
                using (dbPlayerEntities context = new dbPlayerEntities())
                {
                    bool result = (from x in context.tblUsers where x.username == username select x).Any();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }
        #endregion

        #region ADD USER

        public static tblUser AddUser(string username, string password)
        {
            try
            {
                using (dbPlayerEntities context = new dbPlayerEntities())
                {
                    tblUser user = new tblUser();
                    user.username = username;
                    user.password = password;
                    context.tblUsers.Add(user);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region ADD SONG
        public static tblSong AddNewSong(tblSong song)
        {
            try
            {
                using (dbPlayerEntities context = new dbPlayerEntities())
                {
                    tblSong newSong = new tblSong();
                    newSong.author = song.author;
                    newSong.duration = song.duration;
                    newSong.name = song.name;
                    newSong.userId = song.userId;
                    context.tblSongs.Add(newSong);
                    context.SaveChanges();
                    song.songId = newSong.songId;
                    return song;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion
    }
}
