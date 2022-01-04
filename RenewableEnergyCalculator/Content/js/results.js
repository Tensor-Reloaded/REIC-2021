var xValues = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var yValues = [];

for (var i in energies)
    yValues.push([i, energies[i]]);

chart = new Chart("solarChart", {
    type: "bar",
    data: {
        labels: xValues,
        datasets: [{
            backgroundColor: "blue",
            data: yValues
        }]
    },
    options: {
        legend: { display: false },
        title: {
            display: true,
            text: "Monthly Solar Energy Output"
        }
    }
});


$(document).ready(function () {
    $("#btnSubmit").click(sendFormData);

    function sendFormData() {
        var email = document.getElementById("email").value;
        console.log(email);

        var chart = document.getElementById("solarChart");
        var imgURI = chart.toDataURL();


        solarTableElement = document.getElementById("solarTable").outerHTML;
        solarTable = btoa(unescape(encodeURIComponent(solarTableElement)));


        var json = {
            Email: email,
            SolarChart: imgURI,
            SolarTable: solarTable
        };

        var test = JSON.stringify(json);
        console.log(test);

        try {
            $.ajax({
                url: ajaxUrl,
                data: { "json": test },
                type: "POST",
                success: function (res) {
                    //TODO: Add whatever if you want to pass a notification back
                    if (res == true) {
                        alert("The email was sent!");
                    } else {
                        alert("An error occured while sending the email.");
                    }
                },
                error: function (error) {
                    //TODO: Add some code here for error handling or notifications
                    alert("Error - ajax");
                }
            });
        }
        catch (err) {
            console.log(err);
        }

    }
});
