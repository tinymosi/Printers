$.validator.methods.date = function (value, element) {
    // pattern: dd.MM.yyyy
    var dateRegex = /^(0?[1-9]|[12][0-9]|3[01])[\.](0?[1-9]|1[012])[\.]\d{4}$/;
    return this.optional( element ) || dateRegex.test(value) || !/Invalid|NaN/.test( new Date( value ).toString() );
}

$.validator.methods.number = function (value, element) {
    value = value.replace(",", ".").replace(" ", ",");
    return this.optional( element ) || /^(?:-?\d+|-?\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$/.test( value );
}

$.validator.methods.range = function (value, element, param) {
    value = value.replace(",", ".").replace(" ", "");
    return this.optional( element ) || (value >= param[0] && value <= param[1]);
}