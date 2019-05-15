class ManageWeatherDataTemperaturesPartialView {
    constructor(options) {
        this.options = options;
        this.render();

    }
    render() {
        this.temperatures = [];
        for (let i = 0; i < 24; i++) {
            const time = new Date(this.options.date);
            time.setHours(i);
            time.setMinutes(0);
            time.setSeconds(0);
            this.temperatures.push({
                time: time,
                temperature: null
            });
        }

        $(this.options.selector).html(this.createInputs());
        $(`${this.options.selector}-temperatures`).slimScroll({
            position: 'right',
            width: $(this.options.selector).width,
            height: '300px',
            railVisible: true,
            alwaysVisible: true
        });

        this.inputs = [];
        for (let i = 0; i < 24; i++) {
            const inputElement = $(`${this.options.selector.slice(1)}-input-${i}`);
            this.inputs.push(new TTInput({
                selector: `${this.options.selector}-input-${i}`,
                icon: moment(this.temperatures[i].time).format('HH:mm'),
                suffix: `<i class="wi wi-celsius"></i>`,
                placeholder: "-50 - +65",
                description: "Please insert temperature",
                rules: [
                    (value) => (value != null && value.length === 0) ? "temperature is required" : null,
                    (value) => +value > 65 || +value < -50 ? "Value has to be equal -50 - +65" : null,
                ],
                onChange: (newValue) => {
                    this.temperatures[i].temperature = newValue;
                    this.options.onChange(this.temperatures);

                    //TODO change to is valid 
                    const smallMessageElement = $(`${this.options.selector}-small`);
                    if (this.validate()) {
                        $(smallMessageElement).addClass('text-muted');
                        $(smallMessageElement).removeClass('text-danger');
                    } else {
                        $(smallMessageElement).removeClass('text-muted');
                        $(smallMessageElement).addClass('text-danger');
                    }
                    //
                }
            }));
        }

        if (Array.isArray(this.options.initData) && this.options.initData.length === 24) {
            this.inputs.forEach((input, index) => {
                input.setValue(this.options.initData[index].temperatureC);
            });
        } else {
            this.inputs.forEach((input, index) => {
                input.setValue(null);
            });
        }
    }

    get isValid() {
        return this.inputs.every((input) => input.isValid);
    }
    validate() {
        return this.inputs.every((input) => input.validate() == null);
    }

    //private
    createInputs() {
        let html = `<div id="${this.options.selector.slice(1)}-temperatures" class="card p-2">`;
        for (let i = 0; i < 24; i++) {
            html += `<div id="${this.options.selector.slice(1)}-input-${i}"></div >`;
        }
        html += `</div>`;
        html += `<small id="${this.options.selector.slice(1)}-small" class="form-text text-danger">Please fill all 24 hours of the day</small>`;
        return html;
    }
}