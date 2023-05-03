using Firefly.Box;
using ENV.Data;
using ENV;
using Firefly.Box.Flow;
using System;

namespace Northwind.Customers
{
    /// <summary>ShowCustomers(P#2)</summary>
    /// <remark>Last change before Migration: 01/06/2009 15:46:02</remark>
    internal class ShowCustomers : FlowUIControllerBase
    {
        #region Models
        internal readonly Models.Customers Customers = new Models.Customers { AllowRowLocking = true };
        #endregion
        #region Parameters
        readonly Types.CustomerID pi_CustomerID = new Types.CustomerID("pi.Customer ID");
        #endregion
        public ShowCustomers()
        {
            Title = "ShowCustomers";
            InitializeDataViewAndUserFlow();
        }
        void InitializeDataViewAndUserFlow()
        {
            From = Customers;
            OrderBy = Customers.SortByCustomerID;
            #region Column Selection and User Flow
            // parameter for selection task
            Columns.Add(pi_CustomerID);

            Columns.Add(Customers.CustomerID);
            Columns.Add(Customers.CompanyName);
            Columns.Add(Customers.Address);
            Columns.Add(Customers.City);
            Columns.Add(Customers.Phone);
            MarkParameterColumns(pi_CustomerID);
            #endregion
        }


        /// <summary>ShowCustomers(P#2)</summary>
        public void Run(TextParameter ppi_CustomerID = null)
        {

            BindParameter(pi_CustomerID, ppi_CustomerID);
            Execute();

        }
        protected override void OnLoad()
        {
            EnableTransactions = true;
            EnableNonCursorLocks = true;
            OnDatabaseErrorRetry = true;
            RowLocking = LockingStrategy.OnRowLoading;
            TransactionScope = TransactionScopes.Task;
            BindAllowInsert(() => Customers.City != "Madrid");
            AllowExportData = true;
            AllowSelect = true;
            View = () => new Views.ShowCustomersView(this);
        }
        protected override void OnSavingRow()
        {
            if (u.KBGet(1) == Command.Select)
                pi_CustomerID.Value = Customers.CustomerID;
        }
    }
}
