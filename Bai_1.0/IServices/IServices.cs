using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bai_1._0.Models;

namespace Bai_1._0.IServices
{
    interface IServices
    {
        List<Account> GetList();
        List<Account> ResultList();
        string AddAccount(Account Acc);
        string DeleteAccount(Account Acc);
        string UpdateAccount(Account Acc);
        string SaveAccount();
        Guid resultGuidID(string User_Acc);

    }
}
