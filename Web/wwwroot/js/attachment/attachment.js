/// <reference path="../global-const.js" />

$(document).ready(function () {
    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;
    

    //-------------------------------------------------------
    // Functions / Data Calls
    //-------------------------------------------------------

    var attachmentsTable;

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

    OpenViewAttachment = function (pointerAu,id, description) {
        var currentId = id;
        var currentDescription = description;
        var pointerPosition = pointerAu;
        $.ajax({
            type: "Get",
            url: AppConfig + '/Note/GetViewAttachment',
            async: false,
            cache: false,
            data: { pointerPosition: pointerPosition ,id: currentId, description: currentDescription },
            success: function (data) {
                var response = data;
                $('#PreviewViewAttachment').html('');
                $('#PreviewViewAttachment').html(data);
                activateViewAttachementsTable();
                $("#viewAttachments").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    DeleteViewAttachment = function (pointerAu,id, description) {
        var currentId = id;
        var currentDescription = description;
        var pointerPosition = pointerAu;
        $.ajax({
            type: "Post",
            url: AppConfig + '/Note/DeleteAttachment',
            async: false,
            cache: false,
            data: { pointerPosition: pointerPosition ,idAttach: currentId, recordId: currentDescription },
            success: function (data) {
                var response = data;
                //$(".modal-backdrop.fade.show").remove();
                $('#subViewAttachments').html('');
                $('#subViewAttachments').html(data);
                activateViewAttachementsTable();
                $("#viewAttachments").modal("show");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
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
});