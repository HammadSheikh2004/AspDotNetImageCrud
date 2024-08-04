async function loadJSON(url) {
    const response = await fetch(url);
    return response.json();
}

// Populate countries dropdown
async function populateCountries() {
    const countries = await loadJSON('/js/countries.json');
    const countrySelect = document.getElementById('countrySelect');

    countries.forEach(country => {
        const option = document.createElement('option');
        option.value = country.name; 
        option.textContent = country.name;
        countrySelect.appendChild(option);
    });
}

// Populate cities dropdown based on selected country
async function populateCities(countryName) {
    try {
        const cities = await loadJSON('/js/cities.json');
        const citySelect = document.getElementById('citySelect');

        citySelect.innerHTML = '<option selected disabled>Select City</option>';

        cities.forEach(city => {
            if (city.country_name === countryName) { 
                const option = document.createElement('option');
                option.value = city.name; 
                option.textContent = city.name;
                citySelect.appendChild(option);
            }
        });
    } catch (error) {
        console.error('Error loading cities:', error);
    }
}

document.getElementById('countrySelect').addEventListener('change', function () {
    const selectedCountryName = this.value;
    populateCities(selectedCountryName);
});

populateCountries();
