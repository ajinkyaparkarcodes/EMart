$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    var dataTable = $('#producttable').DataTable({
        "ajax": {
            "url": '/admin/product/getall',
           },
        "columns": [
            { "data": "id", "className": "text-center" },
            { "data": "name", "className": "text-center" },
            { "data": "description", "className": "text-center" },
            { "data": "listPrice", "className": "text-center" },
            {
                "data": null,
                "className": "text-center",
                "render": function (data, type, full, meta) {
                    return data.packSizeValue + ' ' + data.packSizeUnit;
                }
            },
            { "data": "category.name", "className": "text-center" },
            {
                data: 'id',
                "className": "text-center",
                "render": function (data) {
                    return `<div class="btn-group w-75">
                                <a href="/admin/product/editproduct?id=${data}" class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-square"></i>Edit
                                </a>
                                <a onClick=ConfirmDelete('/admin/product/deleteproduct?id=${data}') class="btn btn-danger mx-2">
                                    <i class="bi bi-trash-fill"></i>Delete
                                </a>
                            </div>`;
                }
            }
        ]
    });
}
function ConfirmDelete(productId) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            // User confirmed the deletion
            window.location.href = productId;
        }
    });
}

