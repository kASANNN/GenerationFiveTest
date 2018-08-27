var menu = {
    init: function () {
        $('#retirer').on('click', menu.menuretirer);
        $('#consulter').on('click', menu.menuconsulter);
        $('.Exit').on('click', function () {
            resourceCall('ExitATM');
        });
    },
    menumain: function () {
        $('#main').empty();
        var h1 = $('<h1 id="h1menumain">').text('Choisissez une opération :');
        var buttonretirer = $('<button id="retirer" type="button" name="button">').text('retirer');
        var buttonconsulter = $('<button id="consulter" type="button" name="button">').text('consulter');
        $('#main').append(h1).append(buttonretirer).append(buttonconsulter);
        menu.init();
    },
    menuretirer: function () {
        $('#main').empty();

        $('#main').append($('<h1 id="h1retirer">').text('Choisissez un montant :'));
        $('#main').append($('<div id="flexmain">'));
        $('#main').append($('<button id="menubtn" type="button" name="menubtn">').text('Menu'));
        $('#flexmain').append($('<div id="leftretirer">'));
        $('#flexmain').append($('<div id="rightretirer">'));

        var leftretirer = [
            $('<button id="retirer20">').html('<i class="fa fa-arrow-right" aria-hidden="true"></i>20 <i class="fa fa-usd" aria-hidden="true"></i>'),
            $('<button id="retirer40">').html('<i class="fa fa-arrow-right" aria-hidden="true"></i>40 <i class="fa fa-usd" aria-hidden="true"></i>'),
            $('<button id="retirer60">').html('<i class="fa fa-arrow-right" aria-hidden="true"></i>60 <i class="fa fa-usd" aria-hidden="true"></i>')
        ];
        var rightretirer = [
            $('<button id="retirer100">').html('100 <i class="fa fa-usd" aria-hidden="true"></i><i class="fa fa-arrow-left" aria-hidden="true"></i>'),
            $('<button id="retirer500">').html('500 <i class="fa fa-usd" aria-hidden="true"></i><i class="fa fa-arrow-left" aria-hidden="true"></i>'),
            $('<button id="retirerautre">').html('Autre <i class="fa fa-arrow-left" aria-hidden="true"></i>')
        ];

        menu.appendArray('#leftretirer', leftretirer);
        menu.appendArray('#rightretirer', rightretirer);

        var tablobtn = ['#menubtn', '#retirer20', '#retirer40', '#retirer60', '#retirer100', '#retirer500', '#retirerautre'];
        var tablobtn2 = [menu.menumain, interactionserver.retirer20, interactionserver.retirer40, interactionserver.retirer60, interactionserver.retirer100, interactionserver.retirer500, menu.menuretirerautre];
        for (var i = 0; i < tablobtn.length; i++) {
            $(tablobtn[i]).on('click', tablobtn2[i]);
        }
        $('#menubtn').on('click', menu.menumain);
    },
    appendArray: function (parent, array) {
        for (var children in array) {
            $(parent).append(array[children]);
        }
    },
    menuretirerautre: function () {
        $('#main').empty();

        var menuautre = [
            $('<h1 id="h1autreretirer">').text('Indiquez le montant :'),
            $('<form id="formretirer">')
        ];

        var childrenForm = [
            $('<input id="numberautre" type="number" name="" value="">'),
            $('<button id="validerautreretirer" type="button" name="button">').text('Valider').on('click', interactionserver.retirerautre),
            $('<button id="annulerautre" type="button" name="annulerbtn">').text('Annuler')
        ];

        menu.appendArray('#main', menuautre);
        menu.appendArray('#formretirer', childrenForm);
        $('#annulerautre').on('click', menu.menuretirer);
    },
    menuconsulter: function () {
        $('#main').empty();
        var menuconsulter = [
            $('<div id="divconsulter">'),
            $('<nav>')
        ];
        menu.appendArray('#main', menuconsulter);

        var divconsulter = [
            $('<h1 id="h1menuconsulter">').text('Solde de votre compte :'),
            $('<p id="soldeDuCompte">').text(sendConsulterATM() + '$')
        ];
        menu.appendArray('#divconsulter', divconsulter);
        sendConsulterATM();

        var nav = [
            $('<button type="button" name="menubtn">').text('Menu').on('click', menu.menumain),
            $('<button type="button" name="menubtn">').text('Retirer').on('click', menu.menuretirer)
        ];
        menu.appendArray('nav', nav);
    }
};

var interactionserver = {
    retirer20: function () {
        resourceCall('retirerATM', 20);
    },
    retirer40: function () {
        resourceCall('retirerATM', 40);
    },
    retirer60: function () {
        resourceCall('retirerATM', 60);
    },
    retirer100: function () {
        resourceCall('retirerATM', 100);
    },
    retirer500: function () {
        resourceCall('retirerATM', 500);
    },
    retirerautre: function () {
        resourceCall('retirerATM', parseInt($('#numberautre').val()));
    },
    soldeConsulter: function (solde) {
        $('#soldeDuCompte').html(solde + ' &dollar;');
    }
};

menu.init();


function receiveConsulterATM(montant) {
    $('#soldeDuCompte').text(montant);
}

function sendConsulterATM() {
    return resourceCall('consulterATM');
}
