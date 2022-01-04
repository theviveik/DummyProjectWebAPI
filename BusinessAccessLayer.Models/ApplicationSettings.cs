namespace BusinessAccessLayer.Models
{
    public class ApplicationSettings
    {
        private string jWT_Secret = string.Empty;
        private string client_URL = string.Empty;

        public string JWT_Secret { get => jWT_Secret; set => jWT_Secret = value; }
        public string Client_URL { get => client_URL; set => client_URL = value; }
    }
}