using System;             // Math
using Android.Views;      // View & TouchEventArgs
using Android.Hardware;   // SensorManager & ISensorEventListener
using Android.Locations;  // Locationc & ILocationListener
using Android.Graphics;   // Paint & Canvas & PointF
using Android.Content;    // Context
using Android.OS;         // Bundle
using System.Collections.Generic;  // List


namespace APPER1
{

    public class KaartView : View , ISensorEventListener , ILocationListener
    {

        public bool trainingGestart;  //public boolean die bijhoudt of de training gestart is of niet
        Bitmap utrecht, loper;        // declaratie van de bitmaps van de kaart en het icoontje.
        float Schaal;
        public double noord, oost;
        public bool volg = false;  //boolean om aan te duiden of de locatie gevolgd en getekend moet worden
        public List<Opslaan> looppad = new List<Opslaan>();   //lijst van alle punten waar de gebruiker geweest is
        float Hoek;                                    
        PointF uithof = new PointF(142000, 459600);  // Uithof positie, uitgangspunt van de app
        PointF centrum = new PointF(139000, 455500);  // Centrum van de kaart
        float dx, dy, ax, ay;

        public KaartView(Context c) : base(c)
        {
            this.SetBackgroundColor(Color.Black);
            BitmapFactory.Options opt = new BitmapFactory.Options();
            opt.InScaled = false;                                       // Zorgt ervoor dat de kaart oningezoomd wordt getoond.
            utrecht = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.kaart, opt);   // Tekent de kaart
            loper = BitmapFactory.DecodeResource(c.Resources, Resource.Drawable.point, opt);    // tekent de loper

            SensorManager sm = (SensorManager)c.GetSystemService(Context.SensorService);             
            sm.RegisterListener(this, sm.GetDefaultSensor(SensorType.Orientation), SensorDelay.Ui);

            LocationManager lm = (LocationManager)c.GetSystemService(Context.LocationService);
            Criteria crit = new Criteria();
            crit.Accuracy = Accuracy.Fine;
            string lp = lm.GetBestProvider(crit, true);
            if (lp != null)
                lm.RequestLocationUpdates(lp, 0, 0, this);
            this.Touch += RaakAaan;
        }
      
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (Schaal == 0)
                Schaal = Math.Min(((float)this.Width) / this.utrecht.Width, ((float)this.Height) / this.utrecht.Height);

            Paint verf = new Paint();
            float middenX = (centrum.X - 136000) / 2.5f;
            float middenY = (458000 - centrum.Y) / 2.5f;

            Matrix mat = new Matrix();                    
            mat.PostTranslate(-middenX, -middenY);             // Zorgt ervoor dat de kaart op de goede plek wordt getekend
            mat.PostScale(this.Schaal, this.Schaal);
            mat.PostTranslate(this.Width / 2, this.Height / 2);
            canvas.DrawBitmap(utrecht, mat, verf);
            
