$(document).ready(function () {

    $("#ValorPago").on("focusout", function () {

        if ($("#ValorTotal").val() == "") {
            new PNotify('Informe o Valor Total!');
            return;
        }
        var valPago = parseFloat($("#ValorPago").val());
        var valTotal = parseFloat($("#ValorTotal").val());
        var result = valTotal - valPago;
        if (result > 0) {
            $("#Divida").val(result.toString());
            $("#Troco").val(0);
        } else {
            $("#Divida").val(0);
            $("#Troco").val((result * -1).toString());
        }
    });

    $('#Status').select2();
    $('#FormaPagamento').select2();


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
    });
});