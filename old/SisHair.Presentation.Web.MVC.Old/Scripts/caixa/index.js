$(function () {

    formatarCampos();

    $("#ValorPago").on("focusout", function () {

        if ($("#ValorTotal").val() == "") {
            new PNotify('Informe o Valor Total!');
            return;
        }
        if ($("#ValorPago").val() == "") {
            new PNotify('Informe o Valor Pago!');
            return;
        }

        calcularValores();
    });

    $("#btnPagar").on("click", function () {
        if ($("#ValorTotal").val() == "") {
            new PNotify('Informe o Valor Total!');
            return;
        }
        if ($("#ValorPago").val() == "") {
            new PNotify('Informe o Valor Pago!');
            return;
        }
        if ($("#DataPagamento").val() == "") {
            new PNotify('Informe a Data de Pagamento!');
            return;
        }
        if ($("#FormaPagametno").val() == "") {
            new PNotify('Informe a Forma de Pagamento!');
            return;
        }
        
        //--- VALIDAR DATA DE PAGAMENTO
        //var dtPagamento = $("#DataPagamento").val();
        //var dtAgendamento = $("#Agendamento.DataHoraFinal").val();

        //if ((new Date(dtPagamento).getDate() < new Date(dtAgendamento).getDate())) {
        //    new PNotify('Data de Pagamento deve ser maior que Data de Agendamento');
        //    return;
        //}
    });


    function formatarCampos() {

        $('#Status').select2();
        $('#FormaPagamento').select2();

       /* $("#ValorTotal").rules("remove", "number");
        $("#Divida").rules("remove", "number");
        $("#ValorPago").rules("remove", "number");
        $("#Troco").rules("remove", "number");*/

        $("#ValorTotal").mask("#.##0,00", { reverse: true });
        $("#Divida").mask("#.##0,00", { reverse: true });
        $("#ValorPago").mask("#.##0,00", { reverse: true });
        $("#Troco").mask("#.##0,00", { reverse: true });

        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);

        $("#DataPagamento").val(today);
    }

    function calcularValores() {
        var valPago = parseFloat($("#ValorPago").val().replace(',', '.'));
        var valTotal = parseFloat($("#ValorTotal").val().replace(',', '.'));

        var result = valTotal - valPago;

        if (result > 0) {
            $("#Divida").val(result.toFixed(2).toString().replace('.', ','));
            $("#Troco").val(0);
            $("#Status").val("Pendente").change();
        } else {
            $("#Divida").val(0);
            $("#Troco").val((result * -1).toFixed(2).toString().replace('.', ','));
            $("#Status").val("Pago").change();
        }
    }

});