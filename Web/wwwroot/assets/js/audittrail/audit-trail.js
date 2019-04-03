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

    OpenAuditTrails = function (id, desc) {
        var currentId = id;
        var descriptionAuditTrail = desc;
        $.ajax({
            type: "Get",
            url: '/AuditTrail/GetDetails',
            async: false,
            data: { id: currentId, description: descriptionAuditTrail },
            success: function (data) {
                var response = data;
                $('#PreviewAuditTrails').html(data);

                $("#viewTrail").modal("show");
                $('#auditTable').DataTable();
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    ExportAuditTrailList = function () {
        $("#auditTable").table2excel({
            //exclude: ".excludeThisClass",
            name: "Audit Trail List",
            filename: "Audit Trail List", //do not include extension,
            fileext: ".xls"
        });
    };
}));