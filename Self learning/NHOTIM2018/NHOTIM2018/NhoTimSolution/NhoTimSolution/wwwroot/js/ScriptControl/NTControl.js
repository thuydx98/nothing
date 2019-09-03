$(document).ready(function () {
    Resize();
    $("button[data-toggle='dropdown']").click(evtDropdown);
    $(document).on("click", releaseDropdown);
    //alert("Add");
})
function releaseDropdown(e) {
    var doc = $(e.target);
    $("button[data-toggle='dropdown']").each(function (i) {
        var popup = $(".dropdown-menu[aria-labelledby='" + $(this).attr("id") + "']");
        if (($(this).attr("aria-expanded") == "true") && (doc[0] != $(this)[0])) {
            popup.slideUp(300);
            $(this).attr("aria-expanded", false);
            return;
        }
    });
}
function evtDropdown(e) {
    e.preventDefault();
    var id = $(this).attr("id");
    var expand = $(this).attr("aria-expanded");
    var popup = $(".dropdown-menu[aria-labelledby='" + id + "']");
    var wH = $(window).height();
    var wW = $(window).width();
    var dx = $(this).position().top + $(this).outerHeight();
    var dy = $(this).position().left;
    var t, l;
    console.log(expand);
    if (expand == "false") {
        var cur = popup.position();
        if (dx + popup.outerHeight() > wH) {
            t = dx - $(this).outerHeight() - popup.outerHeight() - 5;
        } else {
            t = dx + 5;
        }

        if (dy + popup.outerWidth() > wW) {
            l = dy - $(this).outerWidth() - popup.outerWidth();
        } else {
            l = dy;
        }
        popup.css("top", t);
        popup.css("left", l);
        popup.css("height", "auto");
        $(this).attr("aria-expanded", true);
        $(".dropdown-menu[aria-labelledby='" + id + "']").slideDown(300);
    } else {
        $(".dropdown-menu[aria-labelledby='" + id + "']").slideUp(300);
        $(this).attr("aria-expanded", false);
    }
}
function Resize() {
    $(".btn").mousedown(createRipple);
}
function createRipple(e) {
    e.preventDefault();
    /*
        Note that these next two lines will create a
        NEW ripple element for each click. If this is
        undesirable behavior, try:
        
        * Setting a timeout to delete the element
        * Checking if an element has already been made & reuse it
        * Create an element around line 8 and always reuse it
        * etc.
    */
    $(this).children(".btn-ripple").remove();
    var circle = document.createElement('div');
    this.appendChild(circle);

    var d = Math.max(this.clientWidth * 40 / 100, this.clientHeight * 40 / 100);

    circle.style.width = circle.style.height = d + 'px';

    var p = $(this).offset();


    circle.style.left = e.pageX - p.left - d / 2 + 'px';
    circle.style.top = e.pageY - p.top - d / 2 + 'px';
    circle.classList.add('btn-ripple');
}

if (window.addEventListener && window.requestAnimationFrame && document.getElementsByClassName) window.addEventListener('load', function () {

    // start
    var pItem = document.getElementsByClassName('progressive replace'), pCount, timer;

    // scroll and resize events
    window.addEventListener('scroll', scroller, false);
    window.addEventListener('resize', scroller, false);

    // DOM mutation observer
    if (MutationObserver) {

        var observer = new MutationObserver(function () {
            if (pItem.length !== pCount) inView();
        });
        observer.observe(document.body, { subtree: true, childList: true, attributes: true, characterData: true });

    }

    // initial check
    inView();


    // throttled scroll/resize
    function scroller() {

        timer = timer || setTimeout(function () {
            timer = null;
            inView();
        }, 300);

    }
    
    // image in view?
    function inView() {
        if (pItem.length) requestAnimationFrame(function () {

            var wT = window.pageYOffset, wB = wT + window.innerHeight, cRect, pT, pB, p = 0;
            while (p < pItem.length) {

                cRect = pItem[p].getBoundingClientRect();
                pT = wT + cRect.top;
                pB = pT + cRect.height;

                if (wT < pB && wB > pT) {
                    loadFullImage(pItem[p]);
                    pItem[p].classList.remove('replace');
                }
                else p++;

            }

            pCount = pItem.length;

        });

    }


    // replace with full image
    function loadFullImage(item) {

        var href = item && (item.getAttribute('data-href') || item.href);
        if (!href) return;

        // load image
        var img = new Image();
        if (item.dataset) {
            img.srcset = item.dataset.srcset || '';
            img.sizes = item.dataset.sizes || '';
        }
        img.src = href;
        img.className = 'reveal';
        if (img.complete) addImg();
        else img.onload = addImg;

        // replace image
        function addImg() {

            requestAnimationFrame(function () {

                // disable click
                if (href === item.href) {
                    item.style.cursor = 'default';
                    item.addEventListener('click', function (e) { e.preventDefault(); }, false);
                }

                // preview image
                var pImg = item.querySelector && item.querySelector('img.preview');

                // add full image
                item.insertBefore(img, pImg && pImg.nextSibling).addEventListener('animationend', function () {

                    // remove preview image
                    if (pImg) {
                        img.alt = pImg.alt || '';
                        item.removeChild(pImg);
                    }

                    img.classList.remove('reveal');

                });

            });

        }

    }

}, false);