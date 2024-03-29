﻿$(function () {
    // show modal
    var placeholder = $("#modal-placeHolder");
    $(document).on("click", "button[data-toggle='ajax-modal']", function () {
        var url = $(this).data("url");
        // send get request for get partial view
        $.ajax({
            url: url,
            beforeSend: function () {
                ShowLoading();
            },
            complete: function () {
                window.loading_screen.finish();
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
        ShowLoading();
        var form = $(this).parents(".modal").find("form");
        var url = form.attr("action");
        var formdata = new FormData(form.get(0));
        // send post request
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
                var tableElement = $("#mytable");
                var url = tableElement.data("url");
                $.ajax({
                    url: url,
                    error: function () {
                        showAlert();
                    }
                }).done(function (result) {
                    $("#tableContent").html(result);
                });

                placeholder.find(".modal").modal('hide');
            }
        });
        window.loading_screen.finish();
    })
});

function showAlert(){
    Swal.fire({
        type: 'error',
        title: 'خطایی رخ داده است !!!',
        text: 'لطفا تا برطرف شدن خطا شکیبا باشید.',
        confirmButtonText: 'بستن'
    });
};

function ShowLoading() {
    window.loading_screen = window.pleaseWait({
        backgroundColor: 'rgba(255, 255, 255, 0.63)',
        loadingHtml: "<div class='sk-spinner sk-spinner-wave'><img src='/images/loading.gif' width='128' /></div><p class='loading-message'>لطفا صبر کنید...</p>"
    });
}
