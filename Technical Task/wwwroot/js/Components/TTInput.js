class TTInput {
    constructor(options) {
        this.options = options;
        this.render();
    }

    setValue(value) {
        const element = $(`${this.options.selector}-input`);
        $(element).val(value);
        $(element).trigger('input');
    }
    //public
    render() {
        $(this.options.selector).html(`
        <div class="input-group  mb-2">
            <div class="input-group">

                <div class="input-group-prepend">
                    <div class="input-group-text center-block ${true ? "form-control-sm" : ""}" style="min-width: 45px;">
                        ${this.options.icon}
                    </div>
                </div>

                <input id="${this.options.selector.slice(1)}-input"
                       type="text"
                       style="border: .5px solid rgb(206, 212, 218); border-right: 0; "
                       class="form-control ${true ? "form-control-sm" : ""}"
                       placeholder="${this.options.placeholder != null ? this.options.placeholder : ""}">
                ${this.options.suffix != null ? `
                <div class="input-group-prepend" >
                    <div class="input-group-text ${true ? "form-control-sm" : ""}" 
                         style="background: transparent !important; border-left: 0 !important;">
                        ${this.options.suffix}
                    </div>
                </div>
                `: ``}

            </div>
            <small class="form-text text-muted">
              ${this.createMessage()}
            </small>
        </div>
        `);

        this.oldValueMessage = null;
        this.oldValue = null;
        this.value = null;
        this.message = this.options.message;

        const smallMessageElement = $(`${this.options.selector} small`);
        const inputElement = $(`${this.options.selector}-input`);
        $(inputElement).on('input', (event) => {

            this.oldValue = this.value;
            this.value = $(event.currentTarget).val();

            // mask (regex)
            this.value = this.allowOnlyDecimal(this.value);
            $(event.currentTarget).val(this.value);
            //

            //validate
            this.oldValueMessage = this.message;
            this.message = this.validate();
            //

            if (this.oldValueMessage !== this.message) {
                // add validate for min and max values
                this.afterValidateChangeStyle(inputElement, smallMessageElement);
                $(smallMessageElement).html(this.createMessage());
            }
            //

            //notify about change of value
            if (this.oldValue !== this.value) {
                this.options.onChange(this.value);
            }

        });
    }

    get isValid() {
        return (validate() == null);
    }
    //public
    validate() {
        let newMessage = this.message;
        if (Array.isArray(this.options.rules)) {
            for (let validate of this.options.rules) {
                newMessage = validate(this.value);
                if (newMessage != null) return newMessage;
            }
        }
        return newMessage;
    }

    //private
    allowOnlyDecimal(value) {
        return value.replace(/[^?\-0-9.]/g, '').replace(/(\..*)\./g, '$1').replace(/(\-.*)\-/g, '$1');
    }

    //private
    afterValidateChangeStyle(inputElement, smallMessageElement) {
        if (this.message == null) {
            $(inputElement).addClass('is-valid');
            $(inputElement).removeClass('is-invalid');
            $(smallMessageElement).addClass('text-muted');
            $(smallMessageElement).removeClass('text-danger');
        } else {
            $(inputElement).removeClass('is-valid');
            $(inputElement).addClass('is-invalid');
            $(smallMessageElement).removeClass('text-muted');
            $(smallMessageElement).addClass('text-danger');
        }
    }

    //private
    createMessage() {
        return this.message != null ? this.message : this.options.description;
    }
}