function validarExtension() {
    //Saber extensión - obtener extensión - dividir el nombre en 2 através de un punto (.)
    var input = $('#inptFileImagen')[0].files[0].name.split('.').pop().toLowerCase();
    console.log(input);

    //coparar con otras extensiones de imagen
    var extensionesPermitidas = ['png', 'jpg', 'jpeg', 'webp'];
    var banderaImg = false;

    for (var i = 0; i <= extensionesPermitidas.length; i++) {
        if (input == extensionesPermitidas[i]) {
            banderaImg = true;
        }
    }
    //Si el archivo cargado no es válido
    if (!banderaImg) {
        alert(`No seleccionaste una imagen, debe tener las extensiones: ${extensionesPermitidas}`);
        $('#inptFileImagen').val("");
    }
}

function visualizarImagen(input) {
    if (input.files) {
        var reader = new FileReader(); //FileReader lee cualquier tipo de archivo

        //evento onload => se utiliza para ejecutar una función cuando
        //una página web o un elemento específico ha terminado de cargarse

        reader.onload = function (elemento) {
            $('#imgRestaurante').attr('src', elemento.target.result);
        }
        reader.readAsDataURL(input.files[0]);

    }
}