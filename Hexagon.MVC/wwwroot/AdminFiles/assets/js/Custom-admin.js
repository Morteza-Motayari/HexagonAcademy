function fillPageId(pageId) {
    $("#Page").val(pageId);
    $("#filter-search").submit();
}

function confirmDelete(url, e, title) {

    e.preventDefault();

    Swal.fire({
        text: `آیا از حذف ${title} مطمئن هستید؟`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = url;
        }
    });
}