(function () {
    DrawHPBar = function (duration) {

        var hp = $(".wrapper_hp").each(function (i, obj) {
            var data = obj.dataset;
            var hpP = (data.lvlHp / data.lvlHpFull) * 100;
            duration = duration || 2000;
            $(function () {
                $(function () {
                    $(obj).find(".progress_hp").animate({
                        width: hpP + "%"
                    }, {
                        duration: duration,
                        step: function (now) {
                            $(obj).parent().find(".text_hp").text(Math.round(now) + '%');
                        }
                    });
                    return false;
                });
            });
        })
    }

    DrawMPBar = function () {
        var mp = document.querySelector('#mp'),
            data = mp.dataset;
        var mpP = (data.lvlMp / data.lvlMpFull) * 100;

        $(function () {
            $(function () {
                $(".progress_mp").animate({
                    width: mpP + "%"
                }, {
                    duration: 2000,
                    step: function (now) {
                        $(".text_mp").text(Math.round(now) + '%');
                    }
                });
                return false;
            });
        });
    }

    DrawHpAndManaBar = function DrawHpAndManaBar(){
        DrawHPBar();
        DrawMPBar();
    }

    SetMaxHP = function ChangeHPState(value, name) {
        var hp = $("#hp_" + name).each(function (i, obj) {
            var data = obj.dataset;
            data.lvlHpFull = value
        });
    }

    ChangeHPState = function ChangeHPState(value, name) {
        var hp = $("#hp_" + name).each(function (i, obj) {
            var data = obj.dataset;
            data.lvlHp = data.lvlHp / 1 + value
        });
        DrawHPBar(500);
    }

})()