$(function () {

    $("#Cpf").mask('000.000.000-00', { reverse: true });
    $("#Celular").mask('(00) 00000-0000');
    $("#Telefone").mask('(00) 0000-0000');    

    $("#PermissoesId").select2({
        width: "100%",        
        ajax: {
            url: "/ContaFuncionario/GetPermissoes",
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



