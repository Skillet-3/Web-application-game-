(function () {

    onConnectionStartDone = [];

    var startFactory = function(done){
        $.connection.hub.stop();
        
        if (typeof done === "function") {
            if (onConnectionStartDone.indexOf(done) == -1) {
                onConnectionStartDone.push(done);
            }
        }
        $.connection.hub.start().done(function () {
            for (var i = 0; i < onConnectionStartDone.length; i++) {
                onConnectionStartDone[i]();
            }
        });
    }

    connectionFactory = {
        start: startFactory
    };
})()