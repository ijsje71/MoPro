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


        string xList, yList, verschilList, tijden, snelheden, somList;
        double doubleXList, doubleYList, doubleVerschil1, doubleVerschil2, doubleVerschil, som, afstandGelopen;
        TimeSpan tijd1, tijd2;

        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);

            TextView gemSnelheidTitel = new TextView(this);
            TextView gemSnelheid = new TextView(this);
            TextView maxSnelheidTitel = new TextView(this);
            TextView maxSnelheid = new TextView(this);
            TextView tijdsduurTitel = new TextView(this);
            TextView tijdsduur = new TextView(this);
            TextView gelopenAfstandTitel = new TextView(this);
            TextView gelopenAfstand = new TextView(this);
            TextView xCoordinaten = new TextView(this);
            TextView yCoordinaten = new TextView(this);
            TextView somCoordinaten = new TextView(this);
            TextView verschilCoordinaten = new TextView(this);

           
            string track = this.Intent.GetStringExtra("track");
            string[] splitTrack = track.Split();


            // Lengte -2 want door de \n in de track-string is er een extra lege string
            int lastIndex = splitTrack.Length - 2;
            string laatsteSplit = splitTrack[lastIndex];
            TimeSpan eersteTijd = TimeSpan.Parse(splitTrack[3]);
            TimeSpan laatsteTijd = TimeSpan.Parse(splitTrack[lastIndex]);
            TimeSpan tijdsduurSom = laatsteTijd - eersteTijd;

            
            // Alle x coordinaten
            int x;
            for (x = 0; x < lastIndex; x += 4)
            {
                xList = xList + splitTrack[x] + "\n";
            }
            // Alle y coordinaten
            int y;
            for (y = 1; y < lastIndex; y += 4)
            {
                yList = yList + splitTrack[y] + "\n";
            }
            // Alle x en y coordinaten in aparte arrays zetten
            string[] xListArray = xList.Split();
            string[] yListArray = yList.Split();

            // Alle x en y coordinaten die bij elkaar horen optellen
            for (x = 0; x < xListArray.Length - 1; x++)
            {
                doubleXList = double.Parse(xListArray[x]);
                doubleYList = double.Parse(yListArray[x]);
                som = doubleXList + doubleYList;
                somList = somList + som.ToString() + "\n";
            }

            // Het verschil uitrekenen tussen opeenvolgende coordinaten, om zo de afstand tussen 2 punten uit te rekenen
            string[] verschilArray = somList.Split();
            for (x = 1; x < verschilArray.Length - 1; x++)
            {
                doubleVerschil1 = double.Parse(verschilArray[x]);
                doubleVerschil2 = double.Parse(verschilArray[x - 1]);
                doubleVerschil = Math.Abs(doubleVerschil2 - doubleVerschil1);
                afstandGelopen = afstandGelopen + doubleVerschil;
                verschilList = verschilList + doubleVerschil.ToString() + "\n";
            }
            // Gemiddelde snelheid uitrekenen
            double gemsnelheid = (afstandGelopen / 1000) / tijdsduurSom.TotalHours;
           

            // De verstreken tijd uitrekenen tussen twee gelopen punten
            for (x = 7; x < lastIndex; x += 4)
            {
              
                tijd1 = TimeSpan.Parse(splitTrack[x]);
                tijd2 = TimeSpan.Parse(splitTrack[x-4]);
                tijden = tijden + (tijd1 - tijd2).ToString() + "\n";
            }

            // Aan de hand van de afstanden en verstreken tijd tussen de punten de snelheid berekenen
            string[] afstandverschil = verschilList.Split();
            string[] tijdenverschil = tijden.Split();
            for (x = 0; x < afstandverschil.Length-1; x++)
            {
                double snelheid = (double.Parse(afstandverschil[x]) / 1000) / TimeSpan.Parse(tijdenverschil[0]).TotalHours;
                 snelheden = snelheden + snelheid.ToString() + "\n";
            }

          
            gemSnelheidTitel.Text = "Gemiddelde snelheid:";
            gemSnelheid.Text = gemsnelheid.ToString() + " km/h";

            // verschilCoordinaten.Text = snelheden;
            maxSnelheidTitel.Text = "Maximale snelheid gelopen";

            // Maximale waarde van snelheden 
            maxSnelheid.Text = snelheden.Split().Max() + " km/h";
            tijdsduurTitel.Text = "Totale tijdsduur:";
            tijdsduur.Text = tijdsduurSom.ToString();

            gelopenAfstandTitel.Text = "Gelopen afstand:";
            gelopenAfstand.Text = afstandGelopen.ToString() + " meter";
            



            LinearLayout verticaal = new LinearLayout(this);
            verticaal.Orientation = Orientation.Vertical;
            verticaal.AddView(tijdsduurTitel);
            verticaal.AddView(tijdsduur);
            verticaal.AddView(gelopenAfstandTitel);
            verticaal.AddView(gelopenAfstand);
            verticaal.AddView(maxSnelheidTitel);
            verticaal.AddView(maxSnelheid);
            verticaal.AddView(gemSnelheidTitel);
            verticaal.AddView(gemSnelheid);
            this.SetContentView(verticaal);

        }
    }
}