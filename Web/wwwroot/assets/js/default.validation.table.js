var validationCodeColumnIndex = 0;
var CodeDescriptionColumnIndex = 1;
var showSelfPayColumnIndex = 2;
var showInsuranceColumnIndex = 3;
var DateCreatedColumnIndex = 4;
var selectedValidationFilterColumns = [validationCodeColumnIndex, CodeDescriptionColumnIndex, showSelfPayColumnIndex, showInsuranceColumnIndex, DateCreatedColumnIndex];
$(function () {
    var table = $('#validationTable').DataTable({
        order: [[5, 'desc']],
        "paging": false,
        "info": false,
        columns: [
            {
                title: "VALIDATION CODE", searchable: true,
                width: "10%"
            },
            {
                title: "CODE DESCRIPTION", searchable: true,
                width: "61"
            },
            {
                title: "SHOW SELF-PAY", searchable: true,
                width: "8%"
            },
            {
                title: "SHOW INSURANCE", searchable: true,
                width: "8%"
            },
            {
                title: "DATE CREATED", searchable: true,
                width: "8%"
            },

            {
                title: "ACTIONS", orderable: false, width: "5%"
            }
        ]
    });
    $('#validationTable tbody').on('click', 'button#moreDetails', function () {
        var data = table.row($(this).parents('tr')).data();
        $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td class"text-nowrap">Validation Code<td><td>' + data[1] + '</td></tr><tr><td>Code Description<td><td>' + data[2] + '</td></tr><tr><td>Show Self Pay<td><td>' + data[3] + '</td></tr><tr><td>Show Insurance<td><td>' + data[4] + '</td></tr><tr><td>Date Created<td><td>' + data[5] + '</td></tr></tbody></table>');
        $('#detailsTable').modal('show');
    });
    var oTable = $('#validationTable').DataTable();
    //$('#searchValidationTable').keyup(function () {
    //    oTable.search($(this).val()).draw();
    //});
    $('#validationCodeSearch').on('click', function () {
        var searchValue = $('#searchValidationTable').val();

        var searchTerm = searchValue.toLowerCase();

        $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
            for (var i = 0; i < selectedValidationFilterColumns.length; i++) {
                if (~data[selectedValidationFilterColumns[i]].toLowerCase().indexOf(searchTerm)) return true;
            }
            return false;
        });
        table.draw();
        $.fn.dataTable.ext.search.pop();
    });
    $('.toggle-vis').on('change', function (e) {
        e.preventDefault();
        // Get Column API Object
        var column = table.column($(this).attr('data-column'));
        // Toggle Visibility
        column.visible(!column.visible());
    });
    $('.validation-filter .toggle-vis-filter').on('change', function (e) {
        e.preventDefault();

        var columnIndexChecked = parseInt($(this).attr('data-column'));

        var checked = $(this).is(':checked');
        var filterColumnsArrayIndex = selectedValidationFilterColumns.indexOf(columnIndexChecked);

        if (checked) {
            if (filterColumnsArrayIndex === -1) {
                selectedValidationFilterColumns.push(columnIndexChecked);
            }
        }
        else {
            if (filterColumnsArrayIndex > -1) {
                selectedValidationFilterColumns.splice(filterColumnsArrayIndex, 1);
            }
        }
    });
});