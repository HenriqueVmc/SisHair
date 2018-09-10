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

    $("body").on("click",'.star',function () {
        $("#NotaVoltarNovamente").val($(this).val());
    });

    $("body").on("click", '.star2', function () {
        $("#NotaAgendamento").val($(this).val());
    });

    $("body").on("click", '.star3', function () {
        $("#NotaExperienciaAtendimento").val($(this).val());
    });

    $("body").on("click", '.star4', function () {
        $("#NotaCondicoesFisicasEstabelecimento").val($(this).val());
    });

    $("body").on("click", '.btnVoltariaNovamente', function () {
        debugger;
        $("#VoltariaNovamente").val($(this).val());
    });

    $("body").on("click", '.btnRecomendariaAgendamento', function () {
        debugger;
        $("#RecomendariaAlguem").val($(this).val());
    });


    $("body").on("click", "#modal-avaliar-salvar", function () {
        debugger;
        $.ajax({
            url: "/Solicitacao/SalvarAvaliacao",
            method: "post",
            data: {
                agendamentoId: $("#AgendamentoId").val(),
                avaliacaoUsuario: $("#AvaliacaoUsuario").val(),
                notaVoltarNovamente: $("#NotaVoltarNovamente").val(),
                notaAgendamento: $("#NotaAgendamento").val(),
                notaExperienciaAtendimento: $("#NotaExperienciaAtendimento").val(),
                notaCondicoesFisicasEstabelecimento: $("#NotaCondicoesFisicasEstabelecimento").val(),
                voltariaNovamente: $("#VoltariaNovamente").val(),
                recomendariaAlguem: $("#RecomendariaAlguem").val()

            },
            success: function (data) {
                var resultado = JSON.parse(data);
                limparCampos();
                $("#modal-avaliacao").modal('hide');
                //alert("Avaliacao Realizada!");
                new PNotify({text:'Avaliação realizada com sucesso', type:"success"});                
            }
        });
    });

    function limparCampos() {
        $("#AvaliacaoUsuario").val("");
        $("#NotaVoltarNovamente").val("");
        $("#NotaAgendamento").val("");
        $("#NotaExperienciaAtendimento").val("");
        $("#NotaCondicoesFisicasEstabelecimento").val("");                    
    }
});
