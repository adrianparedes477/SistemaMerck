google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Edad');
    data.addColumn('number', 'Nivel de Fertilidad');

    // Agrega tus datos de reserva ovárica aquí
    data.addRows([
        ['Edad 1', 10],
        ['Edad 2', 15],
        ['Edad 3', 25],
        
    ]);

    // Agrega una fila para marcar el valor proporcionado por el usuario
    data.addRow(['Usted está aquí', reservaOvaricaUsuario]);

    var options = {
        title: 'Curva de Nivel de Fertilidad',
        curveType: 'function',
        legend: { position: 'bottom' }
    };

    var chart = new google.visualization.LineChart(document.getElementById('chart_div'));

    chart.draw(data, options);
}
