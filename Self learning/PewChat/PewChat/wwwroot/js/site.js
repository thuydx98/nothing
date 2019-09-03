$(document).ready(function () {

    var protocol = location.protocol === "https:" ? "wss:" : "ws:";
    var wsUri = protocol + "//" + window.location.host + "/chat";

    var connection = new WebSocketManager.Connection(wsUri);
    //connection.enableLogging = true;

    //connection.connectionMethods.onConnected = () => {

    //};

    //connection.connectionMethods.onDisconnected = () => {

    //};

    connection.clientMethods["receiveMessage"] = (message) => {
        var messageText = 'Thuy said: ' + message;
        console.log("Receiving through HTTP to a controller:" + message);
        $('#messages').append('<li>' + messageText + '</li>');
        $('#messages').scrollTop($('#messages').prop('scrollHeight'));
    };

    connection.start();
    console.log("Start connection");

    var $messagecontent = $('#message-content');
    $messagecontent.keyup(function (e) {
        if (e.keyCode === 13) {
            var message = $messagecontent.val().trim();
            if (message.length === 0) {
                return false;
            }

            console.log("Sending through HTTP to a controller:" + message);

            $.ajax({
                url: location.protocol + "//" + window.location.host + "/messages/sendmessage?message=" + message,
                method: 'GET'
            });

            $messagecontent.val('');
        }
    });

    $('#messages').scrollTop($('#messages').prop('scrollHeight'));
});