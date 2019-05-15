class TTNotification {
    constructor(selector, delayInSec) {
        this.selector = selector;
        this.delayInSec = delayInSec * 1000;
    }

    notify(message, status) {
        let textColor;
        switch (status) {
            case 'success':
                textColor = "text-success";
                break;
            case 'error':
                textColor = "text-danger";
                break;
            case 'info':
                textColor = "text-info";
                break;
            default:
                textColor = "text-dark";
        }
        $(this.selector).append(`
        <div class="toast ml-auto" role="alert" data-delay="${this.delayInSec}" data-autohide="true" data-animation="true">
            <div class="toast-header">
                <strong class="mr-auto ${textColor}">Notification</strong>
                <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                    <span aria-hidden="true">×</span>
                </button>
            </div>
            <div class="toast-body">
                ${message}
            </div>
        </div>
        `)
        $('.toast').toast('show');
        $('.toast').on('hidden.bs.toast', (event) => {
            $(event.currentTarget).remove();
        });
    }
}