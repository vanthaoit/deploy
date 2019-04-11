var serviceTypeCodeColumnIndex = 0;
var CodeDescriptionColumnIndex = 1;
var DateCreatedColumnIndex = 2;
var selectedServiceFilterColumns = [serviceTypeCodeColumnIndex, CodeDescriptionColumnIndex, DateCreatedColumnIndex];
$(function () {
    var table = $('#serviceTypeCodeTable').DataTable(
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
            columns: [
                {
                    data: "ServiceTypeCode", title: "SERVICE TYPE CODE", searchable: true,
                    width: "10%"
                },
                {
                    data: 'CodeDescription', title: "CODE DESCRIPTION", searchable: true,
                    width: "65%"
                },
                {
                    data: "DateCreated", title: "DATE CREATED", searchable: true,
                    width: "15%"
                },
                {
                    data: "Actions", title: "ACTIONS", orderable: false, width: "10%"
                }
            ]
        });
    $('#serviceTypeCodeTable tbody').on('click', 'button#moreDetails', function () {
        var data = table.row($(this).parents('tr')).data();
        $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td>Service Type Code<td><td>' + data[1] + '</td></tr><tr><td>Code Description<td><td>' + data[2] + '</td></tr><tr><td>Date Created<td><td>' + data[3] + '</td></tr></tbody></table>');
        $('#detailsTable').modal('show');
    });
    var oTable = $('#serviceTypeCodeTable').DataTable();
    $('#searchserviceTypeCodeTable').keyup(function () {
        oTable.search($(this).val()).draw();
    });
    $('.toggle-vis').on('change', function (e) {
        e.preventDefault();
        // Get Column API Object
        var column = table.column($(this).attr('data-column'));
        // Toggle Visibility
        column.visible(!column.visible());
    });

    $('#serviceTypeCodeSearch').on('click', function () {
        var searchValue = $('#searchServiceTable').val();

        var searchTerm = searchValue.toLowerCase();

        $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
            for (var i = 0; i < selectedServiceFilterColumns.length; i++) {
                if (~data[selectedServiceFilterColumns[i]].toLowerCase().indexOf(searchTerm)) return true;
            }
            return false;
        });
        table.draw();
        $.fn.dataTable.ext.search.pop();
    });

    $('.service-type-code-filter .toggle-vis-filter').on('change', function (e) {
        e.preventDefault();

        var columnIndexChecked = parseInt($(this).attr('data-column'));

        var checked = $(this).is(':checked');
        var filterColumnsArrayIndex = selectedServiceFilterColumns.indexOf(columnIndexChecked);

        if (checked) {
            if (filterColumnsArrayIndex === -1) {
                selectedServiceFilterColumns.push(columnIndexChecked);
            }
        }
        else {
            if (filterColumnsArrayIndex > -1) {
                selectedServiceFilterColumns.splice(filterColumnsArrayIndex, 1);
            }
        }
    });
});
