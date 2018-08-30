$(function () {
    $("#btnSalvar").on("click", function () {

        var dados = $("#frmAlterarLogin").serialize();
        $.ajax({
            url: "/ContaFuncionario/AlterarLogin",
            method: "POST",
            data: dados,
            success: function (data) {
                alert("Usuário Alterado!")                 
            }
        });

    });
});
