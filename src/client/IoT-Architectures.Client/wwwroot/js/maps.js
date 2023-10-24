let googleMap = null;

// to the base of the flagpole.
function initalizeMap(mapElementId) {
    const map = new google.maps.Map(document.getElementById("googleMap"), {
      zoom: 10,
      center: { lat: 51.94998550415039, lng: 6.746642112731934 },
    });
  
    googleMap = map;
}

function createMarker(title, lat, long, dotNetHelper, showMakerInfoCallbackName) {
    if(googleMap == null) {
        console.error("The map is not initialized yet, call the initialize method first");
        return;
    }

    const map = googleMap;
    const image = {
        url: "https://maps.google.com/mapfiles/ms/icons/red-dot.png",
        size: new google.maps.Size(32, 32),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(16, 32),
    };

    const shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: "poly",
    };

    const marker = new google.maps.Marker({
        position: {lat: lat, lng: long},
        map,
        icon: image,
        shape: shape,
        title: title,
        zIndex: 1,
    });
   
    marker.addListener("click", () => {
        dotNetHelper.invokeMethodAsync(showMakerInfoCallbackName, lat, long);
    });
}
