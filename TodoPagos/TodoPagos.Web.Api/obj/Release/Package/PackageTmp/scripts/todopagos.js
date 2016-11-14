$(function () {
    $('#FromDatetimepickerPerProvider').datetimepicker({
        defaultDate: new Date()
    });

    $('#ToDatetimepickerPerProvider').datetimepicker({
        defaultDate: new Date()
    });

    $('#FromDatetimepickerTotalEarnings').datetimepicker({
        defaultDate: new Date()
    });

    $('#ToDatetimepickerTotalEarnings').datetimepicker({
        defaultDate: new Date()
    });

    $('#PayDateDatetimepicker').datetimepicker({
        defaultDate: new Date(),
        maxDate : 'now'
    });
});