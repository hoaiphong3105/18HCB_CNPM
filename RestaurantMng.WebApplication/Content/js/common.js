function loading() {
    $('body').append('<div class="cssload-container active"><div class="cssload-loading"><i></i><i></i><i></i><i></i></div></div>');
}

function hideloading() {
    $('.cssload-container').remove();
}

///status 0: error, 1:success
function showalert(msg, status) {
    if (status === 0) {
        $.jnoty(msg, {
            header: 'Lỗi',
            sticky: false,
            theme: 'jnoty-danger',
            icon: 'fa fa-check-circle',
            life: 3000,
            position: 'bottom-right'
        });
    }
    else if (status === 1) {
        $.jnoty(msg, {
            header: 'Success',
            sticky: false,
            theme: 'jnoty-success',
            icon: 'fa fa-check-circle',
            life: 5000,
            position: 'bottom-right'
        });
    } else {
        $.jnoty(msg, {
            header: 'Thông tin',
            sticky: false,
            theme: 'jnoty-info',
            icon: 'fa fa-check-circle',
            life: 5000,
            position: 'bottom-right'
        });
    }
}