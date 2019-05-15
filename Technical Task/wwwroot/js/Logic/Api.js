class Api {
    static get baseUrl() { return this._baseUrl; }
    static set baseUrl(value) { this._baseUrl = value; }

    static url(url) {
        return this.baseUrl == null ? `https://localhost:44310/api${url}` : `${this.baseUrl}/api${url}`;
    }

    static getAllCountriesAsync() {
        return $.get(this.url('/Country/GetAllCountries'));
    }
    static addCountry(name) {
        return $.post(this.url('/Country/AddCountry'), {
            name: name
        });
    }
    static removeCountry(id) {
        return $.post(this.url('/Country/DeleteCountry'), {
            id: id
        });
    }

    static getAllCities() {
        return $.get(this.url('/City/GetAllCities'));
    }

    static addCity(name, countryId) {
        return $.post(this.url('/City/AddCity'), {
            name: name,
            countryId: countryId
        });
    }

    static removeCity(id) {
        return $.post(this.url('/City/DeleteCity'), {
            id: id
        });
    }

    static getAllCitiesOfTheCountry(selectedValue) {
        return $.get(this.url('/City/GetAllCitiesOfTheCountry'), {
            selectedCountryId: selectedValue
        });
    }

    static daysWeather(selectedDate, selectedCityId) {
        return $.get(this.url('/Weather/GetWeatherOfTheDay'), {
            selectedDate: selectedDate.toISOString(),
            selectedCityId: selectedCityId
        });
    }
    static addOrEditWeatherOfTheDay(form) {
        return $.post(this.url('/Weather/AddOrEditWeatherOfTheDayAsync'),
            {
                selectedDate: form.selectedDate.toISOString(),
                selectedCityId: form.selectedCityId,
                cloudiness: form.cloudiness,
                humidity: form.humidity,
                rainChance: form.rainChance,
                pressure: form.pressure,
                wind: form.wind,
                temperatures: form.temperatures.map((o) => {
                    return {
                        time: o.time.toISOString(),
                        temperature: o.temperature
                    }
                })
            });
    }
    static deleteWeatherOfTheDay(form) {
        return $.post(this.url('/Weather/DeleteWeatherOfTheDayAsync'),
            {
                selectedDate: form.selectedDate.toISOString(),
                selectedCityId: form.selectedCityId,
            });
    }
}