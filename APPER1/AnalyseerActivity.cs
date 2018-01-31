using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace APPER1
{
    [Activity(Label = "AnalyseerActivity")]
    public class AnalyseerActivity : Activity
    {
        

        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);

            TextView GemSnelheidTitel = new TextView(this);
            GemSnelheidTitel.Text = "Gemiddelde snelheid:";
            TextView gemSnelheid = new TextView(this);
            TextView maxSnelheidTitel = new TextView(this);
            maxSnelheidTitel.Text = "Maximale snelheid gelopen";
            TextView maxSnelheid = new TextView(this);
            TextView tijdsduurTitel = new TextView(this);
            tijdsduurTitel.Text = "Totale tijdsduur:";
            TextView tijdsduur = new TextView(this);
            TextView gelopenAfstandTitel = new TextView(this);
            gelopenAfstandTitel.Text = "Gelopen afstand:";
            TextView gelopenAfstand = new TextView(this);


            string track = this.Intent.GetStringExtra("track");
            string [] splitTrack = track.Split();
            //string test = $" 0: {splitTrack[0]} + 1: {splitTrack[1]} + 2: {splitTrack[2]} + 3: {splitTrack[3]} + 4: {splitTrack[4]} + 5: {splitTrack[5]} + 6: {splitTrack[6]} + 7: {splitTrack[7]} + 8:{splitTrack[8]} + 9: {splitTrack[9]} + 10: {splitTrack[10]} + 11: {splitTrack[11]}";
            //tijdsduur.Text = (splitTrack.Length).ToString();

            // Lengte -2 want door de \n in de track-string is er een extra lege string
            int lastIndex = splitTrack.Length - 2;
            string laatsteSplit = splitTrack[lastIndex];
            TimeSpan eersteTijd = TimeSpan.Parse(splitTrack[3]);
            TimeSpan laatsteTijd = TimeSpan.Parse(splitTrack[lastIndex]);
            TimeSpan tijdsduurSom = laatsteTijd - eersteTijd;



            tijdsduur.Text = tijdsduurSom.ToString();
           // tijdsduur.Text = track;

            //int j = -1;
            //for (int i = 0; i < (splitTrack.Length / 4); i++) {
            //    j = j + 4;
            //    tijdsduur.Text += $"{splitTrack[j]} \n";
            //    
            //}

            LinearLayout verticaal = new LinearLayout(this);
            verticaal.Orientation = Orientation.Vertical;
            verticaal.AddView(gemSnelheid);
            verticaal.AddView(maxSnelheid);
            verticaal.AddView(tijdsduur);
            verticaal.AddView(gelopenAfstand);

            this.SetContentView(verticaal);

        }
    }
}