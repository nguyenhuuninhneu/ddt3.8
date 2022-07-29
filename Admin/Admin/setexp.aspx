<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lever.aspx.cs" Inherits="Lever" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Set Exp</title>
</head>
<body>
<style>
body{
background:url(gtheme_files/main.jpg)
}
</style>
    <form id="form1" runat="server">
    <center>
	<br>
	<br>
	<br>
	<br>
	<asp:HyperLink ID="HyperLink2" runat="server" Font-Size=Large
                                             ForeColor="blue">Set Exp</asp:HyperLink><br>
	<br>
	
	<strong>
	<div>
    
        Mật khẩu xác nhận:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
		<br />
        Tên cần Nâng ( Tên nhân vật ):<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
		<br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Set Exp" />
        <br />
		<br />
        <asp:Label ID="Label1" runat="server" Text="Bản Quyền Được Edit by o0Boss0o"></asp:Label>
        <br />
				* Lưu ý : Thoát Game Mới Sử Dụng Được Chức Năng Này<br>
					
	
		
		
    
    </div></center>
    </form>
</body>
</html>
