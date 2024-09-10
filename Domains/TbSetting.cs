

// Ignore Spelling: FacebookLink Facebook Googol Instagram

namespace Domains
{
    // Form Farid Farid
    public class TbSetting
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(400)]
        [ValidateNever]
        public string? Logo { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        [MaxLength(400)]
        public string? Copyright { get; set; }
        [MaxLength(400)]
        public string? Mail { get; set; }
        [MaxLength(400)]
        public string? Phone { get; set; }
        [MaxLength(400)]
        public string? Facebook_Link { get; set; }
        [MaxLength(400)]
        public string? Googol_Link { get; set; }
        [MaxLength(400)]
        public string? Twitter_Link { get; set; }
        [MaxLength(400)]
        public string? Instagram_Link { get; set; }
        [MaxLength(400)]
        public string? LinkedIn_Link { get; set; }
    }
}
