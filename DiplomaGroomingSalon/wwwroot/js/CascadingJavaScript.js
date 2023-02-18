$(document).ready(function () {

    $("#BreedPetId").attr("disabled", true);
    $("#ServiceTypeId").attr("disabled", true);
    $("#TypePetId").change(function () {

            $.get("/PriceCascading/GetBreedForCascading",
                { TypePetId: $("#TypePetId").val() },
                function (data) {
                    if (data != null) {
                        $("#BreedPetId").empty();
                        $("#ServiceTypeId").empty();
                        $("#BreedPetId").attr("disabled", false);
                        $("#BreedPetId").append('<option value="">--Select Breed Pet--</option>');
                        $("#ServiceTypeId").attr("disabled", true);
                        $("#ServiceTypeId").append('<option value="">--Select Service Type--</option>');
                       
                        $("#Price").empty();
                        $("#Price").append('<option value="">--Price---</option>');
                        $.each(data,
                            function (i, item) {
                                $("select#BreedPetId").append(`<option value="${item.id}">${item.name}</option>`);
                            });
                    } else {
                        alert("Select TypePet");
                        $("#BreedPetId").empty();
                        $("#Price").empty();
                        $("#BreedPetId").attr("disabled", true);
                        $("#ServiceTypeId").attr("disabled", true);
                    }
                });
    })

        $("#BreedPetId").change(function() {
            $.get("/PriceCascading/GetServiceForCascading",
                { BreedPetId: $("#BreedPetId").val() },
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
                        $("#BreedPetId").empty();
                        $("#ServiceTypeId").empty();
                        $("#Price").empty();
                        $("#BreedPetId").attr("disabled", true);
                        $("#ServiceTypeId").attr("disabled", true);
                        $("#Price").attr("disabled", true);
                    }
                });
        })

    $("#ServiceTypeId").change(function () {
        $.getJSON("/PriceCascading/GetPriceForCascading",
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
