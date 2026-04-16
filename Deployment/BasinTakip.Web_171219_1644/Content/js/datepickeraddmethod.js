$(function () {
    try {
        $.validator.addMethod('date',
        function (value, element) {
            if (this.optional(element)) {
                return true;
            }
            var ok = true;
            try {
                ok = moment(value, "DD.MM.YYYY", true).isValid();
            } catch (err) {
                ok = false;
            }
            return ok;
        });
    } catch (e) { }
});