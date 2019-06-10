/// <reference path="../global-const.js" />

var AppConfig = RootURL.prefixURL;
$(document).ready(function () {

    OpenManipulateNote = function (indexNote, id, desc) {
        var currentIndex = indexNote;
        var currentId = id;
        var descriptionNote = desc;
        $.ajax({
            type: "Get",
            url:  AppConfig+'/Note/GetDetails',
            async: false, cache :false,
            data: { pointerNote: currentIndex, id: currentId, description: descriptionNote },
            success: function (data) {
                var response = data;
                $('#PreviewNotes').html(data);
                $("#addNote").modal("show");
                
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    };

    SubmitManipulateNote = function () {
        debugger
        foo = {};
    
        foo["TableName"] = $("#addNote .TableName").val();
        foo["RecordId"] = $("#addNote .RecordId").val();
        foo["Type"] = $("#addNote .Type").val();
        foo["Artifact"] = $("#addNote .Artifact").val();
        $.ajax({
            type: "Post",
            url: AppConfig + '/Note/PostNote',
            async: false, cache: false,
            data: { data: foo},
            success: function (data) {
                var response = data;
                $("#addNote .Artifact").val("");
                $('#reviewSubNotes').html("");
                $('#reviewSubNotes').html(data);

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