function getElementById(id) {
    return document.getElementById(id);
}

document.addEventListener('DOMContentLoaded', function () {
    var paisSelect = getElementById("PaisSeleccionado");
    var provinciaSelect = getElementById("ProvinciaSeleccionada");
    var enviarButton = getElementById("enviarButton");

    if (paisSelect && provinciaSelect && enviarButton) {
        paisSelect.addEventListener("change", function () {
            cargarProvincias();
            capturarProvinciaSeleccionada(this);
        });

        provinciaSelect.addEventListener("change", cargarLocalidades);

        // Deshabilitar el botón de enviar por defecto
        enviarButton.disabled = true;
    }
});

function cargarDropdown(elementId, data) {
    var dropdown = getElementById(elementId);
    if (!dropdown) {
        console.error('Elemento con id "' + elementId + '" no encontrado.');
        return;
    }

    dropdown.innerHTML = '<option value="">Selecciona una opción</option>';

    data.forEach(function (item) {
        var option = document.createElement('option');
        option.value = item.value || item;
        option.text = item.text || item;
        dropdown.appendChild(option);
    });
}

function handleFetchErrors(response) {
    if (!response.ok) {
        throw new Error('Error en la solicitud Fetch: ' + response.statusText);
    }
    return response.json();
}

function cargarProvincias() {
    var paisSeleccionado = getElementById("PaisSeleccionado").value;

    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'pais=' + encodeURIComponent(paisSeleccionado)
    };

    fetch('/Formulario/ObtenerProvinciasFiltradas', requestOptions)
        .then(handleFetchErrors)
        .then(data => {
            cargarDropdown("Provincia", data.provincias);

            // Habilitar el botón de enviar cuando se cargan las provincias
            getElementById("enviarButton").disabled = false;
        })
        .catch(error => {
            console.error(error);
        });
}

function cargarLocalidades() {
    var provinciaSeleccionada = getElementById("Provincia").value;

    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'provincia=' + encodeURIComponent(provinciaSeleccionada)
    };

    fetch('/Formulario/ObtenerLocalidadesFiltradas', requestOptions)
        .then(handleFetchErrors)
        .then(data => {
            cargarDropdown("Localidad", data.localidades);
        })
        .catch(error => {
            console.error(error);
        });
}


var locacionSeleccionada = '';

function capturarProvinciaSeleccionada(selectElement) {
    var provinciaValue = selectElement.value;

    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'provincia=' + encodeURIComponent(provinciaValue)
    };

    fetch('/Formulario/ObtenerLocacionesFiltradas', requestOptions)
        .then(response => response.json())
        .then(data => {
            // Actualizar la lista de locaciones
            actualizarListaLocaciones(data);

            
        })
        .catch(error => {
            console.error('Error en la solicitud Fetch:', error);
        });
}

function actualizarListaLocaciones(locaciones) {
    var lista = document.getElementById('locacionesList');
    var enviarButton = document.getElementById("enviarButton");

    lista.innerHTML = '';

    if (locaciones && locaciones.length > 0) {
        locaciones.forEach(function (locacion, index) {
            var div = document.createElement('div');
            div.className = 'locacion-item-container';

            var icono = document.createElement('i');
            icono.className = 'bi bi-hospital-fill locacion-icon fondo-gris';

            var p = document.createElement('p');
            p.className = 'locacion-nombre';
            p.textContent = locacion.nombre;

            div.addEventListener('click', function () {
                agregarALaLista(div);
            });

            div.appendChild(icono);
            div.appendChild(p);
            lista.appendChild(div);
            enviarButton.disabled = false;
        });
    } else {
        Swal.fire({
            icon: 'error',
            title: 'No hay clínicas disponibles lo sentimos, para esta provincia aun no ahi clinicas',
            text: 'No podra enviar el formulario seleccione otra Provincia.',
        });

        // Deshabilitar el botón de enviar si no hay clínicas disponibles
        enviarButton.disabled = true;

    }

    asignarEventosClic();
}

function agregarALaLista(elemento) {
    var elementos = document.getElementsByClassName('locacion-item-container');
    for (var i = 0; i < elementos.length; i++) {
        elementos[i].classList.remove('selected-item');
    }

    elemento.classList.add('selected-item');

    var iconosNoSeleccionados = document.querySelectorAll('.locacion-icon:not(.selected-item .locacion-icon)');
    iconosNoSeleccionados.forEach(function (icono) {
        icono.classList.remove('fondo-seleccionado');
    });

    var iconoSeleccionado = elemento.querySelector('.locacion-icon');
    iconoSeleccionado.classList.add('fondo-seleccionado');

    // Actualizar la variable locacionSeleccionada
    locacionSeleccionada = elemento.querySelector('.locacion-nombre').textContent;
}

function asignarEventosClic() {
    var elementos = document.getElementsByClassName('locacion-item-container');
    for (var i = 0; i < elementos.length; i++) {
        elementos[i].addEventListener('click', function () {
            agregarALaLista(this);

            // Actualizar el campo oculto LocacionSeleccionada
            var locacionSeleccionadaInput = document.getElementById('ClinicaSeleccionada');
            if (locacionSeleccionadaInput) {
                locacionSeleccionadaInput.value = locacionSeleccionada;
            }
        });
    }
}
