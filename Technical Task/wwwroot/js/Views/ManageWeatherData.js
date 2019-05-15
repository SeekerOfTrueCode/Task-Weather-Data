var countriesList;
var citiesList;
var notification;

var selectCountries;
var selectCities;
var datePicker;

var inputCloudiness;
var inputWind;
var inputHumidity;
var inputRainChance;
var inputPressure;
var inputsTemperatures;

var form = {
    selectedCityId: null,
    selectedDate: null,

    cloudiness: null,
    wind: null,
    humidity: null,
    rainChance: null,
    pressure: null,
    temperatures: []
}

$(document).ready(() => {
    notification = new TTNotification("#notify", 2);

    Api.getAllCountriesAsync().then((data) => {
        countriesList = new TTListCRUD({
            selector: "#homeManageWeatherDataCountriesManager",
            label: "Countries",
            dataColumns: [
                {
                    type: "text",
                    title: "Name",
                    prop: "name"
                },
            ],
            data: data,
            add: (dataObject) => {
                Api.addCountry(dataObject.name).then((result) => {
                    if (result.id !== -1) {
                        countriesList.options.data.push(result);
                        countriesList.render();
                        notification.notify(`Added new Country`, `success`);
                    } else {
                        notification.notify(`Couldn't add new Country`,`error`);
                    }
                }).catch((error) => notification.notify(error.responseJSON.Message,`error`));
            },
            //edit: (dataObject) => {
            //    notification.notify(`Edit ` + dataObject.toString());
            //},
            remove: (dataObject) => {
                Api.removeCountry(dataObject.id).then((result) => {
                    if (result) {
                        countriesList.options.data = countriesList.options.data.filter(item => item !== dataObject);
                        countriesList.render();
                        notification.notify(`Removed the Country`, `success`);
                    } else {
                        notification.notify(`Couldn't remove the Country`, `error`);
                    }
                }).catch((error) => notification.notify(error.responseJSON.Message, `error`));;
            }
        });
    });

    Api.getAllCities().then((data) => {
        citiesList = new TTListCRUD({
            selector: "#homeManageWeatherDataCitiesManager",
            label: "Cities",
            dataColumns: [
                {
                    type: "text",
                    title: "Name",
                    prop: "name"
                },
                {
                    type: "text",
                    title: "CountryId",
                    prop: "countryId"
                }
            ],
            data: data,
            add: (dataObject) => {
                Api.addCity(dataObject.name, dataObject.countryId).then((result) => {
                    if (result.id !== -1) {
                        citiesList.options.data.push(result);
                        citiesList.render();
                        notification.notify(`Added new City`, `success`);
                    } else {
                        notification.notify(`Couldn't add new City`, `error`);
                    }
                }).catch((error) => notification.notify(error.responseJSON.Message, `error`));
            },
            //edit: (dataObject) => {
            //    notification.notify(`Edit ` + dataObject.toString());
            //},
            remove: (dataObject) => {
                Api.removeCity(dataObject.id).then((result) => {
                    if (result) {
                        citiesList.options.data = citiesList.options.data.filter(item => item !== dataObject);
                        citiesList.render();
                        notification.notify(`Removed the City`, `success`);
                    } else {
                        notification.notify(`Couldn't remove the City`, `error`);
                    }
                }).catch((error) => notification.notify(error.responseJSON.Message, `error`));
            }
        });
    });

    datePicker = new TTDatePicker({
        selector: '#homeManageWeatherDataDatepicker',
        onSelect: (selectedValue) => {
            if (selectedValue != null
                && !isNaN(selectedValue.getTime())
                && form.selectedCityId != null) {
                
                form.selectedDate = selectedValue;

                $('#homeManageWeatherDataInputTemperatures').html(`            
                <div class="row align-items-center h-100">
                    <div class="col text-center">
                        <div class="spinner-border text-mute" style="width: 3rem; height: 3rem;" role="status">
                          <span class="sr-only">Bootstrap spinner is not working</span>
                        </div>
                        <p>Loading...</p>
                    </div>
                </div>`);
                Api.daysWeather(form.selectedDate, form.selectedCityId)
                    .then((data) => {
                        inputCloudiness.setValue(data.cloudiness);
                        inputWind.setValue(data.windSpeed);
                        inputHumidity.setValue(data.humidity);
                        inputRainChance.setValue(data.rainChance);
                        inputPressure.setValue(data.pressure);

                        inputsTemperatures = new ManageWeatherDataTemperaturesPartialView({
                            selector: "#homeManageWeatherDataInputTemperatures",
                            date: form.selectedDate,
                            onChange: (newValue) => {
                                form.temperatures = newValue;
                                validateForm();
                            },
                            initData: data.dayTemperatures
                        });
                        $('#homeManageWeatherDataAddOrEditTitle').html(`<p class="text-mute">Edit the weather day data<p>`);
                        $('#homeManageWeatherDataDeleteWeatherDay').show();
                    }).catch((error) => {
                        if (error.responseJSON.StatusCode === 500) {

                            inputCloudiness.setValue('');
                            inputWind.setValue('');
                            inputHumidity.setValue('');
                            inputRainChance.setValue('');
                            inputPressure.setValue('');

                            inputsTemperatures = new ManageWeatherDataTemperaturesPartialView({
                                selector: "#homeManageWeatherDataInputTemperatures",
                                date: form.selectedDate,
                                onChange: (newValue) => {
                                    form.temperatures = newValue;
                                    validateForm();
                                }
                            });
                            $('#homeManageWeatherDataAddOrEditTitle').html(`<p class="text-mute">Add new weather day data<p>`);
                            $('#homeManageWeatherDataDeleteWeatherDay').hide();
                        } else {
                            notification.notify(error.responseJSON.Message, "error");
                            $('#homeManageWeatherDataAddOrEditTitle').html(`<p class="text-error">${error.responseJSON.Message}<p>`);
                            $('#homeManageWeatherDataDeleteWeatherDay').hide();
                        }
                       
                    }).always(() => validateForm());
            }
        },
        immediate: true
    });

    // init selects
    Api.getAllCountriesAsync().then((data) => {
        selectCountries = new TTSelect({
            selector: "#homeManageWeatherDataSelectCountries",
            array: data.map((country) => { return { value: country.id, text: country.name } }),
            onSelect: (selectedValue) => {
                Api.getAllCitiesOfTheCountry(selectedValue).then((data) => {
                    selectCities = new TTSelect({
                        selector: "#homeManageWeatherDataSelectCities",
                        array: data.map((city) => { return { value: city.id, text: city.name } }),
                        onSelect: (selectedValue) => {
                            form.selectedCityId = selectedValue;
                            datePicker.trigger();
                        },
                        immediate: true
                    });
                });
            },
            immediate: true
        });
    });

    inputCloudiness = new TTInput({
        selector: "#homeManageWeatherDataInputCloudiness",
        icon: `<i class="wi wi-cloud weather-icon center-block" data-toggle="tooltip" data-placement="bottom" title="Cloudiness level"></i>`,
        placeholder: "0-100",
        suffix: "%",
        description: "Cloudiness level",
        rules: [
            (value) => value.length === 0 ? "Cloudiness level is required" : null,
            (value) => +value > 100 || +value < 0 ? "Value has to be equal 0-100" : null,
        ],
        onChange: (newValue) => {
            form.cloudiness = newValue;
            validateForm();
        }
    });

    inputWind = new TTInput({
        selector: "#homeManageWeatherDataInputWind",
        icon: `<i class="wi wi-wind weather-icon center-block" data-toggle="tooltip" data-placement="bottom" title="Wind speed"></i>`,
        placeholder: "0-500",
        suffix: "km/h",
        description: "Wind speed",
        rules: [
            (value) => value.length === 0 ? "Wind speed is required" : null,
            (value) => +value > 500 || +value < 0 ? "Value has to be equal 0-100" : null,
        ],
        onChange: (newValue) => {
            form.wind = newValue;
            validateForm();
        }
    });
    inputHumidity = new TTInput({
        selector: "#homeManageWeatherDataInputHumidity",
        icon: `<i class="wi wi-humidity weather-icon center-block" data-toggle="tooltip" data-placement="bottom" title="Humidity level"></i>`,
        placeholder: "0-100",
        suffix: "%",
        description: "Humidity level",
        rules: [
            (value) => value.length === 0 ? "Humidity is required" : null,
            (value) => +value > 100 || +value < 0 ? "Value has to be equal 0-100" : null,
        ],
        onChange: (newValue) => {
            form.humidity = newValue;
            validateForm();
        }
    });
    inputRainChance = new TTInput({
        selector: "#homeManageWeatherDataInputRainChance",
        icon: `<i class="wi wi-umbrella weather-icon center-block" data-toggle="tooltip" data-placement="bottom" title="Chance of rain"></i>`,
        placeholder: "0-100",
        suffix: "%",
        description: "Chance of rain",
        rules: [
            (value) => value.length === 0 ? "Chance of rain is required" : null,
            (value) => +value > 100 || +value < 0 ? "Value has to be equal 0-100" : null,
        ],
        onChange: (newValue) => {
            form.rainChance = newValue;
            validateForm();
        }
    });
    inputPressure = new TTInput({
        selector: "#homeManageWeatherDataInputPressure",
        icon: `<i class="wi wi-barometer weather-icon center-block" data-toggle="tooltip" data-placement="bottom" title="Pressure"></i>`,
        suffix: "hPa",
        placeholder: "0-1200",
        description: "Pressure",
        rules: [
            (value) => value.length === 0 ? "Pressure is required" : null,
            (value) => +value > 1200 || +value < 0 ? "Value has to be equal 0-1200" : null,
        ],
        onChange: (newValue) => {
            form.pressure = newValue;
            validateForm();
        }
    });
    $('#homeManageWeatherDataSubmitWeatherDay').click(() => {
        if (true) { //TODO change to check if valid
            Api.addOrEditWeatherOfTheDay(form).then((result) => {
                notification.notify((result) ? "Added or Edited the weather data" : "Failed to add or edit data", (result) ? "success" : "error");
            }).always(() => datePicker.trigger());
        }
    });
    $('#homeManageWeatherDataDeleteWeatherDay').click(() => {
        Api.deleteWeatherOfTheDay(form).then((result) => {
            notification.notify((result) ? "Deleted the weather data" : "Failed to delete the weather data", (result) ? "success" : "error");
        }).always(() => datePicker.trigger());
    });
});

function validateForm() {
    try {
        if (inputsTemperatures.validate()
            && inputCloudiness.validate() == null
            && inputHumidity.validate() == null
            && inputPressure.validate() == null
            && inputRainChance.validate() == null
            && inputWind.validate() == null) {
            $('#homeManageWeatherDataSubmitWeatherDay').attr("disabled", false);
        } else {
            $('#homeManageWeatherDataSubmitWeatherDay').attr("disabled", true);
        }
    } catch (Exception) {
        //Bad practice 
        $('#homeManageWeatherDataSubmitWeatherDay').attr("disabled", true);

    }
}