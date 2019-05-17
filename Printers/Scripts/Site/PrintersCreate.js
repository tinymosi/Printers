$(document).ready(function () {
    $("#Printer_PrinterBrandID").change(function () {
        var brandId = this.value;

        $.post({
            url: "GetPrintersModelsList",
            data: { id: brandId },
            success: function (data) {
                $("#Printer_PrinterModelID").empty();
                data.forEach(function (el) {
                    $("#Printer_PrinterModelID").append($("<option/>").val(el.Value).text(el.Text));
                });                
            }
        });
    });

    $("#Printer_Price").inputmask({
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