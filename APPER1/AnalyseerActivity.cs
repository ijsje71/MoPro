﻿using System;
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
        KaartView utrecht;

        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);

            // Create your application here
            TextView gemSnelheid = new TextView(this);
            
            TextView maxSnelheid = new TextView(this);
            TextView tijdsduur = new TextView(this);
            TextView gelopenAfstand = new TextView(this);
            utrecht = new KaartView(this);

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



            //tijdsduur.Text = tijdsduurSom.ToString();


            int tijd = -1;
            int datum = -2;
            int y = -3;
            int x = -4;
            for (int i = 0; i < (splitTrack.Length / 4); i++) {
                datum = datum + 4;
                tijd = tijd + 4;
                y = y + 4;
                x = x + 4;
                string datetime = splitTrack[datum] + splitTrack[tijd];
                DateTime datumtijd = DateTime.Parse(datetime);
                int rdx = int.Parse(splitTrack[x]);
                int rdy = int.Parse(splitTrack[y]);
                float rddx = (float)rdx;
                float rddy = (float)rdy;
                utrecht.nepLooppad.Add(new KaartView.Opslaan(rddx, rddy, datumtijd));
            }

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