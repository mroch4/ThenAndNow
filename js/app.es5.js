"use strict";

window.blazorInterop = {
    scrollTo: function scrollTo(elementId) {
        var element = document.getElementById(elementId);
        if (element) {
            var _parent = element.parentElement;
            if (_parent) {
                var scrollPos = element.offsetTop - element.offsetHeight;
                _parent.scrollTo({
                    top: scrollPos,
                    behavior: "smooth"
                });
            }
        }
    },

    scrollTop: function scrollTop() {
        window.scrollTo({ top: 0, behavior: "smooth" });
    }
};

window.bootstrapInterop = {
    showModal: function showModal(elementId) {
        var modal = new bootstrap.Modal(document.getElementById(elementId));
        if (modal) {
            modal.show();
        }
    }
};

window.leafletMapInterop = {
    zoom: 13,

    init: function init(latitude, longitude, entries, dotNetObjectReference) {
        var _this = this;

        this.map = L.map("map", {
            center: [latitude, longitude],
            zoom: this.zoom
        });

        L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png").addTo(this.map);

        entries.forEach(function (entry) {
            return L.marker([entry.b.a, entry.b.b], {

                icon: L.icon({
                    iconSize: [25, 41],
                    iconAnchor: [13, 41],
                    iconUrl: "icons/marker.png",
                    popupAnchor: [-1, -10]
                })
            }).addTo(_this.map).bindPopup("<b>" + entry.e + "</b>").on("click", function () {
                _this.map.setView([entry.b.a, entry.b.b], _this.map.zoom);
                dotNetObjectReference.invokeMethodAsync("OnMarkerOpened", entry.a);
            }).on("popupclose", function () {
                _this.map.setView([latitude, longitude], _this.map.zoom);
                dotNetObjectReference.invokeMethodAsync("OnMarkerClosed");
            });
        });
    }
};

