/// <reference path="../global-const.js" />

$(document).ready(function () {

    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;

    function initializeAuditTable() {

        $('#auditTable').DataTable(
            {
                "columnDefs": [
                    {
                        targets: [1, 2],
                        className: "text-right",
                    }
                ],
                "paging": false,
                "ordering": true,
                "info": false,
                "searching": true,
                //"scrollY": '60vh',
                "responsive":
                {
                    details: false
                }
            });
        var oTable = $('#auditTable').DataTable();
        $('#search-Audit').keyup(function() {
            oTable.search($(this).val()).draw();
        });

    }

    //-------------------------------------------------------
    // Functions / Data Calls
    //-------------------------------------------------------



    OpenAuditTrails = function (indexAu, id, desc) {
        var currentIndex = indexAu;
        var currentId = id;
        var descriptionAuditTrail = desc;
        $.ajax({
            type: "Get",
            url:  AppConfig+'/AuditTrail/GetDetails',
            async: false,
            cache: false,
            data: { pointerAudit: currentIndex, id: currentId, description: descriptionAuditTrail },
            success: function(data) {
                var response = data;
                $('#PreviewAuditTrails').html(data);

                $("#viewTrail").modal("show");
                //$('#auditTable').DataTable();
                initializeAuditTable();
            },
            failure: function(response) {
                alert(response.responseText);
            },
            error: function(response) {
                alert(response.responseText);
            }
        });
    };

    ExportAuditTrailList = function() {
        $("#auditTable").table2excel({
            //exclude: ".excludeThisClass",
            name: "Audit Trail List",
            filename: "Audit Trail List.xls", //do not include extension,
            fileext: ".xls"
        });
    };



});