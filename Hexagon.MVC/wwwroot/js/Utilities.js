function setupMoney(inputElement) {
    if (!inputElement) {
        console.warn("Input element is not provided or invalid.");
        return;
    }

    // Add an event listener for real-time formatting
    inputElement.addEventListener('input', function (e) {
        // Remove all non-digit characters
        let value = e.target.value.replace(/[^0-9]/g, '');

        // Format the number with commas
        e.target.value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    });

    // Add an event listener to strip commas on blur
    inputElement.addEventListener('blur', function (e) {
        e.target.value = e.target.value.replace(/,/g, '');
    });
}

function PreventInput(element) {
    if (!element) {
        console.warn("Input element is not provided or invalid.");
        return;
    }
    // Prevent typing
    element.addEventListener('keydown', (event) => {
        event.preventDefault();
    });
    // Prevent pasting
    element.addEventListener('paste', (event) => {
        event.preventDefault();
    });
}