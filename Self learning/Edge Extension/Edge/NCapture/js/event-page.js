browser.storage.onChanged.addListener(function (changes, storageName) {
    browser.browserAction.setBadgeText({ "text": changes.numbers_in_progress.newValue.toString() });
    if (changes.numbers_in_progress == 0) {
        browser.browserAction.setBadgeBackgroundColor({ color: '#0E3B09' });
    } else {
        browser.browserAction.setBadgeBackgroundColor({ color: '#C26700' });
    }
});
