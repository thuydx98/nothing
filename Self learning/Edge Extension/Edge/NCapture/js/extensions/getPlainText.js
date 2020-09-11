if (document !== null) {
    var paragraphs = document.getElementsByTagName("p");
    var html = '';

    for (var i = 0; i < paragraphs.length; i++) {
        html += '<p>' + paragraphs[i].innerHTML + '</p>' + '<br>';
    }

    document.body.innerHTML = html;
    document.body.style.backgroundColor = "white";
}