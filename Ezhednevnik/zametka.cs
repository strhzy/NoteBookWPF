using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezhednevnik
{
    internal class zametka
    {
        public string Date {  get; set; }
        public string Title {  get; set; }
        public string Text {  get; set; }
        public zametka(string date, string title, string text)
        {
            this.Date = date;
            this.Title = title;
            this.Text = text;
        }
    }
    
}
