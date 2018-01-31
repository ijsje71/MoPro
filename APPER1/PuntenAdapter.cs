using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace APPER1
{
    public class PuntenAdapter : BaseAdapter<PuntItem>
    {
        // Enkele Declaraties
        Activity context;
        IList<PuntItem> items;

        // Verbindt de context en de items uit de database in 1 methode
        public PuntenAdapter(Activity context, IList<PuntItem> items)
        {
            this.context = context;
            this.items = items;
        }

        // Methode om de positie van het opgeslagen punt uit de database te halen
        public override long GetItemId(int position)
        {
            return position;
        }

        // Methode om de track op te halen uit de database
        public override PuntItem this[int position]
        {
            get { return items[position]; }
        }

        // Methode die telt hoe veel records er in de database
        public override int Count
        {
            get { return items.Count; }
        }

        // Regelt de View waar de opgeslagen tracks in worden laten zien
        public override View GetView(int position, View hergebruik, ViewGroup root)
        {
            // Declaratie en toekenningsopdracht van de TextView
            TextView view = (TextView)hergebruik;
            // Checkt of de view al bestaat, indien de view nog niet bestaat wordt een nieuwe view aangemaakt
            if (view == null)
                view = new TextView(context);
            // Zet de grootte van de tekst en de hoogte per track in de lijst
            view.TextSize = 30;
            view.SetHeight(200);

            // Database record wordt in een variabele opgeslagen en als tekst attribuut in de view gezet
            PuntItem item = items[position];
            view.Text = $"{item.Id}: {item.Naam}";
           

            return view;
        }
    }
}