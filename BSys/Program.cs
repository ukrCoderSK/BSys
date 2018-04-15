using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSystem.Classes;

namespace BSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Bank System";

            UI intUI = new UI();
            intUI.UIinterface();
        }
    }
}