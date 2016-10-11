
(function () {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;

    // Create a function that the hub call back to disconnect some one.
    chat.client.userDisconnect = function (id) {
        $('#user_' + id).remove();
    };

    chat.client.userConnect = function (name) {
        $('#active_users').prepend('<li id="user_' + name + '"><img src="http://i.oldbk.com/i/lock.gif"  id="img_' + name + '"> <a id="name_' + name + '">' +  name  + '</a><a href=""> <img src="http://i.oldbk.com/i/inf.gif"  id="img_inf"></a></li>');

        $('#img_' + name).on("click", function () {
            $('#input_field').val('private [' + name + ']: ').focus();
        });

        $('#name_' + name).on("click", function () {
            $('#input_field').val('to [' + name + ']: ').focus();
        });
    };

    chat.client.connectedUsers = function (users) {
        $('#active_users').empty();
        $.each(users, function (index, data) {
            $('#active_users').prepend('<li id="user_' + $(data).val() + '"><img src="http://i.oldbk.com/i/lock.gif" id="img_' + data + '"><a id="name_' + data + '">' + data + '</a><a href=""><img src="http://i.oldbk.com/i/inf.gif"  id="img_inf"></a></li>');

            $('#img_' + data).on("click", function () {
                $('#input_field').val('private [' + data + ']: ').focus();
            });

            $('#name_' + data).on("click", function () {
                $('#input_field').val('to [' + data + ']: ').focus();
            });

        });
    };




    // Create a function that the hub can call back to display messages.
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        if (message) {
            var scrollTop = $('#messages')[0].scrollHeight - $('#messages').height();
            var moveDown = (Math.abs(scrollTop - $('#messages').scrollTop()) < 30);

            var date = new Date();

            $('#messages').append('<div class = "message"><i> ' + date.getHours() + ':' + date.getMinutes() + '  </i>[<a id="name_' + name + '">' + name + '</a>]:  ' + message + "</div>");

            if (moveDown) {
                $('#messages').scrollTop($('#messages')[0].scrollHeight);
            }
        }
    };

    // Set initial focus to message input box.
    $('#input_field').focus();
    // Start the connection.

    connectionFactory.start(function () {
        $('#send_button').click(function () {
            // Call the Send method on the hub.
            chat.server.send($('#input_field').val());
            // Clear text box and reset focus for next comment.
            $('#input_field').val('').focus();
        });

        $('#input_field').keypress(function (e) {
            if (e.which == 13) {
                // Call the Send method on the hub.
                chat.server.send($('#input_field').val());
                // Clear text box and reset focus for next comment.
                $('#input_field').val('').focus();
                e.preventDefault();
            }
        });
    });

})();

