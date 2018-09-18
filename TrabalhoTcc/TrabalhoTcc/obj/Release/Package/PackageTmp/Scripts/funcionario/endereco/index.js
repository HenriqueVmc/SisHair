$(function () {

    $("[name='Endereco.Cep']").mask('00000-000'); 
    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("[name='Endereco.Rua']").val("");
        $("[name='Endereco.Bairro']").val("");
        $("[name='Endereco.Cidade']").val("");
        $("[name='Endereco.Estado']").val("");
    }

    //Quando o campo cep perde o foco.
    $("[name='Endereco.Cep']").blur(function () {

        //Nova variável "cep" somente com dígitos.
        var cep = $(this).val().replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep != "") {

            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {
                //'[name="ElementNameHere"]
                //Preenche os campos com "..." enquanto consulta webservice.
                $("[name='Endereco.Rua']").val("...");
                $("[name='Endereco.Bairro']").val("...");
                $("[name='Endereco.Cidade']").val("...");
                $("[name='Endereco.Estado']").val("...");

                //Consulta o webservice viacep.com.br/
                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        //Atualiza os campos com os valores da consulta.
                        $("[name='Endereco.Rua']").val(dados.logradouro);
                        $("[name='Endereco.Bairro']").val(dados.bairro);
                        $("[name='Endereco.Cidade']").val(dados.localidade);
                        $("[name='Endereco.Estado']").val(dados.uf);
                    } //end if.
                    else {
                        //CEP pesquisado não foi encontrado.
                        limpa_formulário_cep();
                        (new PNotify({
                            title: 'CEP não encontrado',
                            text: 'CEP digitado não foi encontrado, digite o CEP novamente',
                            type: 'error',
                            desktop: {
                                desktop: true
                            }
                        })).get().click(function (e) {
                            if ($('.ui-pnotify-closer, .ui-pnotify-sticker, .ui-pnotify-closer *, .ui-pnotify-sticker *').is(e.target)) return;
                            alert('Digite o CEP novamente');
                        });
                    }
                });
            } //end if.
            else {
                //cep é inválido.
                limpa_formulário_cep();
                (new PNotify({
                    title: 'CEP invalido',
                    text: 'O CEP informado esta escrito incorretamente. Por favor insira um CEP valido',
                    type: 'error',
                    desktop: {
                        desktop: true
                    }
                })).get().click(function (e) {
                    if ($('.ui-pnotify-closer, .ui-pnotify-sticker, .ui-pnotify-closer *, .ui-pnotify-sticker *').is(e.target)) return;
                    alert('Por favor insira um CEP valido!');
                });
            }
        } //end if.
        else {
            //cep sem valor, limpa formulário.
            limpa_formulário_cep();
        }
    });

    $("#btnCadFuncionario").on("click", function () {

        if ($("[name='Endereco.Numero']").val() == "") {
            $("[name='Endereco.Numero']").val("0")
        }    
    });
});


