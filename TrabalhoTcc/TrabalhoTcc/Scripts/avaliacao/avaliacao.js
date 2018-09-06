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

    $('.mdb-select').material_select();

    //$("body").on("click", "#modal-agendamento-salvar", function () {
    //    debugger;
    //    $.ajax({
    //        url: "/Solicitacao/Agendamento",
    //        method: "post",
    //        data: {
    //            funcionarioId: $("#FuncionarioId").val(),
    //            clienteId: $("#ClienteId").val(),
    //            dataHoraInicio: $("#DataHoraInicio").val(),
    //            dataHoraFinal: $("#DataHoraFinal").val(),
    //            descricao: $("#Descricao").val()
    //        },
    //        success: function (data) {
    //            var resultado = JSON.parse(data);
    //            limparCampos();
    //            $("#modal-agendamento").modal('hide');
    //            alert("Solicitação Realizada!");
    //        }
    //    });
    //});

    //function limparCampos() {
    //    $("#DataHoraInicio").val("");
    //    $("#DataHoraFinal").val("");
    //    $("#Descricao").val("");
    //}
});
