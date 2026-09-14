const heroSearchButton =
    document.getElementById("heroSearchButton");


if (heroSearchButton) {

    heroSearchButton.addEventListener(
        "click",
        function () {

            const pickupDate =
                document.getElementById("pickupDate").value;

            const dropoffDate =
                document.getElementById("dropoffDate").value;

            const brand =
                document.getElementById("heroBrand").value;


            const params =
                new URLSearchParams();


            if (pickupDate) {

                params.set(
                    "pickup",
                    pickupDate
                );

            }


            if (dropoffDate) {

                params.set(
                    "dropoff",
                    dropoffDate
                );

            }


            if (brand) {

                params.set(
                    "brand",
                    brand
                );

            }


            window.location.href =
                "/Car?" +
                params.toString();

        }
    );

}
