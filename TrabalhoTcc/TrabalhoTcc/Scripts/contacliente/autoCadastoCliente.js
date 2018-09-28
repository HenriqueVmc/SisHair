$(function () {

    var nome = $('#Cliente_Nome');
    var nomeV = $('#nomeValidation');

    var email = $('#Cliente_Email');
    var emailV = $('#emailValidation');

    var celular = $("#Cliente_Celular");
    var celularV = $('#celularValidation');

    var telefone = $("#Cliente_Telefone");
    var telefoneV = $('#telefoneValidation');

    var dtNascimento = $("#Cliente_Data_nascimento");
    var dtNascimentoV = $('#dataNascimentoValidation');

    var usuario = $("#Usuario");
    var usuarioV = $('#usuarioValidation');

    var senha = $("#Senha");
    var senhaV = $('#senhaValidation');

    var confSenha = $("#ConfirmarSenha");
    var confSenhaV = $('#confSenhaValidation');

    celular.mask('(00) 00000-0000');
    telefone.mask('(00) 0000-0000');

    nome.focusout(function () {
        validaCamposObrigatorios(nome, nomeV);
    });

    telefone.focusout(function () {
        validaCamposObrigatorios(telefone, telefoneV);
        if (telefone.val().length < 14) {
            telefoneV.text("Telefone inválido");
            telefoneV.css("color", "red");
            return;
        } else{
            telefoneV.text("");
        }
    });

    celular.focusout(function () {
        validaCamposObrigatorios(celular, celularV);
        if (celular.val().length < 15) {
            celularV.text("Celular inválido");
            celularV.css("color", "red");
            return;
        } else {
            celularV.text("");
        }
    });

    email.focusout(function () {
        validaCamposObrigatorios(email, emailV);    
    });

    dtNascimento.focusout(function () {
        validaCamposObrigatorios(dtNascimento, dtNascimentoV);
    });

    usuario.focusout(function () {
        validaCamposObrigatorios(usuario, usuarioV);
        if (usuario.val().length < 4) {
            usuarioV.text("Usuario deve possuir pelo menos 4 caracteres");
            usuarioV.css("color", "red");
            return;
        } else {
            usuarioV.text("");
        }
    });

    senha.focusout(function () {
        validaCamposObrigatorios(senha, senhaV);

        if (senha.val().length < 4) {
            senhaV.text("Senha deve possuir pelo menos 4 caracteres");
            senhaV.css("color", "red");
            return;
        } else {
            senhaV.text("");
        }
    });

    confSenha.focusout(function () {
        validaCamposObrigatorios(confSenha, confSenhaV);

        if (senha.val() != confSenha.val()) {
            senha.focus();
            confSenha.val("");
            confSenhaV.text("Senhas não conferem");
            confSenhaV.css('color', 'red');
            return;
        } else {
            confSenhaV.text("");
        }
    });

    function validaCamposObrigatorios(input, validacao) {

        if (input.val() == "") {
            validacao.text("Campo obrigatório!")
            validacao.css('color', 'red');
            return;
        } else {
            validacao.text("");
        }
    }

});

