window.blazorInterop = {
    scrollTo: function (elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            const parent = element.parentElement;
            if (parent) {
                const scrollPos = element.offsetTop - element.offsetHeight;
                parent.scrollTo({
                    top: scrollPos,
                    behavior: "smooth"
                });
            }
        }
    },

    scrollTop: function () {
        window.scrollTo({ top: 0, behavior: "smooth" });
    }
};

window.bootstrapInterop = {
    showModal: function (elementId) {
        const modal = new bootstrap.Modal(document.getElementById(elementId));
        if (modal) {
            modal.show();
        }
    }
};

window.leafletMapInterop = {
    zoom: 13,

    init: function (latitude, longitude, entries, dotNetObjectReference) {
        this.map = L.map("map", {
            center: [latitude, longitude],
            zoom: this.zoom,
        });

        L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png").addTo(this.map);

        entries.forEach((entry) =>

            L.marker([entry.b.a, entry.b.b], {

                icon: L.icon({
                    iconSize: [25, 41],
                    iconAnchor: [13, 41],
                    iconUrl: "icons/marker.png",
                    popupAnchor: [-1, -10],
                }),
            })
                .addTo(this.map)
                .bindPopup(`<b>${entry.e}</b>`)
                .on("click", () => {
                    this.map.setView([entry.b.a, entry.b.b], this.map.zoom);
                    dotNetObjectReference.invokeMethodAsync("OnMarkerOpened", entry.a);
                })
                .on("popupclose", () => {
                    this.map.setView([latitude, longitude], this.map.zoom);
                    dotNetObjectReference.invokeMethodAsync("OnMarkerClosed");
                })
        );
    },
};