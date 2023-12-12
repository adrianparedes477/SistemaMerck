function initMap(apiKey) {
    var map = new Microsoft.Maps.Map('#mapa', {
        credentials: apiKey
    });

    // Crear una instancia de XMLHttpRequest
    var xhr = new XMLHttpRequest();

    // Configurar la solicitud
    xhr.open('GET', '/Home/ObtenerLocacionesJson', true);
    xhr.setRequestHeader('Content-Type', 'application/json');

    // Manejar la respuesta
    xhr.onload = function () {
        if (xhr.status >= 200 && xhr.status < 300) {
            // Éxito
            var locaciones = JSON.parse(xhr.responseText);

            locaciones.forEach(function (clinica) {
                var ubicacion = new Microsoft.Maps.Location(clinica.latitud, clinica.longitud);
                var pin = new Microsoft.Maps.Pushpin(ubicacion, { title: clinica.nombre });
                map.entities.push(pin);
            });
        } else {
            // Manejar errores
            console.error('Error al obtener las locaciones:', xhr.statusText);
        }
    };

    // Manejar errores de red
    xhr.onerror = function () {
        console.error('Error de red al obtener las locaciones');
    };

    // Enviar la solicitud
    xhr.send();
}

document.addEventListener('DOMContentLoaded', function () {
    // Obtén la clave desde el atributo data del div
    var apiKey = document.getElementById('mapa').getAttribute('data-api-key');
    initMap(apiKey);
});





