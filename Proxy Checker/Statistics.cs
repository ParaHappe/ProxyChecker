using System;
using System.Windows.Controls;

namespace WpfApp2
{
    static class Statistics
    {
        public static void IncrementValue(TextBlock textBlock)
        {
            int num = Convert.ToInt32(textBlock.Text);
            textBlock.Text = Convert.ToString(++num);
        } 

        public static void SetToZero(TextBlock textBlock)
        {
            textBlock.Text = "0";
        }
    }
}
