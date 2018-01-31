using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace APPER1
{
    class GrafiekView : View
    {
        public string Invoer = "";
        public List<TimeSpan> tijdlist;
        //public string snelheden;

        public GrafiekView(Context c) : base(c)
        {
            this.SetBackgroundColor(Color.White);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            AnalyseerActivity analyseer = new AnalyseerActivity();

         //   List<TimeSpan> tijdList = new List<TimeSpan>();

            // turf alle letters in de invoer-string
            int aantalTijden = tijdlist.Count();
            int[] tellers = new int[aantalTijden];
           /* foreach (char symbool in Invoer)
            {
                if (symbool >= 'a' && symbool <= 'z')
                    tellers[symbool - 'a']++;
                else if (symbool >= 'A' && symbool <= 'Z')
                    tellers[symbool - 'A']++;
            }*/
            // wat is het hoogste aantal?
            int max = 0;
            foreach (int afstand in tellers)
            {
                if (afstand > max)
                    max = afstand;
            }
            if (max < 10)
                max = 10;  // zodat het diagram aan het begin niet overdreven grote balkjes krijgt

            Paint verf = new Paint();
            int x = 100, y = 0, w = this.Width - x - 10, h = this.Height / 16;

            // teken gridlines
            verf.Color = Color.Red;
            for (int a = 0; a < max; a += 5)
            {
                int d = w * a / max;
                canvas.DrawLine(x + d, 0, x + d, h * 15, verf);
            }
            // teken een balkje met bijschrift voor elk van de 26 aantallen
            int getal = 15;
            verf.TextSize = 40;
            foreach (int aantal in tellers)
            {
                verf.Color = Color.Black;
                canvas.DrawText($"{getal} km/u", 20, y + h - 4, verf);
                verf.Color = Color.Blue;
                canvas.DrawCircle(x , y + h - 2, 8, verf);
                y += h; getal--;
            }
        }


    }
}