function changeCommentStatus(url) {
    fetch(url)
        .then(res => res.json())
        .then(res => {
            if (res.status == 200) {
                Swal.fire({
                    title: "موفق",
                    text: res.message,
                    icon: "success"
                }).then((result) => {
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
                }).then((result) => {
                    if (result.isConfirmed) {
                        location.reload();
                    }
                    else {
                        setTimeout(() => {
                            location.reload();
                        }, 3000);
                    }
                })
            }
        })
}