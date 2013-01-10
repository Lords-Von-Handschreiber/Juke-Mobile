var lsUsernameKey = 'juke-mobile.juker';

function IsNullOrEmpty(thing) {
    if (!thing)
        return true;
    if (thing == '')
        return true;
    return false;
}

function IsUserNameSet() {
    return !IsNullOrEmpty(GetUserName());
}

function GetUserName() {
    var userName = localStorage.getItem(lsUsernameKey);
    if (userName)
        return userName;
    else
        return sessionStorage.getItem(lsUsernameKey);
}

$(document).ready(function () {
    if (!Modernizr.localstorage) {
        alert('Leider unterstützt den Browser den Localstorage nicht');
        return;
    }

    if ($('#signin-page').length > 0) {
        if (IsUserNameSet()) {
            window.location.href = 'list.html';
        }

        $('.form-signin').submit(function () {
            var userName = $('#username').val();
            if (!IsNullOrEmpty(userName) && userName.length >= 3) {
                $('#usernameWarning').hide();
                if ($('#remember').attr('checked'))
                    localStorage.setItem(lsUsernameKey, userName);
                else
                    sessionStorage.setItem(lsUsernameKey, userName);

                window.location.href = 'list.html';
            } else {
                $('#usernameWarning').show();
            }
            return false;
        });
    } else if ($('#error-page').length > 0) {
        //ignore not logged in
    } else if ($('#list-page').length > 0) {
        $.ajax({
            url: "/api/CurrentTrack"
        }).done(function (o) {
            $('#current-artist').html(o['Artist'] + ' (' + o['Album'] + ')');
            $('#current-track').html(o['Title']);
            $('#current-juker').html(o['Artist']);
        });
    } else if (!IsUserNameSet()) {
        window.location.href = 'index.html';
    } else if ($('#add-page').length > 0) {
        $('#myTab a:first').tab('show');
    }
});
