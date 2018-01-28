using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace APPER1
{
    [Activity(Label = "OpslagActivity")]
    public class OpslagActivity : Activity
    {
        ListView kleurLijst;
        string[] kleurNamen = { "AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond", "Blue", "BlueViolet", "Brown", "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Cornsilk", "Crimson", "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenrod", "DarkGray", "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "DarkOrange", "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue", "DimGray", "DodgerBlue", "Firebrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gainsboro", "GhostWhite", "Gold", "Goldenrod", "Gray", "Green", "GreenYellow", "Honeydew", "HotPink", "IndianRed", "Indigo", "Ivory", "Khaki", "Lavender", "LavenderBlush", "LawnGreen", "LemonChiffon", "LightBlue", "LightCoral", "LightCyan", "LightGoldenrodYellow", "LightGray", "LightGreen", "LightPink", "LightSalmon", "LightSeaGreen", "LightSkyBlue", "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta", "Maroon", "MediumAquamarine", "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed", "MidnightBlue", "MintCream", "MistyRose", "Moccasin", "NavajoWhite", "Navy", "OldLace", "Olive", "OliveDrab", "Orange", "OrangeRed", "Orchid", "PaleGoldenrod", "PaleGreen", "PaleTurquoise", "PaleVioletRed", "PapayaWhip", "PeachPuff", "Peru", "Pink", "Plum", "PowderBlue", "Purple", "Red", "RosyBrown", "RoyalBlue", "SaddleBrown", "Salmon", "SandyBrown", "SeaGreen", "SeaShell", "Sienna", "Silver", "SkyBlue", "SlateBlue", "SlateGray", "Snow", "SpringGreen", "SteelBlue", "Tan", "Teal", "Thistle", "Tomato", "Transparent", "Turquoise", "Violet" , "Wheat", "White", "WhiteSmoke", "Yellow", "YellowGreen" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TextView test = new TextView(this);
            test.Text = "Hieronder Zit u uw opgeslagen trainingen.";
            this.SetContentView(test);

            ArrayAdapter<string> kleurAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, kleurNamen);
            // probeer ook eens: SimpleListItemMultipleChoice en SimpleListItemActivated1
            kleurLijst = new ListView(this);
            kleurLijst.Adapter = kleurAdapter;
            kleurLijst.ChoiceMode = ChoiceMode.Multiple;
            kleurLijst.ItemClick += itemklik;
            kleurLijst.SetItemChecked(2, true);
            kleurLijst.SetItemChecked(4, true);
            kleurLijst.SetItemChecked(7, true);
            Button versturen = new Button(this);
            versturen.Text = "Voorkeur versturen";
            versturen.Click += verstuurKlik;
            
            LinearLayout stapel = new LinearLayout(this);
            stapel.Orientation = Orientation.Vertical;
            stapel.AddView(versturen);
            stapel.AddView(kleurLijst);
            this.SetContentView(stapel);
        }
        private void verstuurKlik(object sender, EventArgs e)
        {
            string bericht = "Dit vind ik mooie kleuren:\n";
            SparseBooleanArray a = kleurLijst.CheckedItemPositions;
            for (int k = 0; k < a.Size(); k++)
                if (a.ValueAt(k))
                    bericht += $"{kleurNamen[a.KeyAt(k)]}\n";
            Intent i = new Intent(Intent.ActionSend);
            i.SetType("text/plain");
            i.PutExtra(Intent.ExtraText, bericht);
            this.StartActivity(i);
        }
        private void itemklik(object sender, AdapterView.ItemClickEventArgs e)
        {
            string t = kleurNamen[e.Position];
            Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
        }

    }
}