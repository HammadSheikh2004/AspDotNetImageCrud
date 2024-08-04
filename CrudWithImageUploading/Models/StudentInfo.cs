using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudWithImageUploading.Models
{
    public class StudentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Std_Info_Id { get; set; }
        [Required]
        [DisplayName("Country")]
        public string Country { get; set; }
        [Required]
        [DisplayName("City")]
        public string City { get; set; }
        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [Required]
        [DisplayName("Image")]
        public string Std_Image { get; set; }
        public int Student_Id { get; set; }
        [ForeignKey(nameof(Student_Id))]
        public virtual Student Student { get; set;}
    }
}
