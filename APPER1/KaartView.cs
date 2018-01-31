using System;             // Math
using Android.Views;      // View & TouchEventArgs
using Android.Hardware;   // SensorManager & ISensorEventListener
using Android.Locations;  // Locationc & ILocationListener
using Android.Graphics;   // Paint & Canvas & PointF
using Android.Content;    // Context
using Android.OS;         // Bundle
using System.Collections.Generic;  // List
using Android.Widget;


namespace APPER1
{



    public class KaartView : View, ISensorEventListener, ILocationListener
    {

        public bool trainingGestart;  //public boolean die bijhoudt of de training gestart is of niet
        Bitmap utrecht, loper;        // declaratie van de bitmaps van de kaart en het icoontje.
        float Schaal;
        public double noord, oost;
        public bool volg = false;  //boolean om aan te duiden of de locatie gevolgd en getekend moet worden
        public List<Opslaan> looppad = new List<Opslaan>();   //lijst van alle punten waar de gebruiker geweest is

        public List<Opslaan> nepLooppad = new List<Opslaan>(); //lijst voor de nep track
        float Hoek;

       

        DateTime[] nepTijden = {
new DateTime(   2018,1,30,  10,00,30  ),
new DateTime(   2018,1,30,  10,00,31    ),
new DateTime(   2018,1,30,  10,00,32    ),
new DateTime(   2018,1,30,  10,00,33    ),
new DateTime(   2018,1,30,  10,00,34    ),
new DateTime(   2018,1,30,  10,00,35    ),
new DateTime(   2018,1,30,  10,00,36    ),
new DateTime(   2018,1,30,  10,00,37    ),
new DateTime(   2018,1,30,  10,00,38    ),
new DateTime(   2018,1,30,  10,00,39    ),
new DateTime(   2018,1,30,  10,00,40    ),
new DateTime(   2018,1,30,  10,00,41    ),
new DateTime(   2018,1,30,  10,00,42    ),
new DateTime(   2018,1,30,  10,00,43    ),
new DateTime(   2018,1,30,  10,00,44    ),
new DateTime(   2018,1,30,  10,00,45    ),
new DateTime(   2018,1,30,  10,00,46    ),
new DateTime(   2018,1,30,  10,00,47    ),
new DateTime(   2018,1,30,  10,00,48    ),
new DateTime(   2018,1,30,  10,00,49    ),
new DateTime(   2018,1,30,  10,00,50    ),
new DateTime(   2018,1,30,  10,00,51    ),
new DateTime(   2018,1,30,  10,00,52    ),
new DateTime(   2018,1,30,  10,00,53    ),
new DateTime(   2018,1,30,  10,00,54    ),
new DateTime(   2018,1,30,  10,00,55    ),
new DateTime(   2018,1,30,  10,00,56    ),
new DateTime(   2018,1,30,  10,00,57    ),
new DateTime(   2018,1,30,  10,00,58    ),
new DateTime(   2018,1,30,  10,00,59    ),
new DateTime(   2018,1,30,  10,01,00    ),
new DateTime(   2018,1,30,  10,01,01    ),
new DateTime(   2018,1,30,  10,01,02    ),
new DateTime(   2018,1,30,  10,01,03    ),
new DateTime(   2018,1,30,  10,01,04    ),
new DateTime(   2018,1,30,  10,01,05    ),
new DateTime(   2018,1,30,  10,01,06    ),
new DateTime(   2018,1,30,  10,01,07    ),
new DateTime(   2018,1,30,  10,01,08    ),
new DateTime(   2018,1,30,  10,01,09    ),
new DateTime(   2018,1,30,  10,01,10    ),
new DateTime(   2018,1,30,  10,01,11    ),
new DateTime(   2018,1,30,  10,01,12    ),
new DateTime(   2018,1,30,  10,01,13    ),
new DateTime(   2018,1,30,  10,01,14    ),
new DateTime(   2018,1,30,  10,01,15    ),
new DateTime(   2018,1,30,  10,01,16    ),
new DateTime(   2018,1,30,  10,01,17    ),
new DateTime(   2018,1,30,  10,01,18    ),
new DateTime(   2018,1,30,  10,01,19    ),
new DateTime(   2018,1,30,  10,01,20    ),
new DateTime(   2018,1,30,  10,01,21    ),
new DateTime(   2018,1,30,  10,01,22    ),
new DateTime(   2018,1,30,  10,01,23    ),
new DateTime(   2018,1,30,  10,01,24    ),
new DateTime(   2018,1,30,  10,01,25    ),
new DateTime(   2018,1,30,  10,01,26    ),
new DateTime(   2018,1,30,  10,01,27    ),
new DateTime(   2018,1,30,  10,01,28    ),
new DateTime(   2018,1,30,  10,01,29    ),
new DateTime(   2018,1,30,  10,01,30    ),
new DateTime(   2018,1,30,  10,01,31    ),
new DateTime(   2018,1,30,  10,01,32    ),
new DateTime(   2018,1,30,  10,01,33    ),
new DateTime(   2018,1,30,  10,01,34    ),
new DateTime(   2018,1,30,  10,01,35    ),
new DateTime(   2018,1,30,  10,01,36    ),
new DateTime(   2018,1,30,  10,01,37    ),
new DateTime(   2018,1,30,  10,01,38    ),
new DateTime(   2018,1,30,  10,01,39    ),
new DateTime(   2018,1,30,  10,01,40    ),
new DateTime(   2018,1,30,  10,01,41    ),
new DateTime(   2018,1,30,  10,01,42    ),
new DateTime(   2018,1,30,  10,01,43    ),
new DateTime(   2018,1,30,  10,01,44    ),
new DateTime(   2018,1,30,  10,01,45    ),
new DateTime(   2018,1,30,  10,01,46    ),
new DateTime(   2018,1,30,  10,01,47    ),
new DateTime(   2018,1,30,  10,01,48    ),
new DateTime(   2018,1,30,  10,01,49    ),
new DateTime(   2018,1,30,  10,01,50    ),
new DateTime(   2018,1,30,  10,01,51    ),
new DateTime(   2018,1,30,  10,01,52    ),
new DateTime(   2018,1,30,  10,01,53    ),
new DateTime(   2018,1,30,  10,01,54    ),
new DateTime(   2018,1,30,  10,01,55    ),
new DateTime(   2018,1,30,  10,01,56    ),
new DateTime(   2018,1,30,  10,01,57    ),
new DateTime(   2018,1,30,  10,01,58    ),
new DateTime(   2018,1,30,  10,01,59    ),
new DateTime(   2018,1,30,  10,02,00    ),
new DateTime(   2018,1,30,  10,02,01    ),
new DateTime(   2018,1,30,  10,02,02    ),
new DateTime(   2018,1,30,  10,02,03    ),
new DateTime(   2018,1,30,  10,02,04    ),
new DateTime(   2018,1,30,  10,02,05    ),
new DateTime(   2018,1,30,  10,02,06    ),
new DateTime(   2018,1,30,  10,02,07    ),
new DateTime(   2018,1,30,  10,02,08    ),
new DateTime(   2018,1,30,  10,02,09    ),
new DateTime(   2018,1,30,  10,02,10    ),
new DateTime(   2018,1,30,  10,02,11    ),
new DateTime(   2018,1,30,  10,02,12    ),
new DateTime(   2018,1,30,  10,02,13    ),
new DateTime(   2018,1,30,  10,02,14    ),
new DateTime(   2018,1,30,  10,02,15    ),
new DateTime(   2018,1,30,  10,02,16    ),
new DateTime(   2018,1,30,  10,02,17    ),
new DateTime(   2018,1,30,  10,02,18    ),
new DateTime(   2018,1,30,  10,02,19    ),
new DateTime(   2018,1,30,  10,02,20    ),

new DateTime(   2018,1,30,  10,02,21    ) };



