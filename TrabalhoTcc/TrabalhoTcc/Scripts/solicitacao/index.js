$(document).ready(function () {
    $("table").on("click", ".btnSolicitar", function () {
        //Id do Funcionario
        $id = $(this).data("id");

        $.ajax({
            url: "/Solicitacao/AgendamentoModal?id=" + $id,
            method: "GET",
            success: function (data) {
                $("body").append(data);
                $("#modal-agendamento").modal('show');
            }
        });

    });

    $('#dtDataHoraInicio2, #dtDataHoraFinal2').datetimepicker({
        format: 'DD/MM/YYYY HH:mm A',
        locale: 'pt-br',
        defaultDate: new Date(),
        minDate: new Date()
    });

    $("#modal-agendamento-salvar").on("click", function () { 
        $.ajax({
            url: "/Solicitacao/Agendamento",
            method: "post",
            data: {
                funcionarioId: $("#FuncionarioId").val(),
                clienteId: $("#ClienteId").val(),
                dataHoraInicio: $("#DataHoraInicio").val(),
                dataHoraFinal: $("#DataHoraFinal").val(),
                descricao: $("#Descricao").val(),
                servicos: $("#selectServicos").val()
            },
            success: function (data) {
                limparCampos();
                $("#modal-agendamento").modal('hide');
                new PNotify({
                    title: 'Solicitação Realizada!',
                    text: 'Pronto! Agora é só aguardar nosso retorno :)',
                    type: 'success'
                });                
            }
        });
    });

    function limparCampos() {
        $("#DataHoraInicio").val("");
        $("#DataHoraFinal").val("");
        $("#Descricao").val("");
        $("#selectServicos").val("");
    }

    $("#selectServicos").select2({
        width: "100%",
        ajax: {
            url: "/Servicos/GetServicos",
            dataType: 'json',
            type: "GET",
            data: function (params) {

                var queryParameters = {
                    term: params.term
                }
                return queryParameters;
            },
            processResults: function (data) {
                // Tranforms the top-level key of the response object from 'items' to 'results'
                return {
                    results: data.items
                };
            }
        }
    });
});
