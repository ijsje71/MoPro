using System;           // vanwege EventArgs
using System.Collections.Generic; // vanwege List
using Android.App;      // vanwege Activity
using Android.Content;  // vanwege Intent
using Android.Widget;   // vanwege ListView, ArrayAdapter, ChoiceMode, Button enz.
using Android.OS;       // vanwege Bundle
using System.IO;        // vanwege File


namespace APPER1
{
    [ActivityAttribute(Label = "Gelopen punten", MainLauncher = false)]
    public class OpslagActivity : Activity
    {
        // Enkele declaraties
        ListView puntenLijst; // ListView met daarin de opgeslagen punten
        List<PuntItem> punten; // List van alle punten in de track
        PuntenAdapter puntAdapter; // Formatter voor gegevens uit de database
        public string track; // De track die opgeslagen moet worden

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.BeginPunten(); // Initialiseert de database volgens de BeginPunten methode

            // Toekenningsopdrachten voor de lijst met opgeslagen punten
            puntenLijst = new ListView(this);
            puntenLijst.ChoiceMode = ChoiceMode.None;

            // Event Handler voor wanneer een track wordt aangeklikt
            puntenLijst.ItemClick += itemKlik;

            // Laadt alle punten uit de database volgens de LeesPunten methode
            this.LeesPunten();

            // De gelopen track wordt opgehaald uit de Intent vanuit MainActivity
            track = this.Intent.GetStringExtra("track2");

            // Knop om een nieuw punt op te slaan
            Button nieuw = new Button(this);
            nieuw.Text = "Nieuw item";
            nieuw.Click += nieuwItem;

            Button terug = new Button(this);
            terug.Text = "Terug naar de kaart";
            terug.Click += Terug;

            LinearLayout knoppen = new LinearLayout(this);
            knoppen.AddView(nieuw);
            knoppen.AddView(terug);
            
            // Layout van het scherm
            LinearLayout stapel = new LinearLayout(this);
            stapel.Orientation = Orientation.Vertical;
            stapel.AddView(knoppen);
            stapel.AddView(puntenLijst);
            this.SetContentView(stapel);
        }

        // Event Handler voor het klikken van de "Nieuw item" knop
        private void nieuwItem(object sender, EventArgs e)
        {
            // Intent die de gebruiker doorverwijst naar de Toevoegen activity
            Intent i = new Intent(this, typeof(Toevoegen));
            i.SetType("text/plain");
            // Stuurt de gelopen track mee met de intent
            i.PutExtra("track3", track);
            // Start de activiteit wetende dat er een result vanuit Toevoegen activity gaat komen
            this.StartActivityForResult(i, 1000000);
        }

        // Event Handler voor het klikken op een reeds opgeslagen track
        private void itemKlik(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Bepaald de positie in de database van het geselecteerde item 
            int pos = e.Position;
            PuntItem item = puntAdapter[pos];

            // Zet het geselecteerde item om in een string
            string output = item.Naam.ToString();
            
            // Intent om de intent vanuit MainActivity te sluiten
            Intent i = new Intent();
            // Gekozen track wordt meegegeven in het afsluiten van de intent
            i.PutExtra("eindwaarde", output);
            // Verwijst de gebruiker succesvol terug naar de MainActivity
            this.SetResult(Result.Ok, i);
            this.Finish();

        }

        // Koppelt de database aan een variabele
        SQLiteConnection database;

        // Methode die de database opzoekt in het locale filesysteem
        protected void BeginPunten()
        {
            string docsFolder = System.Environment.GetFolderPath
                                  (System.Environment.SpecialFolder.MyDocuments);
            string pad = System.IO.Path.Combine(docsFolder, "punten.db");
            bool bestaat = File.Exists(pad);
            database = new SQLiteConnection(pad);
            // Creeërt de database als de gebruiker die nog niet lokaal heeft staan.
            if (!bestaat)
            {
                database.CreateTable<PuntItem>();
            }
        }

        // Methode die de inhoud van de database laadt en in de lijst stopt
        protected void LeesPunten()
        {
            punten = new List<PuntItem>();
            TableQuery<PuntItem> query = database.Table<PuntItem>();
            foreach (PuntItem k in query)
                punten.Add(k);
            puntAdapter = new PuntenAdapter(this, punten);
            puntenLijst.Adapter = puntAdapter;
        }

        // Event Handler van de terug knop
        public void Terug(object o, EventArgs ea)
        {
            // Verwijst de gebruiker terug naar MainActivity zonder dat het punt is opgeslagen
            Intent i = new Intent();
            this.SetResult(Result.Canceled, i);
            this.Finish();
        }
        
        // Methode die de response van Toevoegen activity onder handen neemt
        protected override void OnActivityResult(int pos, Result res, Intent data)
        {
            // Wanneer er een positieve result terug is gegeven
            if (res == Result.Ok)
            {
                // Haalt de ingevoerde waarde uit de result en stopt het in een string
                string naam = data.GetStringExtra("naam");
                // Checkt of het wel de goede response is
                if (pos == 1000000)
                    // Voegt het punt toe in de database
                    database.Insert(new PuntItem(naam));
                // Als het een andere response is
                else
                {
                    PuntItem k = new PuntItem(naam);
                    // Bepaald de id van het item meegegeven in de response
                    k.Id = punten[pos].Id;
                    // Update de desbetreffende record in de database
                    database.Update(k);
                }
            }
            else
            {
                // Als het deze specifieke response is
                if (pos < 1000000)
                {
                    PuntItem k = new PuntItem();
                    // Bepaalt de ID van het item meegegeven in de response
                    k.Id = punten[pos].Id;
                    // Delete de desbetreffende record uit de database
                    database.Delete(k);
                }
            }
            // Na de aanpassingen wordt de database weer opnieuw geladen op het scherm
            this.LeesPunten();
        } 
    }
}
