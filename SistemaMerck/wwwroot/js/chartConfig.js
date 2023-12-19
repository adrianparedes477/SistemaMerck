document.addEventListener('DOMContentLoaded', function () {
    // Datos de ejemplo para reserva ovárica
    var reservaOvaricaData = [10, 20, 30, 40, 50]; // Reemplaza esto con tus datos reales

    // Punto correspondiente al resultado del usuario
    var resultadoUsuario = 30; // Reemplaza esto con el resultado real del usuario
    var puntoUsuario = {
        x: reservaOvaricaData.indexOf(resultadoUsuario),
        y: resultadoUsuario,
        marker: {
            symbol: 'circle', // Puedes cambiar el símbolo del marcador según tus preferencias
            fillColor: 'red',
            radius: 8
        }
    };

    // Configuración del gráfico
    Highcharts.chart('container', {
        chart: {
            type: 'spline',
            scrollablePlotArea: {
                minWidth: 700
            },
            backgroundColor: '#fdddca'
        },
        title: {
            text: 'Resultado de Reserva Ovárica'
        },
        subtitle: {
            text: 'Fuente: Tus datos específicos aquí'
        },
        xAxis: {
            categories: ['Categoria1', 'Categoria2', 'Categoria3', 'Categoria4', 'Categoria5'], // Reemplaza con tus categorías
            title: {
                text: 'Fecha' // Cambia según tus necesidades
            }
        },
        yAxis: {
            title: {
                text: 'Reserva Ovárica' // Cambia según tus necesidades
            }
        },
        tooltip: {
            shared: true,
            crosshairs: true
        },
        plotOptions: {
            spline: {
                marker: {
                    radius: 4,
                    lineColor: '#666666',
                    lineWidth: 1
                }
            }
        },
        series: [{
            name: 'Reserva Ovárica',
            data: reservaOvaricaData
        }, {
            name: 'Resultado Usuario',
            data: [puntoUsuario],
            color: 'red'
        }]
    });
});




























