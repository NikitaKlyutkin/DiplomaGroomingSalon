function openModal(parameters) {
    var  id = parameters.data;
    var url = parameters.url;
    var modal = $('#modal');

    if (id === undefined || url === undefined) {
        alert('Упссс.... что-то пошло не так')
        return;
    }

    $.ajax({
        type: 'GET',
        url: url,
        data: { "id": id },
        success: function (response) {
            modal.find(".modal-body").html(response);
            modal.modal('show')
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
};