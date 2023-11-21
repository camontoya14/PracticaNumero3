function ConsultarNombre() {

    let identificacion = $("#Identificacion").val();

    if (identificacion.length > 0) {
        $.ajax({
            url: 'https://apis.gometa.org/cedulas/' + identificacion,
            type: "GET",
            success: function (data) {
                $("#Nombre").val(data.nombre);
            }
        });
    }
    else {
        $("#Nombre").val("");
    }
}