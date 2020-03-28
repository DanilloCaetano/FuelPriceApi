using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPrice.Domain.Interface
{
    public interface IHelpers
    {
        int weekDiff(DateTime d1, DateTime d2, DayOfWeek startOfWeek = DayOfWeek.Monday);
        string searchCode(string state);
    }
}
