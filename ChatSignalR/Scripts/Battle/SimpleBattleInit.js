/// <reference path="SimpleBattleInit.js" />
(function () {
    userBattleInit = function () {
        battleInit(true);
    }
    battleInit = function (non_bot) {
        var onResult = function onResult (result) {
            $.each(result.Log, function (index, data) {
                log(data.ActionName + " HP: " + data.Effects.HP, data.AffectedUser); // Имя пользователя на кого наложен эффект
                if (data.Effects.HP != undefined) {
                    ChangeHPState(data.Effects.HP, data.AffectedUser);
                }
            });
        };

        window.DrawHpAndManaBar();
        var battleWidget = $("#battle_widget");
        if (battleWidget.length > 0) {
            var battle = new Battle();
            battle.init({
                onRegistered: function () {
                    log("Бой начался!!!");
                },
                onFinish: function (result, reward) {
                    onResult(result);
                    log("Бой окончен!!!");
                    if (reward.Win) {
                        log("Вы победили");
                    }
                    else {
                        log("Вы проиграли");
                    }
                },
                onResult: onResult,
                onUserData: function (u1, u2) {
                    if ($("#" + u1.UserName).length > 0) {
                        $("#enemy").attr("id", u2.UserName)
                        $("#hp_").attr("id", "hp_" + u2.UserName)
                        ChangeHPState(u2.Hp, u2.UserName)
                        SetMaxHP(u2.Hp_Full, u2.UserName)
                    } else {
                        $("#enemy").attr("id", u1.UserName)
                        $("#hp_").attr("id", "hp_" + u1.UserName)
                        SetMaxHP(u1.Hp_Full, u1.UserName)
                        ChangeHPState(u1.Hp, u1.UserName)
                    }
                    DrawHpAndManaBar();
                }
            });
            battle.start(non_bot);

            $("#battle_send").click(function () {
                battle.sendMessage(determineAtackType(), determineDefenceType());
            })
            window.GlobalBattleWidget = battle;
        }
    }

    function determineAtackType() {
        return $("#atack_bar > input:checked").data("type");
    }
    function determineDefenceType() {
        return $("#defence_bar > input:checked").data("type");
    }

    function log(message, name) {
        if (!!name) {
            $('#battle_log').prepend('<div class = "message"><a id="name_' + name + '">' + name + '</a>: ' + message + "</div>")
        } else {
            $('#battle_log').prepend    ('<div class = "message">' + message + "</div>")
        }
    }
})();