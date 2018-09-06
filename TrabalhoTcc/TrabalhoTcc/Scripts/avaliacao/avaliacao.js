$(function () {
    $("table").on("click", ".btnRealizarAvaliacao", function () {
        //Id do Agendamento
        $id = $(this).data("id");

        if ($("#modal-avaliacao").length === 0) {
            $.ajax({
                url: "/Solicitacao/AvaliacaoModal?id=" + $id,
                method: "GET",
                success: function (data) {
                    $("body").append(data);
                    $("#modal-avaliacao").modal('show');
                }
            });
        } else {
            $("#modal-avaliacao").modal('show');
            limparCampos();
        }
    });


    $("body").on("click", "#modal-avaliar-salvar", function () {
        debugger;
        $.ajax({
            url: "/Solicitacao/SalvarAvaliacao",
            method: "post",
            data: {
                agendamentoId: $("#AgendamentoId").val(),
                avaliacaoUsuario: $("#AvaliacaoUsuario").val()
            },
            success: function (data) {
                var resultado = JSON.parse(data);
                limparCampos();
                $("#modal-avaliacao").modal('hide');
                alert("Avaliacao Realizada!");
            }
        });
    });

    function limparCampos() {
        $("#DataHoraInicio").val("");
        $("#DataHoraFinal").val("");
        $("#Descricao").val("");
    }
});
