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
    }
}
