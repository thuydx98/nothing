browser.storage.onChanged.addListener(function (changes, storageName) {
    showCaptureList(changes.downloaded_list);
});

$(function () {

    var list = [];

    browser.storage.sync.get('downloaded_list', function (ncapture) {

        if (ncapture.downloaded_list)
            list = ncapture.downloaded_list;

        if (ncapture.numbers_in_progress && ncapture.numbers_in_progress !== 0)
            amount_in_progress = ncapture.numbers_in_progress;

        showCaptureList(list);
    });


});

function showCaptureList(list) {
    var captureHtml = '';

    list.forEach(capture => {
        captureHtml += 
            '<tr id="' + capture.id + '">' +
                '<td><img class="row-image" src="icons/pdf.png">&nbsp;' + capture.name + '</td>' +
                '<td><img class="source-type-image" src="icons/web.png"> </td>' +
                '<td>'+ capture.message + '</td>' +
                '<td><img src="icons/complete.png" title="Capture Completed."></td>' +
            '</tr>';
    });

    $('#notification-table-body').html(captureHtml);
}