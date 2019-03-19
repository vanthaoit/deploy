$(function () {
    var table = $('#validationTable').DataTable({
        "aaSorting": [
            [5, "asc"]],
        "paging": false,
        "info": false
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
        $('#validationTable').DataTable();
        oTable.search($('#searchValidationTable').val()).draw();
    });
	$('.toggle-vis').on('change', function (e) {
		e.preventDefault();
		// Get Column API Object
		var column = table.column($(this).attr('data-column'));
		// Toggle Visibility
		column.visible(!column.visible());
	});
});

