
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
                minlength: 3,
                minWords: 2
            },
            Cpf: {
                required: true,
                cpfBR: true
            },
            Celular: {
                required: true,
            },
            Telefone: {                
            },
            Email: {
                required: true,
                email: true
            },
            DataNascimento: {
                required: true
            }


        },
        messages: {
            Nome: {
                required: "Nome é obrigatório",
                minWords: "Por favor insira nome e sobrenome"
            }
        }


    });

   

});