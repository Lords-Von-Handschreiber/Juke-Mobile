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
	} else if ($('#info-page').length > 0) {
        $.ajax({
            url: "/api/Playlist"
        }).done(function (o) {
            if (o) {
                $('#trackCount').html(o['aaData'].length);
				if (o['aaData'].length > 0) {
					$('#nextTitle').html(o['aaData'][0]['Title']);
					$('#nextArtist').html(o['aaData'][0]['Artist']);
				}
            }
        });
    } else if ($('#list-page').length > 0) {
        $('#playlist').dataTable({
            "sPaginationType": "bootstrap",
            "sAjaxSource": "/api/Playlist",
            "aoColumns": [
                { "mData": "Artist" },
                { "mData": "Title" },
                { "mData": "Juker" }
            ],
            "bSort": false,
            "oLanguage": {
                "sLengthMenu": "_MENU_ Einträge pro Seite"
            }
        });
        $.ajax({
            url: "/api/CurrentTrack"
        }).done(function (o) {
            if (o) {
                $('#current-artist').html(o['Artist']);
                $('#current-track').html(o['Title']);
                $('#current-juker').html(o['Username']);
            }
        });
    } else if (!IsUserNameSet()) {
        window.location.href = 'index.html';
    } else if ($('#add-page').length > 0) {
        $('#myTab a:first').tab('show');
		$('#musicinfolist').dataTable({
            "sPaginationType": "bootstrap",
            "sAjaxSource": "/api/MusicInfo",
            "aoColumns": [   					
                { "mData": "Artist" },
                { "mData": "Title" },
				{ "mData": "Id",
				"mRender": function ( data, type, full ) {
					return '<button class="btn" type="button" onclick="vote(\''+data+'\')"><i class="icon-plus"></i></button>';
				}}
            ],
            "bSort": false,
        });
		$('#musicinfolist button').click(function() {
			alert('success');
			var id =  $(this).find("input").value;
			dataob = new Object()
			dataob.idMusicInfo = id;
			dataob.userName = GetUserName();
			$.ajax({
			type: "POST",
			url: "api/MusicInfo/Vote",			
			data: dataob,
			});
		});
    } else if ($('#history-page').length > 0) {
        $('#historylist').dataTable({
            "sPaginationType": "bootstrap",
            "sAjaxSource": "/api/Historylist",
            "aoColumns": [
                { "mData": "Zeit" },
                { "mData": "Artist" },
                { "mData": "Title" },
                { "mData": "Juker" }
            ],
            "bSort": false,
            "oLanguage": {
                "sLengthMenu": "_MENU_ Einträge pro Seite"
            }
        });
    }
});
