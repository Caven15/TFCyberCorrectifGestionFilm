namespace TFCyberCorrectifGestionFilm.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Annee { get; set; }
    }
}