            mat = new Matrix();
            mat.PostTranslate((-loper.Width) / 2, (-loper.Height) / 2);        // Zorgt ervoor dat de positie op de goede plek wordt getekend
            float r = 10;                                                        // Zorgt ervoor dat de pointer dezelfde grootte blijft bij het in- en uitzoomen
            r = r * this.Schaal;
            mat.PostScale(this.Schaal / r, this.Schaal / r);
            mat.PostRotate(this.Hoek);
            mat.PostTranslate((this.Width / 2 + (uithof.X - centrum.X) / 2.5f * this.Schaal), this.Height / 2 + (centrum.Y - uithof.Y) / 2.5f * this.Schaal);
            canvas.DrawBitmap(this.loper, mat, verf);
            Paint kleur = new Paint();
            kleur.Color = Color.Red;
                             // Zorgt ervoor dat de pointer dezelfde grootte blijft bij het in- en uitzoomen
            foreach (Opslaan punt in looppad)
            {
                
                float bitmapx = (punt.x - centrum.X) / 2.5f;  // Opslaan punten omzetten in schermrelatieve pixels
                float bitmapy = (centrum.Y - punt.y) / 2.5f;

                float schermx = bitmapx * this.Schaal;
                float schermy = bitmapy * this.Schaal;

                float x = this.Width / 2 + schermx;
                float y = this.Height / 2 + schermy;
                mat.PostScale(this.Schaal, this.Schaal);
                canvas.DrawCircle(x, y, 9, kleur);          // Tekent het gelopen pad
            }

        }
        public void OnSensorChanged(SensorEvent e)            // Methode die ervoor zorgt dat de pointer in de goede richting wordt getekend
        {
            this.Hoek = e.Values[0];
            this.Invalidate();
        }
     
        public void OnLocationChanged(Location location)     // Methode die ervoor zorgt dat er elke keer een nieuw punt wordt geregistreerd als de locatie is gewijzigd
        {
            PointF geo = new PointF((float)location.Latitude, (float)location.Longitude);
            uithof = Projectie.Geo2RD(geo);
            if (volg)
            {
                looppad.Add(new Opslaan(uithof)); 
            }
            this.Invalidate();
        }
        public void OnAccuracyChanged(Sensor s, SensorStatus accuracy)      
        {
        }

        public void OnProviderEnabled(string s)
        {
        }



        public void OnProviderDisabled(string s)
        {

        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }
        static float Afstand(PointF p1, PointF p2)         // Berekeningen voor het pinchen
        {
            float a = p2.X - p1.X;
            float b = p2.Y - p1.Y;
            return (float)Math.Sqrt(a * a + b * b);
        }
   
        PointF start1;   // variabelen die nodig zijn voor het pinchen en draggen.
        PointF start2;
        PointF huidig1;
        bool pinchen = false;
        PointF huidig2;
        float oudeSchaal;
        PointF oudeCentrum;

        public void RaakAaan(object o, TouchEventArgs tea)
        {
            huidig1 = new PointF(tea.Event.GetX(0), tea.Event.GetY(0));
            
            if (tea.Event.Action == MotionEventActions.Down)
            {
                start1 = huidig1;
                oudeCentrum = centrum;
                oudeSchaal = Schaal;
            }
            if (tea.Event.PointerCount == 1)   // If-statement voor het draggen
            {
            
                if (!pinchen)
                {
                     dx = huidig1.X - start1.X;  // In scherm pixels de afstand tot midden 
                     dy = huidig1.Y - start1.Y;
                     ax = (dx / oudeSchaal) * 2.5f;  // In meters afstand tot midden
                     ay = (dy / oudeSchaal) * 2.5f;

                    centrum = new PointF(oudeCentrum.X - ax, oudeCentrum.Y + ay);
                 
                    
                    this.Invalidate();
                }
            }

            if (tea.Event.PointerCount == 2)    // If-statement voor het pinchen
            {
                pinchen = true;
                huidig2 = new PointF(tea.Event.GetX(1), tea.Event.GetY(1));
                if (tea.Event.Action == MotionEventActions.Pointer2Down)
                {
                    start1 = huidig1;
                    start2 = huidig2;

                }
                float oud = Afstand(start2, start1);
                float nieuw = Afstand(huidig2, huidig1);
                if (oud != 0 && nieuw != 0)
                {
                    float factor = nieuw / oud;
                    Schaal = oudeSchaal * factor;
                    if (Schaal > 10) Schaal = 10;   // Limiteer schaal zodat er niet oneindig in en uitgezoomd kan worden
                    if (Schaal < 0.4) Schaal = 0.4f;
                }

            }
            if (tea.Event.Action == MotionEventActions.Up)
            {
                pinchen = false;                  // Zorgt ervoor dat je kan draggen na het pinchen
                if (oudeCentrum.X > 145000)
                    oudeCentrum.X = 145000;
                if (oudeCentrum.X < 133000)
                    oudeCentrum.X = 133000;

                if (oudeCentrum.Y > 461500)
                    oudeCentrum.Y = 461500;

                if (oudeCentrum.Y < 449500)
                    oudeCentrum.Y = 449500;

                centrum = new PointF(oudeCentrum.X - ax, oudeCentrum.Y + ay);

                Console.WriteLine("JAWEL");
                this.Invalidate();
            }
         

            this.Invalidate();

        }
        public void Centreren(object o, EventArgs ea)  // Regelt de functionaliteit van de centreerknop
        {
            centrum = uithof;
            this.Invalidate();
        }

        //eventhandler voor de start/stop knop
        public void StartStop(object o, EventArgs ea)
        {
            volg = !volg;
            this.Invalidate();
        }

        /*een van de eventhandlers voor de opschonen knop,
          deze schoont daadwerkelijk het looppad op*/
        public void Opschonen(object o, EventArgs ea)
        {
            looppad.Clear();
            //zorgt ervoor dat de locatie niet meer gevolgd wordt
            volg = false;
            //zorgt ervoor dat de correcte dialoog laten zien wanneer de opschonen knop is ingedrukt
            trainingGestart = false;
            this.Invalidate();
        }
        // Opslaan van alle punten gebeurt in een klasse
        public class Opslaan
        {
            public float x;
            public float y;

            public Opslaan(PointF p)
            {
                x = p.X;
                y = p.Y;
            }

            public Opslaan(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

        }
    }
}