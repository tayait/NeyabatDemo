// validation
function check() {
    var x = document.forms.editSessionFileForm
    x[0].checked = true
}

function uncheck() {
    var x = document.forms.editSessionFileForm
    x[0].checked = false
}
var prevAgendaItemIndex;
var prevAgendaSubItemIndex;
var prevSpeakerIndex;
var prevSpeakerTitle;
var prevFragOrder ;



$(document).ready(function () {
    var startTime = $('.hdstartTime');
    var endTime = $('.hdendTime');
    var currentOrder = $('.hdcurrentOrder');

    // tinymce
    $('textarea.tinymce').tinymce({
        custom_undo_redo : false,
        // Location of TinyMCE script
        script_url: 'scripts/tiny_mce/tiny_mce.js',
        // General options
        theme: "advanced",
        plugins: "pagebreak,directionality,noneditable",
        language: "ar",
        // direction
        directionality: "rtl",
        // clean up
        cleanup: true,
        cleanup_on_startup: true,
        width: '100%',
        height: 200,
        theme_advanced_source_editor_wrap: true,
        // Theme options
        theme_advanced_buttons1: "bold,italic,|,outdent,indent,blockquote",
        theme_advanced_buttons2: "",
        theme_advanced_buttons3: "",
        theme_advanced_buttons4: "",
        theme_advanced_path: false,
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "right",
        theme_advanced_resizing: false,
        // Example content CSS (should be your site CSS)
        content_css: "styles/tinymce_content.css",
        // invalid elements
        invalid_elements: "applet,body,button,caption,fieldset ,font,form,frame,frameset,,head,,html,iframe,img,input,link,meta,object,option,param,script,select,style,table,tbody,tr,td,th,tbody,textarea,xmp",
        // valid elements
        valid_elements: "@[class],span[*],strong,em,blockquote,br",
        force_br_newlines: true,
        force_p_newlines: false,
        forced_root_block: '',
        setup: function (ed) {
            // function to make the span editable
            function editableSpan(ed, e) {
                if (e) {
                    // remove all classes from the editor
                    $('span.editable', ed.contentDocument).removeClass('editable');
                    // add class editable
                    if (e.nodeName == 'SPAN') {
                        // add class editable
                        $(e).addClass('editable');
                        // time from the span
                        var time = Math.floor($(e).attr('data-stime'))
                        // seek
                        $("#jquery_jplayer_1").jPlayer("play", time);
                    }
                }
            }
            // click on text tinyMCE editor
            ed.onMouseUp.add(function (ed, e) {
                editableSpan(ed, e.target)
            });
            // oninit
            ed.onInit.add(function (ed) {
                var AudioPlayer = $("#jquery_jplayer_1");
                // all span segments
                var all_spans_segments = $('span.segment', ed.contentDocument);
                // hover effect
                all_spans_segments.live("mouseover mouseout", function (event) {
                    if (event.type == "mouseover") {
                        // remove all classes
                        $(this).toggleClass('hover');
                    } else {
                        // remove hover class
                        $(this).removeClass('hover');
                    }
                });
                // jplayer
                var playertime;
                /*AudioPlayer.bind($.jPlayer.event.seeking, function(event) {
                    // Add a listener to report the time play began
                    console.log('seeked')
                });*/
                AudioPlayer.jPlayer({
                    swfPath: "/scripts/jPlayer/",
                    wmode:"window",
                    solution: 'html, flash',
                    supplied: "mp3",
                    preload: 'metadata',
                    volume: 1,
                    cssSelectorAncestor: '#jp_container_1',
                    errorAlerts: false,
                    warningAlerts: false,
                    ready: function () {
                        // get start and end time in hidden fields
                        var firstTime = Math.floor(startTime.val())
                        // play the jplayer
                        $(this).jPlayer("setMedia", {
                            mp3: $(".MP3FilePath").val() // mp3 file path
                        }).jPlayer("play", firstTime);
                        // next x seconds button
                        $('.jp-audio .next-jp-xseconds').click(function(e){
                            AudioPlayer.jPlayer("play",playertime + 5)
                        })
                        // prev x seconds button
                        $('.jp-audio .prev-jp-xseconds').click(function(e){
                            AudioPlayer.jPlayer("play",playertime - 5)
                        })
                    },
                    timeupdate: function (event) {
                        if (!$(this).data("jPlayer").status.paused) {
                            // all span segments
                            var all_spans_segments = $('span.segment', ed.contentDocument);
                            firstTime = Math.floor(startTime.val())
                            var lastTime = Math.floor($('span.segment:last',ed.contentDocument).attr('data-stime'))//endTime.val() from hidden field
                            // remove all classes
                            all_spans_segments.removeClass('highlight editable');
                            // highlight the word by time
                            playertime = event.jPlayer.status.currentTime;
                            if (Math.round(playertime) > lastTime && !AudioPlayer.hasClass('playerStoppedBefore')) {
                                AudioPlayer.addClass('playerStoppedBefore').jPlayer('pause',firstTime);
                            } else if(Math.floor(playertime) < firstTime){
                                //$(this).jPlayer('play',Math.round(startTime.val()));
                            } else {
                                //
                                var playerfixedTime = playertime.toFixed(2);
                                var playerfixedTimeString = playerfixedTime.toString();
                                var playerfixedTimeToArray = playerfixedTimeString.split('.');
                                // highlight the span
                                var highlight = all_spans_segments.filter('span.segment[data-stime^=' + playerfixedTimeToArray[0] + '\\.]');
                                if (highlight.length > 1) {
                                    highlight = highlight.filter(function () {
                                        // get the nearest span
                                        var spanTime = $(this).attr('data-stime')
                                        var spanTimeToArray = spanTime.split('.');
                                        var spanfragment = spanTimeToArray[1];
                                        var playerfragment = playerfixedTimeToArray[1];
                                        if (playerfragment >= spanfragment) {
                                            return $(this);
                                        }
                                    }).filter(':last')
                                }
                                // highlight
                                highlight.addClass('highlight')
                            }
                            if($.browser.msie && $.browser.version == '9.0'){
                                if (Math.round(playertime) > lastTime || Math.floor(playertime) < firstTime){
                                    AudioPlayer.jPlayer('stop')
                                }
                            }
                        }
                    }
                });
                // jplayer shorcuts
                $(document).add(ed.dom.doc.body).bind('keydown',function(e){
                     var k = e.keyCode;
                     if($(e.target).find(':input,select').length){ // not input
                         if(k == 88 || k== 67 || k == 86 || k == 66){
                            e.preventDefault();
                            return;
                         }
                     }
                     if(k == 116){
                        window.location.href = window.location.href;
                     }
                }).bind('keydown', 'alt+z',function(){
                    // previous page
                    $(".btn.prev").trigger('click')
                }).bind('keydown', 'alt+w',function(){
                    // play & pause player
                    if (AudioPlayer.data("jPlayer").status.paused) {
                        AudioPlayer.jPlayer("play");
                    } else {
                        AudioPlayer.jPlayer("pause");
                    }
                }).bind('keydown', 'alt+q',function(){
                    // stop player
                    AudioPlayer.jPlayer("stop");
                }).bind('keydown', 'alt+x',function(){
                    // next page
                    $(".btn.next").trigger('click')
                }).bind('keydown', 'alt+5',function(){
                    // next x seconds
                    $('.jp-audio .next-jp-xseconds').trigger('click')
                }).bind('keydown', 'alt+4',function(){
                    // prev x seconds
                    $('.jp-audio .prev-jp-xseconds').trigger('click')
                }).bind('keydown', 'ctrl+x',function(){
                    // split key
                    $(".split").trigger('click')
                })
           });
        }
    });

    // tinymce for the popup window
    $('#MainContent_Textarea1').tinymce({
        custom_undo_redo : false,
        // Location of TinyMCE script
        script_url: 'scripts/tiny_mce/tiny_mce.js',
        // General options
        theme: "advanced",
        plugins: "pagebreak,directionality,noneditable",
        language: "ar",
        // direction
        directionality: "rtl",
        // clean up
        cleanup: true,
        cleanup_on_startup: true,
        width: '100%',
        height: 200,
        theme_advanced_source_editor_wrap: true,
        // Theme options
        theme_advanced_buttons1: "bold,italic,|,outdent,indent,blockquote",
        theme_advanced_buttons2: "",
        theme_advanced_buttons3: "",
        theme_advanced_buttons4: "",
        theme_advanced_path: false,
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "right",
        theme_advanced_resizing: false,
        // Example content CSS (should be your site CSS)
        content_css: "styles/tinymce_content.css",
        // invalid elements
        invalid_elements: "applet,body,button,caption,fieldset ,font,form,frame,frameset,,head,,html,iframe,img,input,link,meta,object,option,param,script,select,style,table,tbody,tr,td,th,tbody,textarea,xmp",
        // valid elements
        valid_elements: "@[class],span[*],strong,em,blockquote,br",
        force_br_newlines: true,
        force_p_newlines: false,
        forced_root_block: ''
    });
});