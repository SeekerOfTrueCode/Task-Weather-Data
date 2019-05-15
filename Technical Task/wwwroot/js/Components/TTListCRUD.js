class TTListCRUD {
    constructor(init) {
        //TODO probably delete the input for the new record and put it somewhere else 
        this.options = init;
        this.render();
    }
    render() {


        $(this.options.selector).html(this.templateHTML());
        if (this.options.edit != null) {
            $(`.${this.options.label}-edit`).click((event) => {
                const editId = event.currentTarget.value;
                if (!isNaN(editId)) {
                    const object = this.options.data.find(o => o.id.toString() === editId.toString());
                    this.options.edit(object);
                }
            });
        }
        if (this.options.remove != null) {
            $(`.${this.options.label}-remove`).click((event) => {
                const removeId = event.currentTarget.value;
                if (!isNaN(removeId)) {
                    const object = this.options.data.find(o => o.id.toString() === removeId.toString());
                    this.options.remove(object);
                }
            });
        }
        if (this.options.add != null) {
            $(`#${this.options.label}-add`).click((event) => {
                let object = {};
                for (let { title, prop } of this.options.dataColumns) {
                    object[prop] = $(`#${this.options.label}-input-${prop}`).val();
                }
                this.options.add(object);
            });
        }

        $(`#${this.options.label }-table`).slimScroll({
            position: 'right',
            height: '400px',
            railVisible: true,
            alwaysVisible: false
        });
    }

    templateHTML() {
        return `
        <div class="container">
            <h2>${this.options.label}</h2>
            <div id="${this.options.label }-table">
                <table class="table">
                    <thead>
                        <tr>
                            <th>#</th>
                            ${this.options.dataColumns.reduce((acc, next) => `${acc}<th>${next.title}</th>`,"")}
                            ${this.options.edit != null || this.options.remove != null ? `<th>Actions</th>` : ``}
                        </tr>
                    </thead>

                    <tbody>
                        ${this.options.data.reduce((acc, next) => `${acc}${this.createRow(next)}`, "")}
                    </tbody>
                </table>
            </div>
            ${this.options.add != null ? `            
            <hr />

            <div class="input-group mb-3">
                ${this.createAddButton()}
            </div>` : ``}
        </div>
        `;
    }


    createRow(object) {
        return `
        <tr>
            <td>${object.id}</td>
            ${this.createRowsColumnData(object)}
            ${ this.options.edit != null || this.options.remove != null ? `            
            <td>
                <div class="btn-group" role="group" aria-label="Basic example">
                    ${this.options.edit != null ? this.createEditButton(object.id) : ``}
                    ${this.options.remove != null ? this.createRemoveButton(object.id) : ``}
                </div>
            </td>` : ``}

        </tr>
        `;
    }

    createRowsColumnData(object) {
        let result = ``;
        for (let { prop } of this.options.dataColumns) {
            result += `<td>${object[prop]}</td>`;
        }
        return result;
    }

    createEditButton(id) {
        return `                            
        <button type="button" value="${id}" class="${this.options.label}-edit btn btn-sm btn-outline-info">
            <i class="fa fa-edit"></i>
        </button>
        `;
    }
    createRemoveButton(id) {
        return `                            
        <button type="button" value="${id}" class="${this.options.label}-remove btn btn-sm btn-outline-danger" aria-label="Close">
            <i class="fa fa-window-close"></i>
        </button>
        `;
    }
    createAddButton() {
        let inputs = ``;
        for (let { title, prop } of this.options.dataColumns) {
            inputs += `<input id="${this.options.label}-input-${prop}" type="text" placeholder="${title}" class="form-control">`;
        }
        return `
            ${inputs}
            <div class="input-group-prepend">
                <button id="${this.options.label}-add" type="button" class="btn btn-outline-secondary">Add</button>
            </div>
        `;
    }
}