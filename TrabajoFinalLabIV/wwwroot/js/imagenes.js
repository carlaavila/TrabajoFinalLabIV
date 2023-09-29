$("#seleccionImg").change(function () {
    readURL(this)
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imagen").attr("src", e.target.result);
        }

        reader.readAsDataURL(input.files[0]); //lee el archivo como string base64
    }
}