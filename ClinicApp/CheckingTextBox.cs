using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClinicApp
{
    public static class CheckingTextBox
    {
        public static void CheckButtPress(KeyEventArgs e, bool NumPad, bool other)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key >= Key.D0 && e.Key <= Key.D9)
                e.Handled = NumPad;
            else e.Handled = other;
        }
    }
}
