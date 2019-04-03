var dateColumnIndex = 0;
var timeColumnIndex = 1;
var actionColumnIndex = 2;
var changeFromColumnIndex = 3;
var changeToColumnIndex = 4;
var usersColumnIndex = 5;
var selectedFilterColumns = [dateColumnIndex, timeColumnIndex, actionColumnIndex, changeFromColumnIndex, changeToColumnIndex, usersColumnIndex];
$(function () {
    var table_ = $('#auditTable').DataTable();
    //{
    //    "aaSorting": [
    //        [0, "asc"]
    //    ],
    //    "paging": false,
    //    "ordering": false,
    //    "info": false
    //});
    $('#auditTable tbody').on('click', 'button#moreDetails', function () {
        var data = table_.row($(this).parents('tr')).data();
        $('.tableMoreDetails').html('<table class="table dtr-details" width="100%"><tbody><tr><td class"text-nowrap">Validation Code<td><td>' + data[1] + '</td></tr><tr><td>Code Description<td><td>' + data[2] + '</td></tr><tr><td>Show Self Pay<td><td>' + data[3] + '</td></tr><tr><td>Show Insurance<td><td>' + data[4] + '</td></tr><tr><td>Date Created<td><td>' + data[5] + '</td></tr></tbody></table>');
        $('#detailsTable').modal('show');
    });
    //var oTable = $('#validationTable').DataTable();
    //$('#searchValidationTable').keyup(function () {
    //    oTable.search($(this).val()).draw();
    //})
    $('.toggle-filter').on('change', function (e) {
        e.preventDefault();
        // Get Column API Object
        var columnIndexChecked = parseInt($(this).attr('data-column'));
        //var columnIndexChecked = table_.column($(this).attr('data-column')).selector.cols;

        var checked = $(this).is(':checked');
        var filterColumnsArrayIndex = selectedFilterColumns.indexOf(columnIndexChecked);

        if (checked) {
            if (filterColumnsArrayIndex === -1) {
                selectedFilterColumns.push(columnIndexChecked);
            }
        }
        else {
            if (filterColumnsArrayIndex > -1) {
                selectedFilterColumns.splice(filterColumnsArrayIndex, 1);
            }
        }
    });

    AuditTrail_GlobalFilter = function () {
        //RemoveFilter();
        var searchValue = $('#auditTrailFilters').val();

        var searchTerm = searchValue.toLowerCase();

        $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
            for (var i = 0; i < selectedFilterColumns.length; i++) {
                if (~data[selectedFilterColumns[i]].toLowerCase().indexOf(searchTerm)) return true;
            }
            //search only in column 1 and 2
            //if (~data[1].toLowerCase().indexOf(searchTerm)) return true;
            //if (~data[2].toLowerCase().indexOf(searchTerm)) return true;
            return false;
        });
        table_.draw();
        $.fn.dataTable.ext.search.pop();
    };

    function RemoveFilter(index) {
        if (index === undefined || index === null || index === -1) {
            table_.columns().search('');
            //.search('')
        }
        else {
            table_.columns(index).search('');
        }
    }

});