const searchInput =
    document.getElementById("carSearch");

const brandFilter =
    document.getElementById("brandFilter");

const categoryFilter =
    document.getElementById("categoryFilter");

const priceFilter =
    document.getElementById("priceFilter");

const resetButton =
    document.getElementById("resetFilters");

const sortCars =
    document.getElementById("sortCars");

const carsGrid =
    document.getElementById("carsGrid");

const carsCount =
    document.getElementById("carsCount");

const noResults =
    document.getElementById("noResults");

const noResultsReset =
    document.getElementById("noResultsReset");


const carColumns =
    Array.from(
        document.querySelectorAll(".car-column")
    );


function setCarVisibility(car, shouldShow) {

    clearTimeout(car.hideTimer);

    if (shouldShow) {

        car.style.display = "";

        requestAnimationFrame(function () {

            car.classList.remove("is-filtered-out");

        });

        return;

    }


    car.classList.add("is-filtered-out");

    car.hideTimer = setTimeout(function () {

        if (car.classList.contains("is-filtered-out")) {

            car.style.display = "none";

        }

    }, 200);

}


function filterCars() {

    const searchValue =
        searchInput.value
            .toLowerCase()
            .trim();


    const brandValue =
        brandFilter.value;


    const categoryValue =
        categoryFilter.value;


    const priceValue =
        priceFilter.value;


    let visibleCars = 0;


    carColumns.forEach(function (car) {

        const brand =
            car.dataset.brand;


        const category =
            car.dataset.category;


        const price =
            Number(car.dataset.price);


        const name =
            car.dataset.name.toLowerCase();


        const matchesSearch =
            name.includes(searchValue) ||
            brand.includes(searchValue);


        const matchesBrand =
            brandValue === "all" ||
            brand === brandValue;


        const matchesCategory =
            categoryValue === "all" ||
            category === categoryValue;


        let matchesPrice = true;


        if (priceValue === "low") {

            matchesPrice =
                price < 70;

        }
        else if (priceValue === "medium") {

            matchesPrice =
                price >= 70 &&
                price <= 100;

        }
        else if (priceValue === "high") {

            matchesPrice =
                price > 150;

        }
        else if (priceValue === "mid-high") {

            matchesPrice =
                price > 100 &&
                price <= 150;

        }


        const shouldShow =
            matchesSearch &&
            matchesBrand &&
            matchesCategory &&
            matchesPrice;


        if (shouldShow) {

            setCarVisibility(car, true);

            visibleCars++;

        }
        else {

            setCarVisibility(car, false);

        }

    });


    if (visibleCars === 0) {

        noResults.style.display = "block";

        carsGrid.style.display = "none";

        carsCount.textContent =
            "0 Cars Available";

    }
    else {

        noResults.style.display = "none";

        carsGrid.style.display = "flex";

        carsCount.textContent =
            `${visibleCars} Car${visibleCars > 1 ? "s" : ""} Available`;

    }

}


function sortCarsList() {

    const value =
        sortCars.value;


    const sortedCars =
        [...carColumns];


    if (value === "low-high") {

        sortedCars.sort(function (a, b) {

            return Number(a.dataset.price) -
                   Number(b.dataset.price);

        });

    }


    else if (value === "high-low") {

        sortedCars.sort(function (a, b) {

            return Number(b.dataset.price) -
                   Number(a.dataset.price);

        });

    }


    sortedCars.forEach(function (car) {

        carsGrid.appendChild(car);

    });


    filterCars();

}


searchInput.addEventListener(
    "input",
    filterCars
);


brandFilter.addEventListener(
    "change",
    filterCars
);


categoryFilter.addEventListener(
    "change",
    filterCars
);


priceFilter.addEventListener(
    "change",
    filterCars
);


sortCars.addEventListener(
    "change",
    sortCarsList
);


resetButton.addEventListener(
    "click",
    function () {

        searchInput.value = "";

        brandFilter.value = "all";

        categoryFilter.value = "all";

        priceFilter.value = "all";

        sortCars.value = "default";


        carColumns.forEach(function (car) {

            carsGrid.appendChild(car);

        });


        filterCars();

    }
);


noResultsReset.addEventListener(
    "click",
    function () {

        resetButton.click();

    }
);


/* =========================
   GET HERO SEARCH DATA
========================= */

const urlParams =
    new URLSearchParams(
        window.location.search
    );


const heroBrand =
    urlParams.get("brand");


const pickupDate =
    urlParams.get("pickup");


const dropoffDate =
    urlParams.get("dropoff");


if (heroBrand) {

    brandFilter.value =
        heroBrand;

}


if (pickupDate) {

    sessionStorage.setItem(
        "pickupDate",
        pickupDate
    );

}


if (dropoffDate) {

    sessionStorage.setItem(
        "dropoffDate",
        dropoffDate
    );

}


filterCars();
