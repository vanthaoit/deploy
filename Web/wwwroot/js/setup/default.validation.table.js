(function (factory) {
    // UMD start
    // https://github.com/umdjs/umd/blob/master/jqueryPluginCommonjs.js
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['jquery'], factory);
    } else if (typeof module === 'object' && module.exports) {
        // Node/CommonJS
        module.exports = function (root, jQuery) {
            if (jQuery === undefined) {
                // require('jQuery') returns a factory that requires window to
                // build a jQuery instance, we normalize how we use modules
                // that require this pattern but the window provided is a noop
                // if it's defined (how jquery works)
                if (typeof window !== 'undefined') {
                    jQuery = require('jquery');
                }
                else {
                    jQuery = require('jquery')(root);
                }
            }
            factory(jQuery);
            return jQuery;
        };
    } else {
        // Browser globals
        factory(jQuery);
    }
}(function ($) {
    $(function () {
        var myVal = $("#highlightAddress").data("value");
        if (myVal !== undefined) {
            var exeHighlight = "#vc" + myVal;
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

    submitValidationCode = function (event) {
        var validationCodeValue = $("#addValidationCode .validation-code-value").val();
        if (!isValidValidationCode(validationCodeValue)) {
            $("#submitValidationCodeForm span.validation-notification")
                .text("The validation code structure is invalid. Please select a different code.")
                .show()
                .fadeOut(5000);
            return false;
        }
        var justcontinue = false;
        $.ajax({
            type: "Get",
            url: '/Setup/ExistValidationCode',
            async: false,
            data: { validationCode: validationCodeValue },
            success: function (data) {
                var response = data;
                if (response === "true") {
                    $("#submitValidationCodeForm span.validation-notification")
                        .text("The validation code is already in use.Please select a different code.")
                        .show()
                        .fadeOut(5000);
                    justcontinue = false;
                } else {
                    justcontinue = true;
                }
            }
        });
        return justcontinue;
    };
    isValidValidationCode = function (validationCode) {
        if (validationCode === null || validationCode === undefined || validationCode === "") return false;
        if (validationCode.substring(0, 3) !== 'AAA') return false;
        if (validationCode.substring(3, 4) !== '*') return false;
        if (validationCode.substring(4, 5) !== 'Y' && validationCode.substring(4, 5) !== 'N') return false;
        if (validationCode.substring(5, 7) !== '**') return false;
        if (validationCode.substring(9, 10) !== '*') return false;
        return true;
    };

    OpenEditValidation = function (id) {
        var currentId = id;
        $.ajax({
            type: "Get",
            url: '/Setup/GetDetails',
            data: { id: currentId },
            success: function (data) {
                var response = data;
                $('#PreviewEditValidationCode').html('');
                $('#PreviewEditValidationCode').html(data);
                $("#editValidationCode").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    DeleteValidation = function (id) {
        var currentValidationCode = id;
        var isDelete = true;
        $.ajax({
            type: "Get",
            url: '/Setup/GetDetails',
            async: false,
            data: { id: currentValidationCode, isDelete: isDelete },
            success: function (data) {
                var response = data;
                $('#PreviewDeleteValidationCode').html('');
                $('#PreviewDeleteValidationCode').html(data);
                $("#deleteValidationCode").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    ExportValidationCodeList = function () {

        $("#validationTable").table2excel({
            //exclude: ".excludeThisClass",
            name: "Validation Code",
            filename: "Validation Code List", //do not include extension,
            fileext: ".xls"
        });

    };
}));