        PointF[] nepPunten = { new PointF(  (float)52.08705, (float)5.168281  ),
new PointF((float)52.08705, (float)5.168281),
new PointF((float)52.08702, (float)5.168191),
new PointF((float)52.08715, (float)5.167818),
new PointF((float)52.08715, (float)5.167841),
new PointF((float)52.08716, (float)5.167843),
new PointF((float)52.08717, (float)5.167835),
new PointF((float)52.08717, (float)5.16787 ),
new PointF((float)52.08717, (float)5.167865),
new PointF((float)52.08717, (float)5.167864),
new PointF((float)52.08716, (float)5.167912),
new PointF((float)52.08716, (float)5.167931),
new PointF((float)52.08716, (float)5.167952),
new PointF((float)52.08716, (float)5.167974),
new PointF((float)52.08716, (float)5.167997),
new PointF((float)52.08716, (float)5.168017),
new PointF((float)52.08716, (float)5.168036),
new PointF((float)52.08716, (float)5.168056),
new PointF((float)52.08715, (float)5.168074),
new PointF((float)52.08715, (float)5.168092),
new PointF((float)52.08714, (float)5.16811 ),
new PointF((float)52.08714, (float)5.168128),
new PointF((float)52.08714, (float)5.168147),
new PointF((float)52.08714, (float)5.168166),
new PointF((float)52.08715, (float)5.168185),
new PointF((float)52.08715, (float)5.168205),
new PointF((float)52.08714, (float)5.168226),
new PointF((float)52.08714, (float)5.168248),
new PointF((float)52.08714, (float)5.16827 ),
new PointF((float)52.08715, (float)5.168296),
new PointF((float)52.08715, (float)5.168322),
new PointF((float)52.08715, (float)5.168348),
new PointF((float)52.08715, (float)5.168374),
new PointF((float)52.08715, (float)5.168401),
new PointF((float)52.08715, (float)5.168429),
new PointF((float)52.08714, (float)5.168451),
new PointF((float)52.08713, (float)5.168466),
new PointF((float)52.08712, (float)5.168473),
new PointF((float)52.0871, (float)5.168478 ),
new PointF((float)52.08709, (float)5.168484),
new PointF((float)52.08708, (float)5.168494),
new PointF((float)52.08707, (float)5.168506),
new PointF((float)52.08706, (float)5.168521),
new PointF((float)52.08705, (float)5.168538),
new PointF((float)52.08704, (float)5.168552),
new PointF((float)52.08704, (float)5.168566),
new PointF((float)52.08703, (float)5.168577),
new PointF((float)52.08702, (float)5.16858 ),
new PointF((float)52.08701, (float)5.168577),
new PointF((float)52.08699, (float)5.168576),
new PointF((float)52.08698, (float)5.168575),
new PointF((float)52.08697, (float)5.168579),
new PointF((float)52.08696, (float)5.168586),
new PointF((float)52.08695, (float)5.168592),
new PointF((float)52.08693, (float)5.168594),
new PointF((float)52.08692, (float)5.168591),
new PointF((float)52.08691, (float)5.168589),
new PointF((float)52.08689, (float)5.168587),
new PointF((float)52.08688, (float)5.168587),
new PointF((float)52.08687, (float)5.16859 ),
new PointF((float)52.08686, (float)5.168599),
new PointF((float)52.08685, (float)5.168612),
new PointF((float)52.08683, (float)5.168625),
new PointF((float)52.08682, (float)5.168633),
new PointF((float)52.0868, (float)5.168636 ),
new PointF((float)52.08678, (float)5.168635),
new PointF((float)52.08677, (float)5.168635),
new PointF((float)52.08675, (float)5.168634),
new PointF((float)52.08674, (float)5.16863 ),
new PointF((float)52.08672, (float)5.168626),
new PointF((float)52.08671, (float)5.168625),
new PointF((float)52.0867, (float)5.168617 ),
new PointF((float)52.08669, (float)5.168604),
new PointF((float)52.08667, (float)5.168591),
new PointF((float)52.08666, (float)5.168585),
new PointF((float)52.08665, (float)5.168586),
new PointF((float)52.08664, (float)5.168589),
new PointF((float)52.08662, (float)5.168592),
new PointF((float)52.08661, (float)5.168596),
new PointF((float)52.0866, (float)5.168597 ),
new PointF((float)52.08658, (float)5.1686 ),
new PointF((float)52.08657, (float)5.168606),
new PointF((float)52.08655, (float)5.168617),
new PointF((float)52.08654, (float)5.168624),
new PointF((float)52.08652, (float)5.168634),
new PointF((float)52.08652, (float)5.168649),
new PointF((float)52.0865, (float)5.168659 ),
new PointF((float)52.08649, (float)5.168664),
new PointF((float)52.08648, (float)5.168659),
new PointF((float)52.08646, (float)5.168656),
new PointF((float)52.08645, (float)5.168651),
new PointF((float)52.08644, (float)5.168652),
new PointF((float)52.08642, (float)5.168655),
new PointF((float)52.08641, (float)5.168662),
new PointF((float)52.0864, (float)5.168663 ),
new PointF((float)52.08638, (float)5.168657),
new PointF((float)52.08637, (float)5.168658),
new PointF((float)52.08636, (float)5.16867 ),
new PointF((float)52.08635, (float)5.168683),
new PointF((float)52.08634, (float)5.168685),
new PointF((float)52.08633, (float)5.168676),
new PointF((float)52.08632, (float)5.168667),
new PointF((float)52.0863, (float)5.168667),
new PointF((float)52.08629, (float)5.168669),
new PointF((float)52.08627, (float)5.168679),
new PointF((float)52.08627, (float)5.168699),
new PointF((float)52.08627, (float)5.168721),
new PointF((float)52.08627, (float)5.168744),
new PointF((float)52.08627, (float)5.168764),
new PointF((float)52.08627, (float)5.168787),
new PointF((float)52.08627, (float)5.168809),
new PointF((float)52.08627, (float)5.168832)};

                                  

