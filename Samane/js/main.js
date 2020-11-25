
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
		$("#addInstrument").removeClass("DisplayNone");
		$("#lblInstrumentId").addClass("DisplayNone");
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
					//alert(response.responseText);
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

	$("#MPH_EditRequestInstrument").click(function (e) {
		const instrumentId = $(this).data().bind;
		var btn = $(this);
		
		$.ajax({
			type: "POST",
			url: '/MyPanel/GetHospitalInstrument',
			data: '{ instrumentId: ' + JSON.stringify(instrumentId) + ' }',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			beforeSend: function () {
				//Show();  Show loader icon  
			},
			success: function (response) {
				alert(response.responseText);
				var thisinstrument = $.parseJSON(response.responseText);
				//Add information to add or update form
				document.getElementById("lbl_instrumentId").value = thisinstrument.InstrumentId;
				document.getElementById("txtModel_Edit").value = thisinstrument.Model;
				document.getElementById("txtCategory_Edit").value = thisinstrument.Category;
				document.getElementById("txtSerialNo_Edit").value = thisinstrument.SerialNo;

				document.getElementById("txtModelBefore_Edit").value = thisinstrument.Model;
				document.getElementById("txtCategoryBefore_Edit").value = thisinstrument.Category;
				document.getElementById("txtSerialNoBefore_Edit").value = thisinstrument.SerialNo;

				btn.closest('tr').remove();//Exclude row from list
				$("#lblInstrumentId").removeClass('DisplayNone');
				$("#editInstrument").removeClass("DisplayNone");
			},
			complete: function () {
				//Hide();  Hide loader icon  
			},
			failure: function (jqXHR, textStatus, errorThrown) {
				alert("HTTP Status: " + jqXHR.status + "; Error Text: " + jqXHR.responseText); // Display error message  
			},
			error: function (response) {
				alert('error');
				btn.closest('tr').removeClass('DisplayNone');//Exclude row from project
				$("#addInstrument").addClass("DisplayNone");
			}
		});  
		
		//console.log('instrumentId is: '+instrumentId);
		//var btn = $(this);
		//btn.closest('tr').addClass('DisplayNone');//Exclude row from project
		//$("#addInstrument").removeClass("DisplayNone");
		////$("#lblInstrumentId").removeClass("DisplayNone");
		//$.ajax({
		//	type: "POST",
		//	url: "/MyPanel/GetHospitalInstrument",
		//	contentType: "application/json; charset=utf-8",
		//	dataType: "json",
		//	data: {},/*'{ instrumentId :' + JSON.stringify(instrumentId) + ' }',*/
		//	success: function (response) {
		//		console.log('response');
		//		//$('#output').append('It is right');  
		//		//var obj = JSON.parse(response);
		//		//alert(obj.InstrumentId);
		//		//console.log(response.user);
		//		//if (response.success) {
		//		//	console.log(response.responseText);
					
		//		//	//response.responseText.
		//		//	//$("#lblInstrumentId").text(response.);
		//		//	console.log('success');

		//		//} else {

		//		//	alert(response.responseText);
		//		//}
		//	},
		//	error: function (response) {
		//		console.log('error');
		//		btn.closest('tr').removeClass('DisplayNone');//Exclude row from project
		//		$("#addInstrument").addClass("DisplayNone");
		//	}
		//});
	});

	$("#MPH_caneclEditInstrument").click(function (e) {
		$("#editInstrument").addClass("DisplayNone");
		
		console.log('MPH_caneclEditInstrument');
		console.log(document.getElementById("lbl_instrumentId").value);

		if ($("#instrumentList>tbody").length) {
			$("#instrumentList>tbody:last").prepend("<tr><td scope='row'>" + "1"
				+ "</td><td>" + document.getElementById("lbl_instrumentId").value
				+ "</td><td>" + document.getElementById("txtModelBefore_Edit").value
				+ "</td><td>" + document.getElementById("txtSerialNoBefore_Edit").value
				+ "</td><td>" + document.getElementById("txtCategoryBefore_Edit").value
				+ "</td><td><a class='modal-link btn btn-success' href='/MyPanel/EditInstrument?instrumentId=" + document.getElementById("lbl_instrumentId").value + "'>ویرایش</a>"
				+ "</td><td><a class='btn btn-danger' href='/MyPanel/DeleteInstrument?insrumentId=" + document.getElementById("lbl_instrumentId").value + "'>حذف</a>"
				+ "</td></tr>");
		}
		else {
			$("#instrumentList").prepend("<tbody><tr><td scope='row'>" + "1"
				+ "</td><td>" + document.getElementById("lbl_instrumentId").value
				+ "</td><td>" + document.getElementById("txtModelBefore_Edit").value
				+ "</td><td>" + document.getElementById("txtSerialNoBefore_Edit").value
				+ "</td><td>" + document.getElementById("txtCategoryBefore_Edit").value
				+ "</td><td><a class='modal-link btn btn-success' href='/MyPanel/EditInstrument?instrumentId=" + document.getElementById("lbl_instrumentId").value + "'>ویرایش</a>"
				+ "</td><td><a class='btn btn-danger' href='/MyPanel/DeleteInstrument?insrumentId=" + document.getElementById("lbl_instrumentId").value + "'>حذف</a>"
				+ "</td></tr>");
		}
		document.getElementById("lbl_instrumentId").value = "";
		document.getElementById("txtModelBefore_Edit").value = "";
		document.getElementById("txtSerialNoBefore_Edit").value = "";
		document.getElementById("txtCategoryBefore_Edit").value = "";
		document.getElementById("txtModel_Edit").value = "";
		document.getElementById("txtCategory_Edit").value = "";
		document.getElementById("txtSerialNo_Edit").value = "";
	});
	
	$("#MPH-EditInstrument").click(function (e) {

		const thisInstrument =
		{
			Category: document.getElementById("txtCategory_Edit").value,
			Model: document.getElementById("txtModel_Edit").value,
			SerialNo: document.getElementById("txtSerialNo_Edit").value,
			UserName: "",
			Hospital: null,
			InstrumentId: document.getElementById("lbl_instrumentId").value
		};
		$.ajax({
			type: "POST",
			url: "/MyPanel/EditInstrument",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: '{ thisInstrument: ' + JSON.stringify(thisInstrument) + ' }',
			success: function (response) {
				console.log(response.statusText);
				if (response.success) {
					//alert(response.responseText);
					var InstrumentId = response.responseText;
					//newRow.attr("scope", "row");
					alert(thisInstrument.SerialNo);
					if ($("#instrumentList>tbody").length) {
						$("#instrumentList>tbody:last").prepend("<tr><td scope='row'>" + "1"
							+ "</td><td>" + InstrumentId
							+ "</td><td>" + thisInstrument.Model
							+ "</td><td>" + thisInstrument.SerialNo
							+ "</td><td>" + thisInstrument.Category
							+ "</td><td><a class='modal-link btn btn-success' href='/MyPanel/EditInstrument?instrumentId=" + InstrumentId + "'>ویرایش</a>"
							+ "</td><td><a class='btn btn-danger' href='/MyPanel/DeleteInstrument?insrumentId=" + InstrumentId + "'>حذف</a>"
							+ "</td></tr>");
					}
					else {
						$("#instrumentList").prepend("<tbody><tr><td scope='row'>" + "1"
							+ "</td><td>" + InstrumentId
							+ "</td><td>" + thisInstrument.Model
							+ "</td><td>" + thisInstrument.SerialNo
							+ "</td><td>" + thisInstrument.Category
							+ "</td><td><a class='modal-link btn btn-success' href='/MyPanel/EditInstrument?instrumentId=" + InstrumentId + "'>ویرایش</a>"
							+ "</td><td><a class='btn btn-danger' href='/MyPanel/DeleteInstrument?insrumentId=" + InstrumentId + "'>حذف</a>"
							+ "</td></tr>");
					}


					$("#editInstrument").addClass("DisplayNone");

					// var reloadTable = function (employees) {
					//	var table = $('#employeesTable');
					//	table.find("tbody tr").remove();
					//	employees.forEach(function (employee) {
					//		table.append("<tr><td>" + employee.id + "</td><td>" + employee.name + "</td></tr>");
					//	});
					//};


				} else {
					$("#instrumentErrorsEdit ul").append(response.responseText);
					$("#instrumentErrorsEdit").removeClass("DisplayNone");
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


//-------------------counter in home page-----------------
const counters = document.querySelectorAll('.counter');
const speed = 2000;

    counters.forEach(counter => {
        const updateCount = () => {
            const target = +counter.getAttribute('data-target');
            const count = +counter.innerText;

            const inc = target / speed;

            if (count < target) {
                counter.innerText = Math.ceil(count + inc);
                setTimeout(updateCount, 1);
            }
            else {
                count.innertext = target;
            }

        }
        updateCount();
    });
