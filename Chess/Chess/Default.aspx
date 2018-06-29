<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chess._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
    <div class="chessboard">
        <!-- 1st -->
        <div ID="ImageButton11d" class="white" >
            <asp:ImageButton ID="ImageButton11" runat="server" onclick="selectSquare" ImageUrl="images/br.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton12d" class="black" >
            <asp:ImageButton ID="ImageButton12" runat="server" onclick="selectSquare" ImageUrl="images/bn.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton13d" class="white" >
            <asp:ImageButton ID="ImageButton13" runat="server" onclick="selectSquare" ImageUrl="images/bb.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton14d" class="black" >
            <asp:ImageButton ID="ImageButton14" runat="server" onclick="selectSquare" ImageUrl="images/bq.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton15d" class="white" >
            <asp:ImageButton ID="ImageButton15" runat="server" onclick="selectSquare" ImageUrl="images/bk.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton16d" class="black" >
            <asp:ImageButton ID="ImageButton16" runat="server" onclick="selectSquare" ImageUrl="images/bb.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton17d" class="white" >
            <asp:ImageButton ID="ImageButton17" runat="server" onclick="selectSquare" ImageUrl="images/bn.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton18d" class="black" >
            <asp:ImageButton ID="ImageButton18" runat="server" onclick="selectSquare" ImageUrl="images/br.png" CssClass="piece"/>
        </div>

        <!-- 2nd -->
        <div ID="ImageButton21d" class="black" >
            <asp:ImageButton ID="ImageButton21" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton22d" class="white" >
            <asp:ImageButton ID="ImageButton22" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton23d" class="black" >
            <asp:ImageButton ID="ImageButton23" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton24d" class="white">
            <asp:ImageButton ID="ImageButton24" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton25d" class="black">
            <asp:ImageButton ID="ImageButton25" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton26d" class="white">
            <asp:ImageButton ID="ImageButton26" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton27d" class="black">
            <asp:ImageButton ID="ImageButton27" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton28d" class="white">
            <asp:ImageButton ID="ImageButton28" runat="server" onclick="selectSquare" ImageUrl="images/bp.png" CssClass="piece"/>
        </div>

        <!-- 3th -->
        <div ID="ImageButton31d" class="white">
            <asp:ImageButton ID="ImageButton31" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton32d" class="black">
            <asp:ImageButton ID="ImageButton32" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton33d" class="white">
            <asp:ImageButton ID="ImageButton33" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton34d" class="black">
            <asp:ImageButton ID="ImageButton34" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton35d" class="white">
            <asp:ImageButton ID="ImageButton35" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton36d" class="black">
            <asp:ImageButton ID="ImageButton36" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton37d" class="white">
            <asp:ImageButton ID="ImageButton37" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton38d" class="black">
            <asp:ImageButton ID="ImageButton38" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>

        <!-- 4st -->
        <div ID="ImageButton41d" class="black">
            <asp:ImageButton ID="ImageButton41" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton42d" class="white">
            <asp:ImageButton ID="ImageButton42" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton43d" class="black">
            <asp:ImageButton ID="ImageButton43" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton44d" class="white">
            <asp:ImageButton ID="ImageButton44" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton45d" class="black">
            <asp:ImageButton ID="ImageButton45" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton46d" class="white">
            <asp:ImageButton ID="ImageButton46" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton47d" class="black">
            <asp:ImageButton ID="ImageButton47" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton48d" class="white">
            <asp:ImageButton ID="ImageButton48" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>

        <!-- 5th -->
        <div ID="ImageButton51d" class="white">
            <asp:ImageButton ID="ImageButton51" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton52d" class="black">
            <asp:ImageButton ID="ImageButton52" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton53d" class="white">
            <asp:ImageButton ID="ImageButton53" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton54d" class="black">
            <asp:ImageButton ID="ImageButton54" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton55d" class="white">
            <asp:ImageButton ID="ImageButton55" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton56d" class="black">
            <asp:ImageButton ID="ImageButton56" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton57d" class="white">
            <asp:ImageButton ID="ImageButton57" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton58d" class="black">
            <asp:ImageButton ID="ImageButton58" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>

        <!-- 6th -->
        <div ID="ImageButton61d" class="black">
            <asp:ImageButton ID="ImageButton61" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton62d" class="white">
            <asp:ImageButton ID="ImageButton62" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton63d" class="black">
            <asp:ImageButton ID="ImageButton63" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton64d" class="white">
            <asp:ImageButton ID="ImageButton64" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton65d" class="black">
            <asp:ImageButton ID="ImageButton65" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton66d" class="white">
            <asp:ImageButton ID="ImageButton66" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton67d" class="black">
            <asp:ImageButton ID="ImageButton67" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton68d" class="white">
            <asp:ImageButton ID="ImageButton68" runat="server" onclick="selectSquare" ImageUrl="images/blank.png" CssClass="piece"/>
        </div>

        <!-- 7th -->
        <div ID="ImageButton71d" class="white" >
            <asp:ImageButton ID="ImageButton71" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton72d" class="black">
            <asp:ImageButton ID="ImageButton72" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton73d" class="white">
            <asp:ImageButton ID="ImageButton73" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton74d" class="black">
            <asp:ImageButton ID="ImageButton74" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton75d" class="white">
            <asp:ImageButton ID="ImageButton75" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>          
        </div>
        <div ID="ImageButton76d" class="black">
            <asp:ImageButton ID="ImageButton76" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton77d" class="white">
            <asp:ImageButton ID="ImageButton77" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton78d" class="black">
            <asp:ImageButton ID="ImageButton78" runat="server" onclick="selectSquare" ImageUrl="images/wp.png" CssClass="piece"/>
        </div>

        <!-- 8th -->
        <div ID="ImageButton81d" class="black">
            <asp:ImageButton ID="ImageButton81" runat="server" onclick="selectSquare" ImageUrl="images/wr.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton82d" class="white">
            <asp:ImageButton ID="ImageButton82" runat="server" onclick="selectSquare" ImageUrl="images/wn.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton83d" class="black">
            <asp:ImageButton ID="ImageButton83" runat="server" onclick="selectSquare" ImageUrl="images/wb.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton84d" class="white">
            <asp:ImageButton ID="ImageButton84" runat="server" onclick="selectSquare" ImageUrl="images/wq.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton85d" class="black">
            <asp:ImageButton ID="ImageButton85" runat="server" onclick="selectSquare" ImageUrl="images/wk.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton86d" class="white">
            <asp:ImageButton ID="ImageButton86" runat="server" onclick="selectSquare" ImageUrl="images/wb.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton87d" class="black">
            <asp:ImageButton ID="ImageButton87" runat="server" onclick="selectSquare" ImageUrl="images/wn.png" CssClass="piece"/>
        </div>
        <div ID="ImageButton88d" class="white">
            <asp:ImageButton ID="ImageButton88" runat="server" onclick="selectSquare" ImageUrl="images/wr.png" CssClass="piece"/>
        </div>
    </div>
     <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>       
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
