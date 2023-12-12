function mostrarFormulario() {
    $.get('/Home/FormularioContacto', function (data) {
        $('#formularioContainer').html(data);
    });
}

$(document).ready(function () {
    $('#mostrarFormulario').click(function () {
        mostrarFormulario();
    });
});