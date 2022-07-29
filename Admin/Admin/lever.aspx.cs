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

public partial class Lever : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Txt_box1.Text == "b0ss996") {
            string connstring = "Data Source=WIN-LDL953ICSK3\\SQLEXPRESS;Initial Catalog=Db_Tank;Persist Security Info=True;User ID=sa;Password=boss@2013";
                   
        try
        {
            SqlConnection connect = new SqlConnection(connstring);
            connect.Open(); 
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable("Sys_Users_Detail");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select* from Sys_Users_Detail where NickName = '"+TextBox2.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            da.SelectCommand = cmd;
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                Label1.Text = "Fall~";

            }
            else
            {
			string updateSql = "UPDATE Sys_Users_Detail SET GP=999999 WHERE NickName = '"+TextBox2.Text+"'";
                SqlCommand UpdateCmd = new SqlCommand(updateSql, connect);
                UpdateCmd.ExecuteNonQuery();
                Label1.Text = "Success ^_^";
            }
        }

        
        catch (SqlException ex)
        {
            // Display error
            Label1.Text = ex.ToString();
            Label1.Visible = true;
        }}
       else {
            Label1.Text = ""; 
        }
    }
}