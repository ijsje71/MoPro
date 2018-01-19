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
    [Activity(Label = "HardloopApp", MainLauncher = true , Icon = "@drawable/icon")]
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


            coordinaten = new TextView(this);
            coordinaten.SetBackgroundColor(Color.Black);
            coordinaten.Text = "Zoeken naar GPS...";
            
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
            
            LinearLayout horizontaal = new LinearLayout(this);
            horizontaal.Orientation = Orientation.Horizontal;
            horizontaal.AddView(centreer);
            horizontaal.AddView(startstop);
            horizontaal.AddView(opschonen);

            LinearLayout verticaal = new LinearLayout(this);
            verticaal.Orientation = Orientation.Vertical;
            verticaal.AddView(horizontaal);
            verticaal.AddView(coordinaten);
            verticaal.AddView(utrecht);

            this.SetContentView(verticaal);
        }
        public void StartStop(object o, EventArgs ea)  // EventHandler voor de Start/Stop knop
        {
           
            if (!utrecht.volg)
            {
                startstop.Text = "Stop";
                utrecht.trainingGestart = true;
            }
            else if (utrecht.volg)
            {
                startstop.Text = "Start";
                utrecht.trainingGestart = false;
            }
        }

        //eventhandler voor de opschonen knop
        public void Opschonen(object o, EventArgs ea) {

         
            if (utrecht.looppad.Count != 0){
                startstop.Text = "Start";
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Weet je het zeker?");
                alert.SetMessage("Weet je zeker dat je jouw training wilt opschonen?");
                alert.SetPositiveButton("Ja", utrecht.Opschonen);
                alert.SetNegativeButton("Nee", this.Cancel);
                alert.Create().Show();
            }
            else 
            {
                AlertDialog.Builder popup = new AlertDialog.Builder(this);
                popup.SetTitle("Training nog niet gestart");
                popup.SetMessage("Er is een fout opgetreden. De training kan niet worden opgeschoond, als de training nog niet is gestart!");
                popup.SetPositiveButton("OK", this.Cancel);
                popup.Create().Show();
            }
        }

       
        public void Cancel(object o, EventArgs ea)  // Eventhandler voor de nee/OK knop in de dialoog.
        {
          
             if (utrecht.looppad.Count != 0)
            {
                if (utrecht.volg)
                {
                    startstop.Text = "Stop";
                    utrecht.volg = true;
                }
                else if (!utrecht.volg)
                {
                    startstop.Text = "start";
                    utrecht.volg = false;
                }
            }
             else
            {
                if (utrecht.looppad.Count == 0)
                {
                    startstop.Text = "Start";
                    utrecht.volg = false;
                }
                else
                {
                    startstop.Text = "Stop";
                    utrecht.volg = false;
                }
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






