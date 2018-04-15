namespace BSystem.Interface
{
    public interface iAccount
    {
        bool withdraw(double outAmt); // Withdraw
        void deposit(double addAmt); // Deposit
        double? getBalAfter(int mos); // Balance After Operations
        void setIntRate(double per); // Rate %
        double? swiftTrans(double? swift = null, bool flag = false); // Swift Translate user money
    } 
}