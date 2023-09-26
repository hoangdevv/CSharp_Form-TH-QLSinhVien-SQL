using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSinhVien_SQL.Model; // Khai báo CSDL
namespace QLSinhVien_SQL
{
    public partial class Form1 : Form
    {
        
        DataBaseStudent dbStudent = new DataBaseStudent(); //Khai báo đối tượng CSDL
        int index = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Faculty> listFaculty = dbStudent.Faculties.ToList();
            cbFaculty.DataSource = listFaculty;
            cbFaculty.DisplayMember = "facultyName";
            cbFaculty.ValueMember = "ID";
            loadDataGridView();
        }


        private void loadDataGridView()
        {
            List<Student> listStudents = dbStudent.Students.ToList();
            dataGridView1.Rows.Clear();
            foreach (var student in listStudents) {
                dataGridView1.Rows.Add(student.studentID, student.fullName, student.Faculty.facultyName, student.averageScore);
            }
        }

        bool KiemTraInput()
        {
            if (txtID.Text == "" || txtName.Text == "" || txtAverageScore.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK);
                return false;
            }
            if(txtID.TextLength < 10 || txtID.TextLength > 10)
            {
                MessageBox.Show("Mã số sinh viên phải có 10 kí tự!", "Lỗi", MessageBoxButtons.OK);
                txtID.Focus();
                return false;
            }
            if(!double.TryParse(txtAverageScore.Text, out double score)) 
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng điểm!", "Lỗi", MessageBoxButtons.OK);
                txtAverageScore.Focus();
                return false;
            }
            return true;
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;

            if (index <0)
            {
                return;
            }

            txtID.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            cbFaculty.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            txtAverageScore.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTraInput())
            {
                return;
            }
            string id = txtID.Text;
            string name = txtName.Text;
            int facultyID = Convert.ToInt32(cbFaculty.SelectedValue);
            double averageScore = Convert.ToDouble(txtAverageScore.Text);

            Student newStudent = new Student();
            newStudent.studentID = id;
            newStudent.fullName = name;
            newStudent.facultyID = facultyID;
            newStudent.averageScore = averageScore;

            dbStudent.Students.Add(newStudent);
            dbStudent.SaveChanges();
            loadDataGridView();
            MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);   
            
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            try
            {
                if (index < 0)
                {
                    MessageBox.Show("Vui lòng chọn bản ghi", "Lỗi", MessageBoxButtons.OK);
                    return;
                }

                string id = txtID.Text;
                string name = txtName.Text;
                int facultyID = Convert.ToInt32(cbFaculty.SelectedValue); //trả về thuộc tính valuemember
                double averageScore = Convert.ToDouble(txtAverageScore.Text);

                var studentToUpdate = dbStudent.Students.FirstOrDefault(s => s.studentID == id);
                if (studentToUpdate != null)
                {
                    studentToUpdate.fullName = name;
                    studentToUpdate.facultyID=facultyID;
                    studentToUpdate.averageScore = averageScore;
                    dbStudent.SaveChanges();
                    loadDataGridView();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy MSSV cần sửa!", "Lỗi", MessageBoxButtons.OK);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (index < 0)
                {
                    MessageBox.Show("Vui lòng chọn bản ghi","Lỗi",MessageBoxButtons.OK);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes)
                {
                    var studentToRemove = dbStudent.Students.FirstOrDefault(s => s.studentID == txtID.Text);

                    dbStudent.Students.Remove(studentToRemove);
                    dbStudent.SaveChanges();
                    loadDataGridView();
                    MessageBox.Show("Xóa dữ liệu thành công", "Thông báo", MessageBoxButtons.OK);
                }           
            }
            catch (Exception)
            {

                MessageBox.Show("Không tìm thấy MSSV cần xóa!","Lỗi", MessageBoxButtons.OK);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn muốn thoát chương trình?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Information) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
