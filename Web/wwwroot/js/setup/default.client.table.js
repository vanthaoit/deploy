/// <reference path="../global-const.js" />

$(document).ready(function () {

    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;
    var npiModalTable;
    var attachmentsTable;
    var table;

    $(function () {
        table = $('#clientTable').DataTable(
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
                "columnDefs": [
                    {
                        responsivePriority: 1,
                        targets: 1
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
                        "defaultContent": "<button id='moreDetails' class='btn btn-sm btn-link lh-0 mr-1'><svg class='icon icon-expand'><use xlink:href='#icon-expand'></use></svg></button>"
                    }]
            });
        $('#clientTable tbody').on('click', 'button#moreDetails', function () {
            var data = table.row($(this).parents('tr')).data();
            $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td>Insurance<td><td>' + data[1] + '</td></tr><tr><td>Payer ID<td><td>' + data[2] + '</td></tr><tr><td>Clients<td><td>' + data[3] + '</td></tr><tr><td>Self Pay<td><td>' + data[4] + '</td></tr></tbody></table>');
            $('#detailsTable').modal('show');
        });
        var oTable = $('#clientTable').DataTable();
        $('#searchClientTable').keyup(function () {
            oTable.search($(this).val()).draw();
        })
        $('.toggle-vis').on('change', function (e) {
            e.preventDefault();
            // Get Column API Object
            var column = table.column($(this).attr('data-column'));
            // Toggle Visibility
            column.visible(!column.visible());
        });
    });
    $(function () {
        $('#audit').DataTable(
            {
                "columnDefs": [
                    {
                        targets: [1, 2],
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
        var oTable = $('#audit').DataTable();
        $('#searchAudit').keyup(function () {
            oTable.search($(this).val()).draw();
        });
    });

    
    
    //-------------------------------------------------------
    // Functions / Data Calls
    //-------------------------------------------------------

    

    OpenUpdateStatus = function (id,name) {
        var currentId = id;
        var currentName = name;
        var response;
        $.ajax({
            type: "Get",
            url:  AppConfig+'/Client/PutStatus',
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
        $("#updateStatus").html("");
        var appendStatus = "";
        if (response.setupStatus.toLowerCase() === "new") {
            appendStatus = "<option ></option>" +
                "<option value='Active'>Active</option>" +
                "<option value='On Hold'>On Hold</option>" +
                "<option value='Term'>Term</option>";
        } else if (response.setupStatus.toLowerCase() === "active") {
            appendStatus = "<option ></option>" +
                "<option value='New'>New</option>" +
                "<option value='On Hold'>On Hold</option>" +
                "<option value='Term'>Term</option>";
        } else if (response.setupStatus.toLowerCase() === "term") {
            appendStatus = "<option ></option>" +
                "<option value='New'>New</option>" +
                "<option value='Active'>Active</option>" +
                "<option value='On Hold'>On Hold</option>";
               
        }
        else {
            appendStatus = "<option ></option>" +
                "<option value='New'>New</option>" +
                "<option value='Active'>Active</option>" +
                "<option value='Term'>Term</option>";
        }
        $("#updateStatus").append(appendStatus);

                
        $("#updateStatusModal").modal("show");
 
};

    OpenNPI = function (clientName,id) {
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
    function activateViewAttachementsTable() {
        //this sets up the table
        attachmentsTable = $('#attachments').DataTable(
            {
                "columnDefs": [
                    {
                        targets: [3],
                        className: "text-center"
                    }
                ],
                "paging": false,
                "ordering": true,
                "info": false,
                "searching": true
            });
        //this allows searching function
        $('#searchAttachmentsTable').on('keyup', function () {
            attachmentsTable.search(this.value).draw();
        });
    };
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

});