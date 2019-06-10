/// <reference path="../global-const.js" />

$(document).ready(function () {

    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;

    //global variables for tables
    var stockTable;
    var catalogTable;


    $(function () {
        stockTable = $('#insuranceList').DataTable(
            {
                "responsive":
                {
                    "details":
                    {
                        "type": 'column',
                        "target": 0
                    }
                },
                "columnDefs": [
                    {
                        targets: 0,
                        className: 'control',
                    },
                    {
                        targets: [0, 1],
                        className: "text-center",
                        orderable: false
                    },],
                "order": [2, 'asc'],
                "searching": true,
                "bLengthChange": false,
                "paging": false,
                "info": false,
                "scrollCollapse": true,
                "bFilter": false,
                "scrollY": '50vh',
                //"responsive":
                //{
                //    "details": false
                //},
            });

        catalogTable = $('#selectedInsurance').DataTable(
            {
                responsive:
                {
                    details:
                    {
                        type: 'column',
                        target: 0
                    }
                },
                columnDefs: [
                    {
                        targets: 0,
                        className: 'control',
                    },
                    {
                        targets: [0, 1],
                        className: "text-center",
                        width: "5%",
                        orderable: false
                    },
                ],
                order: [2, 'asc'],
                searching: true,
                bLengthChange: false,
                paging: false,
                info: false,
                scrollCollapse: true,
                bFilter: false,
                "scrollY": '50vh',
                //responsive:
                //{
                //    details: false
                //},
            });
        insuranceList = $('#insuranceList').DataTable();
        selectedInsurance = $('#selectedInsurance').DataTable();
        var tr,
            row,
            rowData;
        $('#insuranceList').on('click', 'input[type=checkbox]', function () {
            if ($(this).is(':checked')) {
                $(this).attr('checked', 'checked');
                tr = $(this).closest('tr');
                row = insuranceList.row(tr);
                rowData = [];
                tr.find('td').each(function (i, td) {
                    rowData.push($(td).html());
                });
                row.remove().draw();
                selectedInsurance.row.add(rowData).draw();
            }
        });
        $('#selectedInsurance').on('click', 'input[type=checkbox]', function () {
            if ($(this).not(':checked')) {
                $(this).removeAttr('checked', 'checked');
                tr = $(this).closest('tr');
                row = selectedInsurance.row(tr);
                rowData = [];
                tr.find('td').each(function (i, td) {
                    rowData.push($(td).html());
                });
                row.remove().draw();
                insuranceList.row.add(rowData).draw();
            }
        });

    });

    SubmitEditclientConfig = function () {
        var selectedInsurances = [];
        var foo = {};
        $("#selectedInsurance > tbody > tr").each(function () {
            foo = {};
            //foo[]
            var currentRow = $(this); //Do not search the whole HTML tree twice, use a subtree instead

            foo["Id"] = currentRow.find(".Id").val();
            foo["Name"] = currentRow.find(".Name").val();
            foo["PayerId"] = currentRow.find(".PayerId").val();
            foo["SaveLocation"] = currentRow.find(".SaveLocation").val();
           
            foo["Location271"] = currentRow.find(".Location271").val();
            foo["FileNameConvention"] = currentRow.find(".FileNameConvention").val();
            foo["IsSeqNumEnabled"] = currentRow.find(".IsSeqNumEnabled").val();
            foo["State"] = currentRow.find(".State").val();
            selectedInsurances.push(foo);
            //alert(currentRow.find(".FieldNameID").text() + " " + currentRow.fint("#OperatorID").text());
        });
        var mdClientId = $('.MdClientId').val();
        if (selectedInsurances.length !== 0 && mdClientId !== null) {
            $.ajax({
                type: "Post",
                url:  AppConfig+'/Client/PostEditClientConfig',
                async: false, cache :false,
                data: { listMdPayorConfig: selectedInsurances, mdClientId: mdClientId},
                success: function (data) {
                    var response = data;
                    
                }
            });
        }
        return true;
    };


    //Search functions for data tables

    $('#searchInsuranceList').on('keyup', function () {
        stockTable.search(this.value).draw();
    });

    $('#searchValidationTable').on('keyup', function () {
        catalogTable.search(this.value).draw();
    });

    $('#insuranceListState').on('select2:select', function () {
        stockTable.search(this.value).draw();
    });

    $('#selectedInsurancesState').on('select2:select', function () {
        catalogTable.search(this.value).draw();
    });

});