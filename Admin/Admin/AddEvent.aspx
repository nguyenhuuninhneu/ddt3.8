<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEvent.aspx.cs" Inherits="WebApplication1.Admin.AddItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="SearchPanel" runat="server">
        <div class="formPanel">
            <fieldset>
                <legend>Add Item</legend>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Tên tài khoản" AssociatedControlID="UserName_tbx"></asp:Label>
                    <asp:TextBox ID="UserName_tbx" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label2" runat="server" Text="Item Template Id" AssociatedControlID="Template_tbx"></asp:Label>
                    <asp:TextBox ID="Template_tbx" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label3" runat="server" Text="Cấp cường hóa" AssociatedControlID="Level_tbx"></asp:Label>
                    <asp:TextBox ID="Level_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label4" runat="server" Text="Tấn công" 
                        AssociatedControlID="Attack_tbx"></asp:Label>
                    <asp:TextBox ID="Attack_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label5" runat="server" Text="Phòng thủ" 
                        AssociatedControlID="Defence_tbx"></asp:Label>
                    <asp:TextBox ID="Defence_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label6" runat="server" Text="Nhanh nhẹn" 
                        AssociatedControlID="Agility_tbx"></asp:Label>
                    <asp:TextBox ID="Agility_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Luck" runat="server" Text="May mắn" AssociatedControlID="Luck_tbx"></asp:Label>
                    <asp:TextBox ID="Luck_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Error_lbl" runat="server" Text="Label"></asp:Label>
                </p>
                <p>
                    <asp:Button ID="Edit" runat="server" Text="Add Item" OnClick="Edit_Click" />
                </p>
            </fieldset>
        </div>
    </asp:Panel>
</asp:Content>
