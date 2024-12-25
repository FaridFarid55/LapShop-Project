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
        [Required(ErrorMessage = "Please Enter Image")]
        public string? Logo { get; set; }

        [MaxLength(2000)]
        [Required(ErrorMessage = "Please Enter Description")]
        public string? Description { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Copyright")]
        public string? Copyright { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Mail")]
        [DataType(DataType.EmailAddress)]
        public string? Mail { get; set; }

        [Required(ErrorMessage = "Please Enter Phone")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid Number")]
        [RegularExpression(@"^01[0,1,2,5]{1}[0-9]{8}$", ErrorMessage = "Invalid phone number format.")]
        public string? Phone { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Facebook Link")]
        public string? Facebook_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Googol Link")]
        public string? Googol_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter TWitter Link")]
        public string? Twitter_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Instagram Link")]
        public string? Instagram_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter LinkedIN Link")]
        public string? LinkedIn_Link { get; set; }
    }
}
