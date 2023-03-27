
function initializeDataTable(id) {
    $('#' + id).DataTable();
}

function destroyDataTable(id) {
    $('#' + id).DataTable().destroy();
}

function filterDataTable(id, columnIndex, filterValue) {
    $('#' + id).DataTable().column(columnIndex).search(filterValue).draw();
}

function sortDataTable(id, columnIndex, sortOrder) {
    var table = $('#' + id).DataTable();
    table.order([columnIndex, sortOrder]).draw();
}

//new updated Code
window.dataTable = {
    init: function (tableId, options) {
        //var dataTable = $('#' + tableId).DataTable({
        //    autoWidth: false,
        //    columns: columns.map(function (c) {
        //        return {
        //            title: c.title,
        //            data: function (row) {
        //                var key = Object.keys(row).find(function (k) {
        //                    return k.toLowerCase() === c.field.toLowerCase();
        //                });
        //                return row[key];
        //            }
        //        };
        //    }),
        //    data: data,
        //    paging: options.paging,
        //    searching: options.searching,
        //    ordering: options.ordering
        //});

        $('#' + tableId).DataTable(
            {
                autoWidth: false,
                paging: options.paging,
                searching: options.searching,
                ordering: options.ordering
            });
    }
}