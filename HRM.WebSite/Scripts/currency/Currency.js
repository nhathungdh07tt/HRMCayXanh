$(document).ready(function () {
    $(".money").formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: 2 });
    $(".money").blur(function () {
        $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: 2 });
    })
    .keyup(function (e) {
        var e = window.event || e;
        var keyUnicode = e.charCode || e.keyCode;
        if (e !== undefined) {
            switch (keyUnicode) {
                case 16: break; // Shift
                case 27: this.value = ''; break; // Esc: clear entry
                case 35: break; // End
                case 36: break; // Home
                case 37: break; // cursor left
                case 38: break; // cursor up
                case 39: break; // cursor right
                case 40: break; // cursor down
                case 78: break; // N (Opera 9.63+ maps the "." from the number key section to the "N" key too!) (See: http://unixpapa.com/js/key.html search for ". Del")
                case 110: break; // . number block (Opera 9.63+ maps the "." from the number block to the "N" key (78) !!!)
                case 190: break; // .
                default: $(this).formatCurrency({ colorize: true, negativeFormat: '-%s%n', roundToDecimalPlace: -1, eventOnDecimalsEntered: true });
            }
        }
    });

    $(document).submit(function () {
        $("input[type=text].money").each(function () {
            var val = parseFloat($(this).val().replace(/,/g, ""));
            $(this).val(val);

            //$(this).val($(this).val().replace(/./g, ","));
        });
    });

});