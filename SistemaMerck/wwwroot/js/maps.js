function initMap() {
    var map = new Microsoft.Maps.Map('#mapa', {
        credentials: '' /*key*/
    });

    
    var clinicas = [
        { nombre: 'AlBOR', latitud: -38.94905271209734, longitud: -68.07196047417949 },
        { nombre: 'CEGYR', latitud: -34.59604720625888, longitud: -58.38489195948186 },
        { nombre: 'CER', latitud: -34.57898399672684, longitud: -58.4223511436052 }
    ];

    clinicas.forEach(function (clinica) {
        var ubicacion = new Microsoft.Maps.Location(clinica.latitud, clinica.longitud);
        var pin = new Microsoft.Maps.Pushpin(ubicacion, { title: clinica.nombre });
        map.entities.push(pin);
    });
}

document.addEventListener('DOMContentLoaded', initMap);