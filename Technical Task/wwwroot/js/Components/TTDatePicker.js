//ts-strict

/**
 * Options defining behaviour of the TTSelect class
 * @typedef {Object<string, Date, boolean>} TSDatePickerOptions
 * @property {string} selector
 * @property {Date => void} onSelect
 * @property {boolean} immediate
 */

/**
 * @classdesc creating object of this class causes changes in
 * @property {TSDatePickerOptions} options
 */
class TTDatePicker {
    /**
     * 
     * @param {TSDatePickerOptions} init
     */
    constructor(init) {
        this.options = init;
        this.render();
    }

    /**
     * private method
     * 1) rendering datepicker for given options in constructor
     * 2) setting listening of the 'changeDate' event from options (this.options.onSelect) - binds 'changeDate' event
     * 3) if immediate is 'true' then it calls settled onSelect function but doesn't guarantee it's callback (!selects today)
     */
    render() {
        $(this.options.selector).datepicker({
            toggleActive: true,
            todayHighlight: true
        });

        $(this.options.selector).on('changeDate', () => {
            const newSelectedDate = new Date($(this.options.selector).datepicker('getFormattedDate'));
            if (!isNaN(newSelectedDate.getTime())) {
                this.options.onSelect(newSelectedDate);
            }
        });

        if (this.options.immediate === true) {
            $(this.options.selector).datepicker('update', new Date());
            //$(this.options.selector).datepicker().trigger('changeDate');
        }
    }

    trigger() {
        $(this.options.selector).datepicker().trigger('changeDate');
    }
}