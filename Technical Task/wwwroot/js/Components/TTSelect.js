//ts-strict
/**
 * Object representing the option in Select Element
 * @typedef {Object<string, string>} SelectListItem
 * @property {string} text The time of the day (most likely HH:mm)
 * @property {string} value The temperature predicted in the given time of the day
 */

/**
 * Options defining behaviour of the TTSelect class
 * @typedef {Object<string, Array<SelectListItem>, string, boolean>} TSSelectOptions
 * @property {string} selector
 * @property {Array<SelectListItem>} array
 * @property {string | number | string[] => void} onSelect
 * @property {boolean} immediate
 */

/**
 * @classdesc creating object of this class causes changes in
 * @property {TSSelectOptions} options
 */
class TTSelect {
    /**
     * 
     * @param {TSSelectOptions} init
     */
    constructor(init) {
        this.options = init;
        this.render();
    }
    /**
     * public method which re-renders select options
     * @param {Array<SelectListItem>} newArray
     */
    updateData(newArray) {
        this.options.array = newArray;
        this.options.immediate = false;
        this.render();
    }

    /**
     * private method
     * 1) rendering select options for given options in constructor
     * 2) setting listening of the 'select' event from options (this.options.onSelect) - unbinds and binds 'change' event
     * 3) if immediate is 'true' then it calls settled onSelect function
     */
    render() {
        $(this.options.selector).html("");
        $(this.options.selector).html(this.createOptionsHtml(this.options.array));

        $(this.options.selector).unbind('change');
        $(this.options.selector).change((event) => {
            const selectedValue = $(event.currentTarget).val();
            this.options.onSelect(selectedValue);
        });

        if (this.options.immediate === true) {
            $(this.options.selector).change();
        }
    }

    /**
     * @private 
     * private function creating many optionHtml as a string from array of SelectListItem
     *
     * @param {Array<SelectListItem>} array array of objects containing data you want to put as options of select input
     * @returns {string} stringHtml representing options for select
     */
    createOptionsHtml(array) {
        return array.reduce((acc, next, index) => {
            if (typeof next.value === "undefined" || typeof next.text === "undefined") {
                throw new Error('[Method: createOptionsHtml] object array does not fit the {{value: string, text: string}} type element');
            }
            const optionHtml = this.createOptionHtml(next.value, next.text, false);
            return `${acc}${optionHtml}`;
        }, "");
    }

    /**
     * private function creating single optionHtml as a string
     *
     * @param {string} value option value
     * @param {string} text option text
     * @param {boolean} isSelected boolean flag deciding if mark option as selected
     * @returns {string} stringHtml representing options for select
     */
    createOptionHtml(value, text, isSelected) {
        return `<option value="${value}" ${isSelected ? "selected" : ""}>${text}</option>`;
    }
}