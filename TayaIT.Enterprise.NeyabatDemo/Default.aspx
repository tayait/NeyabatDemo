<%@ Page Title="وزارة العدل - الصفحه الرئيسيه" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="TayaIT.Enterprise.Neyabat.Web._Default" %>

<%@ Import Namespace="TayaIT.Enterprise.EMadbatah.Model" %>
<%@ Import Namespace="TayaIT.Enterprise.Neyabat.Web" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="scripts/SessionScript.js" type="text/javascript"></script>
    <script src="scripts/fileuploader.js" type="text/javascript"></script>
    <script src="scripts/DefaultScript.js" type="text/javascript"></script>
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
                نظام تحويل التسجيلات الصوتية إلى نص آلياً:</div>
        </div>
        <div class="clear">
        </div>
        <div class="grid_22">
            <div style="padding-bottom: 20px">
              <asp:Label ID="lblFilelbl" runat="server" Text='اختر الملف المراد تحميله:' class="h2"></asp:Label>
                <asp:FileUpload ID="fuAttAvatar" runat="server" />
                <asp:Button ID="btnAddEditDefAtt" runat="server" Text="تحميل" OnClick="btnAddEditDefAtt_Click"
                    ValidationGroup="VGSession" CssClass="btn" />
                <div class="clear">
                </div>
            </div>
            <asp:GridView ID="gvAudioFiles" runat="server" AutoGenerateColumns="false" DataKeyNames="id"
                CssClass="Gridview" ShowFooter="false" ShowHeader="true">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="lblIcon" runat="server" ImageUrl="~/images/volume-icon-15.gif" Width="25px"
                                Height="25px"></asp:Image>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اسم الملف">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblFileName" runat="server" Text='<%# Eval("Name")%>' PostBackUrl='<%# String.Format("~/EditSessionFile.aspx?sfid={0}", Eval("ID"))%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الحجم">
                        <ItemTemplate>
                            <asp:Label ID="lblFileSize" runat="server" Text='<%# CalculateSize(Eval("FileSize").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تاريخ التحميل">
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedAt" runat="server" Text='<%# Eval("CreatedAt")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الحالة">
                        <ItemTemplate>
                            <asp:Label ID="Label1" Text='<%# GetFileStatusString(Eval("Name").ToString())%>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="عرض">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblFileView" runat="server" Text='عرض' PostBackUrl='<%# String.Format("~/EditSessionFile.aspx?sfid={0}", Eval("ID"))%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="حذف">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<%# Eval("ID")%>'
                                OnClick="DeleteAudioFile" OnClientClick="return confirm('هل أنت متأكد أنك تريد الحذف ؟')"
                                Text="حذف"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="clear">
        </div>
        </form>
    </div>
</asp:Content>
