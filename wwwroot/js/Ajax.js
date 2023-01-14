$(function () {
    // show modal
    var placeholder = $("#modal-placeHolder");
    $("button[data-toggle='ajax-modal']").click(function () {
        var url = $(this).data("url");
        $.get(url).done(function (result) {

            placeholder.html(result);
            placeholder.find(".modal").modal("show");
        });
    });

    placeholder.on("click", "button[data-save='modal']", function () {

        var form = $(this).parents(".modal").find("form");
        var url = form.attr("action");
        var formdata = new FormData(form.get(0));
        $.ajax({
            url: url,
            type: "post",
            data: formdata,
            processData: false,
            contentType: false,
        }).done(function (result) {
            var newBody = $(".modal-body", result);
            placeholder.find(".modal-body").replaceWith(newBody);
            var isValid = placeholder.find("input[name='IsValid']").val();
            if (isValid) {
                var notiPlaceHolder = $("#notification");
                var url = notiPlaceHolder.data("url");
                $.get(url).done(function (result) {
                    notiPlaceHolder.html(result);
                });
            }
        })

    })
});