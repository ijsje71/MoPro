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
      //  Color kleur;
        public EditText naamVeld;
       //SeekBar rood, groen, blauw;
        Button huidig;
        //static int volgnummer = 1;
        KaartView utrecht;

        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);

            string naam = this.Intent.GetStringExtra("naam");
          //  kleur = new Color(this.Intent.GetIntExtra("kleur", 0));

          /*  rood = new SeekBar(this); rood.Max = 255; rood.Progress = kleur.R; rood.SetBackgroundColor(Color.Red);
            groen = new SeekBar(this); groen.Max = 255; groen.Progress = kleur.G; groen.SetBackgroundColor(Color.Green);
            blauw = new SeekBar(this); blauw.Max = 255; blauw.Progress = kleur.B; blauw.SetBackgroundColor(Color.Blue);*/

            LinearLayout stapel = new LinearLayout(this);
            TextView naamLabel = new TextView(this);
            naamLabel.Text = "Naam:";
            Button okButton = new Button(this);
            okButton.Text = "OK";
            Button cancelButton = new Button(this);
            cancelButton.Text = "Cancel";

            utrecht = new KaartView(this);

          /*  if (naam == null)
            {
                naam = $"nieuw {volgnummer}"; volgnummer++;
             //   this.willekeurig(null, null);
            }*/
            naamVeld = new EditText(this);
           
            string lijstString = this.Intent.GetStringExtra("track3");
           // string lijstString = utrecht.Bericht(utrecht.looppad);
            
            
            Console.WriteLine("HOI IK WORDT UBERHAUPT UITGEVOERD" + lijstString);


            naamVeld.Text = lijstString;
            this.huidig = new Button(this);
           // this.veranderd(null, null);

          //  rood.ProgressChanged += veranderd;
           // groen.ProgressChanged += veranderd;
            //blauw.ProgressChanged += veranderd;
            //huidig.Click += willekeurig;
            okButton.Click += ok;
            cancelButton.Click += cancel;

            stapel.Orientation = Orientation.Vertical;
            stapel.AddView(naamLabel);
            stapel.AddView(naamVeld);
          //  stapel.AddView(rood);
           // stapel.AddView(groen);
           // stapel.AddView(blauw);
           // stapel.AddView(huidig);
            stapel.AddView(okButton);
            stapel.AddView(cancelButton);
            this.SetContentView(stapel);
        }

      /*  private void willekeurig(object sender, EventArgs e)
        {
            Random generator = new Random();
            rood.Progress = generator.Next(255);
            groen.Progress = generator.Next(255);
            blauw.Progress = generator.Next(255);
        }*/

       /* private void veranderd(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            int r = rood.Progress;
            int g = groen.Progress;
            int b = blauw.Progress;
            huidig.Text = $"#{r:X2}{g:X2}{b:X2}";
            this.kleur = new Color(r, g, b);
            huidig.SetBackgroundColor(kleur);
        } */

        private void cancel(object sender, EventArgs e)
        {
            Intent i = new Intent();
            this.SetResult(Result.Canceled, i);
            this.Finish();
        }

        private void ok(object sender, EventArgs e)
        {
            Intent i = new Intent();
            i.PutExtra("naam", naamVeld.Text);
           // i.PutExtra("kleur", kleur.ToArgb());
            this.SetResult(Result.Ok, i);
            this.Finish();
        }
    }
}



/*using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Graphics;

namespace APPER1
{
    [ActivityAttribute(Label = "Kleur", MainLauncher = false)]
    class Toevoegen : Activity
    {
       
        EditText naamVeld;
       
       
        static int volgnummer = 1;

        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);

            string naam = this.Intent.GetStringExtra("naam");
            

            LinearLayout stapel = new LinearLayout(this);
            TextView naamLabel = new TextView(this); naamLabel.Text = "Naam:";
            Button okButton = new Button(this); okButton.Text = "OK";
            Button cancelButton = new Button(this); cancelButton.Text = "Cancel";

            if (naam == null)
            {
                naam = $"nieuw {volgnummer}"; volgnummer++;
             
            }
            this.naamVeld = new EditText(this); naamVeld.Text = naam;
          
            
          
            okButton.Click += ok;
            cancelButton.Click += cancel;

            stapel.Orientation = Orientation.Vertical;
            stapel.AddView(naamLabel);
            stapel.AddView(naamVeld);
           
           
            stapel.AddView(okButton);
            stapel.AddView(cancelButton);
            this.SetContentView(stapel);
        }

        

        private void cancel(object sender, EventArgs e)
        {
            Intent i = new Intent();
            this.SetResult(Result.Canceled, i);
            this.Finish();
        }

        private void ok(object sender, EventArgs e)
        {
            Intent i = new Intent();
            i.PutExtra("naam", naamVeld.Text);
           
            this.SetResult(Result.Ok, i);
            this.Finish();
        }
    }
}   */





