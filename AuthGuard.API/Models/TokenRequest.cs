using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthGuard.API.Models
{
    public class TokenRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ClientId { get; set; } 
        
        [Required]
        public string ClientSecret { get; set; } 
        
        [Required]
        public List<string> ClientScopes { get; set; }
        
        [Required]
        public string GrantType { get; set; }
    }
}