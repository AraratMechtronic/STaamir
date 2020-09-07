
(function ($) {
	"use strict";
	$('.column100').on('mouseover',function(){
		var table1 = $(this).parent().parent().parent();
		var table2 = $(this).parent().parent();
		var verTable = $(table1).data('vertable')+"";
		var column = $(this).data('column') + ""; 

		$(table2).find("."+column).addClass('hov-column-'+ verTable);
		$(table1).find(".row100.head ."+column).addClass('hov-column-head-'+ verTable);
	});

	$('.column100').on('mouseout',function(){
		var table1 = $(this).parent().parent().parent();
		var table2 = $(this).parent().parent();
		var verTable = $(table1).data('vertable')+"";
		var column = $(this).data('column') + ""; 

		$(table2).find("."+column).removeClass('hov-column-'+ verTable);
		$(table1).find(".row100.head ."+column).removeClass('hov-column-head-'+ verTable);
	});

	$('.form-holder').delegate("input", "focus", function () {
		$('.form-holder').removeClass("active");
		$(this).parent().addClass("active");
	});
		
	$("#Open_addInstrument").click(function (e) {
		//$("#addInstrument").removeAttr("style")
		$("#addInstrument").removeClass("DisplayNone");
	});

	$(".caneclInstrument").click(function (e) {
		$("#addInstrument").addClass("DisplayNone");//.attr("style", "display:none")
	});
    
	$("#HPanel-addInstrument").click(function (e) {

		const thisInstrument =
		{
			Category: document.getElementById("txtCategory").value,
			Model: document.getElementById("txtModel").value,
			SerialNo: document.getElementById("txtSerial").value,
			UserName: "",
			Hospital: null,
			InstrumentId: 0
		};
		$.ajax({
			type: "POST",
			url: "/MyPanel/InstrumentAdd",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: '{ thisInstrument: ' + JSON.stringify(thisInstrument) + ' }',
			success: function (response) {
				console.log(response.statusText);
				if (response.success) {
					alert(response.responseText);
					var InstrumentId = response.responseText;
					//newRow.attr("scope", "row");
					$("#instrumentList>tbody:last").prepend("<tr><td scope='row'>" + "1"
						+ "</td><td>" + InstrumentId
						+ "</td><td>" + thisInstrument.Model
						+ "</td><td>" + thisInstrument.SerialNo
						+ "</td><td>" + thisInstrument.Category
						+ "</td><td><a class='modal-link btn btn-success' href='/MyPanel/EditInstrument?instrumentId=" + InstrumentId + "'>ویرایش</a>"
						+ "</td><td><a class='btn btn-danger' href='/MyPanel/DeleteInstrument?insrumentId=" + InstrumentId + "'>حذف</a>"
						+ "</td></tr>");


					$("#addInstrument").addClass("DisplayNone");
					
					// var reloadTable = function (employees) {
					//	var table = $('#employeesTable');
					//	table.find("tbody tr").remove();
					//	employees.forEach(function (employee) {
					//		table.append("<tr><td>" + employee.id + "</td><td>" + employee.name + "</td></tr>");
					//	});
					//};

					
				} else {
					$("#instrumentErrors ul").append(response.responseText);
					$("#instrumentErrors").removeClass("DisplayNone");
					alert(response.responseText);
				}        
			},
			error: function (data) {
				console.log('error');
			}
		});
	});

	$("#Open_HospitalEdit").click(function (e) {
		$("#ChangeHospitalInfoDiv").removeClass("DisplayNone");
		$("#MP_HopitalDisplayInfo").addClass("DisplayNone");
	});

	$("#MPH_cancelHospitalEdit").click(function (e) {
		$("#MP_HopitalDisplayInfo").removeClass("DisplayNone");
		$("#ChangeHospitalInfoDiv").addClass("DisplayNone");
	});

	$("#MPH_submitHospitalEdit").click(function (e) {
		const thisHospital =
		{
			hospitalName: document.getElementById("txthospitalName").value,
			phoneNumber: document.getElementById("txtPhoneNumber").value,
			inChargePerson: document.getElementById("txtInChargePerson").value,
			userNamee: "",
			province: "",
			City: "",
			instruments: null
		};
		alert(thisHospital.hospitalName);
		$.ajax({
			type: "POST",
			url: "/MyPanel/EditHopitalInfo",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: '{ thishospital: ' + JSON.stringify(thisHospital) + ' }',
			success: function (response) {
				console.log(response.statusText);
				if (response.success) {
					alert(response.responseText);

					$("#HospitalNamelbl").text(thisHospital.hospitalName);
					$("#InChargePersonlbl").text(thisHospital.inChargePerson);
					$("#PhoneNumberlbl").text(thisHospital.phoneNumber);

					$("#MP_HopitalDisplayInfo").removeClass("DisplayNone");
					$("#ChangeHospitalInfoDiv").addClass("DisplayNone");


				} else {
					$("#hospitalErrors ul").append(response.responseText);
					$("#hospitalErrors").removeClass("DisplayNone");
					alert(response.responseText);
				}
			},
			error: function (data) {
				console.log('error');
			}
		});
	});

	$(".HMP_DeleteInstrument").click(function (e) {
		const instrumentId = $(this).data().bind;
		var btn = $(this);
		$.ajax({
			type: "POST",
			url: "/MyPanel/DeleteInstrument",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: '{ insrumentId: ' + JSON.stringify(instrumentId) + ' }',
			success: function (response) {
				console.log(response.statusText);
				if (response.success) {
					alert(response.responseText);
					btn.closest('tr').addClass('DisplayNone');
					console.log('success');

				} else {
					
					alert(response.responseText);
				}
			},
			error: function (data) {
				console.log('error');
			}
		});
	});

})(jQuery);
//this is a file containing main javascript