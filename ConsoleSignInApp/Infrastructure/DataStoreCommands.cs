using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSignInApp.Entities;
namespace ConsoleSignInApp.Infrastructure
{
    public class DataStoreCommands : IDataStoreCommands
    {
        DataStore db;

        public DataStoreCommands()
        {
            db = DataStore.Instance();
        }

        public bool AddUser(UserCredentials user)
        {
            try
            {
                string userValue = $"{user.UserId} : {user.Password}";
                //GUID is faking Unique ID field in data store
                db.Repo.Add(Guid.NewGuid().ToString(), userValue);
                return true;
            }
            catch (Exception ex)
            {
                //ToDo : Log Error and Param Values
                var error = $"DataStore.AddUser -  User: {user.UserId} PWD: {user.Password} ErrorMessage: {ex.Message}";
                return false;
            }
        }

        public bool SessionInsert(string UserId)
        {
            try
            {
                string UserValue = $"{UserId} Time:{ DateTime.Now.ToShortTimeString()}";
                db.Sessions.Add(UserId, UserValue);
                return true;
            }
            catch (Exception ex)
            {
                //ToDo : Log Error and Param Values
                var error = $"DataStore.InsertToSession -  UserId: {UserId} ErrorMessage: {ex.Message}";
                return false;
            }
        }

        public bool SessionRemove(string UserId)
        {
            try
            {
                db.Sessions.Remove(UserId);
                return true;
            }
            catch(Exception ex)
            {
                var err = ex.Message;
                return false; 
            }
        }

        public bool UpdateUser(UserCredentials userCredentialsOld, UserCredentials userCredentialsNew)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                //ToDo : Log Error and Param Values
                var error = ex.ToString();
                return false;
            }
        }

    }
}
