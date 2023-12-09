function initMap(apiKey) {
    var map = new Microsoft.Maps.Map('#mapa', {
        credentials: apiKey
    });

    // Realizar una solicitud AJAX para obtener las locaciones
    $.ajax({
        url: '/Home/ObtenerLocacionesJson',
        type: 'GET',
        dataType: 'json',
        success: function (locaciones) {
            locaciones.forEach(function (clinica) {
                var ubicacion = new Microsoft.Maps.Location(clinica.latitud, clinica.longitud);
                var pin = new Microsoft.Maps.Pushpin(ubicacion, { title: clinica.nombre });
                map.entities.push(pin);
            });
        },
        error: function (error) {
            console.error('Error al obtener las locaciones:', error);
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    // Obtén la clave desde el atributo data del div
    var apiKey = document.getElementById('mapa').getAttribute('data-api-key');
    initMap(apiKey);
});




