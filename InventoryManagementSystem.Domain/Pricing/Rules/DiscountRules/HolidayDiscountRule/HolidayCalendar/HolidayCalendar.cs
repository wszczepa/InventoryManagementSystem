using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.Domain.Pricing.Rules.DiscountRules.HolidayDiscountRule.HolidayCalendar
{
    public class HolidayCalendar : IHolidayCalendar
    {
        
       public  bool IsHoliday(DateTime date)
        {
            var d = date.Date;

            return IsFixedHoliday(d) || IsMovableHoliday(d);
        }

        private bool IsFixedHoliday(DateTime d)
        {
            return
                (d.Month == 1 && d.Day == 1) ||   // Nowy Rok
                (d.Month == 1 && d.Day == 6) ||   // Trzech Króli
                (d.Month == 5 && d.Day == 1) ||   // Święto Pracy
                (d.Month == 5 && d.Day == 3) ||   // Konstytucja 3 Maja
                (d.Month == 8 && d.Day == 15) ||  // Wniebowzięcie NMP
                (d.Month == 11 && d.Day == 1) ||  // Wszystkich Świętych
                (d.Month == 11 && d.Day == 11) || // Niepodległość
                (d.Month == 12 && d.Day == 25) || // Boże Narodzenie
                (d.Month == 12 && d.Day == 26);   // Drugi dzień świąt
        }

        private bool IsMovableHoliday(DateTime d)
        {
            var easter = GetEasterSunday(d.Year);

            return
                d == easter ||                 // Wielkanoc
                d == easter.AddDays(1) ||     // Poniedziałek Wielkanocny
                d == easter.AddDays(49) ||    // Zielone Świątki
                d == easter.AddDays(60);      // Boże Ciało
        }

        private static DateTime GetEasterSunday(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(year, month, day);
        }

      
    }
}
