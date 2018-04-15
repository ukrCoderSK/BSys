﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace BSystem.Classes
{
    public class UI : Methods
    {
        // Поля
        private double deposidTmp = 0;
        private string exVar = null;
        private string exStr = "Помилка ---> список порожнiй тобто немає жодного аккаунта!";
        private string nmChk;
        private string nmNow = null;
        private string saveText = null;
        private string saveShop_one = null;
        private string saveShop_two = null;
        private string dayEnter = null;
        private int accNum = -1;
        private int mos;
        private bool ok;
        private bool flag = true;

        // obj
        List<BankAccount> bank = new List<BankAccount>();
        Random _random = new Random();
        DateTime dateTime = DateTime.Today;

        // UI метод
        public void UIinterface()
        {
            MessegeShow messege = (mess) => Console.WriteLine(mess);
            EnterInformation enterInformation = () => Console.ReadLine().ToUpper();
            CleanInformation cleanInformation = () => Console.Clear();

            // Загальний цикл
            while (true)
            {
            pointSW:;

                cleanInformation();
                ColorMessege("white");
                MessegeShow();

                string selGo = enterInformation();
                messege("\n");
                cleanInformation();
                
                // Пункт 1
                if (selGo == "1")
                {
                    messege("Введiть iнiцiали: ");
                    string name = enterInformation();
                    
                    if(name.Length <= 2)
                    { 
                        ColorMessege("red");
                        messege("Ім'я не може бути з двох символів! Повторіть операцію!");
                        ColorMessege("while");
                    }
                    else
                    {
                        try
                        {
                            messege("Введiть початковий баланс: ");
                            double? init = double.Parse(enterInformation());

                            int randNumber = _random.Next() % 99999;
                            string randNumberParse = Convert.ToString(randNumber);

                            if (init == null)
                                throw new FormatException();

                            bank.Add(new BankAccount(name, init, randNumberParse));

                            do
                            {
                                int ib = 0;
                                if (bank[ib].userNumber == randNumberParse)
                                {
                                    randNumber = _random.Next() % 99999;
                                    randNumberParse = Convert.ToString(randNumber);
                                    bank[ib].userNumber = randNumberParse;
                                    flag = false;
                                    ib++;
                                }
                            } while (flag);

                            ColorMessege("green");
                            messege("Банкiвський рахунок додано! ");
                            ColorMessege("white");
                        }
                        catch (FormatException)
                        {
                            ColorMessege("red");
                            messege("Не заповнено поле, операцiю не виконано!");
                            ColorMessege("white");
                        }
                    }
                    enterInformation();
                }
                // Пункт 2
                else if (selGo == "2")
                {
                    cheakTheList();
                    if (exVar == exStr)
                        goto _pointOne;

                    messege("Введiть iнiцiали або номер: ");
                    nmChk = enterInformation();

                    for (int ix = 0; ix < bank.Count; ix++)
                    {
                        if ((bank[ix].name == nmChk) || (bank[ix].userNumber == nmChk))
                        {
                            messege($"Рахунок знайдено! \nIм'я: {bank[ix].name} \nБаланс: {bank[ix].balance} UAN\nНомер клiєнта: {bank[ix].userNumber}");
                            break;
                        }
                        else if((nmChk == "") || (nmChk == " "))
                        {
                            messege("Ви нічого не ввели! Повторіть спробу!");
                            break;
                        }
                    }

                _pointOne: exVar = null;
                    enterInformation();
                }
                // Пункт 3
                else if (selGo == "3")
                {
                    cheakTheList();
                    if (exVar == exStr)
                        goto _pointTwo;

                    messege("Введiть iнiцiали: ");

                    nmChk = enterInformation();

                    for (int ix = 0; ix < bank.Count; ix++)
                    {
                        if ((bank[ix].name == nmChk) || (bank[ix].userNumber == nmChk))
                        {
                            nmNow = nmChk;
                            accNum = ix;
                        }
                    }

                    if (accNum != -1)
                    {

                        cleanInformation();
                        zeroDeposid:;

                        messege("Сума внеску(UAN): ");

                        try
                        {
                            deposidTmp = double.Parse(enterInformation());
                            bank[accNum].deposit(deposidTmp);
                        }
                        catch(FormatException)
                        {
                            messege("Ви нічого не ввели, повторіть спробу!");
                            goto zeroDeposid;
                        }

                        cleanInformation();

                        ColorMessege("green");
                        messege("Сума успiшно додана!");
                        ColorMessege("white");

                        PromociaDay();

                        dayEnter = CultureInfo.GetCultureInfo("eng-ENG").DateTimeFormat.GetDayName(dateTime.DayOfWeek);

                        messege(dayEnter.ToString());

                        if (dayEnter == "Monday")
                        {
                            Promocia(dayEnter,ref deposidTmp);
                            bank[accNum].balance += (float)deposidTmp;
                        }
                        else if (dayEnter == "Tuesday")
                        {
                            Promocia(dayEnter,ref deposidTmp);
                            bank[accNum].balance += deposidTmp;

                            if(!File.Exists("keshShop.txt"))
                                File.Create("keshShop.txt");
                            
                            try
                            {
                                using (StreamReader kesh_Shop = new StreamReader(File.Open("keshShop.txt", FileMode.Open)))
                                {
                                    while(!kesh_Shop.EndOfStream)
                                        saveShop_one = kesh_Shop.ReadToEnd();
                                }
                            }
                            catch(IOException)
                            {
                                messege("Файл порожній! Вибачте!");
                            }

                            if(File.Exists("keshShop.txt"))
                            {
                                messege(saveShop_one);
                                messege("Бажаєте розпечатати? 1 - так / 2 - ні");
                            }

                            selGo = enterInformation();

                            if (selGo == "1")
                            {
                                ///<sumary>
                                ///Друк
                                ///</sumary>
                            }
                        }
                        else if (dayEnter == "Thursday")
                        {
                            Promocia(dayEnter,ref deposidTmp);
                            bank[accNum].balance += deposidTmp;

                            if(!File.Exists("vipShop.txt"))
                                File.Create("vipShop.txt");

                            try
                            {
                                using (StreamReader vip_kesh_shop = new StreamReader(File.Open("vipShop.txt", FileMode.Open)))
                                {
                                    while(!vip_kesh_shop.EndOfStream)
                                        saveShop_two = vip_kesh_shop.ReadToEnd();
                                }
                            }
                            catch(IOException)
                            {
                                messege("Файл порожній! Вибачте!");
                            }

                            if (File.Exists("vipShop.txt"))
                            {
                                messege(saveShop_two);
                                messege("Бажаєте розпечатати? 1 - так / 2 - ні");
                            }
                        }
                    }
                    else
                    {
                        ColorMessege("red");
                        messege("Рахунок не знайдено! ");
                        ColorMessege("while");
                    }

                _pointTwo: enterInformation();
                }
                // Пункт 4
                else if (selGo == "4")
                {
                    cheakTheList();
                    if (exVar == exStr)
                        goto _pointTree;

                    messege("Введiть iнiцiали: ");
                    nmChk = enterInformation();

                    for (int ix = 0; ix < bank.Count; ix++)
                    {
                        if ((bank[ix].name == nmChk) || (bank[ix].userNumber == nmChk))
                        {
                            nmNow = nmChk;
                            accNum = ix;
                        }
                    }

                    if (accNum != -1)
                    {
                        messege("Сума, яку потрiбно вилучити(UAN): ");
                        ok = bank[accNum].withdraw(double.Parse(enterInformation()));
                        if (ok)
                        {
                            ColorMessege("green");
                            messege("Сума успiшно вiдкликана! ");
                            if (bank[accNum].balance == 0)
                            {
                                bank.RemoveAt(accNum); //bank[accNum] - error NRE  !!!
                                messege("Рахунок закрито! ");
                                ColorMessege("while");
                            }
                        }
                        else
                        {
                            messege("Недостатньо коштiв!");
                        }
                    }
                    else
                    {
                        messege("Рахунок не знайдено! ");
                    }

                _pointTree: enterInformation();
                }
                // Пункт 5
                else if (selGo == "5")
                {
                    cheakTheList();
                    if (exVar == exStr)
                        goto _pointFor;

                    messege("Введiть iнiцiали: ");
                    nmChk = enterInformation();

                    for (int ix = 0; ix < bank.Count; ix++)
                    {
                        if ((bank[ix].name == nmChk) || (bank[ix].userNumber == nmChk))
                        {
                           nmNow = nmChk;
                           accNum = ix;
                        }
                    }

                    if (accNum != -1)
                    {
                    stavka:;
                        messege("Встановiть вiдсоткову ставку (у вiдсотках): ");
                        try
                        {
                            bank[accNum].setIntRate(double.Parse(enterInformation()));
                            messege("Процентна ставка встановлена! ");
                        }
                        catch (FormatException)
                        {
                            messege("Використання . неможливе будьласка використайте , ");
                            enterInformation();
                            cleanInformation();
                            goto stavka;
                        }
                    }
                    else
                    {
                        messege("Рахунок не знайдено! ");
                    }

                _pointFor: enterInformation();
                }
                // Пункт 6
                else if (selGo == "6")
                {
                    cheakTheList();
                    if (exVar == exStr)
                        goto _pointFive;

                    messege("Введiть iнiцiали: ");
                    nmChk = enterInformation();

                    for (int ix = 0; ix < bank.Count; ix++)
                    {
                        if ((bank[ix].name == nmChk) || (bank[ix].userNumber == nmChk))
                        {
                            nmNow = nmChk;
                            accNum = ix;
                        }
                    }

                    if (accNum != -1)
                    {
                        messege("Мiсяцi процентної ставки: ");
                        mos = int.Parse(enterInformation());
                        messege($"Залишок за {mos} мiсяцями: {bank[accNum].getBalAfter(mos)} UAN");
                    }
                    else
                    {
                        messege("Рахунок не знайдено!");
                    }

                _pointFive: enterInformation();
                }
                // Пункт 7
                else if (selGo == "7")
                {
                    messege("Хто ви: ");
                    cheakTheList();

                    nmNow = enterInformation();
                    try
                    {
                        messege("Яку суму ви збираєтеся переказати: ");
                        double swift = double.Parse(enterInformation());

                        double? tmp = null;
                        string tmp2 = null;
                        for (int ix = 0; ix < bank.Count; ix++)
                        {
                            if ((bank[ix].name == nmNow) || (bank[ix].userNumber == nmNow))
                            {
                                tmp2 = bank[ix].name;
                                tmp = bank[ix].balance;
                                bank[ix].swiftTrans(swift);
                                break;
                            }
                        }

                        messege("Кому ви збираєтеся переказати гроші: ");
                        cheakTheList(nmNow);
                        nmChk = enterInformation();

                        for (int ix = 0; ix < bank.Count; ix++)
                        {
                            if ((bank[ix].name == nmChk) || (bank[ix].userNumber == nmChk))
                            {
                                messege($"Користувач {nmNow} збирається перевести гроші ви користувачу {nmChk}! Адміністратор дає згоду -> 1-Так/2-Ні");
                                selGo = enterInformation();
                                if (selGo == "1")
                                {
                                    bank[ix].swiftTrans(swift, true);
                                }
                                else if (selGo == "2")
                                {
                                    for ( ; ix < bank.Count; ix++)
                                        if (bank[ix].name == tmp2)
                                        { bank[ix].balance = tmp; break; }
                                }
                            }
                        }
                    }
                    catch(FormatException)
                    {
                        messege("Неправильний формат вводу!");
                    }

                    
                }
                // Пункт 8
                else if (selGo == "8")
                {
                    messege("1. Зберегти в новий файл\n2. Зберегти в існуючий файл");
                    selGo = enterInformation();

                    if (selGo == "1")
                    {
                        StreamWriter sw_one = new StreamWriter("newFileName.txt");
                        for (int ix = 0; ix < bank.Count; ix++)
                        {
                            sw_one.WriteLine($"Iм'я           -> {bank[ix].name}");
                            sw_one.WriteLine($"Баланс         -> {bank[ix].balance} UAN");
                            sw_one.WriteLine($"Особовий номер -> {bank[ix].userNumber}");
                            sw_one.WriteLine(new string('~', 15));
                        }
                        sw_one.Close();
                    }
                    else if (selGo == "2")
                    {
                        StreamWriter sw_two = File.AppendText("FileName.txt");
                        for (int ix = 0; ix < bank.Count; ix++)
                        {
                            sw_two.WriteLine($"Iм'я           -> {bank[ix].name}");
                            sw_two.WriteLine($"Баланс         -> {bank[ix].balance} UAN");
                            sw_two.WriteLine($"Особовий номер -> {bank[ix].userNumber}");
                            sw_two.WriteLine(new string('~', 15));
                        }
                        sw_two.Close();
                    }
                    else
                    {
                        messege("Неправельний синтаксис!");
                    }
                }
                // Пункт 9
                else if (selGo == "9")
                {
                    messege("1. Вiдкрити файл з останнюв сесiюв\n2. Вiдкрити файл з усiма сесiями");
                    selGo = enterInformation();

                    if (selGo == "1")
                    {
                        try
                        {
                            StreamReader sr_one = new StreamReader("newFileName.txt");

                            saveText = sr_one.ReadToEnd();
                            messege(saveText);

                            sr_one.Close();
                            enterInformation();
                        }
                        catch (IOException)
                        {
                            if (!(File.Exists("newFileName.txt")))
                            {
                                messege("Файла немає! Бажаєте створити?");
                                messege("1. Так\n2. Нi");
                                selGo = enterInformation();

                                if (selGo == "1")
                                    File.Create("newFileName.txt");
                                else if (selGo == "2")
                                    goto pointSW;
                                else
                                    messege("Неправильний синтаксис!");

                                enterInformation();
                            }

                            if ((saveText == null) || (saveText.Length == 0) || (saveText == " "))
                            {
                                messege("Файл порожній!");
                                enterInformation();
                            }
                        }
                    }
                    else if (selGo == "2")
                    {
                        try
                        {
                            StreamReader sr_two = new StreamReader("FileName.txt");

                            saveText = sr_two.ReadToEnd();
                            messege(saveText);



                            sr_two.Close();
                            enterInformation();
                        }
                        catch (IOException)
                        {
                            if (!(File.Exists("FileName.txt")))
                            {
                                messege("Файла немає! Бажаєте створити?");
                                messege("1. Так\n2. Нi");
                                selGo = enterInformation();

                                if (selGo == "1")
                                    File.Create("FileName.txt");
                                else if (selGo == "2")
                                    goto pointSW;
                                else
                                    messege("Неправильний синтаксис!");
                                enterInformation();
                            }

                            if ((saveText == null) || (saveText.Length == 0) || (saveText == " "))
                            {
                                messege("Файл порожнiй!");

                                enterInformation();
                            }
                        }
                    }
                }
                // Пункт 10
                else if (selGo == "10")
                {
                    
                }
                // Пункт виходу
                else if (selGo == "X")
                {
                    break;
                }
                else
                {
                    messege("Облiковий запис не знайдено! Неправильний синтаксис!");
                    enterInformation();
                }

                //_pointSix:
                cleanInformation();
            }
        }

        // Перебір всіх клієнтів
        private void cheakTheList(string nmNow = "")
        {
            MessegeShow messege = (mess) => Console.WriteLine(mess);
                
            try
            {
                if (bank[0].name == null)
                    throw new ArgumentOutOfRangeException();

                messege("Виберiть клiєнта(За iм'ям або по номеру): ");
                for (int ix = 0; ix < bank.Count; ix++)
                {
                    if((nmNow == bank[ix].name) || (nmNow == bank[ix].userNumber))
                    {
                        this.nmNow = null;
                        continue;
                    }
                    messege($"{bank[ix].name}  ->   {bank[ix].userNumber}");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                messege($"{ exVar = "Помилка ---> список порожнiй тобто немає жодного аккаунта!"}");
            }
        }
    }
}

///<sumary>
///Зробити парсер для вбудованого конвертера валют
///Пофіксити баги
///</sumary>
