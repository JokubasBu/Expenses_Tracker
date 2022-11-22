using ExpensesTracker.Shared.Extensions;
using System;
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

        public static List<Record> History(int userId)
        {
            User? user = GetUser(userId);
            List<Record> sheet = new List<Record>();

            foreach (var expense in user.expenses)
            {
                Record record = new Record();
                record.type = "Expense";
                record.date = expense.Year + "-" + expense.Month + "-" + expense.Day;
                record.amount = expense.Money;
                sheet.Add(record);
            }

            foreach (var income in user.income)
            {
                int day = 0;
                if (day >= 30)
                    day = 0;

                Record record = new Record();
                record.type = "Income";
                record.date = income.Year + "-" + income.Month + "-" + day;
                record.amount = income.Money;
                sheet.Add(record);
            }

            sheet = sheet.OrderBy(o => o.date).Reverse().ToList();
            return sheet;
        }

        public double getRecentExpenses()
        {
            double recentExpenses = 0;
            List<Expense> filter = expenses.FilterBy(month: DateTime.Now.Month);

            foreach (Expense expense in filter)
                recentExpenses += expense.Money;

            return recentExpenses;
        }

        public double getRecentIncome()
        {
            double recentIncome = 0;
            List<Income> filter = income.FilterBy(month: DateTime.Now.Month);

            foreach (Income income in filter)
                recentIncome += income.Money;

            return recentIncome;
        }

    }
}
