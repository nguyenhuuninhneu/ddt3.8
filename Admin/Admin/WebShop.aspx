<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebShop.aspx.cs" Inherits="gunny" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản Lý Web Shop</title>
	
</head>
<link rel="icon" type="image/ico" href="http://127.0.0.1/favicon.ico">
<body>

    <form id="form1" runat="server">
    <div>	    
	    <br/>
                <FONT face="Arial">
                <B>
                <font size=4 style="text-shadow: 0px 0px 6px rgb(800, 0, 100), 0px 0px 5px rgb(800, 0,100), 0px 0px 5px rgb(800, 0,100);" color="#EDEA1F">
                <marquee scrollamount="12">Quản Lí User by Tuấn Long Nguyễn - DDTeam</marquee></font>
                </B>
                </FONT><br/><br/>				   
		    </td></tr></table></div> 
		<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
		<div class="khungset"><table border = 1>
        <tr><td>
        <b>Pass xác nhận: </b><asp:TextBox ID="textBox1" runat="server"></asp:TextBox>Thêm item Web Shop
        <br />
        <b>Id item: </b><asp:TextBox ID="textBox2" runat="server"></asp:TextBox>
		<b>Giá: </b><asp:TextBox ID="textBox3" placeholder="Trống = 0" runat="server"></asp:TextBox>
		<b>Vị trí bán: </b><asp:TextBox ID="textBox4" placeholder="Trống = 0" runat="server"></asp:TextBox>
		<b>Phụ đề: </b><asp:TextBox ID="textBox5" runat="server"></asp:TextBox>
		<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="OK" />
		    </td></tr></table></div><br />
		<asp:Label ID="Label3" runat="server" Text=""></asp:Label>


		    <div class="khungset">
			<table border = 1>
            <tr>
            <td>
		<b>Pass xác nhận Admin: </b><asp:TextBox ID="textBox6" runat="server"></asp:TextBox>Cộng Trừ Cash member</br>
		<b>User Name: </b><asp:TextBox ID="textBox7" runat="server"></asp:TextBox>
		<b></b><asp:TextBox ID="textBox8" placeholder="+,- trống là bằng" runat="server"></asp:TextBox>
		<b>Cash: </b><asp:TextBox ID="textBox9" placeholder="Cash cần set" runat="server"></asp:TextBox>
		<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="OK" />			
		</td></tr></table>  <br /> 
		<asp:Label ID="Label4" runat="server" Text=""></asp:Label>		
       <div class="linhtinh">
 		    <div class="khungset">
			<table border = 1>
            <tr>
            <td>
		<b>Pass xác nhận Admin: </b><asp:TextBox ID="textBox10" runat="server"></asp:TextBox>Thêm code cho game</br>
		<b>Code</b><asp:TextBox ID="textBox11" runat="server"></asp:TextBox>
		<b>ActiveID</b><asp:TextBox ID="textBox12" runat="server"></asp:TextBox>
		<asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="OK" />			
		</td></tr></table>  <br />
		<asp:Label ID="Label5" runat="server" Text=""></asp:Label>		
       <div class="linhtinh">
 		    <div class="khungset">
			<table border = 1>
            <tr>
            <td>
		<b>Pass xác nhận Admin: </b><asp:TextBox ID="textBox13" runat="server"></asp:TextBox>
		<b>Id Item: </b><asp:TextBox ID="textBox14" placeholder="ID Item cần xóa" runat="server"></asp:TextBox>
		<asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="OK" />			
		</td></tr></table>  <br /> 		
       <div class="linhtinh">               
		</div>	
        <div class="quantrong"><center></center></div>
<font size=3 style="text-shadow: 0px 0px 6px rgb(800, 0, 100), 0px 0px 5px rgb(800, 0,100), 0px 0px 5px rgb(800, 0,100);" color="#EDEA1F"><center>DDteam</center></font>
    </div>
    </form>
</body>
</html>
