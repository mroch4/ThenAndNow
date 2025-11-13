window.bootstrapInterop = {
    showModal: function (selector) {
        var modal = new bootstrap.Modal(document.getElementById(selector));
        if (modal) {
            modal.show();
        }
    }
};