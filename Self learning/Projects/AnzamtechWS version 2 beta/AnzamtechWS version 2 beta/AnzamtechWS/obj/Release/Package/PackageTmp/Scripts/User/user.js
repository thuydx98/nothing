$(document).ready(function () {/*
    
    */
    //alert($(document).width());
    $("#mSanPham").click(function () {
        $("#mSanPham").toggleClass("active");
        $(".mega-menu").slideToggle();
    });
    $(document).on("click", function (e) {
        var curr = $(e.target);
        if ($("#mobile-menu").has(curr).length == 0 && $(".main-menu-mobile").has(curr).length == 0 && $(".main-menu-mobile").hasClass("active")) {
            $("#mobile-menu").slideUp();
            $(".main-menu-mobile").toggleClass("active");
        }
        //  console.log($(".mega-menu").has(curr));
        if ($(".mega-menu").has(curr).length == 0 && $("#mSanPham").find(curr).length == 0 && $("#mSanPham").hasClass("active")) {
            $("#mSanPham").toggleClass("active");
            $(".mega-menu").slideUp();
        }

    });
    $("#mobile-menu > .navbar-nav").on("click", function (e) {
        var curr = $(e.target);
        var parrent = curr.parent();
        if (parrent.hasClass("has-dropdown-menu")) {

            var child = parrent.children(".dropdown-menu");
            if (parrent.hasClass("toggled")) {
                parrent.toggleClass("toggled");
                child.slideUp();
            } else {
                parrent.toggleClass("toggled");
                child.slideToggle(300);
            }

        }
    });

    var event = $(".main-menu-mobile").find("a");
    event.click(function () {
        if ($(".main-menu-mobile").hasClass("active")) {
            $("#mobile-menu").slideUp();
            $(".main-menu-mobile").toggleClass("active");
        } else {
            $(".main-menu-mobile").toggleClass("active");
            $("#mobile-menu").slideToggle(320);
        }
    });


});
$(function () {

    $('.lazy').lazy({
        afterLoad: function (element) {
            element.parent().addClass("loaded");
        }
    });

});