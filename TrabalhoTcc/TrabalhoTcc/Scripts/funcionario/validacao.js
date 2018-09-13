
//$(document).ready(function(){
//    $("#formCadastroFuncionario").validate({
//        rules: {
//            Cpf: {                
//                cpfBR: true
//            },
//            Nome: {
//                minwords: 2
//            }
//        }
//    })
//})
$(function(){

    $("#formCadastroFuncionario").validate({
        rules: {
            Nome: {
                required: true,
                maxlength: 100,
                minlength: 10,
                minWords: 2
            },
            Cpf: {
                cpfBR: true
            },
            Celular: {
                minlength: 8,
                maxlength: 100
            }

        },
        messages: {
            Nome: {
                required: "Nome é obrigatório"
            }
        }


    });

    $("#formCadastroFuncionario").on("submit", function (e) {
        e.preventDefault;
        if ($("#formCadastroFuncionario").valid()) {
            alert("asdasd");
        } else {
            
        }
    })

});