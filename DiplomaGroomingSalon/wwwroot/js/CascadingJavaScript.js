$(document).ready(function () {

    $("#BreedId").attr("disabled", true);
    $("#ServiceTypeId").attr("disabled", true);
    $("#PetTypeId").change(function () {

        $.get("/ServiceJS/GetBreedForCascading",
                { PetTypeId: $("#PetTypeId").val() },
                function (data) {
                    if (data != null) {
                        $("#BreedId").empty();
                        $("#ServiceTypeId").empty();
                        $("#BreedId").attr("disabled", false);
                        $("#BreedId").append('<option value="">--Select Breed Pet--</option>');
                        $("#ServiceTypeId").attr("disabled", true);
                        $("#ServiceTypeId").append('<option value="">--Select Service Type--</option>');
                       
                        $("#Price").empty();
                        $("#Price").append('<option value="">--Price---</option>');
                        $.each(data,
                            function (i, item) {
                                $("select#BreedId").append(`<option value="${item.id}">${item.name}</option>`);
                            });
                    } else {
                        alert("Select TypePet");
                        $("#BreedId").empty();
                        $("#Price").empty();
                        $("#BreedId").attr("disabled", true);
                        $("#ServiceTypeId").attr("disabled", true);
                    }
                });
    })

        $("#BreedId").change(function() {
            $.get("/ServiceJS/GetServiceForCascading",
                { BreedId: $("#BreedId").val() },
                function (data) {
                    if (data != null) {
                        $("#ServiceTypeId").empty();
                        $("#Price").empty();
                        $("#ServiceTypeId").attr("disabled", false);
                        $("#ServiceTypeId").append('<option value="">--Select Service Type--</option>');
                        $("#Price").append('<option value="">--Price---</option>');
                        $.each(data,
                            function (i, item) {
                                $("select#ServiceTypeId").append(`<option value="${item.id}">${item.name}</option>`);
                                
                            });
                    } else {
                        alert("Select Breed Pet");
                        $("#BreedId").empty();
                        $("#ServiceTypeId").empty();
                        $("#Price").empty();
                        $("#BreedId").attr("disabled", true);
                        $("#ServiceTypeId").attr("disabled", true);
                        $("#Price").attr("disabled", true);
                    }
                });
        })

    $("#ServiceTypeId").change(function () {
        $.getJSON("/ServiceJS/GetPriceForCascading",
                { ServiceTypeId: $("#ServiceTypeId").val() },
            function (data) {
                if (data != null) {
                    $("#Price").empty();
                        $.each(data,
                            function (i, item) {
                                $("select#Price").append(`<option value="${item.name}"> ${item.name} BYN</option>`);
                            });

                } else {
                    alert("Select Breed Pet");
                    $("#ServiceTypeId").empty();
                    $("#Price").empty();
                }
            });

        })
})
