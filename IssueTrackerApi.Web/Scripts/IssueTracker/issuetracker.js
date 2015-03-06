$(document).ready(function () {

    // Activate Dropdown menus
    $('.ui.dropdown:not(.link)').dropdown({
        transition: 'slide down'
    });

    $('.ui.link.dropdown').dropdown({
        transition: 'slide down',
        action: 'hide'
    });

    $('.ui.radio.checkbox').checkbox();


    // Activate tablesorter
    if ($.tablesort) {
        $('.ui.table').tablesort();
    }

    $('[data-modal-trigger]').click(function (e) {
        e.preventDefault();

        var modal = $(e.target).attr('data-modal-trigger');

        $(modal).modal('show');
    });

});
