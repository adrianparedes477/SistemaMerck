document.addEventListener('DOMContentLoaded', function () {
    var paisSelect = document.getElementById("PaisSeleccionado");
    var provinciaSelect = document.getElementById("ProvinciaSeleccionada");
    var enviarButton = document.getElementById("enviarButton");

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

function cargarProvincias() {
    var paisSeleccionado = document.getElementById("PaisSeleccionado").value;

    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'pais=' + encodeURIComponent(paisSeleccionado)
    };

    fetch('/Formulario/ObtenerProvinciasFiltradas', requestOptions)
        .then(response => response.json())
        .then(data => {
            var provinciaDropdown = document.getElementById("Provincia");

            if (provinciaDropdown) {
                provinciaDropdown.innerHTML = '<option value="">Selecciona una opción</option>';
                data.forEach(function (provincia) {
                    var option = document.createElement('option');
                    option.value = provincia;
                    option.text = provincia;
                    provinciaDropdown.appendChild(option);
                });

                // Habilitar el botón de enviar cuando se cargan las provincias
                var enviarButton = document.getElementById("enviarButton");
                enviarButton.disabled = false;
            } else {
                console.error('Elemento con id "ProvinciaSeleccionada" no encontrado.');
            }
        })
        .catch(error => {
            console.error('Error en la solicitud Fetch:', error);
        });
}


function cargarLocalidades() {
    var provinciaSeleccionada = document.getElementById("Provincia").value;

    var requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'provincia=' + encodeURIComponent(provinciaSeleccionada)
    };

    fetch('/Formulario/ObtenerLocalidadesFiltradas', requestOptions)
        .then(response => response.json())
        .then(data => {
            var localidadDropdown = document.getElementById("Localidad");
            localidadDropdown.innerHTML = '<option value="">Selecciona una opción</option>';
            data.forEach(function (localidad) {
                var option = document.createElement('option');
                option.value = localidad;
                option.text = localidad;
                localidadDropdown.appendChild(option);
            });
        })
        .catch(error => {
            console.error('Error en la solicitud Fetch:', error);
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

            // No actualizar el campo oculto LocacionSeleccionada aquí
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
            icon: 'info',
            title: 'No hay locaciones disponibles',
            text: 'Lo sentimos, no hay clínicas disponibles para esta provincia.',
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
