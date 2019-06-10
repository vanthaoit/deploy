/// <reference path="../global-const.js" />

$(document).ready(function () {
    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;
    var npiModalTable;
    var table;

    table = $('#payerTable').DataTable(
        {
            "aaSorting": [
                [1, "asc"]
            ],
            "paging": false,
            "ordering": true,
            "info": false,
            "searching": true,
            "scrollY": '50vh',
            "responsive":
            {
                details: false
            },
            columns: [
                {
                    data: "More", title: "", orderable: false, width: "3%"
                },
                {
                    data: "Insurance", title: "INSURANCE", searchable: true,
                    width: "16%"
                },
                {
                    data: 'Payer ID', title: "PAYER ID", searchable: true,
                    width: "6%"
                },
                {
                    data: "Client(s)", title: "CLIENT(S)", searchable: true,
                    width: "20%"
                },
                {
                    data: "Self Pay", title: "SELF PAY", searchable: true, width: "6%"
                },
                {
                    data: "File Name", title: "FILE NAME", searchable: true,
                    width: "20%"
                },
                {
                    data: 'Date Diff', title: "DATE DIFF", searchable: true,
                    width: "6%"
                },
                {
                    data: "Run Time", title: "RUN TIME", searchable: true,
                    width: "6%"
                },
                {
                    data: "Run Day(s)", title: "RUN DAY(S)", searchable: true, width: "6%"
                },
                {
                    data: "271 Response Time", title: "271 RESPONSE TIME", searchable: true,
                    width: "6%"
                },
                {
                    data: "Actions", title: "ACTIONS", orderable: false, width: "5%"
                }
            ],
            "columnDefs": [
                {
                    responsivePriority: 1,
                    targets: -1
                },
                {
          
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
    $('#payerTable tbody').on('click', 'button#moreDetails', function () {
        var data = table.row($(this).parents('tr')).data();
        $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td>Insurance<td><td>' + data[1] + '</td></tr><tr><td>Payer ID<td><td>' + data[2] + '</td></tr><tr><td>Clients<td><td>' + data[3] + '</td></tr><tr><td>Self Pay<td><td>' + data[4] + '</td></tr><tr><td>File Name<td><td>' + data[5] + '</td></tr><tr><td>Date Difference<td><td>' + data[6] + '</td></tr><tr><td>Run Time<td><td>' + data[7] + '</td></tr><tr><td>Run Days<td><td>' + data[8] + '</td></tr><tr><td>Status<td><td>' + data[9] + '</td></tr></tbody></table>');
        $('#detailsTable').modal('show');
    });
    var oTable = $('#payerTable').DataTable();
    $('#searchPayerTable').keyup(function () {
        oTable.search($(this).val()).draw();
    });
    $('.toggle-vis').on('change', function (e) {
        e.preventDefault();
        // Get Column API Object
        var column = table.column($(this).attr('data-column'));
        // Toggle Visibility
        column.visible(!column.visible());
    });

    //-------------------------------------------------------
    // Functions / Data Calls
    //-------------------------------------------------------

    $('.filter-payer').on('click', function () {
        var lengthOfCheckBox = $("input.filter-payer:checkbox:not(:checked)").length;
        if (lengthOfCheckBox === 2)
            table.search("").draw();
        else {
            $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                if ($("#selfPayYes").is(":checked")) {
                    if (~data[4].toLowerCase().indexOf("yes")) return true;
                }
                if ($("#selfPayNo").is(":checked")) {
                    if (~data[4].toLowerCase().indexOf("no")) return true;
                }
                return false;
            });
            table.draw();
            $.fn.dataTable.ext.search.pop();
        }
    });

    OpenUpdateStatus = function (id, name) {
        var currentId = id;
        var currentName = name;
        var response;
        $.ajax({
            type: "Get",
            url: AppConfig + '/Client/PutStatus',
            data: { id: currentId },
            async: false, cache :false,
            success: function (data) {
                response = data;
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
        $('#updateStatusModal input[name=Id]').val(response.id);
        $('#updateStatusModal input[name=ClientName]').val(response.clientName);

        $('#updateStatusModal input[name=NaturalId]').val(response.naturalId);
        $('#updateStatusModal input[name=Address1]').val(response.address1);
        $('#updateStatusModal input[name=Address2]').val(response.address2);
        $('#updateStatusModal input[name=City]').val(response.city);
        $('#updateStatusModal input[name=State]').val(response.state);
        $('#updateStatusModal input[name=ZipCode]').val(response.zipCode);
        $('#updateStatusModal input[name=NPI]').val(response.npi);
        $('#updateStatusModal input[name=TaxId]').val(response.taxId);
        $('#updateStatusModal input[name=ClientCode]').val(response.clientCode);

        $('#updateStatusModal input[name=IsMainLocation]').val(response.isMainLocation);
        $('#updateStatusModal input[name=ParentNaturalId]').val(response.parentNaturalId);
        $('#updateStatusModal input[name=SetupStatus]').val(response.setupStatus);
        $('#updateStatusModal input[name=ClientType]').val(response.clientType);

        $("#updateStatusModal").modal("show");
    };

    OpenNPI = function (clientName, id) {
        var currentId = id;
        var currentClientName = clientName;

        $.ajax({
            type: "Get",
            url: AppConfig + '/Client/GetLocationsByNaturalId',
            data: { clientName: currentClientName, id: currentId },
            success: function (data) {
                var response = data;
                $('#PreviewNPI').html('');
                $('#PreviewNPI').html(data);
                activateNpiModalTable();
                $("#sharedNPIModal").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    submitUpdateStatus = function () {
        var chooseStatus = $('#updateStatus').val();
        if (chooseStatus === "") {
            $("#updateStatusModal span.validation-notification")
                .text("Please choose status for Client.")
                .show()
                .fadeOut(5000);
            return false;
        }
        return true;
    };

    function activateNpiModalTable() {
        //this sets up the table
        npiModalTable = $('#sharedNPI').DataTable(
            {
                responsive:
                {
                    details:
                    {
                        type: 'column',
                        target: 0
                    }
                },
                order: [2, 'asc'],
                searching: true,
                bLengthChange: false,
                paging: false,
                info: false,
                scrollCollapse: true,
                bFilter: false,
            });

        //this allows searching function
        $('#npiModalSearch').on('keyup', function () {
            npiModalTable.search(this.value).draw();
        });
    }
    $('.filter-client').on('click', function () {
        var lengthOfCheckBox = $("input.filter-client:checkbox:not(:checked)").length;
        if (lengthOfCheckBox === 7)
            table.search("").draw();
        else {
            $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                if ($("#collapseOne #typeEmergency").is(":checked")) {
                    if (~data[3].toLowerCase().indexOf("emergency department")) return true;
                }
                if ($("#collapseOne #typeSpecialty").is(":checked")) {
                    if (~data[3].toLowerCase().indexOf("specialty")) return true;
                }
                if ($("#collapseOne #typeCombined").is(":checked")) {
                    if (~data[3].toLowerCase().indexOf("combined")) return true;
                }
                if ($("#collapseTwo #statusNew").is(":checked")) {
                    if (~data[5].toLowerCase().indexOf("new")) return true;
                }
                if ($("#collapseTwo #statusActive").is(":checked")) {
                    if (~data[5].toLowerCase().indexOf("active")) return true;
                }
                if ($("#collapseTwo #statusTerm").is(":checked")) {
                    if (~data[5].toLowerCase().indexOf("term")) return true;
                }
                if ($("#collapseTwo #statusHold").is(":checked")) {
                    if (~data[5].toLowerCase().indexOf("on hold")) return true;
                }
                return false;
            });
            table.draw();
            $.fn.dataTable.ext.search.pop();
        }
    });

    ExportClientList = function () {
        $("#clientTable").table2excel({
            //exclude: ".excludeThisClass",
            name: "Client Setup",
            filename: "Client Setup.xls", //do not include extension,
            fileext: ".xls"
        });
    };

    DeleteTarget = function (id) {
        var currentCode = id;
        var isDelete = true;
        $.ajax({
            type: "Get",
            url: AppConfig + '/Payer/GetDetails',
            async: false, cache :false,
            data: { id: currentCode, isDelete: isDelete },
            success: function (data) {
                var response = data;
                $('#PreviewDelete').html('');
                $('#PreviewDelete').html(data);
                $("#deleteInsurance").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };
    ExportPayerList = function () {
        $("#payerTable").table2excel({
            //exclude: ".excludeThisClass",
            name: "Payer Configuration Setup",
            filename: "Payer Configuration Setup.xls", //do not include extension,
            fileext: ".xls"
        });
    };
});