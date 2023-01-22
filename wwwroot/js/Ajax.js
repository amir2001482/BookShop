$(function () {
    // show modal
    var placeholder = $("#modal-placeHolder");
    $("button[data-toggle='ajax-modal']").click(function () {
        var url = $(this).data("url");
        $.ajax({
            url: url,
            beforeSend: function () {
                $("body").preloader();
            },
            complete: function () {
                $("body").preloader("remove");
            },
            Error: function () {
                showAlert();
            }
        }).done(function (result) {

            placeholder.html(result);
            placeholder.find(".modal").modal("show");
        });
    });

    placeholder.on("click", "button[data-save='modal']", function () {

        var form = $(this).parents(".modal").find("form");
        var url = form.attr("action");
        var formdata = new FormData(form.get(0));
        $("body").preloader();
        // send post request for create
        $.ajax({
            url: url,
            type: "post",
            data: formdata,
            processData: false,
            contentType: false,
            Error: function () {
                showAlert();
            }
        }).done(function (result) {
            var newBody = $(".modal-body", result);
            placeholder.find(".modal-body").replaceWith(newBody);
            var isValid = placeholder.find("input[name='IsValid']").val();
            if (isValid) {
                var notiPlaceHolder = $("#notification");
                var url = notiPlaceHolder.data("url");
                $.ajax({
                    url: url,
                    Error: function () {
                        showAlert();
                    }
                }).done(function (result) {
                    notiPlaceHolder.html(result);
                });
            }
        });
        $("body").preloader("remove");
    })
});

function showAlert() {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Something went wrong!',
    });
}