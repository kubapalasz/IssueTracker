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


    /* Define API endpoints once globally */
    $.fn.api.settings.api = {
        'create project': 'http://localhost:8766/Project',
    };

    $('form .submit.button')
      .api({
          action: 'create project',
          serializeForm: true,
          beforeSend: function (settings) {
              // form data is editable in before send
              if (settings.data.username == '') {
                  settings.data.username = 'New User';
              }
              // open console to inspect object
              console.log(settings.data);
              return settings;
          }
      });


});
