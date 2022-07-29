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
            string connstring = "Data Source=VINH-HACKER\\DDTANK;Initial Catalog=Db_Tank;User ID=sa;Password=123456";        
        try
        {
            SqlConnection connect = new SqlConnection(connstring);
            connect.Open(); 
            {
                string updateSql = "INSERT [dbo].[Active_Number] ([AwardID], [ActiveID]) VALUES ('"+textBox3.Text+"', '"+textBox4.Text+"')";
                SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);

                UpdateCmd.ExecuteNonQuery();
                Label1.Text = "Tạo Thành Công Code: "+textBox3.Text+"";
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
		
    }
