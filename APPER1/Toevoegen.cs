using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Graphics;

namespace APPER1
{
    [ActivityAttribute(Label = "Toevoegen", MainLauncher = false)]
    class Toevoegen : Activity
    {
        // Declaratie van de EditText view waar de track ingevoerd wordt
        public EditText naamVeld;
        

        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);

            LinearLayout stapel = new LinearLayout(this);
            // Letterlijk de bovenste regel van het scherm met de tekst "Naam:"
            TextView naamLabel = new TextView(this);
            naamLabel.Text = "Naam:";

            // Declaratie van de knoppen
            Button okButton = new Button(this);
            okButton.Text = "OK";
            Button cancelButton = new Button(this);
            cancelButton.Text = "Cancel";

            // Toekenningsopdrachten voor de EditText view waar de track in komt
            // EditText zodat de gebruiker nog eventueel wat aan kan passen aan de track voordat het opgeslagen wordt
            naamVeld = new EditText(this);

            // Maximum hoogte van de TextView zodat de knoppen altijd zicht- en klikbaar blijven
            naamVeld.SetMaxHeight(1000);

            // Gelopen track wordt uit de intent gehaald en in een string gestopt
            string lijstString = this.Intent.GetStringExtra("track3");
           
            // Gelopen track wordt in de EditText view gezet
            naamVeld.Text = lijstString;
            
           
            // Toekenning van de event handlers voor de knoppen
            okButton.Click += ok;
            cancelButton.Click += cancel;

            // Layout van het scherm
            stapel.Orientation = Orientation.Vertical;
            stapel.AddView(naamLabel);
            stapel.AddView(naamVeld);
            stapel.AddView(okButton);
            stapel.AddView(cancelButton);
            this.SetContentView(stapel);
        }

        // Event handler voor de cancel knop
        private void cancel(object sender, EventArgs e)
        {
            // Verwijst de gebruiker terug naar OpslagActivity zonder dat het punt is opgeslagen
            Intent i = new Intent();
            this.SetResult(Result.Canceled, i);
            this.Finish();
        }

        // Event Handler voor de OK knop
        private void ok(object sender, EventArgs e)
        {
            // Intent de de result voor het toevoegen van de knop dirigeert
            Intent i = new Intent();
            // Geeft de track mee met de response
            i.PutExtra("naam", naamVeld.Text);
            // Verwijst de gebruiker terug naar OpslagActivity
            this.SetResult(Result.Ok, i);
            this.Finish();
        }
    }
}
