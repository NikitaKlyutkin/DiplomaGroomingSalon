// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$(document).ready(function () {
//	$('#TypePetId').attr('disabled', true);
//	$('#BreedPetId').attr('disabled', true);
//	$('#ServiceTypeId').attr('disabled', true);
//	LoadCountries();
//	$('#TypePetId').change(function () {
//		var TypePetId = $(this).val();
//		if (TypePetId > 0) {
//			LoadStates(TypePetId);
//		}
//		else {
//			alert('Select TypePet');
//			$('#BreedPetId').empty();
//			$('#ServiceTypeId').empty();
//			$('#BreedPetId').attr('disabled', true);
//			$('#ServiceTypeId').attr('disabled', true);
//			$('#BreedPetId').append('<option>--Select BreedPet--</option>');
//			$('#ServiceTypeId').append('<option>--Select ServiceType--</option>');
//		}
//	});

//	$('#ServiceTypeId').change(function () {
//		var BreedPetId = $(this).val();
//		if (BreedPetId > 0) {
//			LoadCities(BreedPetId);
//		}
//		else {
//			alert('Select BreedPet');

//			$('#ServiceTypeId').attr('disabled', true);

//			$('#ServiceTypeId').append('<option>--Select ServiceType--</option>');
//		}
//	});

//});

//function LoadCountries() {
//	$('#TypePetId').empty();
//	$.ajax({
//		url: '/Order/GetTypeCascading',
//		success: function (response) {
//			if (response != null && response != undefined && response.length > 0) {
//				$('#TypePetId').attr('disabled', false);
//				$('#TypePetId').append('<option>--Select TypePet--</option>');
//				$('#BreedPetId').append("<option>--Select BreedPet--</option>");
//				$('#ServiceTypeId').append("<option>--Select ServiceType--</option>");
//				$.each(response,
//					function (i, data) {
//						$('#TypePetId').append('<option value=' + data.id + '>' + data.name + '</option>');
//					});
//			} else {
//				$('#TypePetId').attr('disabled', true);
//				$('#BreedPetId').attr('disabled', true);
//				$('#ServiceTypeId').attr('disabled', true);
//				$('#TypePetId').append('<option>--TypePet Not av--</option>');
//				$('#BreedPetId').append('<option>--BreedPet Not av--</option>');
//				$('#ServiceTypeId').append('<option>--ServiceType Not av--</option>');
//			}
//		},
//		error: function (error) {
//			alert(error);
//		}
//	});
//}
//function LoadStates(TypePetId) {
//	$("#BreedPetId").empty();
//	$("#ServiceTypeId").empty();
//	$("#ServiceTypeId").attr("disabled", true);
//	$.ajax({
//		url: "/Order/GetBreedCascading?Id=" + TypePetId,
//		success: function (response) {
//			if (response != null && response != undefined && response.length > 0) {
//				$("#BreedPetId").attr("disabled", false);
//				$("#BreedPetId").append("<option>--Select BreedPet--</option>");
//				$("#ServiceTypeId").append("<option>--Select ServiceType--</option>");
//				$.each(response,
//					function (i, data) {
//						$("#BreedPetId").append("<option value=" + data.id + ">" + data.name + "</option>");
//					});
//			} else {
//				$("#BreedPetId").attr("disabled", true);
//				$("#ServiceTypeId").attr("disabled", true);
//				$("#BreedPetId").append("<option>--BreedPet Not av--</option>");
//				$("#ServiceTypeId").append("<option>--ServiceType Not av--</option>");
//			}
//		},
//		error: function (error) {
//			alert(error);
//		}
//	});
//}
//function LoadCities(BreedPetId) {
//	$("#ServiceTypeId").empty();
//	$.ajax({
//		url: "/Order/GetServiceCascading?Id=" + BreedPetId,
//		success: function (response) {
//			if (response != null && response != undefined && response.length > 0) {
//				$("#ServiceTypeId").attr("disabled", false);
//				$("#ServiceTypeId").append("<option>--Select ServiceType--</option>");
//				$.each(response,
//					function (i, data) {
//						$("#ServiceTypeId").append("<option value=" + data.id + ">" + data.name + "</option>");
//					});
//			} else {
//				$("#ServiceTypeId").attr("disabled", true);
//				$("#ServiceTypeId").append("<option>--ServiceType Not av--</option>");
//			}
//		},
//		error: function (error) {
//			alert(error);
//		}
//	});
//}