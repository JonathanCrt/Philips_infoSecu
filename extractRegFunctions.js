/**
 * Created by Jonathan Crété on 02/18/18
 */

/*********  Form functions   *************/
function isNum(s) {
	return (!isNaN(s));
}
function isChiffres(s) {
	var regEx = new RegExp(/^([0-9]+)$/);
	return (regEx.test(s));
}
function isGoodImmat(s) {
	var regEx = new RegExp(/^([a-zA-Z0-9\ -]+)$/);
	return (regEx.test(s));
}