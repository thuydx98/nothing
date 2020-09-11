function ImageSlider(c) {
    var name = "." + c;
    var ResizeImage = function () {
        var id = $(name);
        id.find(".slide img").css("width", $(document).width()); 
    }
    $(document).ready(function () {
        ResizeImage();
    })
       $(window).resize(function () {
        ResizeImage();
    })
    return Init;
}

