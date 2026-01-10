window.territoryMap = {
    init: async function (geoUrl, dotnetRef) {
        const map = L.map('map').setView([37.8, -96], 4);

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '© OpenStreetMap'
        }).addTo(map);

        const res = await fetch(geoUrl);
        const geojson = await res.json();

        function onEachFeature(feature, layer) {
            const props = feature.properties || {};
            const name = props.NAME || 'Unknown';
            const code = props.STATE || null; 

            layer.bindPopup(name);
            layer.on('click', () => {
                if (code) dotnetRef.invokeMethodAsync('OnTerritorySelected', code);
            });
        }

        L.geoJSON(geojson, {
            style: { color: '#1976d2', weight: 1, fillOpacity: 0.2 },
            onEachFeature
        }).addTo(map);
    }
};
