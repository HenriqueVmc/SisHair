$(function () {
    $("#btnSalvar").on("click", function () {

        var dados = $("#frmAlterarLogin").serialize();
        $.ajax({
            url: "/ContaFuncionario/AlterarLogin",
            method: "POST",
            data: dados,
            success: function (data) {
                alert("Usuário Alterado!");   
                window.location.replace("/ContaFuncionario/Login");
            }
        });
    });

    $("#CpfLogin").mask('000.000.000-00', { reverse: true });

    $("#email").on("focusout", function () {
        var sEmail = $('#email').val();
        if ($.trim(sEmail).length == 0) {
            new PNotify("Informe seu Email");
        }
        if (!isEmail(sEmail)) {
            $('#emailValidation').text("Email Inválido!");
            $('#emailValidation').css('color', 'red');
            $('#email').focus();
            return;
        } else{
            $('#emailValidation').text("");
        }
    });

    function isEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }
    
});
