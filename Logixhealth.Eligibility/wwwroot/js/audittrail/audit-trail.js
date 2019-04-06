$(document).ready(function() {

    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------

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



    OpenAuditTrails = function(id, desc) {
        var currentId = id;
        var descriptionAuditTrail = desc;
        $.ajax({
            type: "Get",
            url: '/AuditTrail/GetDetails',
            async: false,
            data: { id: currentId, description: descriptionAuditTrail },
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
            filename: "Audit Trail List", //do not include extension,
            fileext: ".xls"
        });
    };



});