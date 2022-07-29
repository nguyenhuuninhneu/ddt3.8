<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gunny.aspx.cs" Inherits="gunny" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản Lí User</title>
	
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
                <marquee scrollamount="10">Quản Lí User</marquee></font>
                </B>
                </FONT><br/><br/>				
            <div class="khungset">
			<table border = 1>
            <tr>
            <td>          
        <b>Pass xác nhận:</b><asp:TextBox ID="textBox1" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <b>Tên Nhân Vật:</b><asp:TextBox ID="textBox2" runat="server"></asp:TextBox>
		    </td></tr></table></div><br />        
		<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
		<div class="khungset"><table border = 1>
        <tr><td>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Band" />
        <br />
		<asp:Button ID="Button3" runat="server" onclick="button3_Click" Text="Set Phó Bản" />
        <br />
		<asp:Button ID="Button4" runat="server" onclick="button4_Click" Text="Add Xu" />
        <br />
		<asp:Button ID="Button5" runat="server" onclick="button5_Click" Text="Add Tiền Vàng" />
        <br />
		<asp:Button ID="Button6" runat="server" onclick="button6_Click" Text="Add Xu Khoá" />
        <br />
		<asp:Button ID="Button7" runat="server" onclick="button7_Click" Text="Set EXP" />
        <br />
		<asp:Button ID="Button8" runat="server" onclick="button8_Click" Text="Un Band" />		
		</td></tr></table></div><br /><br />    
		
        <div class="linhtinh">
        
		</div>	
        <div class="quantrong"><center></center></div>
    </div>
    </form>
</body>
</html>
