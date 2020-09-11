// get the buttons by id
let aliceblue = document.getElementById('aliceblue');
let cornsilk = document.getElementById('cornsilk');
let reset = document.getElementById('reset');

// use tabs.insertCSS to change header color on button click

// aliceblue
aliceblue.onclick = () => {
    browser.tabs.insertCSS({ code: "body { background: aliceblue !important; }" });
};

// cornsilk
cornsilk.onclick = () => {
    browser.tabs.insertCSS({ code: "body { background: cornsilk !important; }" });
};

// back to original
reset.onclick = () => {
    browser.tabs.insertCSS({ code: "body { background: none !important; }" });
};

function notifyMe() {
    if (!window.Notification) {
        console.log('Browser does not support notifications.');
    } else {
        // check if permission is already granted
        if (Notification.permission === 'granted') {
            // show notification here
            var notify = new Notification('Hi there!', {
                body: 'How are you doing?',
                icon: 'https://cdn4.vectorstock.com/i/1000x1000/08/78/check-icon-check-mark-icon-check-list-vector-24060878.jpg',
            });
            console.log('Granted');
        } else {
            // request permission from user
            Notification.requestPermission().then(function (p) {
                if (p === 'granted') {
                    // show notification here
                    var notify = new Notification('Hi there!', {
                        body: 'How are you doing?',
                        icon: 'https://cdn4.vectorstock.com/i/1000x1000/08/78/check-icon-check-mark-icon-check-list-vector-24060878.jpg',
                    });

                    console.log('Ask Granted');
                } else {
                    console.log('User blocked notifications.');
                }
            }).catch(function (err) {
                console.error(err);
            });
        }
    }
}