        PointF uithof = new PointF(140500, 455000);  // Uithof positie, uitgangspunt van de app
        PointF centrum = new PointF(139000, 455500);  // Centrum van de kaart
        float dx, dy, ax, ay;
        List<DateTime> tijdstippen = new List<DateTime>();
        int i = 0;
        float drawX;
        float drawY;



        public KaartView(Context c) :
            base(c)
        {
            foreach (PointF punt in nepPunten)
            {
                PointF rdPoint = Projectie.Geo2RD(punt);
                nepLooppad.Add(new Opslaan(rdPoint.X, rdPoint.Y, nepTijden[i]));
                i++;
            }

            this.SetBackgroundColor(Color.Black);
            // Declareert de bitmap options aan een variabele
            BitmapFactory.Options opt = new BitmapFactory.Options();
            opt.InScaled = false;                                       // Zorgt ervoor dat de kaart oningezoomd wordt getoond.
            utrecht = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.kaart, opt);   // Declareert de kaart aan een variabele
            loper = BitmapFactory.DecodeResource(c.Resources, Resource.Drawable.point, opt);    // Declareert de loper aan een variabele

            // Declareert de sensor manager
            SensorManager sm = (SensorManager)c.GetSystemService(Context.SensorService);
            sm.RegisterListener(this, sm.GetDefaultSensor(SensorType.Orientation), SensorDelay.Ui);

