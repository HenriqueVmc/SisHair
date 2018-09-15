$(function () {
    $("table").on("click", ".btnSolicitar", function () {
        //Id do Funcionario
        debugger;
        $id = $(this).data("id");

        if ($("#modal-agendamento").length == 0) {
            $.ajax({
                url: "/Solicitacao/AgendamentoModal?id=" + $id,
                method: "GET",
                success: function (data) {
                    $("body").append(data);
                    $("#modal-agendamento").modal('show');
                },
                error: function () {
                    $("#modal-agendamento").modal('hide');
                    new PNotify({
                        title: 'Erro:',
                        text: 'Não foi possível realizar a solicitação',
                        type: 'error'
                    });
                }
            });

        } else {
            $("#modal-agendamento").modal('show');
            limparCampos();
        }
    });

    $('#dtDataHoraInicio2, #dtDataHoraFinal2').datetimepicker({
        format: 'DD/MM/YYYY HH:mm A',
        locale: 'pt-br',
        defaultDate: new Date(),
        minDate: new Date()
    });

    $("#modal-agendamento-salvar").on("click", function () {
        debugger;
        if (validarCampos() == 0) {
            return;
        }

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
            },
            error: function () {
                new PNotify({
                    title: 'Erro:',
                    text: 'Não foi possível realizar a solicitação',
                    type: 'error'
                });
            }
        });
    });

    function limparCampos() {
        $("#DataHoraInicio").val("");
        $("#DataHoraFinal").val("");
        $("#Descricao").val("");
        $("#selectServicos").val("").change();
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

    $('#modal-agendamento').on('hidden', function () {
        $(this).data('modal', null);
    });

    function validarCampos() {
        debugger;
        if ($('#DataHoraInicio').val().trim() == "") {
            new PNotify('Horário de inicio deve ser preenchido!');
            return 0;
        }
        if ($('#DataHoraFinal').val().trim() == "") {
            new PNotify('Horário de finalização deve ser preenchido!');
            return 0;
        }

        if ($('#selectServicos').val() == "") {
            new PNotify('Selecione um Serviço!');
            return 0;
        }
        //else {

        var hrInicio = moment($('#DataHoraInicio').val(), "DD/MM/YYYY HH:mm A").toDate();
        var hrFinal = moment($('#DataHoraFinal').val(), "DD/MM/YYYY HH:mm A").toDate();
        if (hrInicio >= hrFinal) {
            new PNotify({
                title: 'Horários Inválidos',
                text: 'Data e Horário de finalização deve ser maior que inicial...'
            });
            return 0;
        }
    }
});
