﻿using ExpensesTracker.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class User
    {
        public int userId { get; set; }
        public string nickname { get; set; }

        [RegularExpression(@"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$")]
        public string birth { get; set; }
        public double balance { get; set; } = 0;
        public string email { get; set; }

        private static int idQueue = 0;

        public List<Expense> expenses { get; set; } = new List<Expense>();
        public List<Income> income { get; set; } = new List<Income>();

        public static List<User> users = new List<User>();

        public struct Record
        {
            public string type { get; set; }
            public string date { get; set; }
            public double amount { get; set; }
        }

        public User(string nickname, string birth, string email, double balance = 0)
        {
            this.nickname = nickname;
            this.birth = birth;
            this.email = email;
            this.balance = balance;

            userId = idQueue++;

            users.Add(this);
        }

        public static User? GetUser(int userId)
        {
            return users.FirstOrDefault(x => x.userId == userId);
        }

        public static void EditUser(int userId, string? nickname = null, double balance = 0, string? email = null, string? birth = null)
        {
            User? user = GetUser(userId);

            if (user == null)
                return;

            if (nickname != null)
                user.nickname = nickname;

            if (balance != 0)
                user.balance = balance;

            if (email != null)
                user.email = email;

            if (birth != null)
                user.birth = birth;

            users[FindUser(userId)] = user;
        }

        public static int FindUser(int userId)
        {
            return users.FindIndex(a => a.userId == userId);
        }

        public static void setExpenses(int userId, List<Expense> expense)
        {
            users[FindUser(userId)].expenses = expense;
        }

        public static void setIncome(int userId, List<Income> income)
        {
            users[FindUser(userId)].income = income;
        }

        public static List<Record> History(int userId, int filterMonth = 0, int filterYear = 0, bool descending = false)
        {
            User? user = GetUser(userId);
            List<Record> sheet = new List<Record>();

            foreach (var expense in user.expenses.FilterBy(month: filterMonth, year: filterYear))
            {
                Record record = new Record();
                record.type = "Expense";
                record.date = expense.Month.ToString().PadLeft(2, '0') + "-" + expense.Day.ToString().PadLeft(2, '0');
                record.amount = expense.Money;
                sheet.Add(record);
            }

            int day = 1;
            foreach (var income in user.income.FilterBy(month: filterMonth, year: filterYear))
            {
                if (day >= 30)
                    day = 1;

                Record record = new Record();
                record.type = "Income";
                record.date = income.Month.ToString().PadLeft(2, '0') + "-" + day.ToString().PadLeft(2, '0');
                record.amount = income.Money;
                sheet.Add(record);
                day += 2;
            }

            sheet = Sort.ListSort<Record>(sheet, "date", descending);
            return sheet;
        }

        public static double getRecentExpenses(int i)
        {
            User? user = GetUser(FindUser(i));
            double recentExpenses = 0;
            List<Expense> filter = user.expenses.FilterBy(month: DateTime.Now.Month, year: DateTime.Now.Year);

            foreach (Expense expense in filter)
                recentExpenses += expense.Money;

            return recentExpenses;
        }

        public static double getRecentIncome(int i)
        {
            User? user = GetUser(FindUser(i));
            double recentIncome = 0;
            List<Income> filter = user.income.FilterBy(month: DateTime.Now.Month, year: DateTime.Now.Year);

            foreach (Income income in filter)
                recentIncome += income.Money;

            return recentIncome;
        }

        public static double getBalance(int userId)
        {
            User? user = GetUser(userId);
            List<Record> history = History(FindUser(userId));
            double balance = user.balance;

            foreach (Record record in history)
            {
                if (record.type == "Income")
                    balance += record.amount;
                else
                    balance -= record.amount;
            }

            balance = Math.Round(balance, 2);

            return balance;
        }

    }
}
