<%@ Page Title="وزارة الصحة - الدخول الى النظام" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="TayaIT.Enterprise.Neyabat.Web._Login" %>

<%@ Import Namespace="TayaIT.Enterprise.EMadbatah.Model" %>
<%@ Import Namespace="TayaIT.Enterprise.Neyabat.Web" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="scripts/fileuploader.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Literal Text="" runat="server" ID="litStartupScript" />
    <link href="styles/uniform.aristo.css" rel="stylesheet" type="text/css" />
    <link href="styles/fileuploader.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Gridview
        {
            border: 1px solid #DEDEDE;
            color: black;
            font-size: 130%;
            font-weight: bold;
        }
        .Gridview th
        {
            border: 1px solid #DEDEDE;
            color: black;
            padding: 10px;
        }
        .Gridview td
        {
            border: 1px solid #DEDEDE;
            color: black;
            background-color: white;
            padding: 10px;
        }
    </style>
    <div id="mainContent" runat="server">
        <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="lblInfo1" Visible="false" CssClass="lInfo"></asp:Label>
        </div>
        <div class="grid_24 xxlargerow">
            <div class="Ntitle">
                الدخول الى النظام:</div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_22">
            <div style="padding-bottom: 20px">
                <div class="grid_3">
                    <asp:Label ID="lblFilelbl" runat="server" Text='الاسم:' class="h2"></asp:Label>
                </div>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="textfield" Width="300"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" style="color:red"
                    ErrorMessage="*" ValidationGroup="VGSession"></asp:RequiredFieldValidator>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_22">
            <div style="padding-bottom: 20px">
                <div class="grid_3">
                    <asp:Label ID="Label1" runat="server" Text='كلمة السر:' class="h2"></asp:Label>
                </div>
                <asp:TextBox ID="txtPass" runat="server" CssClass="textfield" Width="300" TextMode="Password"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass" style="color:red"
                    ErrorMessage="*" ValidationGroup="VGSession"></asp:RequiredFieldValidator>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_10">
            <div style="float:left">
                <asp:Button ID="btnLogin" runat="server" Text="الدخول" ValidationGroup="VGSession"
                    CssClass="btn" onclick="btnLogin_Click" />
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        </form>
    </div>
</asp:Content>
