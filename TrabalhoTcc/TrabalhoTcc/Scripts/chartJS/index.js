$(function () {
    
    var registros = [];

    var ctx = document.getElementById('chartAgendamentos').getContext('2d');

    UpdateGrafico();

    function UpdateGrafico() {
        registros = new Array();

        $.ajax({
            type: "GET",
            dataType: 'json',
            url: "/Agendamentos/GetAgendamentosByMes",
            success: function (valores) {
                for (var i = 0; i < 12; i++) {
                    chart.data.datasets[0].data[i] = valores[i];
                }
                chart.update();
            }
        });
    }

    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
            datasets: [{
                label: "Agendamentos",
                backgroundColor: 'rgb(125, 15, 174)',
                borderColor: 'rgb(255, 99, 132)',
            }]
        },
        // Configuration options go here
        options: {}
    });

});