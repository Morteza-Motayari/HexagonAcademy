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
            icon: "Success"
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