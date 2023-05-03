# Transactions, locking post migration from ISAM/DB2 to Sql Server

In [this video](youtube) I explain transactions & locking for Btrieve, ISAM, DB2 and Sql Server - and explain what we've done in the Sql Server migration to solve them - and how you can locally enable Transactions and standard locking if you need to.

[Here's a link to the commit with the changes](https://github.com/FireflyMigration/enable-transactions-for-old-isam-apps/commit/e3b3ad022ca6df0a78daec15a84b403cd5d36921)


# SQLs used in the demo

## Show transactions
```sql
select * from sys.dm_tran_session_transactions
```

## Show locked rows in customer table
```sql
SELECT *
FROM Customers
WHERE %%lockres%% in
    (SELECT resource_description
     FROM sys.dm_tran_locks
     WHERE request_mode='U')
```