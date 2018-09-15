$(function () {
    $("#tableCliente").pagination();
    $("#tableFuncionarios").pagination();
    $("#tableServicos").pagination();
    $("#tableCargos").pagination();

    $("#pesquisa").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("table tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

});