            // Declareert de Location managers voor de GPS
            LocationManager lm = (LocationManager)c.GetSystemService(Context.LocationService);
            Criteria crit = new Criteria();
            crit.Accuracy = Accuracy.Fine;
            string lp = lm.GetBestProvider(crit, true);
            if (lp != null)
                lm.RequestLocationUpdates(lp, 0, 0, this);
            // Event handler voor wanneer de kaart wordt aangeraakt
            this.Touch += RaakAaan;


        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            

            // Schaal indien de applicatie net is opgestart
            if (Schaal == 0)
                Schaal = Math.Min(((float)this.Width) / this.utrecht.Width, ((float)this.Height) / this.utrecht.Height);

            Paint verf = new Paint();
            // Middelpunt omzetten naar schermrelatieve pixels
            float middenX = (centrum.X - 136000) / 2.5f;
            float middenY = (458000 - centrum.Y) / 2.5f;

            // Matrix voor de kaart
            Matrix mat = new Matrix();
            mat.PostTranslate(-middenX, -middenY);             // Zorgt ervoor dat de kaart op de goede plek wordt getekend
            mat.PostScale(this.Schaal, this.Schaal);
            mat.PostTranslate(this.Width / 2, this.Height / 2);
            // Tekenen van de kaart met de eigen matrix
            Paint kleur = new Paint();
            kleur.Color = Color.Red;
            canvas.DrawBitmap(utrecht, mat, verf);
            

