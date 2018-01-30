using Android.Graphics;


namespace APPER1
{
    public class KleurItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Naam { get; set; }
       /* public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }*/

        public KleurItem()
        {
        }

        public KleurItem(string naam)
        {
            this.Naam = naam;
          
        }
      /*  public Color Kleur
        {
            get { return new Color(R, G, B); }
        }*/
    }
}