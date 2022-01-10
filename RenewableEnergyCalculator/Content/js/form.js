var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab

function showTab(n) {
    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        document.getElementById("nextBtn").innerHTML = "Submit";
    } else {
        document.getElementById("nextBtn").innerHTML = "Next";
    }
    //... and run a function that will display the correct step indicator:
    fixStepIndicator(n)
}

function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    if (n == 1 && !validateForm()) return false;
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form...
    if (currentTab >= x.length) {
        // ... the form gets submitted:
        document.getElementById("formSection").style.display = "none";
        $("#loader").show();
        document.getElementById("regForm").submit();
        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}

function validateForm() {
    // This function deals with validation of the form fields
    var x, y, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByTagName("input");
    s = x[currentTab].getElementsByTagName("select");
    // A loop that checks every input field in the current tab:
    for (i = 0; i < y.length; i++) {
        var ariaInvalid = y[i].getAttribute("aria-invalid");
        // If a field is empty...
        if (ariaInvalid == "true" || y[i].value == "") {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            // and set the current valid status to false
            valid = false;
        }
    }


    // A loop that checks every input field in the current tab:
    for (i = 0; i < s.length; i++) {
        var ariaInvalid = s[i].getAttribute("aria-invalid");
        // If a field is empty...
        if (ariaInvalid == "true" || s[i].value == "0") {
            // add an "invalid" class to the field:
            s[i].className += " invalid";
            // and set the current valid status to false
            valid = false;
        }
    }
    // If the valid status is true, mark the step as finished and valid:
    if (valid) {
        document.getElementsByClassName("step")[currentTab].className += " finish";
    }
    return valid; // return the valid status
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
}

// Initialize and add the map
function initMap() {
    // The location of Uluru
    const uluru = { lat: 47.158, lng: 27.601 };
    // The map, centered at Uluru
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10,
        center: uluru,
    });
    // The marker, positioned at Uluru
    const marker = new google.maps.Marker({
        position: uluru,
        map: map,
        draggable: true
    });

    var infowindow = new google.maps.InfoWindow();
    const geocoder = new google.maps.Geocoder();

    var input = document.getElementById('searchTextField');
    var autocomplete = new google.maps.places.Autocomplete(input);

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    });

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var place = autocomplete.getPlace();
        document.getElementById('Address').value = place.name;
        document.getElementById('Lat').value = place.geometry.location.lat();
        document.getElementById('Lng').value = place.geometry.location.lng();

        lat = document.getElementById('Lat').value;
        lng = document.getElementById('Lng').value;

        var newLatLng = new google.maps.LatLng(lat, lng);

        updateMap(map, newLatLng, marker, infowindow)
        //alert("This function is working!");
        //alert(newLoc.l);
        //alert(place.address_components[0].long_name);

    });

    google.maps.event.addListener(marker, 'dragend', function () {

        geocoder
            .geocode({ location: marker.getPosition() })
            .then((response) => {
                if (response.results[0]) {
                    input.value = response.results[0].formatted_address;
                    input.focus();
                } else {
                    input.value = "No results found";
                    input.focus();
                }
            })
            .catch((e) => window.alert("Geocoder failed due to: " + e));


        updateMap(map, marker.getPosition(), marker, infowindow)
        //alert(marker.getPosition()); // new LatLng-Object after dragend-event...
    });
}

function updateMap(map, newLatLng, marker, infowindow) {
    map.setZoom(16);
    map.setCenter(newLatLng);
    marker.setPosition(newLatLng);
    infowindow.setContent("Lat: " + newLatLng.lat() + " Lng: " + newLatLng.lng() + "\n");
}


window.onload = window.onresize = function () {
    var canvas = document.getElementById('canvas');
    canvas.width = window.innerWidth * 0.8;
    canvas.height = window.innerHeight * 0.8;
}
