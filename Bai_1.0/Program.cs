using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai_1._0.Context;
using Bai_1._0.Models;

namespace Bai_1._0
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
            // DatabaseContext DB = new DatabaseContext();
            // Account acc = new Account
            // {
            //     IdAcc = Guid.Parse("9800fb6c-9818-4b09-b2ff-3c8bf493eae1"),
            //     PassAcc = "123123123",
            //     SexAcc = false,
            //     StatusAcc = false,
            //     UserAcc = "dothiduyen",
            //     YearOfBirthAcc = 2002
            // };
            // DB.Accounts.Update(acc);
            // DB.SaveChanges();
        }
    }
}
