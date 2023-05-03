using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Customers
{
    internal class LongTask : BusinessProcessBase
    {
        Models.OrderDetails od = new Models.OrderDetails() { AllowRowLocking = true };
        public LongTask()
        {
            From = od;
        }
        protected override void OnLoad()
        {
            EnableTransactions = true;
            EnableNonCursorLocks = true;
            RowLocking = Firefly.Box.LockingStrategy.OnRowLoading;
            TransactionScope = Firefly.Box.TransactionScopes.Task;
        }
        public void Run()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Execute();
            sw.Stop();
            System.Windows.Forms.MessageBox.Show(sw.Elapsed.ToString());

        }
    }
}
