function initMap(apiKey) {
    var map = new Microsoft.Maps.Map('#mapa', {
        credentials: apiKey
    });

    fetch('/Home/ObtenerLocacionesJson')
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al obtener las locaciones: ' + response.statusText);
            }
            return response.json();
        })
        .then(locaciones => {
            locaciones.forEach(clinica => {
                var ubicacion = new Microsoft.Maps.Location(clinica.latitud, clinica.longitud);
                var pin = new Microsoft.Maps.Pushpin(ubicacion, { title: clinica.nombre });
                map.entities.push(pin);
            });
        })
        .catch(error => {
            console.error('Error al obtener las locaciones:', error);
        });
}

document.addEventListener('DOMContentLoaded', () => {
    var apiKey = document.getElementById('mapa').getAttribute('data-api-key');
    initMap(apiKey);
});






