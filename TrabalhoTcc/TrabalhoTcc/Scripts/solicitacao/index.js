$(function () {
    $("table").on("click", ".btnSolicitar", function () {
        //Id do Funcionario
        $id = $(this).data("id");

        if ($("#modal-agendamento").length == 0) {
            $.ajax({
                url: "/Solicitacao/AgendamentoModal?id=" + $id,
                method: "GET",
                success: function (data) {
                    $("body").append(data);
                    $("#modal-agendamento").modal('show');
                }
            }); 
        } else {
            $("#modal-agendamento").modal('show');
            limparCampos();
        }
    });

    $("body").on("click", "#modal-agendamento-salvar", function () {
        $.ajax({
            url: "/Solicitacao/Agendamento",
            method: "POST",
            data: {
                funcionarioId: $("#FuncionarioId").val(),
                clienteId: $("#ClienteId").val(),
                data: $("#Data").val(),
                horaInicio: $("#HoraInicio").val(),
                descricao: $("#Descricao").val()
            },
            success: function (data) {
                var resultado = JSON.parse(data);
                limparCampos();
                $("#modal-agendamento").modal('hide');
                alert("Solicitação Realizada!");
            }
        });
    });

    function limparCampos() {
        $("#Data").val("");
        $("#HoraInicio").val("");
        $("#Descricao").val("");
    }
});
