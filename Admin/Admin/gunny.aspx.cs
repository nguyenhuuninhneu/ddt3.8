using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
public partial class gunny : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {

        }
		
		protected void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql 
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '" + textBox2.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {
                        string updateSql = "Update Sys_Users_Detail SET PvePermission='FFFFFFFFFFFFFFFFFFFFFFFFFFFFF' where NickName = '" + textBox2.Text + "'";
                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set PVE thành công!";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass</font>";
            }
            else
            {
                Label1.Text = "Fall";
            }
        }

        protected void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '" + textBox2.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {                       
                        
                        string updateSql = "Update Sys_Users_Detail SET Money='1000000000',Gold='1000000000',GoXu='1000000000',GiftToken='1000000000' where NickName = '" + textBox2.Text + "'";
                                                
                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);
                        
                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add xu/vàng/goxu/xu khóa thành công! Nhận được 1000000000 Xu/Vàng/Goxu/Xu khóa.";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }

        protected void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '" + textBox2.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET MagicStonePoint='1000000000' where NickName = '" + textBox2.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Tăng Điểm ma thạch lên 1000000000. Thành công";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Fall";
            }
        }

        protected void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '" + textBox2.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Chưa nhập tên nhân vật sao Set @@~</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET GP='447657138' where NickName = '" + textBox2.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add EXP thành công! Lên Cấp 65.";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Fall";
            }
        }

			        protected void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '" + textBox2.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET myHonor='1000000000' where NickName = '" + textBox2.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add thành công! Nhận được 1000000000 Điểm vinh dự";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Card");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Card where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Card SET Attack='800',Defence='800',Luck='800',Agility='800' WHERE UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Điểm Thành Cộng Full 800";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '" + textBox2.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET CardSoul='1000000000' where NickName = '" + textBox2.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add thành công! Nhận được 1000000000 Điểm Thẻ bài";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Card");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Card where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập ID Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "ID nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Card SET CardType='4' WHERE UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Thẻ Bạch Kim Thành Công";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }

			        protected void button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox2.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập ID Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "ID nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "update Sys_Users_Detail set totemid = '10350' where  UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Vật tổ thành công!!!!";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
		protected void button10_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql 
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên NV</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {
                        string updateSql = "Update Sys_Users_Detail SET PvePermission='FFFFFFFFFFFFFFFFFFFFFFFFFFFFF' where UserID = '" + textBox3.Text + "'";
                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set PVE thành công!";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass</font>";
            }
            else
            {
                Label1.Text = "Fall";
            }
        }

        protected void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {                       
                        
                        string updateSql = "Update Sys_Users_Detail SET Money='99999999' where UserID = '" + textBox3.Text + "'";
                                                
                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);
                        
                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add xu thành công! Nhận được 99999999 Xu";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }

        protected void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET MagicStonePoint='9999999' where UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Điểm ma thạch thành công";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Fall";
            }
        }

        protected void button13_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET GiftToken='99999999' where UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add Xu Khoá thành công! Nhận được 99999999 Xu khóa";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }

        protected void button14_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Chưa nhập tên nhân vật sao Set @@~</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET GP='447657138' where UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add EXP thành công! Nhận được 447657138 EXP";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Fall";
            }
        }
		
	        protected void button15_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET GoXu='99999999' where UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add GoXu Khoá thành công! Nhận được 99999999 GoXu";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button16_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET myHonor='99999999' where UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add thành công! Nhận được 99999999 Điểm vinh dự";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button17_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Card");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Card where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập ID Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "ID nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Card SET Attack='80',Defence='80',Luck='80',Agility='80' WHERE UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Điểm Thành Cộng Full 80";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button18_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Detail where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập Tên Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET CardSoul='99999999' where UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add thành công! Nhận được 99999999 Điểm Thẻ bài";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button19_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_Users_Card");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_Users_Card where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập ID Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "ID nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Card SET CardType='4' WHERE UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Thẻ Bạch Kim Thành Công";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button20_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_VIP_Info");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_VIP_Info where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập ID Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "ID nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_VIP_Info SET VIPLevel='12',VIPExp='400000' WHERE UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Vip 12 Thành Công";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
			        protected void button21_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";
				

                try
                {
                    SqlConnection connect = new SqlConnection(connstring);
                    connect.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable("Sys_User_Drills");//Khai bao Dbo Sql
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select* from Sys_User_Drills where UserID = '" + textBox3.Text + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (textBox3.Text.Length < 1)
                    {
                        Label1.Text = "<font color='red'>Nhập ID Nhân Vật</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "ID nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_User_Drills SET HoleLv='5' WHERE UserID = '" + textBox3.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Set Full lổ 5 Thành Công";
                    }
                }

                catch (SqlException ex)
                {
                    // Display error
                    Label1.Text = ex.ToString();
                    Label1.Visible = true;
                }
            }
			else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
            else
            {
                Label1.Text = "Pass xác nhận sai!>.<";
            }
        }
}