$(document).ready(function () {

    var myVal = $("#notificationAddress").data("value");


    //TODO: This threw error when 
    $(function () {
        var myVal = $("#highlightAddress").data("value");
        if (myVal !== undefined) {
            var exeHighlight = ".vc" + myVal;
            $(exeHighlight).addClass('highlighted');
            setTimeout(function () {
                $(exeHighlight).removeClass('highlighted');
            }, 4000);
        }
    });

    $(function () {
        var myVal = $("#notificationAddress").data("value");

        if (myVal !== undefined && myVal !== "") {
            $.notify(myVal, { globalPosition: 'bottom right', className: 'success' });
        } else {
            var myErrorVal = $("#notificationAddressError").data("value");
            $.notify(myErrorVal, { globalPosition: 'bottom right', className: 'error' });
        }
    });

});