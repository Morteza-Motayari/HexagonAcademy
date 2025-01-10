function ShowAddingTicketForm() {
    fetch(`/UserManagement/Ticket/Create`)
        .then(res => res.text())
        .then(data => {
            $("#myLargeModalLabel").html("افزودن تیکت جدید");
            $("#largeModalBody").html(data);
            $("#LargeModal").modal('show');
        });
};
function DeleteTicketMessage(ticketMessageId,e) {

    e.preventDefault();

    Swal.fire({
        text: `آیا از حذف این پیام تیکت مطمئن هستید؟`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "/UserManagement/Ticket/Delete/" + ticketMessageId;
        }
    });
}