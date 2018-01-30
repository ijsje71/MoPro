using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace APPER1
{
    public class KleurenAdapter : BaseAdapter<KleurItem>
    {
        Activity context;
        IList<KleurItem> items;

        public KleurenAdapter(Activity context, IList<KleurItem> items)
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override KleurItem this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View hergebruik, ViewGroup root)
        {
            TextView view = (TextView)hergebruik;
            if (view == null)
                view = new TextView(context);
            view.TextSize = 30;
            view.SetHeight(100);
            KleurItem item = items[position];
            view.Text = $"{item.Id}: {item.Naam}";
           

            return view;
        }
        /*  alternatieve versie:
        public override View GetView(int position, View hergebruik, ViewGroup root)
        {   TextView view = (TextView)(hergebruik
                                      ?? context.LayoutInflater.Inflate
                                           (Android.Resource.Layout.SimpleListItem1, null)
                                      );
            KleurItem item = items[position];
            view.Text = $"{item.Id}: {item.Naam}";
            view.SetBackgroundColor(item.Kleur);
            return view;
        }
        */
    }
}