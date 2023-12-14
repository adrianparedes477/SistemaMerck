document.addEventListener('DOMContentLoaded', (event) => {
    const chartDiv = document.getElementById('chart_div');
    const edadActual = parseInt(chartDiv.dataset.edadActual);
    const edadPrimeraMenstruacion = parseInt(chartDiv.dataset.edadPrimeraMenstruacion);

    Highcharts.chart('chart_div', {
        chart: {
            type: 'line',
            backgroundColor: '#fdddca' // Color de fondo estilo piel humana
        },
        title: {
            text: 'Taza de fertilidad'
        },
        xAxis: {
            categories: ['Edad Actual', 'Edad Primera Menstruación']
        },
        yAxis: {
            title: {
                text: 'Valor'
            }
        },
        series: [{
            name: 'Reserva Ovárica',
            data: [edadActual, edadPrimeraMenstruacion]
        }]
    });
});













