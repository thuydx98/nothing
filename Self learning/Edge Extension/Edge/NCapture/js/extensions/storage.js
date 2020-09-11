function getCaptureList() {
    browser.storage.sync.get('downloaded_list', function (ncapture) {
        var list = [];

        if (ncapture.downloaded_list)
            list = ncapture.downloaded_list;

        return list;
    });
}

function addCaptureToList(newCapture) {
    browser.storage.sync.get('downloaded_list', function (ncapture) {

        var list = [];

        if (ncapture.downloaded_list) {
            list = ncapture.downloaded_list;
        }

        list.push(newCapture);

        chrome.storage.sync.set({ 'downloaded_list': list });
    });
}

function changeStatus(captureId, newStatus) {
    browser.storage.sync.get('downloaded_list', function (ncapture) {

        var list = [];

        if (ncapture.downloaded_list) {
            list = ncapture.downloaded_list;
        }

        list.forEach(capture => {
            if (capture.id == captureId) {
                capture.status = newStatus;
                // break;
            }
        });

        chrome.storage.sync.set({ 'downloaded_list': list });
    });
}