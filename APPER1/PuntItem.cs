using Android.Graphics;


namespace APPER1
{
    public class PuntItem
    {
        [PrimaryKey, AutoIncrement]
        
        // Zet de ID van het nieuw opgeslagen track, of haalt de id van de al bestaande track op
        public int Id { get; set; }

        // Zet de waarden van het nieuw opgeslagen track, of haalt deze op
        public string Naam { get; set; }

        public PuntItem()
        {
        }

        // Koppelt de meegegeven waarde aan het Naam attribuut in de database
        public PuntItem(string naam)
        {
            this.Naam = naam;
          
        }
    }
}