function capturarProvinciaSeleccionada(selectElement) {
    var provinciaValue = selectElement.value;

    // Configuración de la solicitud Fetch
    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'provincia=' + encodeURIComponent(provinciaValue)
    };

    // Realizar la solicitud Fetch
    fetch('/Formulario/ObtenerLocacionesFiltradas', requestOptions)
        .then(response => response.json())
        .then(data => {
            // Actualizar la lista de locaciones en la página
            actualizarListaLocaciones(data);
        })
        .catch(error => {
            console.error('Error en la solicitud Fetch:', error);
        });
}


// Función para actualizar la lista de locaciones en la página
function actualizarListaLocaciones(locaciones) {
    var lista = document.getElementById('locacionesList');

    // Limpiar la lista actual
    lista.innerHTML = '';

    if (locaciones && locaciones.length > 0) {
        // Mostrar las locaciones filtradas
        locaciones.forEach(function (locacion, index) {
            var div = document.createElement('div');
            div.className = 'locacion-item-container';

            var icono = document.createElement('i');
            icono.className = 'bi bi-hospital-fill locacion-icon';

            var p = document.createElement('p');
            p.className = 'locacion-nombre'; 
            p.textContent = locacion.nombre;

            // Asignar evento de clic al nuevo elemento
            div.addEventListener('click', function () {
                agregarALaLista(div);
            });

            div.appendChild(icono);
            div.appendChild(p);
            lista.appendChild(div);
        });
    } else {
        // Mostrar mensaje si no hay locaciones disponibles
        var div = document.createElement('div');
        div.className = 'alert alert-info text-center';
        div.textContent = 'No hay locaciones disponibles.';
        lista.appendChild(div);
    }

    // Asignar eventos de clic a los elementos después de actualizar la lista
    asignarEventosClic();
}

// Función para agregar la locación a la lista
function agregarALaLista(elemento) {
    

    // Remover la clase 'selected-item' de todos los elementos
    var elementos = document.getElementsByClassName('locacion-item-container');
    for (var i = 0; i < elementos.length; i++) {
        elementos[i].classList.remove('selected-item');

        // Remover el fondo de color de los íconos no seleccionados
        var iconoNoSeleccionado = elementos[i].querySelector('.locacion-icon');
        iconoNoSeleccionado.style.backgroundColor = '';
    }

    // Agregar la clase 'selected-item' al elemento seleccionado
    elemento.classList.add('selected-item');

    // Obtener el nombre del elemento seleccionado
    var nombre = elemento.querySelector('.locacion-nombre').textContent;

    // Asignar el nombre al campo 'ClinicaSeleccionada'
    document.getElementById('ClinicaSeleccionada').value = nombre;

    // Resaltar el ícono del elemento seleccionado con un color de fondo
    var iconoSeleccionado = elemento.querySelector('.locacion-icon');
    iconoSeleccionado.style.backgroundColor = '#c33b80';
}



// Función para asignar eventos de clic a los elementos después de agregarlos a la lista
function asignarEventosClic() {
    var elementos = document.getElementsByClassName('locacion-item-container');
    for (var i = 0; i < elementos.length; i++) {
        elementos[i].addEventListener('click', function () {
            agregarALaLista(this);
        });
    }
}

// Asignar evento de clic a los elementos después de cargar la página
document.addEventListener('DOMContentLoaded', function () {
    var elementos = document.getElementsByClassName('locacion-item-container');
    for (var i = 0; i < elementos.length; i++) {
        elementos[i].addEventListener('click', function () {
            agregarALaLista(this);
        });
    }
});














