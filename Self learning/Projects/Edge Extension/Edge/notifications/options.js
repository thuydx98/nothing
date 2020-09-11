$(function () {
    $('#resetTotal').click(function () {
        browser.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            
            var notifyOptions = {
                type: "basic",
                iconUrl: "icon48.png",
                title: "NCapture Notification",
                message: tabs[0].title,
                requireInteraction: true,
                buttons: [
                    { title: 'Show capture progress page'}
                  ]
            };

            browser.notifications.onButtonClicked.addListener(function(){
                    browser.tabs.create({ url: "/notifications.html" });
                    window.open("/notifications.html"); 
            });
    
            browser.notifications.create(notifyOptions);
        });     
    });
});

self.addEventListener('notificationclick', function(event) {
    var messageId = event.notification.data;
  
    event.notification.close();
  
    if (event.action === 'like') {
      silentlyLikeItem();
    }
    else if (event.action === 'reply') {
      clients.openWindow("/messages?reply=" + messageId);
    }
    else {
      clients.openWindow("/messages?reply=" + messageId);
    }
  }, false);