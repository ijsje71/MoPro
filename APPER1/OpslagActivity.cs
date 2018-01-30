using System;           // vanwege EventArgs
using System.Collections.Generic; // vanwege List
using Android.App;      // vanwege Activity
using Android.Content;  // vanwege Intent
using Android.Widget;   // vanwege ListView, ArrayAdapter, ChoiceMode, Button enz.
using Android.OS;       // vanwege Bundle
using Android.Graphics; // vanwege Color;
using System.IO;        // vanwege File


namespace APPER1
{
    [ActivityAttribute(Label = "Kleuren3", MainLauncher = false)]
    public class OpslagActivity : Activity
    {
        ListView kleurLijst;
        List<KleurItem> kleuren;
        KleurItem[] defaultKleuren = { new KleurItem("Rood"), new KleurItem("Groen"), new KleurItem("Geel") };
        KleurenAdapter kleurAdapter;
        KaartView utrecht;
        public string track;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.BeginKleuren();
            kleurLijst = new ListView(this);
            kleurLijst.ChoiceMode = ChoiceMode.None;
            kleurLijst.ItemClick += itemKlik;
            this.LeesKleuren();

            track = this.Intent.GetStringExtra("track2");

            Button nieuw = new Button(this);
            nieuw.Text = "Nieuw item";
            nieuw.Click += nieuwItem;
            Button versturen = new Button(this);
            versturen.Text = "Voorkeur versturen";
            versturen.Click += verstuurKlik;

            LinearLayout rij = new LinearLayout(this);
            rij.Orientation = Orientation.Horizontal;
            rij.AddView(nieuw);
            rij.AddView(versturen);

            LinearLayout stapel = new LinearLayout(this);
            stapel.Orientation = Orientation.Vertical;
            stapel.AddView(rij);
            stapel.AddView(kleurLijst);
            this.SetContentView(stapel);
        }
        private void verstuurKlik(object sender, EventArgs e)
        {
             string bericht = "<html><body><table>\n";
            //string bericht = "F U C C me up jeron \n";
            foreach (KleurItem item in kleuren)
            {
               // bericht += $"<tr bgcolor=#{item.Kleur.R:X2}{item.Kleur.G:X2}{item.Kleur.B:X2}>";
               //ericht += $"<td>{item.Naam}</td></tr>\n";

               // bericht += $"bgcolor=#{item.Kleur.R:X2}{item.Kleur.G:X2}{item.Kleur.B:X2}";
               // bericht += $"{item.Naam}\n";
            }
            //bericht += "</table></body></html>\n";
            Intent i = new Intent(Intent.ActionSend);
            i.SetType("text/html");
            i.PutExtra(Intent.ExtraText, bericht);
            this.StartActivity(i);
        }

        private void nieuwItem(object sender, EventArgs e)
        {
            utrecht = new KaartView(this);
            //string bericht = utrecht.Bericht(utrecht.looppad);
            Console.WriteLine("OPSLAGACTIVITY HIERO HALLO" + track);
            Intent i = new Intent(this, typeof(Toevoegen));
            i.SetType("text/plain");
            i.PutExtra("track3", track);
            this.StartActivity(i);
            this.StartActivityForResult(i, 1000000);
        }

        private void itemKlik(object sender, AdapterView.ItemClickEventArgs e)
        {
            int pos = e.Position;
            Intent i = new Intent(this, typeof(Toevoegen));
            KleurItem item = kleurAdapter[pos];
            i.PutExtra("track3", track);
         //   i.PutExtra("kleur", item.Kleur.ToArgb());
            this.StartActivityForResult(i, pos);

            utrecht.nepLooppad.Clear();

            string [] berichtSplit = track.Split();
            foreach (string s in berichtSplit)
            {
               //  utrecht.nepLooppad.Add(s);
               // utrecht.nepLooppad.Add(new KaartView.Opslaan(rdPoint.X, rdPoint.Y, nepTijden[1]));

            }


        } 

        // Dit is de klasse KleurenApp3
        // Tot hier was het hetzelfde als KleurenApp2. Maar de rest is anders:

        SQLiteConnection database;

        protected void BeginKleuren()
        {
            string docsFolder = System.Environment.GetFolderPath
                                  (System.Environment.SpecialFolder.MyDocuments);
            string pad = System.IO.Path.Combine(docsFolder, "kleuren.db");
            bool bestaat = File.Exists(pad);
            database = new SQLiteConnection(pad);
            if (!bestaat)
            {
                database.CreateTable<KleurItem>();
                foreach (KleurItem k in defaultKleuren)
                    database.Insert(k);
            }
        }

        protected void LeesKleuren()
        {
            kleuren = new List<KleurItem>();
            TableQuery<KleurItem> query = database.Table<KleurItem>();
            foreach (KleurItem k in query)
                kleuren.Add(k);
            kleurAdapter = new KleurenAdapter(this, kleuren);
            kleurLijst.Adapter = kleurAdapter;
        }

        protected override void OnActivityResult(int pos, Result res, Intent data)
        {
            if (res == Result.Ok)
            {
                string naam = data.GetStringExtra("naam");
                Color kleur = new Color(data.GetIntExtra("kleur", 0));
                if (pos == 1000000)
                    database.Insert(new KleurItem(naam));
                else
                {
                    KleurItem k = new KleurItem(naam);
                    k.Id = kleuren[pos].Id;
                    database.Update(k);
                }
            }
            else
            {
                if (pos < 1000000)
                {
                    KleurItem k = new KleurItem();
                    k.Id = kleuren[pos].Id;
                    database.Delete(k);
                }
            }
            this.LeesKleuren();
        } 
    }
}
