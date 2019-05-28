$(document).ready(function () {
    if ($(".select-building").length == 0) {
        return;
    }

    $(".select-building").change(function () {
        var buildingId = this.value;

        $.post({
            url: "/Cabinets/GetCabinets",
            data: { id: buildingId },
            success: function (data) {
                $(".select-cabinet").empty();
                data.forEach(function (el) {
                    $(".select-cabinet").append($("<option/>").val(el.Value).text(el.Text));
                });
            }
        });
    });
});