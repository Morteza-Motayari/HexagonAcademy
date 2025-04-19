document.addEventListener('DOMContentLoaded', function () {
    // Initialize all clickable images
    document.querySelectorAll('.clickable-image').forEach(img => {
        img.addEventListener('click', function () {
            const modalImg = document.getElementById('modalImage');
            modalImg.src = this.src;
            modalImg.alt = this.alt;

            // Initialize and show modal
            const modal = new bootstrap.Modal(document.getElementById('imageModal'));
            modal.show();

            // Cleanup when modal hides
            document.getElementById('imageModal').addEventListener('hidden.bs.modal', function () {
                // Force remove any leftover backdrop
                document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
                document.body.classList.remove('modal-open');
                document.body.style.overflow = '';
                document.body.style.paddingRight = '';
            });
        });
    });

    // Close modal when clicking outside image (on backdrop)
    document.getElementById('imageModal').addEventListener('click', function (e) {
        if (e.target === this) { // If click is directly on backdrop
            bootstrap.Modal.getInstance(this).hide();
        }
    });
});