            // Matrix voor de loper
            mat = new Matrix();
            mat.PostTranslate((-loper.Width) / 2, (-loper.Height) / 2);        // Zorgt ervoor dat de positie op de goede plek wordt getekend
            float r = 10;                                                        // Zorgt ervoor dat de pointer dezelfde grootte blijft bij het in- en uitzoomen
            r = r * this.Schaal;
            mat.PostScale(this.Schaal / r, this.Schaal / r);
            mat.PostRotate(this.Hoek);
            mat.PostTranslate((this.Width / 2 + (uithof.X - centrum.X) / 2.5f * this.Schaal), this.Height / 2 + (centrum.Y - uithof.Y) / 2.5f * this.Schaal);
            // Tekenen van de loper met de eigen schalen
            canvas.DrawBitmap(this.loper, mat, verf);

            // Loop die alle punten in de klasse af gaat
            foreach (Opslaan punt in looppad)
            {

                float bitmapx = (punt.x - centrum.X) / 2.5f;  // Opslaan punten omzetten in schermrelatieve pixels
                float bitmapy = (centrum.Y - punt.y) / 2.5f;

                float schermx = bitmapx * this.Schaal;        // Omzetten naar de gebruikte schaal
                float schermy = bitmapy * this.Schaal;

                float x = this.Width / 2 + schermx;
                float y = this.Height / 2 + schermy;
                mat.PostScale(this.Schaal, this.Schaal);
                canvas.DrawCircle(x, y, 9, kleur);          // Tekent het gelopen pad
            }
            foreach (Opslaan punt in nepLooppad)
            {

                float bitmapx = (punt.x - centrum.X) / 2.5f;  // nep punten omzetten in schermrelatieve pixels
                float bitmapy = (centrum.Y - punt.y) / 2.5f;

                float schermx = bitmapx * this.Schaal;        // Omzetten naar de gebruikte schaal
                float schermy = bitmapy * this.Schaal;

                float x = this.Width / 2 + schermx;
                float y = this.Height / 2 + schermy;
                mat.PostScale(this.Schaal, this.Schaal);
                canvas.DrawCircle(x, y, 9, kleur);          // Tekent het nep pad
            }

            foreach (Opslaan punt in nepLooppad)
            {

                float bitmapx = (punt.x - centrum.X) / 2.5f;  // nep punten omzetten in schermrelatieve pixels
                float bitmapy = (centrum.Y - punt.y) / 2.5f;

                float schermx = bitmapx * this.Schaal;        // Omzetten naar de gebruikte schaal
                float schermy = bitmapy * this.Schaal;

                float x = this.Width / 2 + schermx;
                float y = this.Height / 2 + schermy;
                mat.PostScale(this.Schaal, this.Schaal);
                canvas.DrawCircle(x, y, 9, kleur);          // Tekent het nep pad
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


            if (looppad.Count == 0) {
                drawX = uithof.X;
                drawY = uithof.Y;
                if (volg)
                looppad.Add(new Opslaan(uithof.X, uithof.Y, DateTime.Now));

            }
            // Huidige locatie in RD coordinaten
            if (volg && (Math.Abs(uithof.X - drawX) > 10 || Math.Abs(uithof.Y - drawY) > 10))
            {
                // Huidige punt toevoegen aan de klasse
                drawX = uithof.X;
                drawY = uithof.Y;

              

                looppad.Add(new Opslaan(uithof.X, uithof.Y, DateTime.Now));

            }
            this.Invalidate();
        }
        // Verplichte maar ongebruikte methoden voor de sensormanager en locationmanager
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
        public string stringLooppad = "";

