document.addEventListener('DOMContentLoaded', () => {
    const provinciasDropdown = document.getElementById('Provincia');
    const motivosDropdown = document.getElementById('MotivoConsulta');
    const clinicasDropdown = document.getElementById('Clinicas');

    // Eventos de cambio en los desplegables para filtrar las ubicaciones
    provinciasDropdown.addEventListener('change', () => {
        filtrarUbicaciones();
    });

    motivosDropdown.addEventListener('change', () => {
        filtrarUbicaciones();
    });

    // Función para obtener y mostrar las ubicaciones filtradas
    function filtrarUbicaciones() {
        const provinciaSeleccionada = provinciasDropdown.value;
        const motivoSeleccionado = motivosDropdown.value;

        // Realizar una solicitud al servidor para obtener todas las ubicaciones
        fetch('/Formulario/ObtenerLocacionesJson')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Error al obtener las locaciones: ' + response.statusText);
                }
                return response.json();
            })
            .then(locaciones => {
                // Log para verificar las ubicaciones antes del filtrado
                console.log('Ubicaciones antes del filtrado:', locaciones);

                // Filtrar las ubicaciones según las selecciones del usuario
                let ubicacionesFiltradas = locaciones;

                if (provinciaSeleccionada) {
                    ubicacionesFiltradas = ubicacionesFiltradas.filter(loc => loc.Provincia.toLowerCase() === provinciaSeleccionada.toLowerCase());
                }

                clinicasDropdown.innerHTML = '';
                const tablaDatos = document.getElementById('tablaDatos');
                tablaDatos.innerHTML = ''; // Limpiar la tabla antes de agregar nuevos datos

                ubicacionesFiltradas.forEach(clinica => {
                    const option = document.createElement('option');
                    option.value = clinica.Nombre;
                    option.text = clinica.Nombre;
                    clinicasDropdown.appendChild(option);

                    // Agregar fila a la tabla
                    const fila = tablaDatos.insertRow();
                    const celdaNombre = fila.insertCell(0);
                    const celdaProvincia = fila.insertCell(1);

                    celdaNombre.textContent = clinica.Nombre;
                    celdaProvincia.textContent = clinica.Provincia;
                });


            })
            .catch(error => {
                console.error('Error al obtener las locaciones filtradas:', error);
            });
    }
});





