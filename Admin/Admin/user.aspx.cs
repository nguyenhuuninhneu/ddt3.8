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
//Sửa name Sql + pass cho phù hợp! thanks 1 phát lấy tinh thần nhá ^^
public partial class gunny : System.Web.UI.Page
{
        protected void Page_Load(object sender, EventArgs e)
        {

        }
		
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "@") {
            string connstring = "Data Source=WIN-LDL953ICSK3\\SQLEXPRESS;Initial Catalog=Db_Tank;User ID=sa;Password=boss@2013";
            //Khai Bao name Sql luu y phai co \\       
        try
        {
            SqlConnection connect = new SqlConnection(connstring);
            connect.Open(); 
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable("Sys_Users_Detail");//Khai bao Dbo Sql
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '"+textBox2.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dt);
			if (textBox2.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Điền Tên Nhân Vật Chưa vậy @@~</font>";
            }
            else if (dt.Rows.Count == 0)
            {
                Label1.Text = "Tên nhân vật không tồn tại";
            }			
            else
            {
                string updateSql = "Update Sys_Users_Detail SET IsExist='False' WHERE NickName = '"+textBox2.Text+"'";
                SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                UpdateCmd.ExecuteNonQuery();
                Label1.Text = "Thành Công";
            }
        }       
                    
        catch (SqlException ex)
        {
            // Display error
            Label1.Text = ex.ToString();
            Label1.Visible = true;
        }}
		else if (textBox1.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập Pass xác nhận</font>";
            }
       else 
	    {
            Label1.Text = "Fall"; 
        }
        }
		
		protected void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "@")
            {
                string connstring = "Data Source=WIN-LDL953ICSK3\SQLEXPRESS;Initial Catalog=Db_Tank;User ID=sa;Password=boss@2013";
				//Khai Bao name Sql luu y phai co \\

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
                        Label1.Text = "<font color='red'>Nhập Tên NV</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {
                        string updateSql = "Update Sys_Users_Detail SET PvePermission='FFFFFFFFFFFFFFFFFFFFFFFFFFFFF' WHERE NickName = '" + textBox2.Text + "'";
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

        protected void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "@")
            {
                string connstring = "Data Source=WIN-LDL953ICSK3\SQLEXPRESS;Initial Catalog=Db_Tank;User ID=sa;Password=boss@2013";
				//Khai Bao name Sql luu y phai co \\

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
                        
                        string updateSql = "Update Sys_Users_Detail SET Money='99999999' WHERE NickName = '" + textBox2.Text + "'";
                                                
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

        protected void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "@")
            {
                string connstring = "Data Source=WIN-LDL953ICSK3\SQLEXPRESS;Initial Catalog=Db_Tank;User ID=sa;Password=boss@2013";
				//Khai Bao name Sql luu y phai co \\

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

                        string updateSql = "Update Sys_Users_Detail SET Gold='99999999' WHERE NickName = '" + textBox2.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add Vàng thành công! Nhận được 99999999 Vàng";
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

        protected void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "@")
            {
                string connstring = "Data Source=WIN-LDL953ICSK3\SQLEXPRESS;Initial Catalog=Db_Tank;User ID=sa;Password=boss@2013";
				//Khai Bao name Sql luu y phai co \\

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

                        string updateSql = "Update Sys_Users_Detail SET GiftToken='99999999' WHERE NickName = '" + textBox2.Text + "'";

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

        protected void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "@")
            {
                string connstring = "Data Source=WIN-LDL953ICSK3\SQLEXPRESS;Initial Catalog=Db_Tank;User ID=sa;Password=boss@2013";
				//Khai Bao name Sql luu y phai co \\

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
                        Label1.Text = "<font color='red'>Chưa nhập tên nhân vật sao Set @@~</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET GP='99999999' WHERE NickName = '" + textBox2.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Add EXP thành công! Nhận được 99999999 EXP";
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
		protected void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "@")
            {
                string connstring = "Data Source=WIN-LDL953ICSK3\SQLEXPRESS;Initial Catalog=Db_Tank;User ID=sa;Password=boss@2013";
				//Khai Bao name Sql luu y phai co \\

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
                        Label1.Text = "<font color='red'>Điền Tên Nhân Vật Vào</font>";
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        Label1.Text = "Tên nhân vật không tồn tại";
                    }
                    else
                    {

                        string updateSql = "Update Sys_Users_Detail SET IsExist='True' WHERE NickName = '" + textBox2.Text + "'";

                        SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                        UpdateCmd.ExecuteNonQuery();
                        Label1.Text = "Thành Công";
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
    }
