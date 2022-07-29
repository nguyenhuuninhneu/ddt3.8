<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeGame.aspx.cs" Inherits="gunny" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Full Chức Năng</title>
	
</head>
<link rel="shortcut icon" href="/tools/images/45.jpg">
<link href="/tools/images/style.css" rel="stylesheet" type="text/css" media="all">
<body>

    <form id="form1" runat="server">
    <div>	    
	    <br/>
                <FONT face="Times New Roman">
                <B>
                <font size=3 style="text-shadow: 0px 0px 6px rgb(800, 0, 100), 0px 0px 5px rgb(800, 0,100), 0px 0px 5px rgb(800, 0,100);" color="#EDEA1F">
                <marquee scrollamount="10">Tạo Code game by Vũ Liêm ^^!</marquee></font>
                </B>
                </FONT><br/><br/>				
		<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
		<div class="khungset"><table border = 1>
        <tr><td>
        <b>Pass xác nhận Admin:</b><asp:TextBox ID="textBox1" runat="server"></asp:TextBox>
        <br />
		<b>Code:</b><asp:TextBox ID="textBox3" runat="server"></asp:TextBox>
		<b>ID Active:</b><asp:TextBox ID="textBox4" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Ok" />
        <br />

		</td></tr></table></div><br /><br />    
		
        <div class="linhtinh">
        
		</div>	
        <div class="quantrong"><center></center></div>
    </div>
    </form>
</body>
</html>
