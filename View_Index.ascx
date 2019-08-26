<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View_Index.ascx.cs" Inherits="DNNGo.Modules.DNNGalleryProGame.View_Index" %>
<asp:Panel ID="panNavigation" runat="server"  CssClass="Navigation">
        <asp:HyperLink ID="hlNewItem" runat="server" CssClass="hlNewItem" Text="<i class='icon-edit'></i> New Item" resourcekey="Actions_NewItem" Target="_blank"></asp:HyperLink>
    &nbsp;<asp:HyperLink ID="hlManager" runat="server" CssClass="hlManager" Text="<i class='icon-file'></i> Manager" resourcekey="Actions_Manager" Target="_blank"></asp:HyperLink>
  
</asp:Panel>
<asp:Panel ID="plLicense" runat="server">
<asp:PlaceHolder ID="phScript" runat="server"></asp:PlaceHolder>
 <asp:PlaceHolder  ID="phContainer" runat="server"></asp:PlaceHolder>
 
</asp:Panel>