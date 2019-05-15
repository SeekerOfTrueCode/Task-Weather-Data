// @ts-check

/**
 * A person object with a name and age.
 * @typedef {Object<Date, number>} WeatherDayTemperature
 * @property {Date} dayTime The time of the day (most likely HH:mm)
 * @property {number} temperatureC The temperature predicted in the given time of the day
 */

/**
 * A person object with a name and age.
 * @typedef {Object<Date, number, number, number, number, number, number, number, string, Array<WeatherDayTemperature>>} Options
 * @property {Date} date
 * @property {number} temperatureC
 * @property {number} temperatureF
 * @property {number} cloudiness
 * @property {number} windSpeed
 * @property {number} humidity
 * @property {number} rainChance
 * @property {number} pressure
 * @property {string} forecastMessage
 * @property {Array<WeatherDayTemperature>} dayTemperatures
 */

/**
 * @classdesc Class responsible for containing weather data
 * @class 
 * @property {Date} date
 * @property {number} temperatureC
 * @property {number} temperatureF
 * @property {number} cloudiness
 * @property {number} windSpeed
 * @property {number} humidity
 * @property {number} rainChance 
 * @property {number} pressure
 * @property {string} forecastMessage
 * @property {Array<WeatherDayTemperature>} dayTemperatures
 */
class WeatherModel {
    /**
     * 
     * @param {Options} init
     */
    constructor(init) {
        if (init) {
            Object.assign(this, init);
        } else {
            this.date = null;
            this.temperatureC = null;
            this.temperatureF = null;
            this.cloudiness = null;
            this.windSpeed = null;
            this.humidity = null;
            this.rainChance = null;
            this.pressure = null;
            this.forecastMessage = null;
            this.dayTemperatures = null;
        }

    }
}

/**
 * Class responsible for displaying weather data from WeatherModel as a responsive html element
 * @class
 */
class WeatherElement {
    /**
     * 
     * @param {string} containerId
     * @param {WeatherModel} weatherModel
     * @returns {void}
     */
    appendHTMLElement(containerId, weatherModel) {
        $(`#${containerId}`).html(this.buildHTMLElement(weatherModel));

        $('#forecastMessage').slimScroll({
            position: 'right',
            height: '100px',
            railVisible: true,
            alwaysVisible: true
        });

        $('#dayTemperatures').slimScroll({
            axis: 'x',
            height: '120px',
            position: 'bottom',
            railVisible: true,
            alwaysVisible: true
        });
    }

    appendHTMLLoading(containerId) {
        $(`#${containerId}`).html(`
            <div class="row align-items-center h-100">
                <div class="col text-center">
                    <div class="spinner-border text-warning" style="width: 3rem; height: 3rem;" role="status">
                      <span class="sr-only">Bootstrap spinner is not working</span>
                    </div>
                    <p>Loading...</p>
                </div>
            </div>
        `);
    }

    appendHTMLError(containerId, errorMessage) {
        $(`#${containerId}`).html(`
            <div class="row align-items-center h-100">
                <div class="col">
                    <div class="alert alert-danger m-5" role="alert">
                        <h4 class="alert-heading">${errorMessage}</h4>
                    </div>
                </div>
            </div>
        `);
    }

    /**
     * 
     * @param {WeatherModel} weatherModel
     * @returns {string}
     */
    buildHTMLElement(weatherModel) {
        if (weatherModel.date == null) {
            return `<div class="row align-items-center h-100">
                        <div class="col">
                            <div class="alert alert-warning m-5" role="alert">
                                <h4 class="alert-heading">No Data</h4>
                            </div>
                        </div>
                    </div>`;
        }
        return `<div class="m-1" style="height: 100% !important">
                        <div>
                            <h5><i class="wi wi-time-1 weather-icon"></i>${moment(weatherModel.date).format('LL')}</h5>
                                <div class="row display-flex">
                                    <div class="col-lg-6 col-sm-12">
                                        <div class="row display-flex align-items-center pl-4 h-100">
                                            <div class="col">
                                                <p class="display-3"><i class="wi wi-thermometer display-4"></i> ${weatherModel.temperatureC}<i class="wi wi-celsius"></i></p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-sm-12">
                                        <div class="row pt-2">
                                            <div class="col">
                                                <div class="row no-gutters">
                                                    <div class="col-2">
                                                        <i class="wi wi-cloud weather-icon" data-toggle="tooltip" data-placement="bottom" title="Cloudiness level"></i>
                                                    </div>
                                                    <div class="col">
                                                        ${weatherModel.cloudiness}% 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col"> 
                                                <div class="row no-gutters">
                                                    <div class="col-2">
                                                        <i class="wi wi-wind weather-icon" data-toggle="tooltip" data-placement="bottom" title="Wind speed"></i> 
                                                    </div>
                                                    <div class="col">
                                                        ${weatherModel.windSpeed}m/s 
                                                    </div>
                                                </div>
                                            </div> 
                                            <div class="col"> 
                                                <div class="row no-gutters">
                                                    <div class="col-2">
                                                        <i class="wi wi-humidity weather-icon" data-toggle="tooltip" data-placement="bottom" title="Humidity level"></i>
                                                    </div>
                                                    <div class="col">
                                                        ${weatherModel.humidity} 
                                                    </div>
                                                </div>
                                            </div>
                                        </div> 
                                        <div class="row pt-2">
                                            <div class="col">
                                                <div class="row no-gutters">
                                                    <div class="col-2">
                                                        <i class="wi wi-umbrella weather-icon" data-toggle="tooltip" data-placement="bottom" title="Rain chance"></i>
                                                    </div>
                                                    <div class="col">
                                                        ${weatherModel.rainChance}%
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="row no-gutters">
                                                    <div class="col-2">
                                                        <i class="wi wi-barometer weather-icon" data-toggle="tooltip" data-placement="bottom" title="Pressure"></i>
                                                    </div>
                                                    <div class="col">
                                                        ${weatherModel.pressure}h/Pa
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col">
                                                <div class="row no-gutters">
                                                    <div class="col-2">
                                                        <i class="wi wi-thermometer weather-icon" data-toggle="tooltip" data-placement="bottom" title="Temperature"></i><br/>
                                                    </div>
                                                    <div class="col">
                                                        ${weatherModel.temperatureF}<i class="wi wi-fahrenheit"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="row no-gutters">
                                                    <div class="col pb-2 pt-2 pr-4">
                                                        <div class="card h-100">
                                                          <div id="forecastMessage" class="card-body" style="height: 150px;overflow-y: scroll;">
                                                            ${weatherModel.forecastMessage}
                                                          </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row display-flex pr-4">
                                    <div class="col">
                                        <div class="card">
                                            <div id="dayTemperatures" class="card-body" style="padding: 0;">
                                                <table class="table">
                                                    <tbody>
                                                        <tr>
                                                          <th>Day time:</th>
                                                            ${ weatherModel.dayTemperatures.map(el => `<td>${moment(el.dayTime).format('HH:mm')}</td>`).reduce((acc, string) => `${acc}${string}`)}
                                                        </tr>
                                                        <tr>
                                                        <th>Temp:</th>
                                                            ${ weatherModel.dayTemperatures.map(el => `<td>${el.temperatureC} <i class="wi wi-celsius"></i></td>`).reduce((acc, string) => `${acc}${string}`)}
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>  
                    </div>`;
    }


}