using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace QuanLyQuanCaPhe
{
    public partial class QuanLyNhanVien : Form
    {


        public QuanLyNhanVien()
        {
            InitializeComponent();

        }

        #region biêntoacuc


        #endregion

        public SqlConnection GetConnection()
        {
            string con = @"Data Source=PHAM_TUAN_ANH\SQLEXPRESS;Initial Catalog=QuanLyQuanCaPhe;Integrated Security=True";
            SqlConnection conn = new SqlConnection(con);

            return conn;
        }





        public void loadData()
        {
            DataTable dt = new DataTable();
            try
            {

                SqlConnection conn = GetConnection();
                conn.Open();
                string sql = "SELECT * FROM NHANVIEN";

                SqlDataAdapter data = new SqlDataAdapter(sql, conn);
                data.Fill(dt);

                dgv.DataSource = dt;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }


        }
        public DataTable Red(string cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = GetConnection();
            SqlCommand sc = new SqlCommand(cmd, con);
            SqlDataAdapter sdc = new SqlDataAdapter(sc);

            sdc.Fill(dt);
            con.Close();

            return dt;
        }

        public void btnadd_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            int manv = rd.Next(6660000, 6800000);
            string tennv = txttennv.Text;
            string ngaysinh = datengaysinh.Text;
            string chucvu = txtchucvu.Text;
            string quequan = txtquequan.Text;
            string sdt = txtsdt.Text;
            string email = txtemail.Text;

            if (tennv.Trim()==""||ngaysinh.Trim()==""||chucvu.Trim()==""||quequan.Trim()==""||sdt.Trim()==""||email.Trim()=="")
            {
                MessageBox.Show("Vui Lòng Thử Lại!Không được để trống","Thông Báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection conn = GetConnection();
                conn.Open();

                string query = $"SELECT TenNhanVien FROM NHANVIEN WHERE TenNhanVien= N'{manv}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    MessageBox.Show("Tên nhân viên đã tồn tại", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlConnection con = GetConnection();
                    con.Open();
                    string INSERT = "INSERT INTO NHANVIEN (MaNhanVien,TenNhanVien,NgaySinh,QueQuan,email,ChucVu,Sdt) VALUES (@manv,@tennv,@ngaysinh,@quequan,@email,@chucvu,@sdt)";
                    SqlCommand cmd1 = new SqlCommand(INSERT, con);
                    cmd1.Parameters.AddWithValue("@manv", manv);
                    cmd1.Parameters.AddWithValue("@tennv", tennv);
                    cmd1.Parameters.AddWithValue("@ngaysinh", ngaysinh);
                    cmd1.Parameters.AddWithValue("@quequan", quequan);
                    cmd1.Parameters.AddWithValue("@email", email);
                    cmd1.Parameters.AddWithValue("@chucvu", chucvu);
                    cmd1.Parameters.AddWithValue("@sdt", sdt);


                    DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn muốn Thêm ", "Thông Báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes)
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("Thêm Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Vui Lòng!Thử Lại", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    loadData();
                    reset();
                }
                
                //Thêm Nhân viên tại đây
            }
        }

        //try
        //{
        //    SqlConnection con = GetConnection();
        //    con.Open();
        //    string INSERT = "INSERT INTO NHANVIEN (MaNhanVien,TenNhanVien,NgaySinh,QueQuan,email,ChucVu,Sdt) VALUES (@manv,@tennv,@ngaysinh,@quequan,@email,@chucvu,@sdt)";
        //    SqlCommand cmd = new SqlCommand(INSERT, con);
        //    cmd.Parameters.AddWithValue("@manv", manv);
        //    cmd.Parameters.AddWithValue("@tennv", tennv);
        //    cmd.Parameters.AddWithValue("@ngaysinh", ngaysinh);
        //    cmd.Parameters.AddWithValue("@quequan", quequan);
        //    cmd.Parameters.AddWithValue("@email", email);
        //    cmd.Parameters.AddWithValue("@chucvu", chucvu);
        //    cmd.Parameters.AddWithValue("@sdt", sdt);

        //    if (tennv.Trim()=="" || quequan.Trim()=="" || email.Trim()==" "||chucvu.Trim()=="" || sdt.Trim()=="" ) {

        //        MessageBox.Show("Vui lòng! không để trống","Thông Báo ", MessageBoxButtons.OK,MessageBoxIcon.Error);

        //    }
        //    else {
        //       SqlConnection mycon = GetConnection();
        //        mycon.Open();

        //        string sql = "select TenNhanVien from NHANVIEN ";
        //        SqlCommand cmd = new SqlCommand(sql, mycon);


        //    }
        //else
        //{


        //        DialogResult result= MessageBox.Show("Bạn Có Chắc Chắn muốn Thêm ","Thông Báo ",MessageBoxButtons.YesNo,MessageBoxIcon.Error);
        //        if(result == DialogResult.Yes)
        //        {
        //                cmd.ExecuteNonQuery();       
        //                MessageBox.Show("Thêm Thành Công", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        //        }
        //    else
        //    {
        //        MessageBox.Show("Vui Lòng!Thử Lại","Thông Báo ",MessageBoxButtons.OK,MessageBoxIcon.Error);
        //    }

        //}
        //loadData();
        //reset();


        //}
        //catch (Exception)
        //{
        //    MessageBox.Show("lỗi mã nhân viên bị trùng" );
        //}
    
            

        private void btnupde_Click(object sender, EventArgs e)
        {
            String manv, tennv, ngaysinh, quequan, email, sdt, chucvu;
            manv = labelmanv.Text;
            tennv = txttennv.Text;
            ngaysinh = datengaysinh.Text;
            quequan = txtquequan.Text; 
            email = txtemail.Text;
            chucvu = txtchucvu.Text;
            sdt = txtsdt.Text;


            try
            {
                SqlConnection conn = GetConnection();
                conn.Open();
                string update = "UPDATE NHANVIEN SET MaNhanVien =@manv,TenNhanVien=@tennv,NgaySinh=@ngaysinh,QueQuan=@quequan,email=@email,ChucVu = @chucvu,Sdt=@sdt WHERE MaNhanVien =@manv ";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@manv", manv);
                cmd.Parameters.AddWithValue("@tennv", tennv);
                cmd.Parameters.AddWithValue("@ngaysinh", ngaysinh);
                cmd.Parameters.AddWithValue("@quequan", quequan);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@chucvu", chucvu);
                cmd.Parameters.AddWithValue("@sdt", sdt);



                {
                    DialogResult result = MessageBox.Show("Bạn Có Chắc Chắn muốn Sủa ", "Thông Báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes)
                    {
                        int check = cmd.ExecuteNonQuery();
                        if (check >0)
                        {
                            MessageBox.Show("Sửa  Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        loadData();
                        reset();
                    }
                    else
                    {

                        MessageBox.Show("Vui Lòng!Thử Lại", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    loadData();
                    reset();
                }
            
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi" + e);
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            
            string manv = labelmanv.Text;
            try
            {
                SqlConnection con = GetConnection();
                con.Open();
                string delete = "DELETE FROM NHANVIEN WHERE MaNhanVien = @manv";
                SqlCommand cmd = new SqlCommand(delete, con);

                cmd.Parameters.AddWithValue("manv", manv);
                int check = cmd.ExecuteNonQuery();
                {
                    if( check >0 )
                    {
                        MessageBox.Show("Xóa Thành Công","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        
                    }
                    else
                    {
                        MessageBox.Show("Xóa Thất Bại");
                    }
                    loadData ();
                    reset();
                }
            }catch (Exception)
            {
                MessageBox.Show("lỗi" + e);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            labelmanv.Text = dgv.CurrentRow.Cells["MaNhanVien"].Value.ToString() ;
            txttennv.Text = dgv.CurrentRow.Cells["TenNhanVien"].Value.ToString();
            datengaysinh.Text = dgv.CurrentRow.Cells["NgaySinh"].Value.ToString();
            txtquequan.Text = dgv.CurrentRow.Cells["QueQuan"].Value.ToString();
            txtemail.Text = dgv.CurrentRow.Cells["email"].Value.ToString();
            txtchucvu.Text = dgv.CurrentRow.Cells["Chucvu"].Value.ToString();
            txtsdt.Text = dgv.CurrentRow.Cells["Sdt"].Value.ToString();        
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
            
            

        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                string search = txttimkiem.Text;
                string sqlsearch = "SELECT * FROM NHANVIEN WHERE MaNhanVien LIKE '%" + search + "%' ";
                DataTable dt = Red(sqlsearch);
                if (dt != null)
                {
                    dgv.DataSource = dt;
                }
                
                
                
            }catch(Exception)
            {
                MessageBox.Show("lỗi" + e);
            }
        }

        private void btnload_Click(object sender, EventArgs e)
        {
           reset();
        }
        private void reset()
        {
         
            
            labelmanv.ResetText();
            txttennv.ResetText();
            txtemail.ResetText();
            txtchucvu.ResetText();
            txtquequan.ResetText();
            txtsdt.ResetText();
            txttimkiem.ResetText();
            
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnexidt_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
