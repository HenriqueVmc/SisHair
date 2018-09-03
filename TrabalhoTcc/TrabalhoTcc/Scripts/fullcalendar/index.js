$(document).ready(function () {
    var agendamentos = [];
    var agendamentoSelecionado = null;
    UpdateAgenda();
    function UpdateAgenda() {
        agendamentos = [];
        $.ajax({
            type: "GET",
            url: "/Agendamentos/GetAgendamentos",
            success: function (data) {
                $.each(data, function (i, a) {
                    agendamentos.push({
                        agendamentoId: a.Id,
                        start: moment(a.DataHoraInicio),
                        end: a.DataHoraFinal != null ? moment(a.DataHoraFinal) : null,
                        funcionarioId: a.FuncionarioId,
                        clienteId: a.ClienteId,
                        situacao: a.Situacao,
                        cliente: a.Cliente,
                        funcionario: a.Funcionario,
                        title: "Cliente: " + a.Cliente + " - Situação: " + a.Situacao
                    });
                })

                GerarAgenda(agendamentos);
            },
            error: function (error) {
                alert('failed');
            }
        })
    }

    function GerarAgenda(agendamentos) {
        $('#agenda').fullCalendar('destroy');
        $('#agenda').fullCalendar({

            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay,agenda'
            },
            defaultView: 'agenda',
            allDaySlot: false,
            eventLimit: true,
            
            eventTextColor: 'white',
            eventColor: '#404040',
            events: agendamentos,
            eventClick: function (Agendamento, jsEvent, view) {
                agendamentoSelecionado = Agendamento;
                $('#agendamento-modal #eventTitle').text(Agendamento.cliente);

                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Horario Inicial: </b>' + Agendamento.start.format("DD/MMM/YYYY HH:mm a")));
                if (Agendamento.end != null) {
                    $description.append($('<p/>').html('<b>Horario Final: </b>' + Agendamento.end.format("DD/MMM/YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Cliente: </b>' + Agendamento.cliente));
                $description.append($('<p/>').html('<b>Funcionário: </b>' + Agendamento.funcionario));
                $description.append($('<p/>').html('<b>Situação: </b>' + Agendamento.situacao));
                $('#agendamento-modal #pDetails').empty().html($description);

                $('#agendamento-modal').modal();
            },
            selectable: true,
            select: function (start, end) {
                agendamentoSelecionado = {
                    agendamentoId: 0,
                    start: start,
                    end: end,
                    situacao: ''
                };
                frmEditarAgendamento();
                $('#agenda').fullCalendar('unselect');
            },
            editable: true,
            eventDrop: function (agendamento) {
                var data = {
                    Id: agendamento.agendamentoId,
                    DataHoraInicio: agendamento.start.format('DD/MM/YYYY HH:mm A'),
                    DataHoraFinal: agendamento.end != null ? agendamento.end.format('DD/MM/YYYY HH:mm A') : null,
                    Situacao: agendamento.situacao,
                    FuncionarioId: agendamento.funcionarioId,
                    ClienteId: agendamento.clienteId
                };
                SalvarAgendamento(data);
            },
            eventResize: function (agendamento) {
                var data = {
                    Id: agendamento.agendamentoId,
                    DataHoraInicio: agendamento.start.format('DD/MM/YYYY HH:mm A'),
                    DataHoraFinal: agendamento.end != null ? agendamento.end.format('DD/MM/YYYY HH:mm A') : null,
                    Situacao: agendamento.situacao,
                    FuncionarioId: agendamento.funcionarioId,
                    ClienteId: agendamento.clienteId
                };
                SalvarAgendamento(data);
            }
        })
    }

    $('#btnEditar').click(function () {
        //Open modal dialog for edit event
        frmEditarAgendamento();
    })
    $('#btnDeletar').click(function () {
        if (agendamentoSelecionado != null && confirm('Are you sure?')) {
            $.ajax({
                type: "POST",
                url: '/Agendamentos/Deletar',
                data: { 'Id': agendamentoSelecionado.agendamentoId },
                success: function (data) {
                    if (data.status) {
                        //Refresh the calender
                        UpdateAgenda();
                        $('#agendamento-modal').modal('hide');
                    }
                },
                error: function () {
                    alert('Failed');
                }
            })
        }
    })

    $('#dtDataHoraInicio, #dtDataHoraFinal').datetimepicker({
        format: 'DD/MM/YYYY HH:mm A'
    });

    function frmEditarAgendamento() {
        if (agendamentoSelecionado != null) {

            $('#Id').val(agendamentoSelecionado.agendamentoId);
            $('#DataHoraInicio').val(agendamentoSelecionado.start.format('DD/MM/YYYY HH:mm A'));
            $('#DataHoraFinal').val(agendamentoSelecionado.end != null ? agendamentoSelecionado.end.format('DD/MM/YYYY HH:mm A') : '');
            $('#Situacao').val(agendamentoSelecionado.situacao);
            $('#FuncionarioId').val(agendamentoSelecionado.funcionarioId);
            $('#ClienteId').val(agendamentoSelecionado.clienteId);
        }
        $('#agendamento-modal').modal('hide');
        $('#agendamento-modal-salvar').modal();
    }

    $('#btnSalvarAgendamento').click(function () {
        debugger;
        //Validation/
        //if ($('#DataHoraInicio').val().trim() == "") {
        //    alert('start date required');
        //    return;
        //}
        //if ($('#DataHoraFinal').val().trim() == "") {
        //    alert('End date required');
        //    return;
        //}
        //else {
        var hrInicio = moment($('#DataHoraInicio').val(), "DD/MM/YYYY HH:mm A").toDate();
        var hrFinal = moment($('#DataHoraFinal').val(), "DD/MM/YYYY HH:mm A").toDate();
        if (hrInicio > hrFinal) {
            alert('Invalid end date');
            return;
        }
        //}

        var data = {
            Id: $('#Id').val(),
            DataHoraInicio: $('#DataHoraInicio').val().trim(),
            DataHoraFinal: $('#DataHoraFinal').val().trim(),
            Situacao: $('#Situacao').val(),
            FuncionarioId: $('#FuncionarioId').val(),
            ClienteId: $('#ClienteId').val()
        }
        SalvarAgendamento(data);
        // call function for submit data to the server
    })

    function SalvarAgendamento(data) {
        debugger;
        $.ajax({
            type: "POST",
            url: '/Agendamentos/Salvar',
            data: data,
            success: function (data) {
                UpdateAgenda();
                $('#agendamento-modal-salvar').modal('hide');
            },
            error: function () {
                alert('Failed');
            }
        })
    }
})