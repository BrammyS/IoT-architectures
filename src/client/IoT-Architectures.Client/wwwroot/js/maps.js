// to the base of the flagpole.
function initalizeMap() {
    const mapElementId = "googleMap";
    const map = new google.maps.Map(document.getElementById(mapElementId), {
      zoom: 8,
      center: { lat: -33.9, lng: 151.2 },
    });
  
    console.log(map);
    // return map;
}