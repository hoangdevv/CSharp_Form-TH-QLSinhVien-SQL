using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSinhVien_SQL.Model;

namespace QLSinhVien_SQL
{
    public partial class FormSearch : Form
    {
        DataBaseStudent dbStudent = new DataBaseStudent();
        public FormSearch()
        {
            InitializeComponent();
        }
        private void FormSearch_Load(object sender, EventArgs e)
        {
            List<Faculty> listFaculty = dbStudent.Faculties.ToList();
            comboBoxfaculty.DataSource = listFaculty;
            comboBoxfaculty.DisplayMember = "facultyName";
            comboBoxfaculty.ValueMember = "ID";
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string id = textBoxID.Text;
            string name = textBoxName.Text;
            int facultyID = 0; // Giá trị mặc định

            if (!string.IsNullOrEmpty(comboBoxfaculty.Text))
            {
                facultyID = Convert.ToInt32(comboBoxfaculty.SelectedValue);
            }

            var query = from student in dbStudent.Students
                        where (string.IsNullOrEmpty(id) || student.studentID == id)
                            && (string.IsNullOrEmpty(name) || student.fullName.Contains(name))
                            && (facultyID == 0 || student.facultyID == facultyID)
                        select student;

            dataGridView1.Rows.Clear();

            foreach (var item in query.ToList())
            {
                // Giả sử bạn có ba cột trong dataGridView1: "MSSV," "Họ tên," và "Khoa"
                dataGridView1.Rows.Add(item.studentID, item.fullName, item.Faculty.facultyName);
            }

        }
    }
}

//        private void buttonSearch_Click(object sender, EventArgs e)
//        {
//            string id = textBoxID.Text;
//            string name = textBoxName.Text;
//            int facultyID = Convert.ToInt32(comboBoxfaculty.Text);

//            var query = from Student in Student
//                        where (string.IsNullOrEmpty(id) || Student.id == id)
//                            && (string.IsNullOrEmpty(name) || Student.Name.Contains(name))
//                            && (int.(facultyID) || Student.StudentID == facultyID)
//                        select student;

//            dataGridView1.DataSource = query.ToList();
//        }
//    }
//}