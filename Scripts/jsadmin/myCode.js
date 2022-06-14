/*global $, alert, console, jQuery, Facebook*/

$(document).ready(function () {

    "use strict";
        
    $("html").niceScroll();
    
    
    $('.Mynavbar .myList li.yy').click(function () {
      
        $(this).next('.xx').fadeToggle(600);
        
        $('.Mynavbar .myList .xx').not($(this).next('.xx')).hide();
       
    });
    
    
    $('#bar1').barfiller({

        // color of bar
        barColor: '#90B82D',

        // shows a tooltip
        tooltip: true,

        // duration in ms
        duration: 1000,

        // re-animate on resize
        animateOnResize: true,
        
        // custom symbol
        symbol: "%"
  
    });
    
    $('#bar2').barfiller({

        // color of bar
        barColor: '#90B82D',

        // shows a tooltip
        tooltip: true,

        // duration in ms
        duration: 1000,

        // re-animate on resize
        animateOnResize: true,

        // custom symbol
        symbol: "%"
  
    });
    
    $('#bar3').barfiller({

        // color of bar
        barColor: '#90B82D',

        // shows a tooltip
        tooltip: true,

        // duration in ms
        duration: 1000,

        // re-animate on resize
        animateOnResize: true,

        // custom symbol
        symbol: "%"
  
    });
    
    
    $('#bar4').barfiller({

        // color of bar
        barColor: '#90B82D',

        // shows a tooltip
        tooltip: true,

        // duration in ms
        duration: 1000,

        // re-animate on resize
        animateOnResize: true,

        // custom symbol
        symbol: "%"
  
    });
    
    
    /* ********************************************************************** */
    
    $(".product .myDiv .zz:not(:first-of-type)").css({display: "none"});
        
    $("#my-tabs li").click(function () {
        
        $(".product .myDiv .zz:not(:first-of-type)").css({display: "none"});
        
        var myID = $(this).attr("id");
        
        $(this).removeClass("In-active").siblings().addClass("In-active");
        
        $(".product #my-tabs").next('div').hide();
        
        $("#" + myID + "-content").fadeIn(400);
        
    });
    
    /* ********************************************************************** */
    
    
    $(".product .myDiv .zz:not(:first-of-type)").css({ display: "none" });
        
    $("#my-tabs2 li").click(function () {
        
        $(".product .myDiv .zz:not(:first-of-type)").css({ display: "none" });
        
        var myID = $(this).attr("id");
        
        $(this).removeClass("In-active").siblings().addClass("In-active");
        
        $(".product #my-tabs2").next('div').hide();
        
        $("#" + myID + "-content").fadeIn(400);

     

        
    });
    
    
     /* ********************************************************************** */
    
    
    $(".product .myDiv .zz:not(:first-of-type)").css({ display: "none" });
        
    $("#my-tabs3 li").click(function () {
        
        $(".product .myDiv .zz:not(:first-of-type)").css({display: "none"});
        
        var myID = $(this).attr("id");
        
        $(this).removeClass("In-active").siblings().addClass("In-active");
        
        $(".product #my-tabs3").next('div').hide();
        
        $("#" + myID + "-content").fadeIn(400);
        
    });
    
     /* ********************************************************************** */
    
    $(".faq .ff:not(:first-of-type)").css({display: "none"});
        
    $("#my-tabs5 li").click(function () {
        
        $(".faq .ff:not(:first-of-type)").css({display: "none"});
        
        var myID = $(this).attr("id");
        
        $(this).removeClass("In-active").siblings().addClass("In-active");
        
        $(".faq #my-tabs5").next('div').hide();
        
        $("#" + myID + "-content").fadeIn(400);
        $("#" + myID + "-content").css("margin-bottom",0);
        
    });
    
     /* ********************************************************************** */
    
    
    $(".manger .dd:not(:first-of-type)").css({display: "none"});
        
    $("#my-tabs6 li").click(function () {
        
        $(".manger .dd:not(:first-of-type)").css({display: "none"});
        
        var myID = $(this).attr("id");
        
        $(this).removeClass("In-active").siblings().addClass("In-active");
        
        $(".manger #my-tabs6").next('div').hide();
        
        $("#" + myID + "-content").fadeIn(400);
        
    });
    
     /* ********************************************************************** */
    
    $(".details .ee:not(:first-of-type)").css({display: "none"});
        
    $("#my-tabs7 li").click(function () {
        
        $(".details .ee:not(:first-of-type)").css({display: "none"});
        
        var myID = $(this).attr("id");
        
        $(this).removeClass("In-active").siblings().addClass("In-active");
        
        $(".details #my-tabs7").next('div').hide();
        
        $("#" + myID + "-content").fadeIn(400);
        
    });
    
    
    /* ********************************************************************** */
    
    $(".gallery .hh:not(:first-of-type)").css({display: "none"});
        
    $("#my-tabs9 li").click(function () {
        
        $(".gallery .hh:not(:first-of-type)").css({display: "none"});
        
        var myID = $(this).attr("id");
        
        $(this).removeClass("In-active").siblings().addClass("In-active");
        
        $(".gallery #my-tabs9").next('div').hide();
        
        $("#" + myID + "-content").fadeIn(400);

        var
            left_side = $(".left-side").height(),
            faq = $(".faq").height(),
            footer = $(".footer").height(),
            Team = $(".Team .my-panel").height(),
            vendor = $(".vendor .my-panel").height(),
            gallery = $(".gallery .vendor-edit-content").height(),
            faqX = $(".faq .vendor-edit-content").height(),
            calc = left_side - faq - footer - 108 + "px",
            calc2 = left_side - Team - footer - 200 + "px",
            calc3 = left_side - vendor - footer - 200 + "px",
            calc4 = left_side - gallery - footer - 270 + "px",
            calc5 = left_side - faqX - footer - 223 + "px";


        $(".faqNew").next(".footer").css("marginTop", calc);

        $(".Team .my-panel").css("marginBottom", calc2);

        $(".vendor .my-panel").css("marginBottom", calc3);

        $(".gallery .vendor-edit-content").css("marginBottom", calc4);

        $(".faq .vendor-edit-content").css("marginBottom", calc5);


        
    });
    
    
    /* ********************************************************************** */
    
    
 
    $(".gallery .mylists .button").click(function () {
        
        $(this).addClass("skills active").siblings().removeClass("skills active");

        
        var value = $(this).attr("data-filter");
        
        if (value === "all") {
            
            $(".filter").removeClass('is-animated')
                        .hide().promise().done(function () {
                    $(".filter").addClass('is-animated').show();
                });
            
        } else {
            
            
            
            $(".filter").removeClass('is-animated')
                    .hide().promise().done(function () {
                    $(".filter").filter("." + value)
                            .addClass('is-animated').show();
                });
            
        }
    });
    
    /* ******************************************************************** */
    
    $("[data-toggle=tooltip]").tooltip();
    
    /* ******************************************************************** */
    
    var
        left_side = $(".left-side").height(),
        faq = $(".faq").height(),
        footer = $(".footer").height(),
        Team  = $(".Team .my-panel").height(),
        vendor = $(".vendor .my-panel").height(),
        gallery = $(".gallery .vendor-edit-content").height(),
        faqX = $(".faq .vendor-edit-content").height(),
        calc = left_side - faq - footer - 108  + "px",
        calc2 = left_side - Team - footer - 200  + "px",
        calc3 = left_side - vendor - footer - 200  + "px",
        calc4 = left_side - gallery - footer - 270  + "px",
        calc5 = left_side - faqX - footer - 223  + "px";
    
    
    $(".faqNew").next(".footer").css("marginTop", calc);
    
    $(".Team .my-panel").css("marginBottom", calc2);
    
    $(".vendor .my-panel").css("marginBottom", calc3);
    
    $(".gallery .vendor-edit-content").css("marginBottom", calc4);
    
    $(".faq .vendor-edit-content").css("marginBottom", calc5);
    
    /* ********************************************************* */
    
    $(window).on("resize", function () {
       
        var
            left_side = $(".left-side").height(),
            faq = $(".faq").height(),
            footer = $(".footer").height(),
            Team  = $(".Team .my-panel").height(),
            vendor = $(".vendor .my-panel").height(),
            gallery = $(".gallery .vendor-edit-content").height(),
            calc = left_side - faq - footer - 108  + "px",
            calc2 = left_side - Team - footer - 200  + "px",
            calc3 = left_side - vendor - footer - 200  + "px",
            calc4 = left_side - gallery - footer - 270  + "px";
            
        $(".faqNew").next(".footer").css("marginTop", calc);
    
        $(".Team .my-panel").css("marginBottom", calc2);

        $(".vendor .my-panel").css("marginBottom", calc3);

        $(".gallery .vendor-edit-content").css("marginBottom", calc4);

        
    });
        
    
});