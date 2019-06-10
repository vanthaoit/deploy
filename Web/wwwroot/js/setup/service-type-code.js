/// <reference path="../global-const.js" />

$(document).ready(function () {

    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;
    var table;
    $(function () {
        table = $('#serviceTable').DataTable(
            {
                "aaSorting": [
                    [1, "asc"]
                ],
                "paging": false,
                "ordering": true,
                "info": false,
                "searching": true,
                "responsive":
                {
                    details: false
                },
                "columnDefs": [
                    {
                        responsivePriority: 1,
                        targets: 0
                    },
                    {
                        responsivePriority: 2,
                        targets: -1
                    },
                    {
                        targets: [4],
                        className: "text-center",
                        orderable: false,
                        searching: false
                    },
                    {
                        // More Details Button
                        "targets": 0,
                        "orderable": false,
                        "data": null,
                        "defaultContent": "<button id='moreDetails' class='btn btn-sm btn-link lh-0 mr-1'><svg class='icon icon-expand'><use xlink:href='#icon-expand'></use></svg></button>"
                    }]
            });
        $('#serviceTable tbody').on('click', 'button#moreDetails', function () {
            var data = table.row($(this).parents('tr')).data();
            $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td>Service Type Code<td><td>' + data[1] + '</td></tr><tr><td>Code Description<td><td>' + data[2] + '</td></tr><tr><td>Date Created<td><td>' + data[3] + '</td></tr></tbody></table>');
            $('#detailsTable').modal('show');
        });
        var oTable = $('#serviceTable').DataTable();
        $('#searchServiceTable').keyup(function () {
            oTable.search($(this).val()).draw();
        })
        $('.toggle-vis').on('change', function (e) {
            e.preventDefault();
            // Get Column API Object
            var column = table.column($(this).attr('data-column'));
            // Toggle Visibility
            column.visible(!column.visible());
        });


        $('#audit').DataTable(
            {
                "columnDefs": [
                    {
                        targets: [0, 1],
                        className: "text-right",
                    },],
                "paging": false,
                "ordering": true,
                "info": false,
                "searching": true,
                "responsive":
                {
                    details: false
                },
            });
        $('#addServiceType, #editServiceType').on('shown.bs.modal', function () {
            $(this).find('input:first').focus();
        });

    });



    //-------------------------------------------------------
    // Functions / Data Calls
    //-------------------------------------------------------


    submitServiceCode = function (event) {
        var serviceCodeValue = $("#addServiceType #newServiceTypeCode").val();
        var nameStoreValue = "eligibility.sp_GetServiceCodeKeys";
        var justcontinue = false;
        $.ajax({
            type: "Get",
            url:  AppConfig+'/ServiceTypeCode/ExistServiceCode',
            async: false, cache :false,
            data: { serviceCode: serviceCodeValue, nameStore: nameStoreValue },
            success: function (data) {
                var response = data;
                if (response === "true") {
                    $("#submitServiceCodeForm span.validation-notification")
                        .text("The service code is already in use.Please select a different code.")
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

    OpenEditService = function (id) {
        var currentId = id;

        $.ajax({
            type: "Get",
            url:  AppConfig+'/ServiceTypeCode/GetServiceDetails',
            data: { id: currentId },
            success: function (data) {
                var response = data;
                $('#PreviewEditServiceTypeCode').html('');
                $('#PreviewEditServiceTypeCode').html(data);
                $("#editServiceTypeCode").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    DeleteService = function (id) {
        var currentValidationCode = id;
        var isDelete = true;
        $.ajax({
            type: "Get",
            url:  AppConfig+'/ServiceTypeCode/GetServiceDetails',
            async: false, cache :false,
            data: { id: currentValidationCode, isDelete: isDelete },
            success: function (data) {
                var response = data;
                $('#PreviewDeleteServiceTypeCode').html('');
                $('#PreviewDeleteServiceTypeCode').html(data);
                $("#deleteServiceTypeCode").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    ExportServiceCodeList = function () {
        $("#serviceTable").table2excel({
            //exclude: ".excludeThisClass",
            name: "Service Type Code",
            filename: "Service Type Code List.xls", //do not include extension,
            fileext: ".xls"
        });
    };

    RemoveMemory = function () {
        $("#newServiceTypeCode").val('');
        $("#newCodeDescription").val('');
    };

    ActiveApply = function () {
        $("#columnPicker").removeClass("show")
    };

});



