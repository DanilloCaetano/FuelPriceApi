using FuelPrice.Domain.Interface;
using System;
using System.Globalization;

namespace FuelPrice.Domain.Helpers
{
    public class Helpers : IHelpers
    {
        public string searchCode(string state)
        {
            var upperState = state.ToUpperInvariant();
            var code = "";

            switch (upperState)
            {
                case "ACRE":
                    code = "AC";
                    break;
                case "ALAGOAS":
                    code = "AL";
                    break;
                case "AMAPA":
                    code = "AP";
                    break;
                case "AMAZONAS":
                    code = "AM";
                    break;
                case "BAHIA":
                    code = "BA";
                    break;
                case "CEARA":
                    code = "CE";
                    break;
                case "DISTRITO FEDERAL":
                    code = "DF";
                    break;
                case "DISTRITO":
                    code = "DF";
                    break;
                case "FEDEREAL":
                    code = "DF";
                    break;
                case "ESPIRITO SANTO":
                    code = "ES";
                    break;
                case "GOIAS":
                    code = "GO";
                    break;
                case "MARANHAO":
                    code = "MA";
                    break;
                case "MATO GROSSO":
                    code = "MT";
                    break;
                case "MATO GROSSO DO SUL":
                    code = "MS";
                    break;
                case "MINAS GERAIS":
                    code = "MG";
                    break;
                case "PARA":
                    code = "PA";
                    break;
                case "PARAIBA":
                    code = "PB";
                    break;
                case "PARANA":
                    code = "PR";
                    break;
                case "PERNANBUCO":
                    code = "PE";
                    break;
                case "PIAUI":
                    code = "PI";
                    break;
                case "RIO DE JANEIRO":
                    code = "RJ";
                    break;
                case "RIO GRANDE DO NORTE":
                    code = "RN";
                    break;
                case "RIO GRANDE DO SUL":
                    code = "RS";
                    break;
                case "RONDONIA":
                    code = "RO";
                    break;
                case "RORAIMA":
                    code = "RR";
                    break;
                case "SANTA CATARINA":
                    code = "SC";
                    break;
                case "SAO PAULO":
                    code = "SP";
                    break;
                case "SERGIPE":
                    code = "SE";
                    break;
                case "TOCANTINS":
                    code = "TO";
                    break;
            }

            return code;
        }

        public int weekDiff(DateTime d1, DateTime d2, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            var diff = d2.Subtract(d1);

            var weeks = (int)diff.Days / 7;

            var remainingDays = diff.Days % 7;
            var cal = CultureInfo.InvariantCulture.Calendar;
            var d1WeekNo = cal.GetWeekOfYear(d1, CalendarWeekRule.FirstFullWeek, startOfWeek);
            var d1PlusRemainingWeekNo = cal.GetWeekOfYear(d1.AddDays(remainingDays), CalendarWeekRule.FirstFullWeek, startOfWeek);

            if (d1WeekNo != d1PlusRemainingWeekNo) weeks++;

            if (weeks > 0) weeks = weeks - 1;

            return weeks;
        }
    }
}
