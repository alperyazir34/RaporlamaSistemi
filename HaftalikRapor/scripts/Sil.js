$(document).ready(function () {
    $(".delete-row").click(function () {
        var id = $(this).attr("data-id");
        var closestTR = $(this).closest("tr");
        var confirmDelete = confirm( id + '  kodlu kayıdı silmek istediğinizden emin misiniz?');
        if (confirmDelete) {
            $.ajax({
                url: '/Admin/PerSil/' + id,
                type: 'POST',
                success: function (result) {
                    if (result == true) {
                        closestTR.fadeOut(1000, function () {
                            closestTR.remove();
                        });
                    }
                    else alert("Silme işlemi sırasında hata oluştu");
                }
            });
        }

    });

});