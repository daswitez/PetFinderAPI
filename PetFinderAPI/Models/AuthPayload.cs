namespace PetFinderAPI.Models
{
    public class AuthPayload
    {
        public string Token { get; set; } = null!; 
        public Usuario Usuario { get; set; } = null!; 
    }
}
