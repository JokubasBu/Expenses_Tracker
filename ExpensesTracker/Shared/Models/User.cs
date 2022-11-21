using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesTracker.Shared.Models
{
    public class User
    {
        public int userId { get; set; }
        public string nickname { get; set; }
        public string birth { get; set; }
        public double balance { get; set; }
        public string email { get; set; }
        private static int idQueue = 0;

        public List<Expense> expenses { get; set; } = new List<Expense>();
        public List<Income> income { get; set; } = new List<Income>();

        public User(string nickname, string birth, string email)
        {
            this.nickname = nickname;
            this.birth = birth;
            this.email = email;

            userId = idQueue++;
        }




    }
}
