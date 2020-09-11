
var AdminPage = function () {
    this.construct = function () {
        
    };

    this.ActiveLeftSideBar = function () {
        AddBar();
        AddWaves();
        SetMenuHeight();
        $('.menu-toggle').on('click', function (e) {
            var $this = $(this);
            var $content = $this.next();

            if ($($this.parents('ul')[0]).hasClass('list')) {
                var $not = $(e.target).hasClass('menu-toggle') ? e.target : $(e.target).parents('.menu-toggle');

                $.each($('.menu-toggle.toggled').not($not).next(), function (i, val) {
                    if ($(val).is(':visible')) {
                        $(val).prev().toggleClass('toggled');
                        $(val).slideUp();
                    }
                });
            }

            $this.toggleClass('toggled');
            $content.slideToggle(320);
        });
        $(window).resize(function () {
            SetMenuHeight();
        });
    }

    this.ActiveNotification = function () {

    }

    var SetMenuHeight = function () {
        if (typeof $.fn.slimScroll != 'undefined') {
            var height = $(window).height() - $("#header").outerHeight() - $(".user-info").outerHeight() - $(".legal").outerHeight();
          
            $('.menu').slimScroll({
                height: height + 'px',
                color: 'rgba(0,0,0,0.5)',
                size: '4px',
                position: 'right',
                alwaysVisible: false,
                railBorderRadius: '1px',
                borderRadius: '1px',
            });
        }
    };
    var AddBar = function () {
        $(window).on("click", function (e) {
          //  alert(e.target);
            var $target = $(e.target);
            if (!$target.hasClass('bars') && $("body").hasClass("open") && $target.parents('#leftsidebar').length === 0)
            {
                $("body").removeClass("open");
            }
        });
        $(".bars").on("click", function () {
            if ($("body").hasClass("open"))
            {
                $("body").removeClass("open");
            } else {
                $("body").addClass("open");
            }
        });

    };
    var AddWaves = function () {
        Waves.attach('.dropdown-menu li a', ['waves-block']);
        Waves.init();
    }
    
}

$(function () {
    var myadmin = new AdminPage();
    myadmin.ActiveLeftSideBar();
});