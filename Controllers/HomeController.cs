﻿using FarmDBMongoV1.Models;
using FarmDBMongoV1;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PagedList.Core;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace mFarmDBMongoV1.Controllers
{
    public class HomeController : Controller
    {
        MongoContext context = new MongoContext();
        public ActionResult Index()
        {
            int year = DateTime.Today.Year - 1;
            ViewBag.firstDay = "01/01/" + year.ToString();
            ViewBag.lastDay = "12/31/" + year.ToString();
            GetAccounts g = new GetAccounts();
            SelectList s = g.AccountPersonSelectList("Allen");         
            ViewBag.accountpersons = s;
            return View();
        }

        [HttpPost]
        public ActionResult TransactionList(IFormCollection forms, int? page)
        {
            string personid = forms["personid"];
            string firstday = forms["firstday"];
            string lastday = forms["lastday"];
            GetAccounts g = new GetAccounts();
            List<TransactionsWithAccounts>  returnList = 
            g.TA(firstday, lastday, personid);
            ViewBag.CurrentSort = "TransactionDate";
            ViewBag.DateSortParm = "TransactionDate";
            ViewBag.AcctSortParm = "AcctNumber";
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
/*             int pageSize = 20;
            int pageNumber = (page ?? 1); */
            return View(returnList);
        }

        public ActionResult TransactionList(int? page, string personid, string firstday, string lastday, string sortOrder, bool? ChangeSort)
        {
            // DateTime fromDate = Convert.ToDateTime(firstday);
            // DateTime toDate = Convert.ToDateTime(lastday);
            GetAccounts g = new GetAccounts();
            List<TransactionsWithAccounts> returnList =
            g.TA(firstday, lastday, personid);

            switch (sortOrder)
            {
                case "TransactionDate_desc":
                    if (ChangeSort ?? false)
                    {
                        returnList.Sort((x, y) => DateTime.Compare(x.TransactionDate, y.TransactionDate));
                        ViewBag.DateSortParm = "TransactionDate";
                        ViewBag.AcctSortParm = "AcctNumber";
                        ViewBag.CurrentSort = "TransactionDate";
                    }
                    else
                    {
                        returnList.Sort((x, y) => -1 * DateTime.Compare(x.TransactionDate, y.TransactionDate));
                        ViewBag.DateSortParm = "TransactionDate_desc";
                        ViewBag.AcctSortParm = "AcctNumber";
                        ViewBag.CurrentSort = "TransactionDate_desc";
                    }
                    break;
                case "TransactionDate":
                    if (ChangeSort ?? false)
                    {
                        returnList.Sort((x, y) => -1 * DateTime.Compare(x.TransactionDate, y.TransactionDate));
                        ViewBag.DateSortParm = "TransactionDate_desc";
                        ViewBag.AcctSortParm = "AcctNumber";
                        ViewBag.CurrentSort = "TransactionDate_desc";
                    }
                    else
                    {
                        returnList.Sort((x, y) => DateTime.Compare(x.TransactionDate, y.TransactionDate));
                        ViewBag.DateSortParm = "TransactionDate";
                        ViewBag.AcctSortParm = "AcctNumber";
                        ViewBag.CurrentSort = "TransactionDate";
                    }
                    break;
                case "AcctNumber":
                    if (ChangeSort ?? false)
                    {
                        returnList.Sort((x, y) => -1 * x.AccountNumber1.CompareTo(y.AccountNumber1));
                        ViewBag.DateSortParm = "TransactionDate";
                        ViewBag.AcctSortParm = "AcctNumber_desc";
                        ViewBag.CurrentSort = "AcctNumber_desc";
                    }
                    else
                    {
                        returnList.Sort((x, y) => x.AccountNumber1.CompareTo(y.AccountNumber1));
                        ViewBag.DateSortParm = "TransactionDate";
                        ViewBag.AcctSortParm = "AcctNumber";
                        ViewBag.CurrentSort = "AcctNumber";
                    }
                    break;
                case "AcctNumber_desc":
                    if (ChangeSort ?? false)
                    {
                        returnList.Sort((x, y) => x.AccountNumber1.CompareTo(y.AccountNumber1));
                        ViewBag.DateSortParm = "TransactionDate";
                        ViewBag.AcctSortParm = "AcctNumber";
                        ViewBag.CurrentSort = "AcctNumber";
                    }
                    else
                    {
                        returnList.Sort((x, y) => -1 * x.AccountNumber1.CompareTo(y.AccountNumber1));
                        ViewBag.DateSortParm = "TransactionDate";
                        ViewBag.AcctSortParm = "AcctNumber_desc";
                        ViewBag.CurrentSort = "AcctNumber_desc";
                    }
                    break;
                default:  
                    break;
            }
/*             int pageSize = 20;
            int pageNumber = (page ?? 1); */
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return View(returnList);
        }

        // GET: Transactions/Details/
        public ActionResult TransactionDetails(string id, string personid, string firstday, string lastday)
        {
            MongoDB.Bson.BsonObjectId myId = new BsonObjectId(ObjectId.Parse(id));
            var trans = context.db.GetCollection<Transaction>("Transactions")
                .Aggregate()
                .Match(x => x.Id == myId)
                .Lookup("Accounts", "AccountNumber1", "AccountNumber", "Account_docs").ToList();
            List<TransactionsWithAccounts> myColl = new List<TransactionsWithAccounts>();
            foreach (BsonDocument bd in trans)
            {
                TransactionsWithAccounts ta = BsonSerializer.Deserialize<TransactionsWithAccounts>(bd);
                myColl.Add(ta);
            }
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return View(myColl[0]);
        }

        // GET: 
        public ActionResult TransactionEdit(string id, string personid, string firstday, string lastday)
        {
            Transaction trans = context.db.GetCollection<Transaction>("Transactions")
                .Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();
            GetAccounts g = new GetAccounts();
            SelectList s = g.AccountsSelectList(trans.AccountNumber1);
            ViewBag.accounts = s;
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return View(trans);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransactionEdit(IFormCollection forms)
        {
            string personid = forms["personid"];
            string firstday = forms["firstday"];
            string lastday = forms["lastday"];
            var collection = context.db.GetCollection<Transaction>("Transactions");
            GetAccounts g = new GetAccounts();
            var date = g.StrToDate(forms["TransactionDate"]);
            var filter = Builders<Transaction>.Filter.Eq("_id", ObjectId.Parse(forms["Id"]));
            var update = Builders<Transaction>.Update
                .Set("TransactionID", Convert.ToInt32(forms["TransactionID"]))
                .Set("TransactionDescription", forms["TransactionDescription"])
                .Set("TransactionDate", date)
                .Set("AccountNumber1", Convert.ToInt32(forms["AccountNumber1"]))
                .Set("Account1Amount", Convert.ToDecimal(forms["Account1Amount"]));
            var result = collection.UpdateOne(filter, update);
            Transaction trans = collection.Find(x => x.Id == ObjectId.Parse(forms["Id"])).FirstOrDefault();
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return RedirectToAction("TransactionList", new {personid=ViewBag.personid, 
            firstDay = ViewBag.firstDay, lastDay = ViewBag.lastDay});
        }


        // GET: 
        public ActionResult TransactionCreate(string personid, string firstday, string lastday)
        {
            GetAccounts g = new GetAccounts();
            SelectList s = g.AccountsSelectList(4610);
            ViewBag.accounts = s;
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransactionCreate(IFormCollection forms)
        {   string personid = forms["personid"];
            string firstday = forms["firstday"];
            string lastday = forms["lastday"];
            var collection = context.db.GetCollection<Transaction>("Transactions");
            var MaxTransactionID =
                (from c in collection.AsQueryable<Transaction>()
                 select c.TransactionID)
                .Max();
            int NewTransactionID = MaxTransactionID + 1;
            Transaction tran = new Transaction();
            tran.TransactionID = NewTransactionID;
            tran.TransactionDescription = forms["TransactionDescription"];
            tran.TransactionDate = Convert.ToDateTime(forms["TransactionDate"]);
            tran.AccountNumber1 = Convert.ToInt32(forms["AccountNumber1"]);
            tran.Account1Amount = Convert.ToDecimal(forms["Account1Amount"]);
            collection.InsertOne(tran);
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return RedirectToAction("TransactionList", new {personid=ViewBag.personid, 
            firstDay = ViewBag.firstDay, lastDay = ViewBag.lastDay});
        }

        // GET: 
        public ActionResult TransactionDelete(string id, string personid, string firstday, string lastday)
        {
            var collection = context.db.GetCollection<Transaction>("Transactions");
            var filter = Builders<Transaction>.Filter.Eq("_id", ObjectId.Parse(id));
            Transaction transaction = collection.Find<Transaction>(filter).FirstOrDefault();
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return View(transaction);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransactionDelete(IFormCollection forms)
        {
            string personid = forms["personid"];
            string firstday = forms["firstday"];
            string lastday = forms["lastday"];
            var collection = context.db.GetCollection<Transaction>("Transactions");
            collection.DeleteOne(a => a.Id == ObjectId.Parse(forms["Id"]));
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;
            return RedirectToAction("TransactionList", new {personid=ViewBag.personid, 
            firstDay = ViewBag.firstDay, lastDay = ViewBag.lastDay});
        }

        public ActionResult AccountList()
        {
            GetAccounts g = new GetAccounts();
            List<Account> accounts = g.A().OrderBy(x => x.AccountNumber).ToList();
            return View(accounts);
        }

        // GET:
        public ActionResult AccountDetails(string id)
        {
            var collection = context.db.GetCollection<Account>("Accounts");
            Account account  = collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();
            return View(account);
        }

        // GET: 
        public ActionResult AccountEdit(string id)
        {
            var collection = context.db.GetCollection<Account>("Accounts");
            Account account = collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefault();
            GetAccounts g = new GetAccounts();
            SelectList s = g.AccountPersonSelectList(account.AccountPerson.AccountPerson1);
            ViewBag.accountpersons = s;
            SelectList s2 = g.AccountTypesSelectList(account.AccountSubType.AccountType.AccountType1);
            ViewBag.accounttypes = s2;
            return View(account);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountEdit(IFormCollection forms)
        {
            string s1= forms["Active"];
            char[] charSeparators = new char[] {','};
            string[] formsactive = s1.Split(charSeparators, StringSplitOptions.None);
            bool active = Convert.ToBoolean(formsactive[0]);
            var collection = context.db.GetCollection<Account>("Accounts");
            var filter = Builders<Account>.Filter.Eq("_id", ObjectId.Parse(forms["Id"]));
            string person = forms["AccountPerson1"];
            Account acounts = collection.Find(x => x.Id == ObjectId.Parse(forms["Id"])).FirstOrDefault();
            var update = Builders<Account>.Update
                .Set("AccountNumber", Convert.ToInt32(forms["AccountNumber"]))
                .Set("AccountName", forms["AccountName"])
                .Set("TaxFormRef", forms["TaxFormRef"])
                .Set("Active", active)
                .Set("AccountPerson.AccountPerson1", forms["AccountPerson1"])
                .Set("AccountSubType.AccountSubType1", forms["AccountSubType.AccountSubType1"])
                .Set("AccountSubType.SortOrder", Convert.ToInt32(forms["AccountSubType.SortOrder"]))
                .Set("AccountSubType.AccountType.AccountType1", forms["AccountType1"]);
            var result = collection.UpdateOne(filter, update);
            return RedirectToAction("AccountList");
        }


        // GET: 
        public ActionResult AccountCreate()
        {
            GetAccounts g = new GetAccounts();
            SelectList s = g.AccountPersonSelectList("Mike");
            ViewBag.accountpersons = s;
            SelectList s2 = g.AccountTypesSelectList("Expenses");
            ViewBag.accounttypes = s2;
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountCreate(IFormCollection forms)
        {
            string s1= forms["Active"];
            char[] charSeparators = new char[] {','};
            string[] formsactive = s1.Split(charSeparators, StringSplitOptions.None);
            bool active = Convert.ToBoolean(formsactive[0]);
            var collection = context.db.GetCollection<Account>("Accounts");
            Account account = new Account();
            string ap = forms["AccountPerson.AccountPerson1"];
            string ap1 = forms["AccountPerson"];
            account.AccountNumber = Convert.ToInt32(forms["AccountNumber"]);
            account.AccountName = forms["AccountName"];
            account.TaxFormRef = forms["TaxFormRef"];
            account.Active = active;
            account.AccountPerson = new AccountPerson();
            account.AccountPerson.AccountPerson1 = forms["AccountPerson1"];
            account.AccountSubType = new AccountSubType();
            account.AccountSubType.AccountSubType1 = forms["AccountSubType.AccountSubType1"];
            account.AccountSubType.SortOrder = Convert.ToInt32(forms["AccountSubType.SortOrder"]);
            account.AccountSubType.AccountType = new AccountType();
            account.AccountSubType.AccountType.AccountType1 = forms["AccountType1"];
            collection.InsertOne(account);
            return RedirectToAction("AccountList");
        }

        // GET: 
        public ActionResult AccountDelete(string id)
        {
            var collection = context.db.GetCollection<Account>("Accounts");
            var filter = Builders<Account>.Filter.Eq("_id", ObjectId.Parse(id));
            Account account = collection.Find<Account>(filter).FirstOrDefault();
            return View(account);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountDelete(IFormCollection forms)
        {
            var collection = context.db.GetCollection<Account>("Accounts");
            collection.DeleteOne(a => a.Id == ObjectId.Parse(forms["Id"]));
            return RedirectToAction("AccountList");
        }

        public ActionResult AccountPersons()
        {
            var persons = new SelectList(context.db.GetCollection<Account>("Accounts").Distinct<AccountPerson> ("AccountPerson",new BsonDocument()).ToList(), "AccountPerson1", "AccountPerson1");
            ViewData["persons"] = persons;
            return View();
        }

        [HttpPost]
        public ActionResult ProfitAndLoss(IFormCollection forms)
        {
            string personid = forms["PersonID"];
            string firstday = forms["firstDay"];
            string lastday = forms["lastDay"];
            ViewBag.firstday = firstday;
            ViewBag.lastday = lastday;
            ViewBag.personid = personid;

            List<Transaction> transactions = context.db.GetCollection<Transaction>("Transactions")
            .AsQueryable().ToList();

            List<Transaction> filteredtransactions = transactions.AsQueryable()
            .Where(x => x.TransactionDate >= Convert.ToDateTime(firstday))
            .Where(x => x.TransactionDate <= Convert.ToDateTime(lastday)).ToList();
            
            GetAccounts g = new GetAccounts();
            List<Account> accounts = g.A().Where(c => c.AccountPerson.AccountPerson1 == personid)
            .Where(x => x.Active == true).ToList();

            List<FilteredTransactions> ta = filteredtransactions
                .Join(accounts, t => t.AccountNumber1, a => a.AccountNumber, (t, a) =>
                new FilteredTransactions {
                AccountNumber = t.AccountNumber1,
                AccountName = a.AccountName,
                AccountSubType = a.AccountSubType.AccountSubType1,
                SortOrder = a.AccountSubType.SortOrder,
                AccountType = a.AccountSubType.AccountType.AccountType1,
                Total = t.Account1Amount,
                Count = 1,
                AccountPerson = a.AccountPerson.AccountPerson1
            }).ToList();

            List<FilteredTransactions> AGroup = ta
            .GroupBy(s => s.AccountNumber)
            .Select(group => new FilteredTransactions{
                AccountNumber = group.First().AccountNumber,
                AccountName = group.First().AccountName,
                AccountSubType =  group.First().AccountSubType,
                SortOrder = group.First().SortOrder,
                AccountType = group.First().AccountType,
                Total = group.Sum(s => s.Total),
                Count = group.Count(),
                AccountPerson = group.First().AccountPerson
            }).ToList();

            List<FilteredTransactions> ASTGroup = ta
            .GroupBy(s => s.AccountSubType)
            .Select(group => new FilteredTransactions {
                AccountNumber = null,
                AccountName = "Total",
                AccountSubType = group.First().AccountSubType,
                SortOrder = group.First().SortOrder,
                AccountType = group.First().AccountType,
                Total = group.Sum(s => s.Total),
                Count = group.Count(),
                AccountPerson = group.First().AccountPerson
            }).ToList();

            List<FilteredTransactions> ATGroup = ta
            .GroupBy(s => s.AccountType)
            .Select(group => new FilteredTransactions {
                AccountNumber = null,
                AccountName = "",
                AccountSubType = "Total",
                SortOrder = 999,
                AccountType = group.First().AccountType,
                Total = group.Sum(s => s.Total),
                Count = group.Count(),
                AccountPerson = group.First().AccountPerson
            }).ToList();

            List<FilteredTransactions> listFinal = AGroup.Union(ASTGroup).Union(ATGroup)
                .OrderByDescending(x=>x.AccountType)
                .ThenBy(x=>x.SortOrder).ToList();

            return View(listFinal);
        }
    }
}