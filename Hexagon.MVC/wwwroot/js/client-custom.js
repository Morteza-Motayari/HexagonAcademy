function fillPageId(pageId) {
    $("#Page").val(pageId);
    $("#filter-search").submit();
}
function DeleteComment(commentId, e, title) {

    e.preventDefault();

    Swal.fire({
        text: `آیا از حذف نظر در ${title} مطمئن هستید؟`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "/UserManagement/ClassComment/DeleteForever/" + commentId;
        }
    });
}