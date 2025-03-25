//function confirmDeleteClassOrder(id,e,title) {

//    e.preventDefault();
//    var url = "/UserManagement/Order/Delete/" + id;
//    Swal.fire({
//        text: `آیا از حذف کلاس ${title} از فاکتور مطمئن هستید؟`,
//        icon: "question",
//        showCancelButton: true,
//        confirmButtonColor: "#d33",
//        cancelButtonColor: "#3085d6",
//        confirmButtonText: "بله",
//        cancelButtonText: "خیر"
//    }).then((result) => {
//        if (result.isConfirmed) {
//            location.href = url;
//        }
//    });
//}
function confirmDeleteClassOrder(id, e, title) {

    e.preventDefault();

    Swal.fire({
        text: `آیا از حذف کلاس ${title} از فاکتور مطمئن هستید؟`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/UserManagement/Order/Delete/" + id, function () {
                $("#tr_" + id).hide('slow');
            });
        }
    });
};