        public void RaakAaan(object o, TouchEventArgs tea)
        {
            // Opslaan van de positie van de eerste vinger
            huidig1 = new PointF(tea.Event.GetX(0), tea.Event.GetY(0));

            if (tea.Event.Action == MotionEventActions.Down)
            {
                // Zet huidige positie van de vinger, het centrum en de huidige schaal in variabelen
                start1 = huidig1;
                oudeCentrum = centrum;
                oudeSchaal = Schaal;
            }
            if (tea.Event.PointerCount == 1)   // If-statement voor het draggen
            {
                // If-statement zodat de kaart niet dragt en pincht tegelijkertijd
                if (!pinchen)
                {
                    dx = huidig1.X - start1.X;  // In scherm pixels de afstand tot midden 
                    dy = huidig1.Y - start1.Y;
                    ax = (dx / oudeSchaal) * 2.5f;  // In meters afstand tot midden
                    ay = (dy / oudeSchaal) * 2.5f;

                    // Nieuwe centrum van de kaart vaststellen
                    centrum = new PointF(oudeCentrum.X - ax, oudeCentrum.Y + ay);


                    this.Invalidate();
                }
            }

            if (tea.Event.PointerCount == 2)    // If-statement voor het pinchen
            {
                // Aangeven dat er gepinched wordt
                pinchen = true;
                // Opslaan van de positie van de tweede vinger
                huidig2 = new PointF(tea.Event.GetX(1), tea.Event.GetY(1));

                if (tea.Event.Action == MotionEventActions.Pointer2Down)
                {
                    // Opslaan van de startpositie van de twee vingers
                    start1 = huidig1;
                    start2 = huidig2;

                }
                // Berekent de afstand tussen de vingers in startpositie
                float oud = Afstand(start2, start1);
                // Berekent de afstand tussen de vingers in de tweede positie
                float nieuw = Afstand(huidig2, huidig1);

                if (oud != 0 && nieuw != 0)
                {
                    // Berekent de nieuwe schaal tijdens het pinchen
                    float factor = nieuw / oud;
                    Schaal = oudeSchaal * factor;
                    // Limiteer schaal zodat er niet oneindig in en uitgezoomd kan worden
                    if (Schaal > 10) Schaal = 10;
                    if (Schaal < 0.4) Schaal = 0.4f;
                }

            }
            if (tea.Event.Action == MotionEventActions.Up)
            {
                pinchen = false;                  // Zorgt ervoor dat je kan draggen na het pinchen
                // Limiteren van hoe ver er gedragged kan worden
                if (oudeCentrum.X > 145000)
                    oudeCentrum.X = 145000;

                if (oudeCentrum.X < 133000)
                    oudeCentrum.X = 133000;

                if (oudeCentrum.Y > 461500)
                    oudeCentrum.Y = 461500;

                if (oudeCentrum.Y < 449500)
                    oudeCentrum.Y = 449500;

                // Initializeren van nieuwe centrum na draggen
                centrum = new PointF(oudeCentrum.X - ax, oudeCentrum.Y + ay);
                this.Invalidate();
            }

         
            // Zorgt ervoor dat de kaart meeschaalt tijdens het pinchen
            this.Invalidate();

        }
        public void Centreren(object o, EventArgs ea)  // Regelt de functionaliteit van de centreerknop
        {
            // Zet het centrum naar de uitgangspositie (Uithof)
            centrum = uithof;
            this.Invalidate();
        }

        //eventhandler voor de start/stop knop
        public void StartStop(object o, EventArgs ea)
        {
            // Zet de boolean op de tegenovergestelde waarde
            volg = !volg;
            if (volg)
                nepLooppad.Clear();
            this.Invalidate();
        }

        /*een van de eventhandlers voor de opschonen knop,
          deze schoont daadwerkelijk het looppad op*/
        public void Opschonen(object o, EventArgs ea)
        {
            // List wordt opgeschoond
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
            public DateTime tijd;

            public Opslaan(PointF p)
            {

                // Zet de x en de y in 1 punt
                x = p.X;
                y = p.Y;
            }
            public Opslaan(float x, float y, DateTime tijd)
            {
                this.x = x;
                this.y = y;
                this.tijd = tijd;

            }

        }
        public string Bericht(List<Opslaan> looppad)
        {
            //stringLooppad = "";
            foreach (Opslaan punt in looppad)
            {

                PointF convertPunt = new PointF(punt.x, punt.y);
                //PointF geoPunt = Projectie.RD2Geo(convertPunt);
                stringLooppad += $"{convertPunt.X} {convertPunt.Y} {punt.tijd}\n";
            }

            return stringLooppad;
        }
        
    }
}