
function contentLoader(id) {
    var currentPage = $("#currentPage").val();
    var ww = document.documentElement.clientWidth;
    if (ww > 992) {
        if (id.toString().includes("Event"))
            id = currentPage;
        $(".desk-content").css("display", "none");

        if (id != currentPage && currentPage != "") {
            $("#" + currentPage).css("display", "none");
            $("#" + id).slideToggle();
        }
        if ($("#" + id).css("display") != "block")
            $("#" + id).css("display", "block");
    }
    else {
        $(".desk-content").css("display", "block");


    }

    if (!id.toString().includes("Event"))
        $("#currentPage").val(id);
}
