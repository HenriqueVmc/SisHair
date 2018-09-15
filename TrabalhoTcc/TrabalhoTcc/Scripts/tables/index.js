$(function () {
    $("#tableCliente").pagination();
    $("#tableFuncionarios").pagination();
    $("#tableServicos").pagination();
    $("#tableCargos").pagination();
    $("#tableCaixa").pagination();

    $("#pesquisa").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("table tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#tableCliente").on("dblclick", "tr", function () {
        var id = $(this).find("#Id").val();
        window.location.replace("/Cliente/Detalhes?id=" + id);        
    });

    $("#tableFuncionarios").on("dblclick", "tr", function () {
        var id = $(this).find("#Id").val();
        window.location.replace("/Funcionarios/Detalhes?id=" + id);
    });

    $("#tableServicos").on("dblclick", "tr", function () {
        var id = $(this).find("#Id").val();
        window.location.replace("/Servicos/Detalhes?id=" + id);
    });

    $("#tableServicos").on("dblclick", "tr", function () {
        var id = $(this).find("#Id").val();
        window.location.replace("/Servicos/Detalhes?id=" + id);
    });
});