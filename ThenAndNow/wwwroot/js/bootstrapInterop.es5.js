"use strict";

window.bootstrapInterop = {
    showModal: function showModal(selector) {
        var modal = new bootstrap.Modal(document.getElementById(selector));
        if (modal) {
            modal.show();
        }
    }
};

