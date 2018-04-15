using BSystem.Interface;

namespace BSystem.Classes
{
    delegate void MessegeShow(string messege);
    delegate void CleanInformation();
    delegate string EnterInformation();

    class BankAccount : iAccount
    {
        public string name;
        //int accountNum;
        public string userNumber;
        public double? balance = 0;
        double interest = 0;

        public event AccountStateHandler Withdraw;
        public event AccountStateHandler Deposit;
        public event AccountStateHandler SwiftTranslate;

        public BankAccount(string name, double? balance, string userNumber)
        {
            this.name = name;
            this.balance += balance;
            this.userNumber = userNumber;
        }

        public void setIntRate(double per)
        {
            interest = per * 0.01;
        }
        public double? getBal()
        {
            return balance;
        }
        public string getName()
        {
            return this.name;
        }
        public void deposit(double addAmt)
        {
            balance += addAmt;
            if (Deposit != null)
                Deposit(this, new AccountEventArgs($"На рахунок поступило: {addAmt}", addAmt));
        }
        public bool withdraw(double outAmt)
        {
            bool chk = true;
            if (outAmt <= balance)
            {
                balance -= outAmt;
                if (Withdraw != null)
                    Withdraw(this, new AccountEventArgs($"З рахунка знято: {outAmt}", outAmt));
            }
            else if (outAmt > balance)
            {
                chk = false;
                if (Withdraw != null)
                    Withdraw(this, new AccountEventArgs("Не достатня сума на рахунку!", (double)balance));
            }

            return chk;
        }
        public double? getBalAfter(int mos)
        {
            double? prin = balance;
            double? intFeed;
            for (int g = 1; g <= mos; g++)
            {
                intFeed = prin * interest;
                prin += intFeed;
            }

            return prin;
        }

        public double? swiftTrans(double? swift = null, bool flag = false)
        {
            if ((swift > 0.0) && (flag == false))
            {
                if (swift <= balance)
                {
                    balance -= swift;
                    if (Withdraw != null)
                        Withdraw(this, new AccountEventArgs($"Ви переказали {swift}", (double)swift));
                }
            }
            else if (flag = true)
            {
                balance += swift;
                if (SwiftTranslate != null)
                    SwiftTranslate(this, new AccountEventArgs($"Гроші не було переведено!"));
            }
            else
            {
                return -1;
            }

            return balance;
        }
    }
}