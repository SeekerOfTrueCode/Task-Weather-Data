var selectCountries;
var selectCities;
var datePicker;
var model;

var selectedCityId;
var selectedDate;

$(document).ready(() => {
    model = new WeatherElement();

    datePicker = new TTDatePicker({
        selector: "#homeBrowseWeatherDataDatePicker",
        onSelect: (selectedValue) => {
            selectedDate = selectedValue;

            if (selectedDate != null && !isNaN(selectedDate.getTime()) && selectedCityId != null) {
                model.appendHTMLLoading('homeBrowseWeatherDataWeatherContainer');
                Api.daysWeather(selectedDate, selectedCityId)
                    .then((data) => model.appendHTMLElement('homeBrowseWeatherDataWeatherContainer', new WeatherModel(data)))
                    .catch((error) => error.responseJSON.StatusCode !== 500
                        ? model.appendHTMLError('homeBrowseWeatherDataWeatherContainer', error.responseJSON.Message)
                        : model.appendHTMLElement('homeBrowseWeatherDataWeatherContainer', new WeatherModel(null)));
            }

        },
        immediate: true
    });

    // init selects
    Api.getAllCountriesAsync().then((data) => {
        selectCountries = new TTSelect({
            selector: "#homeBrowseWeatherDataSelectCountries",
            array: data.map((country) => { return { value: country.id, text: country.name } }),
            onSelect: (selectedValue) => {
                Api.getAllCitiesOfTheCountry(selectedValue).then((data) => {
                    selectCities = new TTSelect({
                        selector: "#homeBrowseWeatherDataSelectCities",
                        array: data.map((city) => { return { value: city.id, text: city.name } }),
                        onSelect: (selectedValue) => {
                            selectedCityId = selectedValue;
                            datePicker.trigger();
                        },
                        immediate: true
                    });
                });
            },
            immediate: true
        });
    });
});