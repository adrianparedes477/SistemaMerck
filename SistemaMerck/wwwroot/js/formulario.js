function capturarProvinciaSeleccionada(selectElement) {
    // Obtener el valor seleccionado
    var provinciaValue = selectElement.value;

    // Realizar una solicitud AJAX para obtener las locaciones filtradas
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Formulario/ObtenerLocacionesFiltradas', true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            // Actualizar la lista de locaciones en la página
            actualizarListaLocaciones(JSON.parse(xhr.responseText));
        }
    };
    xhr.send('provincia=' + encodeURIComponent(provinciaValue));
}

// Función para actualizar la lista de locaciones en la página
function actualizarListaLocaciones(locaciones) {
    var lista = document.getElementById('locacionesList');

    // Limpiar la lista actual
    while (lista.firstChild) {
        lista.removeChild(lista.firstChild);
    }

    if (locaciones && locaciones.length > 0) {
        // Mostrar las locaciones filtradas
        locaciones.forEach(function (locacion) {
            // Asegúrate de que las propiedades nombre estén presentes y no sean undefined
            var nombre = locacion && locacion.nombre !== undefined ? locacion.nombre : 'Nombre no disponible';

            var div = document.createElement('div');
            div.className = 'locacion-item';
            div.setAttribute('data-nombre', nombre);
            div.setAttribute('onclick', 'agregarALaLista(this)');

            var icono = document.createElement('i');
            icono.className = 'bi bi-hospital-fill locacion-icon';

            var p = document.createElement('p');
            p.className = 'locacion-nombre';
            p.textContent = nombre;

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
}


function agregarALaLista(elemento) {
    // Restaurar el color de todos los elementos
    var elementos = document.getElementsByClassName('locacion-item');
    for (var i = 0; i < elementos.length; i++) {
        elementos[i].classList.remove('selected-item');
    }

    // Cambiar el color del elemento seleccionado
    elemento.classList.add('selected-item');

    var nombre = elemento.getAttribute('data-nombre');
    var locacionSeleccionada = nombre;

    // Agrega la locación al campo adicional
    document.getElementById('ClinicaSeleccionada').value = locacionSeleccionada;
}






