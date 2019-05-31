$(document).ready(function () {
    $("#PrinterBrandID").change(function () {
        var brandId = this.value;

        $.post({
            url: "/Printers/GetPrintersModels",
            data: { id: brandId },
            success: function (data) {
                $("#PrinterModelID").empty();
                data.forEach(function (el) {
                    $("#PrinterModelID").append($("<option/>").val(el.Value).text(el.Text));
                });                
            }
        });
    });

    $("#Price").inputmask({
        alias: "decimal",
        digits: 2,
        radixPoint: ",",
        removeMaskOnSubmit: false,
        allowPlus: false,
        allowMinus: false,
        integerOptional: false,
        rightAlign: false
    });
});