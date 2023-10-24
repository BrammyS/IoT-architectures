// to the base of the flagpole.
function initalizeMap(mapElementId) {
    const map = new google.maps.Map(document.getElementById("googleMap"), {
      zoom: 8,
      center: { lat: -33.9, lng: 151.2 },
    });
  
    console.log(map);
    return stringify(map);
}

// function createMarker(map, title, lat, long) {
//     console.log("test")
//     const image = {
//         url: "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png",
//         // This marker is 20 pixels wide by 32 pixels high.
//         size: new google.maps.Size(20, 32),
//         // The origin for this image is (0, 0).
//         origin: new google.maps.Point(0, 0),
//         // The anchor for this image is the base of the flagpole at (0, 32).
//         anchor: new google.maps.Point(0, 32),
//     };

//     const shape = {
//         coords: [1, 1, 1, 20, 18, 20, 18, 1],
//         type: "poly",
//     };

//     new google.maps.Marker({
//         position: {lat: lat, lng: long},
//         map,
//         icon: image,
//         shape: shape,
//         title: title,
//         zIndex: 1,
//     });
// }


function stringify(obj) {
    let cache = [];
    let str = JSON.stringify(obj, function(key, value) {
      if (typeof value === "object" && value !== null) {
        if (cache.indexOf(value) !== -1) {
          // Circular reference found, discard key
          return;
        }
        // Store value in our collection
        cache.push(value);
      }
      return value;
    });
    cache = null; // reset the cache
    return str;
  }
