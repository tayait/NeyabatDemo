﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditVideoSessionFile.aspx.cs" Inherits="TayaIT.Enterprise.Neyabat.Web.EditVideoSessionFile" MasterPageFile="~/Site.master" Title="تايا اى تى - تحويل الملف الصوتى الى نص - عرض الملف الصوتي" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
 <script type="text/javascript" src="scripts/jquery.hotkeys.js"></script>
    <script type="text/javascript" src="scripts/tiny_mce/jquery.tinymce.js"></script>
    <script type="text/javascript" src="scripts/jPlayer/jquery.jplayer.min.js"></script>
    <script type="text/javascript" src="scripts/videoForMMSScript.js"></script>
    <link href="styles/jplayer.blue.monday.css" rel="stylesheet" type="text/css" />
    <link href="styles/select2.min.css" rel="stylesheet" type="text/css" />

        <style>
       
        div.jp-audio div.jp-type-single div.prev-jp-xseconds{
            -webkit-transform: scaleY(-1);
            -moz-transform: scaleY(-1);
            transform: scaleY(-1);
        }
        
        div.jp-audio div.jp-type-single div.jp-progress{
            width: 55%;
        }
        
        div.jp-audio div.jp-type-single div.jp-duration{
            left: 67%;
        }
        
        div.jp-video-270p div.jp-type-single a.jp-play, div.jp-video-270p div.jp-type-single a.jp-pause{
            left: 100% !important;
    margin-left: -35px;
        }
        
        div.jp-audio div.jp-type-single div.next-jp-xseconds{
                left: 93%;
    margin-left: -30px;
        }
        
        div.jp-audio div.jp-type-single div.prev-jp-xseconds{
            left: 87%;
    margin-left: -30px;
        }
        .editSessionFileForm {
            margin: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form id="editSessionFileForm" runat="server">
    <input id="MP3FilePath" class="MP3FilePath" type="hidden" runat="server" value="" />
    <input id="sessionID" type="hidden" value="" runat="server" class="sessionID" />
    <input id="eparId" type="hidden" value="" runat="server" class="eparId" />
    <input id="XmlFilePath" type="hidden" value="" runat="server" class="hdxmlFilePath" />
    <div id="editSessionFile" class="container_24">
     <div>
            <asp:Label runat="server" ID="lblInfo1" Visible="false" CssClass="lInfo"></asp:Label>
        </div>
          <div class="clear">
        </div>
        <div class="row">
           <div class="grid_26">
                <div class="borderBD row h2">
                    <div class="fr">الملف: <strong><asp:Label ID="lblMP3FileName" runat="server"></asp:Label></strong></div>
                    <div class="fr spaceR">بتاريخ: <strong><asp:Label ID="lblFileDate" runat="server"></asp:Label></strong></div>
                    <div class="fr spaceR">الحجم: <strong><asp:Label ID="lblFileSize" runat="server"></asp:Label></strong></div>
                    <div class="fl"><strong><a href="Default.aspx" style="font-size: 115%">عودة للصفحة الرئيسية</a></strong></div>
                    <div class="clear"></div>
                </div>
     
                <div class="player_conatiner mb-20">
                  <div id="jquery_jplayer_1" class="jp-jplayer jp-jplayer-video">
        </div>
         <div id="jp_container_1" class="jp-audio">
                        <div class="jp-type-single">
                            <div id="jp_interface_1" class="jp-interface">
                                <ul class="jp-controls">
                                    <li><a href="#" class="jp-play" tabindex="1" title="play"></a></li>
                                    <li><a href="#" class="jp-pause" tabindex="1" title="pause"></a></li>
                                </ul>
                                <div class="jp-progress">
                                    <div class="jp-seek-bar">
                                        <div class="jp-play-bar">
                                        </div>
                                    </div>
                                </div>
                                <div class="jp-current-time">
                                </div>
                                <div class="jp-duration">
                                </div>
                                <div class="next-jp-xseconds" title="تقديم 5 ثوانى">
                                </div>
                                <div class="prev-jp-xseconds" title="تاخير 5 ثوانى">
                                </div>
                            </div>
                        </div>
                    </div>
	<br />
                    <textarea id="elm1" runat="server" name="elm1" rows="3" style="width: 100%" class="tinymce"></textarea>
                </div>
       
            </div>
        </div>
        <div class="row">
            <div class="grid_21 suffix_3">
               
            </div>
        </div>
    </div>

    </form>
</asp:Content>
