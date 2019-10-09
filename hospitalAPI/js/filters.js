var docFilter = function () {
    $(document).ready(function () {
        $("#doctor-seach-list-text").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#doctor-search-list li").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
    });
};
var patFilter = function () {
    $(document).ready(function () {
        $("#patient-seach-list-text").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#patient-search-list li").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
    });
};
