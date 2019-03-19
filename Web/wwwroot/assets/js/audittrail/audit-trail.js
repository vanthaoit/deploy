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


    var table_ = $('#auditTable').DataTable(
        {
            "aaSorting": [
                [0, "asc"]
            ],
            "paging": false,
            "ordering": false,
            "info": false
            
        });
    $('#auditTable tbody').on('click', 'button#moreDetails', function () {
        var data = table_.row($(this).parents('tr')).data();
        $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td class"text-nowrap">Validation Code<td><td>' + data[1] + '</td></tr><tr><td>Code Description<td><td>' + data[2] + '</td></tr><tr><td>Show Self Pay<td><td>' + data[3] + '</td></tr><tr><td>Show Insurance<td><td>' + data[4] + '</td></tr><tr><td>Date Created<td><td>' + data[5] + '</td></tr></tbody></table>');
        $('#detailsTable').modal('show');
    });
    //var oTable = $('#validationTable').DataTable();
    //$('#searchValidationTable').keyup(function () {
    //    oTable.search($(this).val()).draw();
    //})
    $('.toggle-filter').on('change', function (e) {
        e.preventDefault();
        // Get Column API Object
        var column = table_.column($(this).attr('data-column'));
        // Toggle Visibility
        column.visible(!column.visible());
    });


    
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
                $('#auditTable').DataTable();
                $("#viewTrail").modal("show");
                
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    AuditTrail_GlobalFilter = function () {
        $('#auditTable').DataTable();
        $('#auditTable').DataTable().search(
            $('#auditTrailFilters').val()

        ).draw();
    };

    $('input.auditTrailFilters').on('keyup click', function () {
        var abc = "";
    });

    $(".toggle-filter").change(function () {
        alert("Handler for .change() called.");
    });
    checkColumFilter = function (e) {
        e.preventDefault();
        table_;
        // Get Column API Object
        var column = table_.column(e.currentTarget.attributes['data-column'].nodeValue);
        // Toggle Visibility
        column.visible(!column.visible());

    
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