/**
 * Created by Jonathan Crété on 02/18/17
 */

/*********  Check functions   *************/
function checkerror() {
	$('#NotErrorDetected').hide();
	$('#ErrorDetected').show();
}

function checkNom(n) {
	return (isAlphaNum($('#fnom').val(), true))
}
function checkPrenom(p) {
	if (!isAlphaNum($('#fprenom').val(), true)) {
		return (false);
	} else return (true);
}