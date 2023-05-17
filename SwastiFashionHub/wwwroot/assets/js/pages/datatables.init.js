
function initializeDataTable(id) {
    $('#' + id).DataTable();
}

function destroyDataTable(id) {
    $('#' + id).DataTable().destroy();
}

function filterDataTable(id, columnIndex, filterValue) {
    $('#' + id).DataTable().column(columnIndex).search(filterValue).draw();
}

var dataTableObj
//new updated Code
window.dataTable = {
    init: function (tableId, options) {
        dataTableObj = $('#' + tableId).DataTable(
            {
                autoWidth: false,
                aoColumnDefs: [{
                    bSortable: false,
                    aTargets: ['nosort']
                }],
                paging: options.paging,
                searching: options.searching,
                ordering: options.ordering
            });
    },

    refreshDataTable: function () {
        debugger
        // Redraw the DataTable
        dataTableObj.draw();
    },
}
