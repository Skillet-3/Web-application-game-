(function () {
    Battle = function Battle() {
        this.battle = $.connection.battleHub;
        this.messages = null;
        this.battleMessage = "BLL.BattleEngine.Messages.SimpleExecutableMessage";
        this.standartMessage = "BLL.BattleEngine.Messages.ADExecutableMessage";
        this.battleId = null;
        this.battle.client.hello = function () { console.log("hello") };
        this.battle.client.helloAll = function () { console.log("hello all") };
    }

    Battle.prototype.init = function (options) {
        var self = this;

        self.battle.client.battleResult = function (result) {
            console.log("battleResult");
            console.log(result);
            if (typeof options.onResult === "function") {
                options.onResult(result);
            }
        }

        self.battle.client.battleFinished = function (result, reward) {
            console.log("battleFinished");
            console.log(result);
            console.log(reward);
            if (typeof options.onFinish === "function") {
                options.onFinish(result, reward);
            }
        }

        self.battle.client.battleRegistered = function (battleId) {
            console.log("battleRegistered");
            console.log(battleId);
            self.battleId = battleId;

            if (typeof options.onRegistered === "function") {
                options.onRegistered(battleId);
            }
        }

        if (typeof options.onUserData === "function")
            self.battle.client.sendData = options.onUserData;
    }

    Battle.prototype.start = function (non_bot) {
        var self = this;
        connectionFactory.start(function () {
            console.log("Done");

            self.battle.server.getMessages()
                .done(function (data) {
                    self.messages = JSON.parse(data);
                })
                .then(function () {
                    if (non_bot) {
                        self.battle.server.initBattle().fail(function (error) {
                            console.log('initBattle error: ' + error)
                        });
                    } else {
                        self.battle.server.initBotBattle().fail(function (error) {
                            console.log('initBotBattle error: ' + error)
                        });
                    }
                });
        });
    }

    Battle.prototype.sendMessage = function (atack, defence) {
        var self = this;
        var message = {
            "BattleId": self.battleId,
            "MessageKey": "key",
            "psx": self.messages[self.battleMessage]
        }
        if (!!atack && !!defence) {
            message.AtackPosition = atack;
            message.DefencePosition = defence;
            message.psx = self.messages[self.standartMessage]
        }

        self.battle.server.sendMessageBotBattle(JSON.stringify(message));
    }
})();