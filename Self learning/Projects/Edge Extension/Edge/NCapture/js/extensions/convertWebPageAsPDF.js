var filename = document.title + ".nvcx";
var data = "some data";
var blob = new Blob([data], { type: 'text' });

// setTimeout(function () {
    window.navigator.msSaveBlob(blob, filename);
// }, 10000);

// Check Edge or Chrome
// if (window.navigator.msSaveOrOpenBlob) {
//     window.navigator.msSaveBlob(blob, filename);
// }
// else {
//     var elem = window.document.createElement('a');
//     elem.href = window.URL.createObjectURL(blob);
//     elem.download = filename;
//     document.body.appendChild(elem);
//     elem.click();
//     document.body.removeChild(elem);
// }