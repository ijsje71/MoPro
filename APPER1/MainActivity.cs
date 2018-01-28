using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Views;
using Android.Content;
using System;
using Android.Hardware;
using Android.Locations;
using Android.Runtime;

namespace APPER1
{
    [Activity(Label = "HardloopApp", MainLauncher = true /*, Icon = "@drawable/icon"*/)]
    public class MainActivity : Activity , ILocationListener
    {
        TextView coordinaten;   
        KaartView utrecht;
        Button startstop;
        
        protected override void OnResume()
        {
            base.OnResume();
            // Gps Signaal zoeken voor de tekst
            LocationManager lm = (LocationManager)base.GetSystemService(Context.LocationService);
            Criteria crit = new Criteria();
            crit.Accuracy = Accuracy.Fine;
            string lp = lm.GetBestProvider(crit, true);
            if (lp != null)
                lm.RequestLocationUpdates(lp, 0, 0, this);
        }
        protected override void OnCreate(Bundle b)
        {
            base.OnCreate(b);

            //verwijzing naar klasse KaartView
            utrecht = new KaartView(this);
            KaartView kaartview = new KaartView(this);

            // TekstView met de huidige coordinaten
            coordinaten = new TextView(this);
            coordinaten.SetBackgroundColor(Color.Black);
            coordinaten.Text = "Zoeken naar GPS...";

            // Centreer knop
            Button centreer = new Button(this);
            centreer.Text = "Centreer";
            centreer.SetTextColor(Color.White);

            //eventhandler voor de centreerknop in de kaartview klasse
            centreer.Click += utrecht.Centreren;

            //declaraties voor de start/stop knop
            startstop = new Button(this);
            startstop.Text = "Start";
            startstop.SetTextColor(Color.White);

            //declaratie van de eventhandler in de MainActivity
            startstop.Click += this.StartStop;

            //declaratie van de eventhandler in de kaartview klasse
            startstop.Click += utrecht.StartStop;

            //declaraties voor de opschonen knop
            Button opschonen = new Button(this);
            opschonen.Text = "Opschonen";
            opschonen.SetTextColor(Color.White);

            //declaratie van de eventhandler in de MainActivity
            opschonen.Click += this.Opschonen;

            // Declaraties voor de Share knop
            Button delen = new Button(this);
            delen.Text = "Delen";
            delen.SetTextColor(Color.White);

            // Declaraties voor de 'opslag' knop
            Button opslaan = new Button(this);
            opslaan.Text = "Save";
            opslaan.SetTextColor(Color.White);
          

            // Declaraties voor de 'opslag' knop
            Button laden = new Button(this);
            laden.Text = "Load";
            laden.SetTextColor(Color.White);
            laden.Click += this.Opslag;


            // Event handler voor de share knop
            delen.Click += Delen;
            
            // Opmaak van de knoppen
            LinearLayout horizontaal = new LinearLayout(this);
            horizontaal.Orientation = Orientation.Horizontal;
            horizontaal.AddView(centreer);
            horizontaal.AddView(startstop);
            horizontaal.AddView(opschonen);
            horizontaal.AddView(delen);

            LinearLayout horizontaal2 = new LinearLayout(this);
            horizontaal2.Orientation = Orientation.Horizontal;
            horizontaal2.AddView(opslaan);
            horizontaal2.AddView(laden);

            // Opmaak van het scherm
            LinearLayout verticaal = new LinearLayout(this);
            verticaal.Orientation = Orientation.Vertical;
            verticaal.AddView(horizontaal);
            verticaal.AddView(horizontaal2);
            verticaal.AddView(coordinaten);
            verticaal.AddView(utrecht);

            this.SetContentView(verticaal);
        }
        public void StartStop(object o, EventArgs ea)  // EventHandler voor de Start/Stop knop
        {
           // If-statement als de knop nog niet is ingedrukt
            if (!utrecht.volg)
            {
                startstop.Text = "Stop";
                utrecht.trainingGestart = true;
            }
            // If-statement als de knop al eerder in is gedrukt
            else if (utrecht.volg)
            {
                startstop.Text = "Start";
                utrecht.trainingGestart = false;
            }
        }

