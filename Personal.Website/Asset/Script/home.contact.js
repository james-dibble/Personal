function OnSend() {
    var button = $('button[type="submit"]');

    button.button('sending');
}

function OnComplete() {
    var button = $('button[type="submit"]');

    button.button('reset');
}

function OnSent() {
    var alert = $('.alert-info');

    peekAlert(alert);

    $('form').trigger('reset');
}

function OnFailure() {
    var alert = $('.alert-danger');

    peekAlert(alert);
}

function peekAlert(alert) {

    alert.removeClass('hidden');
    alert.fadeIn(250);

    window.setTimeout(function () { alert.fadeOut(500); }, 5000);
}

$(function () {
    $('.animate').hide();
    $('#loading').toggle(
               {
                   effect: 'slide',
                   duration: 250,
                   direction: 'left',
                   complete: function () {
                       $('.animate').showSquare(0, function () { });
                   }
               });
});