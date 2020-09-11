$(function () {

  // Tabs function
  $("#tabs").tabs();

  // $('#tabs').addClass("ui-tabs ui-corner-all ui-widget ui-widget-content");
  $('#tabs ul').attr('role', 'tablist');
  $('#tabs ul li').attr('role', 'tab').addClass('ui-tabs-tab ui-tab ui-tabs-active').removeClass('ui-tabs-selected');
  $('#tabs ul li a').addClass('ui-tabs-anchor').attr('role', 'presentation').attr('tabindex', '-1');

  $('#tab-desc').attr('tabindex', '0').attr('aria-controls', 'tabs-1').attr('aria-labelledby', 'description-tab').attr('aria-selected', 'true').attr('aria-expanded', 'true');

  $('#tab-memo').removeClass('ui-tabs-active').attr('tabindex', '-1').attr('aria-controls', 'tabs-2').attr('aria-labelledby', 'memo-tab').attr('aria-selected', 'false').attr('aria-expanded', 'false');
  $('#memoIcon').remove();

  $('#tabs-1').attr('aria-labelledby', 'description-tab').attr('role', 'tabpanel').attr('style', "background: none 0% 0% / auto repeat scroll padding-box border-box rgba(0, 0, 0, 0)");

  $('#description-tab').click(function () {
    $('#tab-desc').addClass('ui-state-active ui-tabs-active').attr('tabindex', '0').attr('aria-selected', 'true').attr('aria-expanded', 'true');
    $('#tab-memo').removeClass('ui-state-active ui-tabs-active').attr('tabindex', '-1').attr('aria-selected', 'false').attr('aria-expanded', 'false');
    $('#tabs-1').attr('style', "background: none 0% 0% / auto repeat scroll padding-box border-box rgba(0, 0, 0, 0)");
    $('#tabs-2').attr('style', "display: none; background: none 0% 0% / auto repeat scroll padding-box border-box rgba(0, 0, 0, 0)");
  });

  $('#tabs-2').attr('aria-labelledby', 'memo-tab').attr('role', 'tabpanel').attr('style', "display: none; background: none 0% 0% / auto repeat scroll padding-box border-box rgba(0, 0, 0, 0)");

  $('#memo-tab').click(function () {
    $('#tab-desc').removeClass('ui-state-active ui-tabs-active').attr('tabindex', '-1').attr('aria-selected', 'false').attr('aria-expanded', 'false');
    $('#tab-memo').addClass('ui-state-active ui-tabs-active').attr('tabindex', '0').attr('aria-selected', 'true').attr('aria-expanded', 'true');
    $('#tabs-1').attr('style', "display: none; background: none 0% 0% / auto repeat scroll padding-box border-box rgba(0, 0, 0, 0)");
    $('#tabs-2').attr('style', "background: none 0% 0% / auto repeat scroll padding-box border-box rgba(0, 0, 0, 0)");
  });
  // End Tabs function

  // Storage
  browser.storage.sync.get(['downloaded_list', 'numbers_in_progress'], function (ncapture) {

    if (!ncapture.downloaded_list)
      ncapture.downloaded_list = [];

    if (!ncapture.numbers_in_progress)
      ncapture.numbers_in_progress = 0;

    browser.browserAction.setBadgeText({ "text": ncapture.numbers_in_progress.toString() });
    if (ncapture.numbers_in_progress == 0) {
      browser.browserAction.setBadgeBackgroundColor({ color: '#0E3B09' });
    } else {
      browser.browserAction.setBadgeBackgroundColor({ color: '#C26700' });
    }
  });
  // End storage



  browser.tabs.query({ active: true, currentWindow: true }, function (tabs) {

    var numOfSlash = 0;
    var tabUrl = tabs[0].url.toString();

    for (const element of tabUrl) {
      if (element == '/') {
        numOfSlash++;
      }
    }

    if (numOfSlash < 3 || (numOfSlash == 3 && tabUrl[tabUrl.length - 1] == '/')) {
      $('#radio-source-type').hide();
      $('#capture').addClass("disabled");
      $('#source-name-TextBox').val('');
    } else {
      $('#radio-source-type').show();
      $('#capture').removeClass("disabled");
      $('#source-name-TextBox').val(tabs[0].title);
    }
  });

  $('#captured-list').click(function () {
    browser.tabs.create({ url: "/notifications.html" });
    window.open("/notifications.html");
  });

  $("#node-tags").tagit({
    allowSpaces: true,
    onTagExists: function (event, ui) {
      var existsValue = $('input[type="text"].ui-autocomplete-input').val();

      bootstrap_alert.error('Node name "' + existsValue + '" is a duplicate.');
    }
  });

  $('#ArticleAsPDF-label').click(function () {

    var radios = document.getElementsByName('toggle');

    if (!radios[1].checked) {
      browser.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        browser.tabs.executeScript(tabs[0].id, { file: "js/extensions/getPlainText.js" });
      });
    }

  });

  $('#WebPageAsPDF-label').click(function () {
    browser.tabs.query({ active: true, currentWindow: true }, function (tabs) {
      browser.tabs.executeScript(tabs[0].id, { file: "js/extensions/reload.js" });
    });
  });

  $('#capture').click(function () {

    var btnCaptureStatus = $('#capture').hasClass('disabled');

    if (btnCaptureStatus == false) {

      // Add capture to storage
      browser.storage.sync.get(['downloaded_list', 'numbers_in_progress'], function (ncapture) {

        var list = [];
        var amount_in_progress = 0;

        if (ncapture.downloaded_list) {
          list = ncapture.downloaded_list;
        }

        if (ncapture.numbers_in_progress)
          amount_in_progress = ncapture.numbers_in_progress;

        var newCapture = {
          id: 'capture-' + list.length,
          name: $('#source-name-TextBox').val(),
          description: $('#sourceName').val(),
          meno: $('#sourceName').val(),
          nodes: [],
          type: "Web",
          message: "In Progress"
        };

        list.push(newCapture);
        amount_in_progress += 1;

        browser.storage.sync.set({ 'downloaded_list': list, 'numbers_in_progress': amount_in_progress });
      });

      // Desktop notification Capture started
      var notifyOptions = {
        type: "basic",
        iconUrl: "icons/ajax-loader-green-48.gif",
        title: "NCapture Notification",
        message: document.getElementById("source-name-TextBox").value,
        requireInteraction: false,
        buttons: [
          { title: 'Show capture progress page' }
        ]
      };

      browser.notifications.onButtonClicked.addListener(function () {
        browser.tabs.create({ url: "/notifications.html" });
        window.open("/notifications.html");
      });
      
      browser.notifications.create(notifyOptions);

      // Capturing
      browser.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        browser.tabs.executeScript(tabs[0].id, { file: "js/extensions/convertWebPageAsPDF.js" }, function () {

          // Desktop notice success
          var notifyDoneOptions = {
            type: "basic",
            iconUrl: "icons/complete-48.png",
            title: "NCapture Notification",
            message: document.getElementById("source-name-TextBox").value,
            requireInteraction: false,
            buttons: [
              { title: 'Show capture progress page' }
            ]
          };
          browser.notifications.create(notifyDoneOptions);

          // Change status of capture in storege and nums of capuring in storage

          window.close();
        });
      });
    }
  });
});

bootstrap_alert = function () { }
bootstrap_alert.error = function (message) {
  $('#error-message-placeholder').html('<div id="error-message" class="alert alert-error" aria-label="Close"><a id="error-message-close" class="close" data-dismiss="alert">×</a><strong>Error! </strong>' + message + '</div>')

  $('#error-message-close').click(function () {
    $('#error-message-placeholder').html('');
  });
}

