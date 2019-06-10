/// <reference path="../global-const.js" />

$(document).ready(function () {
    //-------------------------------------------------------
    // VIEW FUNCTIONS - DO NOT REMOVE
    //-------------------------------------------------------
    var AppConfig = RootURL.prefixURL;

    //global variables for tables
    var stockTable;
    var catalogTable;

    table = $('#linkedInsurancesTable').DataTable({
        "aaSorting": [
            [2, "asc"]
        ],
        "paging": false,
        "ordering": true,
        "info": false,
        "searching": true,
        "scrollY": '50vh',
        "responsive": {
            details: false
        },
        "columnDefs": [{
            targets: 0,
            className: 'control',
        }, {
            responsivePriority: 1,
            targets: 2
        }, {
            targets: [1],
            className: "text-center",
            orderable: false,
            searching: false
        }, {
            // More Details Button
            "targets": 0,
            "orderable": false,
            "data": null,
            "defaultContent": "<button id='moreDetails' class='btn btn-sm btn-link lh-0 mr-1'><svg class='icon icon-expand'><use xlink:href='#icon-expand'></use></svg></button>"
        }]
    });
    $('#linkedInsurancesTable tbody').on('click', 'button#moreDetails', function () {
        var data = table.row($(this).parents('tr')).data();
        $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td>Insurance Name<td><td>' + data[2] + '</td></tr><tr><td>Payer Number<td><td>' + data[3] + '</td></tr><tr><td>State<td><td>' + data[4] + '</td></tr><tr><td>City<td><td>' + data[5] + '</td></tr></tbody></table>');
        $('#detailsTable').modal('show');
    });
    var oTable = $('#linkedInsurancesTable').DataTable();
    $('#searchLinkedInsurancesTable').keyup(function () {
        oTable.search($(this).val()).draw();
    });
    $('.toggle-vis').on('change', function (e) {
        e.preventDefault();
        // Get Column API Object
        var column = table.column($(this).attr('data-column'));
        // Toggle Visibility
        column.visible(!column.visible());
    });
    $('#attachments').DataTable({
        "columnDefs": [{
            targets: [3],
            className: "text-center",
            orderable: false,
        },],
        "paging": false,
        "ordering": true,
        "info": false,
        "searching": false
    });

    // Add Schedule Select 2
    $('.addSched').select2({ //apply select2 to my element
        placeholder: "Select...",
        containerCssClass: 'form-control-sm'
    });
    $(document).on("click", ".button-remove", function () {
        $(".button-add").attr("disabled", false);
        $(this).closest(".tooltest0").remove();
        var noOfDivs = $('.tooltest0').length - 1;
        if (noOfDivs !== 0)
            $("#tooltest" + noOfDivs + " [name='show-hide-" + noOfDivs + "']").show();
    });
    $('.button-add').click(function () {
        var noOfDivs = $('.tooltest0').length;
        if (noOfDivs < 8) {
            $('.addSched').select2("destroy");
            var clonedDiv = $('.tooltest0').first().clone(true);
            clonedDiv.find("input").val("");
            clonedDiv.find("select").val("");
            clonedDiv.insertBefore("#schedule-placeholder");
            clonedDiv.attr('id', 'tooltest' + noOfDivs);
            $('.addSched').select2({ //apply select2 to my element
                placeholder: "Select...",
                containerCssClass: 'form-control-sm'
            });
            $("#tooltest" + noOfDivs + " .ClientId").attr('name', '[' + noOfDivs + '].ClientId');
            $("#tooltest" + noOfDivs + " .IsSelfPay").attr('name', '[' + noOfDivs + '].IsSelfPay');
            $("#tooltest" + noOfDivs + " .DateDifference").attr('name', '[' + noOfDivs + '].DateDifference');
            $("#tooltest" + noOfDivs + " .RunTime").attr('name', '[' + noOfDivs + '].RunTime');
            $("#tooltest" + noOfDivs + " .RunDays").attr('name', '[' + noOfDivs + '].RunDays');
            $("#tooltest" + noOfDivs + " .ResponceTime").attr('name', '[' + noOfDivs + '].ResponceTime');
            $("#tooltest" + noOfDivs + " .button-remove").attr('name', 'show-hide-' + noOfDivs);
            $(".tooltest0 .button-remove").hide();
            $('.tooltest0').each(function (e, i) {
                $(this).find('.count_agent').text($(this).index() + 1);
            });
            $("#tooltest" + noOfDivs + " [name='show-hide-" + noOfDivs + "']").show();
            if (noOfDivs === 7) $(".button-add").attr("disabled", true);
        }
    });
    $('label.custom-control-label.isSeq').on('click', function (e) {
        if ($('label.custom-control-label.isSeq').hasClass('off') === true
            && $('label.custom-control-label.isSeq').hasClass('on') === false) {
            $('input.custom-control-input.isSeq').removeAttr("checked");
            $('input.custom-control-input.isSeq').val(false);
        } else if ($('label.custom-control-label.isSeq').hasClass('off') === false
            && $('label.custom-control-label.isSeq').hasClass('on') === true) {
            $('input.custom-control-input.isSeq').attr("checked", "checked");
            $('input.custom-control-input.isSeq').val(true);
        }
    });

    SubmitCreatePayerConfig = function () {
        var justcontinue = false;
        var noOfDivs = $('.tooltest0').length;
        $(".totalSchedule").val(noOfDivs);
        var convertRunDay = "";
        for (var i = 0; i < noOfDivs; i++) {
            var index = i + 1;
            var currentPointer = "#tooltest" + i + " .SelectRunDays";
            var lengthOfCurrentRunDay = $(currentPointer).val().length;
            for (var j = 0; j < lengthOfCurrentRunDay; j++) {
                if (j === 0)
                    convertRunDay = $(currentPointer).val()[j];
                else
                    convertRunDay = convertRunDay + "," + $(currentPointer).val()[j];
            }
            $("#tooltest" + i + " .RunDays").val(convertRunDay);
            convertRunDay = "";
        }

        $("#linkedInsurancesTable > tbody .insurance-checked:checked").each(function (e, i) {
            var currentRow = $(this).parent().parent().parent(); //Do not search the whole HTML tree twice, use a subtree instead

            currentRow.find(".Id").attr("name", "[" + e + "].Id");
            currentRow.find(".CarrierName").attr("name", "[" + e + "].CarrierName");
            currentRow.find(".PayerId").attr("name", "[" + e + "].PayerId");
            currentRow.find(".State").attr("name", "[" + e + "].State");
            currentRow.find(".City").attr("name", "[" + e + "].City");
        });

        var payerFileName = $("#payerFileName").val();
        var oldValue = $(".hiddenFileName").val();

        $.ajax({
            type: "Get",
            url: AppConfig + '/Payer/IsAppendSequentialNumer',
            async: false, cache: false,
            data: { fileName: payerFileName },
            success: function (data) {
                var response = data;
                if (response !== undefined) {

                    var isChecked = $("#IsSeqNumEnabled").val();
                    
                    if (oldValue === null || oldValue === undefined) {
                        if (isChecked === "true") {
                            compareFileNameString(payerFileName, response);

                            justcontinue = true;
                        } else {
                            $(".submit-payer-config span.validation-notification")
                                .text("The file name is already in use.Please select a different file name or tick on the sequential numbers checkbox.")
                                .show()
                                .fadeOut(7000);
                            $("#payerFileName").focus();
                            justcontinue = false;
                        }
                    } else {
                        if (oldValue === payerFileName) {
                            justcontinue = true;
                        } else {
                            if (isChecked === "true") {
                                compareFileNameString(payerFileName, response);

                                justcontinue = true;
                            } else {
                                $(".submit-payer-config span.validation-notification")
                                    .text("The file name is already in use.Please select a different file name or tick on the sequential numbers checkbox.")
                                    .show()
                                    .fadeOut(7000);
                                $("#payerFileName").focus();
                                justcontinue = false;
                            }
                        }
                    }
                } else {
                    justcontinue = true;
                }
            }
        });

        return justcontinue;
    };

    function compareFileNameString(payerFileName, response) {
        var replace = "";
        var payerFileName_ = payerFileName.replace(".txt", "");
        var count = 0;
        if (response.length > 0) {
            for (var i = 0; i < response.length; i++) {
                if (payerFileName_ === replaceString(response[i].fileNameConvention))
                    count++;
            }
        }
        replace = payerFileName.replace(".txt", "(" + count + ").txt");
        $("#payerFileName").val(replace);
    }
    function replaceString(input) {
        var replaceS = input.replace(".txt", "");
        var lengthOfInput = replaceS.length;
        var res1 = replaceS.substr(lengthOfInput - 3, 1);
        var res2 = replaceS.substr(lengthOfInput - 4, 1);
        var convert = "";
        if (res1 === "(") {
            convert = replaceS.substr(0, lengthOfInput - 3);
            return convert;
        }

        else if (res2 === "(") {
            convert = replaceS.substr(0, lengthOfInput - 4);
            return convert;
        } else
            return replaceS;
    }
});