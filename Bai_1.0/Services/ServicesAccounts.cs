using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai_1._0.Context;
using Bai_1._0.Models;

namespace Bai_1._0
{
    class ServicesAccounts : IServices.IServices
    {
        private DatabaseContext _dbcontext;
        List<Account> _lstAccount;
        public ServicesAccounts()
        {
            _dbcontext = new DatabaseContext();
            _lstAccount = new List<Account>();
        }

        public List<Account> GetList()
        {
            _lstAccount = _dbcontext.Accounts.ToList();
            return _lstAccount;
        }

        public List<Account> ResultList()
        {
            return _lstAccount;
        }

        public string AddAccount(Account Acc)
        {
            try
            {
                _dbcontext.Accounts.Add(Acc);
                _lstAccount.Add(Acc);
                return "thêm Thành công.";
            }
            catch
            {
                return "string AddAccount(Account Acc) lỗi !!!";
            }
        }

        public string DeleteAccount(Account Acc)
        {
            try
            {
                _dbcontext = new DatabaseContext();
                _dbcontext.Accounts.Remove(Acc);
                _lstAccount.Remove(Acc);
                return "Done";
            }
            catch (Exception e)
            {
                return "public string DeleteAccount(Account Acc) : Lỗi " + e.Message;
            }
        }

        public string UpdateAccount(Account Acc)
        {
            try
            {
                _dbcontext = new DatabaseContext();
                _dbcontext.Accounts.Update(Acc);
                _lstAccount[_lstAccount.FindIndex(x => x.IdAcc == Acc.IdAcc)] = Acc;
                return "Done";
            }
            catch(Exception e)
            {
                return "public string UpdateAccount(Account Acc) : lỗi"+e.Message;
            }
        }

        public string SaveAccount()
        {
            try
            {
               _dbcontext.SaveChanges();
               return "Done";
            }
            catch
            {
                return "public string SaveAccount() : Lỗi";
            }
        }

        public Guid resultGuidID(string User_Acc)
        {
            Guid ID = _lstAccount.Find(x => x.UserAcc == User_Acc).IdAcc;
            return ID ;
        }

        public List<int> resultsYear()
        {
            List<int> year = new List<int>();
            var yearStart = 1900;
            for (int i = yearStart; i < DateTime.Now.Year; i++)
            {
                year.Add(yearStart);
            }

            return year;
        }
    }
}
