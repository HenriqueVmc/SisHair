$(function () {

    var registros = [];
    var funcionarios = [];

    var ctx = document.getElementById("chartFuncionarios");

    UpdateGrafico();

    function UpdateGrafico() {
        registros = new Array();
        funcionarios = new Array();

        $.ajax({
            type: "GET",
            dataType: 'json',
            url: "/Estatisticas/GetAgendamentosByMesFuncionarios",
            success: function (valores) {
                for (var i = 0; i < 3; i++) {
                    registros[i] = valores[i];
                }
                chart.update();
                //registros.push(valores);
            }
        });
        debugger;
        $.ajax({
            type: "GET",
            dataType: 'json',
            url: "/Estatisticas/GetFuncionarios",
            success: function (valores) {
                for (var i = 0; i < 3; i++) {
                    funcionarios[i] = valores[i];
                }
                chart.update();
                //funcionarios.push(valores);
            }
        });
    }

    var chart = new Chart(ctx, {
        type: 'doughnut',
        tooltipFillColor: "rgba(51, 51, 51, 0.55)",
        data: {
            labels: funcionarios,
            datasets: [{
                label: "Teste",
                data: registros,
                backgroundColor: [
                    "#455C73",
                    "#9B59B6",
                    "#BDC3C7",
                    "#26B99A",
                    "#3498DB"
                ],
                hoverBackgroundColor: [
                    "#34495E",
                    "#B370CF",
                    "#CFD4D8",
                    "#36CAAB",
                    "#49A9EA"
                ]
            }]
        }
    });
});