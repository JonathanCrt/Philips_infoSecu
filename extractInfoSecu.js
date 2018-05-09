function sendinfotrp() {
	if (!checkNom()) return;
	if (!checkPrenom()) return;
	if (!checkSociety()) return;

	var params = {};
	params.a = 'nt';
	params.lname = $('#fnom').val();
	params.fname = $('#fprenom').val();
	params.society = $('#fsociete').val();
	params.vehicule1 = $('#fnumV1').val();
	params.vehicule2 = $('#opt-fnumV2').val();
	params.vehicule3 = $('#opt-fnumV3').val();
	params.onucode = $('#onu').val();
	params.languageSelected = $('#inputLangHide').val();
	params.type = $('#inputTypeHide').val();


	params.svcexp = $('#idBoxExp').attr('data-value');
	params.svcrep = $('#idBoxRep').attr('data-value');
	params.svcret = $('#idBoxRet').attr('data-value');
	params.svcres = $('#idBoxRes').attr('data-value');
	params.svcmai = $('#idBoxMai').attr('data-value');
	params.svccom = $('#idBoxCom').attr('data-value');

	params.opcha = $('#idBoxCha').attr('data-value');
	params.opdce = $('#idBoxDce').attr('data-value');
	params.oprce = $('#idBoxRce').attr('data-value');
	params.opdec = $('#idBoxDec').attr('data-value');
	params.opliv = $('#idBoxLiv').attr('data-value');
	params.optra = $('#idBoxTra').attr('data-value');

	params.mdanger = $('input[name="o1"]:checked').val();

	params.loading = $('input[name="o2"]:checked').val();
	params.signature = $('#sig').signature('toJSON');
	params.signature = btoa($('#sig').signature('toSVG'));
	

	
	$.ajax({
		url: myUrl,
		type: 'POST',
		data: params,
		beforeSend: function () {
		},
		success: function (data) {
		},
		error: function () {
			alert('Error');
		},
		complete: function () {
		}
	});
}


function updateDateSafetyRules() {

	var params = {};
	params.a = 'upsr';
	params.id = $('#vsgcodeend').val();
	params.identifier = $('#fnom').val();


	$.ajax({
		url: myUrl,
		type: 'POST',
		data: params,
		beforeSend: function () {
		},
		success: function (data) {
			
			
		},
		error: function () {
			alert('Error : Impossible to send the update of DateConsigne');
		},
		complete: function () {
		}
	});

}