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
    public partial class FormFaculty : Form
    {
        DataBaseStudent dbStudent = new DataBaseStudent();

        int index = -1;
        public FormFaculty()
        {
            InitializeComponent();
        }
        void loadDGVfaculty()
        {
            List<Faculty> listFaculty = dbStudent.Faculties.ToList();
            dataGridViewFaculty.Rows.Clear();
            foreach(var item in listFaculty)
            {
                dataGridViewFaculty.Rows.Add(item.ID, item.facultyName, item.totalProfessor);
            }
        }

        //int KTTonTai()
        //{
            
        //}
        private void FormFaculty_Load(object sender, EventArgs e)
        {
            loadDGVfaculty();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddAndFix_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text);
            string name = txtName.Text;
            int totalProfessor = Convert.ToInt32(txtTotalProfessor.Text);

            bool ktTonTai = dbStudent.Faculties.Any(p => (p.ID == id));
            Faculty newFaculty; ;
            if (!ktTonTai) //neu khong ton tai
            {
                newFaculty = new Faculty();
                newFaculty.ID = id;
                newFaculty.facultyName = name;
                newFaculty.totalProfessor = totalProfessor;
                dbStudent.Faculties.Add(newFaculty);
                dbStudent.SaveChanges();
                loadDGVfaculty();
                MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
               
                    var facultyToUpdate = dbStudent.Faculties.FirstOrDefault(p => p.ID == id);
                    if (facultyToUpdate != null)
                    {
                        facultyToUpdate.facultyName = name;
                        facultyToUpdate.totalProfessor = totalProfessor;
                        dbStudent.SaveChanges();
                        loadDGVfaculty();
                        MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                
            }
        }

        private void dataGridViewFaculty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;

            if(index < 0)
            {
                return;
            }

            txtID.Text = dataGridViewFaculty.Rows[index].Cells[0].Value.ToString();
            txtName.Text = dataGridViewFaculty.Rows[index].Cells[1].Value.ToString();
            txtTotalProfessor.Text = dataGridViewFaculty.Rows[index].Cells[2].Value.ToString();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (index < 0)
                {
                    MessageBox.Show("Vui lòng chọn bản ghi", "Lỗi", MessageBoxButtons.OK);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(txtID.Text);
                    var facultyToRemove = dbStudent.Faculties.FirstOrDefault(p=> p.ID == id);

                    dbStudent.Faculties.Remove(facultyToRemove);
                    dbStudent.SaveChanges();
                    loadDGVfaculty();
                    MessageBox.Show("Xóa dữ liệu thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Không tìm thấy MSSV cần xóa!", "Lỗi", MessageBoxButtons.OK);
            }
        }
    }
}
