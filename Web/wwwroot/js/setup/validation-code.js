/// <reference path="../global-const.js" />
$(document).ready(function () {
    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;
    var table;
    $(function () {
        table = $('#validationTable').DataTable(
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
                        targets: [6],
                        className: "text-center",
                        orderable: false,
                        searching: false
                    },
                    {
                        // More Details Button
                        "targets": 0,
                        "orderable": false,
                        "data": null,
                        "defaultContent":
                            "<button id='moreDetails' class='btn btn-sm btn-link lh-0 mr-1'><svg class='icon icon-expand'><use xlink:href='#icon-expand'></use></svg></button>"
                    }
                ]
            });
        $('#validationTable tbody').on('click',
            'button#moreDetails',
            function () {
                var data = table.row($(this).parents('tr')).data();
                $('.tableMoreDetails').html(
                    '<table class="table dtr-details" width="100%"><tbody><tr><td class"text-nowrap">Validation Code<td><td>' +
                    data[1] +
                    '</td></tr><tr><td>Code Description<td><td>' +
                    data[2] +
                    '</td></tr><tr><td>Show Self Pay<td><td>' +
                    data[3] +
                    '</td></tr><tr><td>Show Insurance<td><td>' +
                    data[4] +
                    '</td></tr><tr><td>Date Created<td><td>' +
                    data[5] +
                    '</td></tr></tbody></table>');
                $('#detailsTable').modal('show');
            });
        var oTable = $('#validationTable').DataTable();
        $('#searchValidationTable').keyup(function () {
            oTable.search($(this).val()).draw();
        });
        $('.toggle-vis').on('change',
            function (e) {
                e.preventDefault();
                // Get Column API Object
                var column = table.column($(this).attr('data-column'));
                // Toggle Visibility
                column.visible(!column.visible());
            });

        $(function () {
            $('#audit').DataTable(
                {
                    "columnDefs": [
                        {
                            targets: [0, 1],
                            className: "text-right",
                        },
                    ],
                    "paging": false,
                    "ordering": true,
                    "info": false,
                    "searching": true,
                    "responsive":
                    {
                        details: false
                    },
                });
        });
        $('#addValidationCode, #editValidationCode').on('shown.bs.modal',
            function () {
                $(this).find('input:first').focus();
            });
    });

    //-------------------------------------------------------
    // Functions / Data Calls
    //-------------------------------------------------------



    $('.filter-validation').on('click', function () {
        var lengthOfCheckBox = $("input.filter-validation:checkbox:not(:checked)").length;
        if (lengthOfCheckBox === 4)
            table.search("").draw();
        else {
            $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                if ($("#collapseOne #filterXoptA").is(":checked")) {
                    if (~data[3].toLowerCase().indexOf("yes")) return true;
                }
                if ($("#collapseOne #filterXoptB").is(":checked")) {
                    if (~data[3].toLowerCase().indexOf("no")) return true;
                }
                if ($("#collapseTwo #filterYoptA").is(":checked")) {
                    if (~data[4].toLowerCase().indexOf("yes")) return true;
                }
                if ($("#collapseTwo #filterYoptB").is(":checked")) {
                    if (~data[4].toLowerCase().indexOf("no")) return true;
                }
                return false;
            });
            table.draw();
            $.fn.dataTable.ext.search.pop();
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
            url:  AppConfig+'/ValidationCode/ExistValidationCode',
            async: false, cache :false,
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
            url:  AppConfig+'/ValidationCode/GetDetails',
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
            url:  AppConfig+'/ValidationCode/GetDetails',
            async: false, cache :false,
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
            filename: "Validation Code List.xls", //do not include extension,
            fileext: ".xls"
        });
    };

    RemoveMemory = function () {
        $("#serviceTypeCode").val('');
        $("#addCodeDescription").val('');
    };
    ActiveApply = function () {
        $("#columnPicker").removeClass("show")
    };
});