using System;
namespace BSystem.Classes
{
    delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        private string v;

        public String Messege { get; }
        public Double Sum { get; }

        public AccountEventArgs(String messege, Double sum)
        {
            Messege = messege;
            Sum = sum;
        }

        public AccountEventArgs(string v)
        {
            this.v = v;
        }
    }
}
