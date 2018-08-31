var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
var CLIENTID = '24516187391-0uvvsgqgutr1ang3flleo3esu45feu13.apps.googleusercontent.com';
var REDIRECT = 'http://localhost:49977/';
var LOGOUT = 'http://localhost:49977/';
var TYPE = 'token';
var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
var acToken;
var tokenType;
var expiresIn;
var user;
var loggedIn = false;
function login() {

    var win = window.open(_url, "windowname1", 'width=800, height=600');
    var pollTimer = window.setInterval(function () {
        try {
            if (win.document.URL.indexOf(REDIRECT) != -1) {
                window.clearInterval(pollTimer);
                var url = win.document.URL;
                acToken = gup(url, 'access_token');
                tokenType = gup(url, 'token_type');
                expiresIn = gup(url, 'expires_in');

                win.close();
                debugger;
                validateToken(acToken);
            }
        }
        catch (e) {
        }
    }, 500);
}

function gup(url, name) {
    namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\#&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url);
    if (results == null)
        return "";
    else
        return results[1];
}

function validateToken(token) {
    getUserInfo();
    $.ajax(
        {
            url: VALIDURL + token,
            data: null,
            success: function (responseText) {
            },

        });

}

function preencher(user) {
    $('#campo-nome').val(user.name);
    $('#campo-email').val(user.email);
    console.log(user.name);
    console.log(user.email);
    alert("teste")
}

function getUserInfo() {


    $.ajax({

        url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
        data: null,
        success: function (resp) {
            preencher(resp);

        },


    });




}

