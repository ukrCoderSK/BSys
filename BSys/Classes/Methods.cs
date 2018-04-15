using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace BSystem.Classes
{
    public class Methods
    {
        MessegeShow messege = (mess) => Console.WriteLine(mess);

        protected virtual void MessegeShow()
        {
            messege("1. Налаштувати рахунок");
            messege("2. Перевiрити залишок");
            messege("3. Депозит");
            messege("4. Зняти");
            messege("5. Встановiть процентну ставку");
            messege("6. Перевiрте суттєвий iнтерес у часi");
            messege("7. Перевести на інший рахунок");
            messege("8. Зберегти останню сесiю");
            messege("9. Вiдкрити останню сесiю");
            messege("10. Спеціальні можливості");
            messege("X. Припинiть роботу\n");
            messege("Виберiть дiю: ");
        }

        protected virtual void PromociaDay()
        {
            messege("\n");
            messege("Акцiя дiє в такi днi: ");
            messege("Неділя -> Вівторок -> Четвер");
        }

        public void ColorMessege(string color)
        {
            switch(color)
            {
                case "green": Console.ForegroundColor = ConsoleColor.Green; break;
                case "red": Console.ForegroundColor = ConsoleColor.Red; break;
                case "white": Console.ForegroundColor = ConsoleColor.Black; break;
                default: Console.ForegroundColor = ConsoleColor.White; break;
            }
        }

        protected virtual double Promocia(string day, ref double deposit)
        {
            switch(day)
            {
                case "Monday": messege("Тільки в понеділок ми нараховуємо" +
                 " вам до 10% від депозиту");
                           deposit *= 0.10; break;
                case "Tuesday": messege("Сьогодні кешбек 3.2%");
                           deposit *= 0.032; break;
                case "Thursday": messege("Купляй в наших партнерів з промокодом BSystem " +
                                                   " та отримуй 15% кешбеку!"); deposit *= 0.15; break;
                default: messege($"{day} не являється акційним днем!"); break;
            }
            return 0.0f;
        }
    }
}
