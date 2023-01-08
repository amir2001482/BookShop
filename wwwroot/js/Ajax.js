$(function () {
    // show details of book in modal
    var placeholder = $("#modal-placeHolder");
    $("button[data-toggle='ajax-modal']").click(function () {
        var url = $(this).data("url");
        $.get(url).done(function (result) {

            placeholder.html(result);
            placeholder.find(".modal").modal("show");
        });
    });
});