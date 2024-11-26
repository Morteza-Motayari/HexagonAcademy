function ShowAddingItemForm(url, title) {
    fetch(url)
        .then(res => res.text())
        .then(data => {
            $("#myLargeModalLabel").html(title);
            $("#largeModalBody").html(data);
            $("#LargeModal").modal('show');
        })
}

function OnSuccessAddingItem(res) {
    if (res.status == 200) {
        Swal.fire({
            title: "موفق",
            text: res.message,
            icon: "success"
        }).then((result)=> {
            if (result.isConfirmed) {
                location.reload();
            }
            else {
                setTimeout(() => {
                    location.reload();
                }, 5000);
            }
        })
    }
    else {
        Swal.fire({
            title: "خطا",
            text: res.message,
            icon: "error"
        });
    }
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
function fillPageId(pageId) {
    $("#Page").val(pageId);
    $("#filter-search").submit();
}