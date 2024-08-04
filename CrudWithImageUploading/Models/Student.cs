using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudWithImageUploading.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Std_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        [StringLength(50)]
        public string Std_Name { get; set;}
        [Required]
        [DisplayName("Email")]
        [StringLength(150)]
        [EmailAddress]
        public string Std_Email { get; set;}
        [Required]
        [StringLength(11)]
        [DisplayName("Phone")]
        public string Std_Phone { get; set;}
        public virtual ICollection<StudentInfo> StudentInfos { get; set; }

    }
}
