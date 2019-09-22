var blockUIConfig = {
	applyPlatformOpacityRules: false,
	message: '<p>Hệ thống đang tải thông tin. <br/> Vui lòng đợi giây lát...</p>'
};

var $body = $('body');

$(document).ajaxSend(function (event, request, settings) {
	$body.block(blockUIConfig);
}).ajaxStop(function (event, request, settings) {
	$body.unblock();
});

window.onbeforeunload = function () {
	$body.block(blockUIConfig);
};

window.onpageshow = function () {
	$body.unblock();
};