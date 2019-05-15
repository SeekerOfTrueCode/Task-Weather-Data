
$(document).ready(() => {
    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    $('.toast').toast('show');
    $('.toast').on('hidden.bs.toast', (event) => {
        $(event.currentTarget).remove();
    });
});