        // Eventhandler voor de opschonen knop, deze neemt de dialoog in handen
        public void Opschonen(object o, EventArgs ea) {

            //If opdracht die checkt of de training überhaupt is begonnen
            if (utrecht.looppad.Count != 0){
                startstop.Text = "Start";
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Weet je het zeker?");
                alert.SetMessage("Weet je zeker dat je jouw training wilt opschonen?");
                alert.SetPositiveButton("Ja", utrecht.Opschonen);
                // Verwijzing naar de Cancel methode
                alert.SetNegativeButton("Nee", this.Cancel);
                alert.Create().Show();
            }
            // Foutmelding als er nog geen looppad is om op te schonen
            else 
            {
                AlertDialog.Builder popup = new AlertDialog.Builder(this);
                popup.SetTitle("Training nog niet gestart");
                popup.SetMessage("Er is een fout opgetreden. De training kan niet worden opgeschoond, als de training nog niet is gestart!");
                // Verwijzing naar de Cancel methode
                popup.SetPositiveButton("OK", this.Cancel);
                popup.Create().Show();
            }
        }

        public void Delen(object o, EventArgs ea)
        {
            // If-statement die checkt of de training al is gestart voordat de track gedeeld
            if (utrecht.looppad.Count == 0)
            {
                AlertDialog.Builder deelPopup = new AlertDialog.Builder(this);
                deelPopup.SetTitle("Training nog niet gestart");
                deelPopup.SetMessage("Er is een fout opgetreden. De training kan niet worden gedeeld, als de training nog niet is gestart!");
                // Verwijzing naar de Cancel methode
                deelPopup.SetPositiveButton("OK", this.Cancel);
                deelPopup.Create().Show();
            } else { 

            string bericht = utrecht.Bericht(utrecht.looppad);
            Intent i = new Intent(Intent.ActionSend);
            i.SetType("text/plain");
            i.PutExtra(Intent.ExtraText, bericht);
            this.StartActivity(i);
            }
        }

        public void Opslag(object o, EventArgs ea)
        {
            StartActivity(typeof(OpslagActivity));
        }


        public void Cancel(object o, EventArgs ea)  // Eventhandler voor de nee/OK knop in de dialoog.
        {
                // Aanpassen van de Start/Stop knop met verschillende situaties
             if (utrecht.looppad.Count != 0)
            {
                // Pad wordt gelopen en nog gevolgd. Opschonen wordt gecancelled
                if (utrecht.volg)
                {
                    startstop.Text = "Stop";
                    utrecht.volg = true;
                }
                // Pad wordt gelopen maar het volgen is gepauzeerd. Opschonen wordt gecancelled
                else if (!utrecht.volg)
                {
                    startstop.Text = "Start";
                    utrecht.volg = false;
                }
            }
             else
            {
                // Wanneer er wordt opgeschoond en de foutmelding wordt weergeven
                if (utrecht.looppad.Count == 0)
                {
                    startstop.Text = "Start";
                    utrecht.volg = false;
                }
                /*else
                {
                    startstop.Text = "Stop";
                    utrecht.volg = false;
                } */
            }
       
       
        }
        public void OnLocationChanged(Location location)       // Zorgt ervoor dat de coordinaten goed worden opgeslagen en weergegeven voor de gebruiker
        {
            utrecht.noord = location.Latitude;
            utrecht.oost = location.Longitude;
            String info = $"{utrecht.noord} graden noorderbreedte, {utrecht.oost} graden oosterlengte";
            coordinaten.Text = info;

        }
        // Benodigde methoden die verplicht aangeroepen moeten worden
        public void OnProviderDisabled(string provider)
        {
          
        }

        public void OnProviderEnabled(string provider)
        {
           
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
           
        }
    }
}






