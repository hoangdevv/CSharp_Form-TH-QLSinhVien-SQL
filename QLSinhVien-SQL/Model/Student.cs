namespace QLSinhVien_SQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [StringLength(50)]
        public string studentID { get; set; }

        [StringLength(50)]
        public string fullName { get; set; }

        public double? averageScore { get; set; }

        public int? facultyID { get; set; }

        public virtual Faculty Faculty { get; set; }
    }
}
