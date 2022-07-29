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
		
         protected void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1") {
            string connstring = "Data Source=VINH-HACKER\\DDTANK; Initial Catalog=Db_Tank;User ID=sa;Password=123456";        
        try
        {
            SqlConnection connect = new SqlConnection(connstring);
            connect.Open(); 
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable("Shop_Goods");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select TemplateID from Shop_Goods where TemplateID = '"+textBox2.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dt);
            DataTable dc = new DataTable("Shop_Item");
            cmd.CommandText = "Select ID from Shop_Item where ID = '"+textBox2.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dc);
			if (textBox2.Text.Length < 1)
            {
                Label1.Text = "<font color='red'>Chưa nhập ID Item</font>";
            }
            else if (dt.Rows.Count == 0)
            {
                Label1.Text = "Item " + textBox2.Text + " không tồn tại trong server không thể thêm";
            }
            else if (dc.Rows.Count == 1)
            {
                Label1.Text = "Item " + textBox2.Text + " Đã có trong web shop không thể thêm";
            }			
            else
            {
                string updateSql = "INSERT [dbo].[Shop_Item] ([Id], [Price], [CategoryID], [Notice], [Issell], [Power]) VALUES ('"+textBox2.Text+"', '"+textBox3.Text+"', '"+textBox4.Text+"', '"+textBox5.Text+"', 1,N'1')";
                SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                UpdateCmd.ExecuteNonQuery();
                Label1.Text = "Thêm Item "+textBox2.Text+" thành công";
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
                Label1.Text = "<font color='red'>Chưa nhập Pass</font>";
            }
       else 
	    {
            Label1.Text = "Pass không đúng"; 
        }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "1") {
            string connstring = "Data Source=WINDOWS2008\GUN2015; Initial Catalog=Db_Tank;User ID=sa;Password=123456";        
        try
        {
            SqlConnection connect = new SqlConnection(connstring);
            connect.Open(); 
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable("Ws_user");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select* from Ws_user where UserName = '"+textBox7.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dt);
			if (textBox7.Text.Length < 1)
            {
                Label3.Text = "<font color='red'>Chưa nhập tài khoản</font>";
            }
			else if (textBox9.Text.Length < 1)
            {
                Label3.Text = "<font color='red'>Điền Cash Cần Set</font>";
            }
            else if (dt.Rows.Count == 0)
            {
                Label3.Text = "User Name không tồn tại";
            }			
            else
            {
                string updateSql = "Update Ws_user SET Cash"+textBox8.Text+"='"+textBox9.Text+"' where UserName = '"+textBox7.Text+"'";
                SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                UpdateCmd.ExecuteNonQuery();
                Label3.Text = "Thành công";
            }
        }       
                    
        catch (SqlException ex)
        {
            // Display error
            Label3.Text = ex.ToString();
            Label3.Visible = true;
        }}
		else if (textBox8.Text.Length < 1)
            {
                Label3.Text = "<font color='red'>Chưa nhập Pass</font>";
            }
       else 
	    {
            Label3.Text = "Pass không đúng"; 
        }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "1") {
            string connstring = "Data Source=WINDOWS2008\GUN2015; Initial Catalog=Db_Tank;User ID=sa;Password=123456";        
        try
        {
            SqlConnection connect = new SqlConnection(connstring);
            connect.Open(); 
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable("Active_Number");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select AwardID from Active_Number where AwardID = '"+textBox11.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dt);
            DataTable de = new DataTable("Active");
            cmd.CommandText = "Select* from Active where ActiveID = '"+textBox12.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(de);
            DataTable dr = new DataTable("Active");
            cmd.CommandText = "Select* from Active where ActiveID = '"+textBox12.Text+"' and haskey = 1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dr);
			if (textBox11.Text.Length < 1)
            {
                Label4.Text = "<font color='red'>Chưa nhập Code</font>";
            }
			else if (textBox12.Text.Length < 1)
            {
                Label4.Text = "<font color='red'>Chưa nhập Activeid</font>";
            }
            else if (dt.Rows.Count == 1)
            {
                Label4.Text = "Code đã tồn tại không thể thêm";
            }	
			else if (de.Rows.Count == 0)
            {
                Label4.Text = "Không có ActiveID "+textBox12.Text+" trong Active";
            }
			else if (dr.Rows.Count == 0)
            {
                Label4.Text = "ActiveID "+textBox12.Text+"Không phải code";
            }
            else
            {
                string updateSql = "INSERT [dbo].[Active_Number] ([AwardID], [ActiveID]) VALUES ('"+textBox11.Text+"', '"+textBox12.Text+"')";
                SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                UpdateCmd.ExecuteNonQuery();
                Label4.Text = "Thành công";
            }
        }       
                    
        catch (SqlException ex)
        {
            // Display error
            Label3.Text = ex.ToString();
            Label3.Visible = true;
        }}
		else if (textBox10.Text.Length < 1)
            {
                Label4.Text = "<font color='red'>Chưa nhập Pass</font>";
            }
       else 
	    {
            Label4.Text = "Pass không đúng"; 
        }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "1") {
            string connstring = "Data Source=WINDOWS2008\GUN2015;Initial Catalog=Db_Tank;User ID=sa;Password=123456";        
        try
        {
            SqlConnection connect = new SqlConnection(connstring);
            connect.Open(); 
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable("Shop_Item");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select* from Shop_Item where ID = '"+textBox14.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dt);
			if (textBox14.Text.Length < 1)
            {
                Label5.Text = "<font color='red'>Không nhập Id Item</font>";
            }
            else if (dt.Rows.Count == 0)
            {
                Label5.Text = "ID Item không tồn tại";
            }			
            else
            {
                string updateSql = "DELETE [dbo].[Shop_Item] WHERE ID = '"+textBox14.Text+"'";
                SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                UpdateCmd.ExecuteNonQuery();
                Label5.Text = "Ok xóa item "+textBox14.Text+" Thành Công";
            }
        }       
                    
        catch (SqlException ex)
        {
            // Display error
            Label1.Text = ex.ToString();
            Label1.Visible = true;
        }}
		else if (textBox13.Text.Length < 1)
            {
                Label5.Text = "<font color='red'>Chưa nhập Pass</font>";
            }
       else 
	    {
            Label5.Text = "Pass không đúng"; 
        }
        }
		
	}
