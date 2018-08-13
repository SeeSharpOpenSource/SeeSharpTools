$(document).ready(function() {
	/* Demo Start */
	
	/* jQuery-UI Widgets */
	
	$(".mws-accordion").accordion();
	
	$(".mws-tabs").tabs();
	
	$(".mws-datepicker").datepicker({showOtherMonths:true});
	
	$(".mws-datepicker-wk").datepicker({showOtherMonths:true, showWeek:true});
	
	$(".mws-datepicker-mm").datepicker({showOtherMonths:true, numberOfMonths:3});
	
	$(".mws-datepicker-btn").datepicker({showOtherMonths:true, showButtonPanel: true});
	
	$(".mws-slider").slider({range: "min"});
	
	$(".mws-progressbar").progressbar({value: 37});
	
	$(".mws-range-slider").slider({range: true, min:0, max: 500, values: [75, 300]});
	
	var availableTags = [
		"ActionScript",
		"AppleScript",
		"Asp",
		"BASIC",
		"C",
		"C++",
		"Clojure",
		"COBOL",
		"ColdFusion",
		"Erlang",
		"Fortran",
		"Groovy",
		"Haskell",
		"Java",
		"JavaScript",
		"Lisp",
		"Perl",
		"PHP",
		"Python",
		"Ruby",
		"Scala",
		"Scheme"
	];
	$( ".mws-autocomplete" ).autocomplete({
		source: availableTags
	});
	
	$("#mws-jui-dialog").dialog({
		autoOpen: false, 
		title: "jQuery-UI Dialog", 
		modal: true, 
		width: "640", 
		buttons: [{
				text: "Close Dialog", 
				click: function() {
					$( this ).dialog( "close" );
				}}]
	});
	$("#mws-jui-dialog-btn").bind("click", function(event) {
		$("#mws-jui-dialog").dialog("option", {modal: false}).dialog("open");
		event.preventDefault();
	});
	$("#mws-jui-dialog-mdl-btn").bind("click", function(event) {
		$("#mws-jui-dialog").dialog("option", {modal: true}).dialog("open");
		event.preventDefault();
	});
	
	$(".mws-slider-vertical").slider({
		orientation: "vertical", 
		range: "min",
		min: 0,
		max: 100,
		value: 60
	});
	
	$( "#eq > span" ).each(function() {
			// read initial values from markup and remove that
			var value = parseInt( $( this ).text(), 10 );
			$( this ).empty().slider({
				value: value,
				range: "min",
				animate: true, 
				orientation: "vertical"
			});
		});
	
	/* Spinner */
	
	var itemList = [
		{url: "http://ejohn.org", title: "John Resig"},
		{url: "http://bassistance.de/", title: "J&ouml;rn Zaefferer"},
		{url: "http://snook.ca/jonathan/", title: "Jonathan Snook"},
		{url: "http://rdworth.org/", title: "Richard Worth"},
		{url: "http://www.paulbakaus.com/", title: "Paul Bakaus"},
		{url: "http://www.yehudakatz.com/", title: "Yehuda Katz"},
		{url: "http://www.azarask.in/", title: "Aza Raskin"},
		{url: "http://www.karlswedberg.com/", title: "Karl Swedberg"},
		{url: "http://scottjehl.com/", title: "Scott Jehl"},
		{url: "http://jdsharp.us/", title: "Jonathan Sharp"},
		{url: "http://www.kevinhoyt.org/", title: "Kevin Hoyt"},
		{url: "http://www.codylindley.com/", title: "Cody Lindley"},
		{url: "http://malsup.com/jquery/", title: "Mike Alsup"}
	];
	
	var opts = {
		's1': {decimals:2},
		's2': {stepping: 0.25},
		's3': {currency: '$'}
	};

	for (var n in opts)
		$("#"+n).spinner(opts[n]);
	
	/* ColorPicker */
	
	$(".mws-colorpicker").ColorPicker({
		onSubmit: function(hsb, hex, rgb, el) {
			$(el).val(hex);
			$(el).ColorPickerHide();
		}, 
		onBeforeShow: function () {
			$(this).ColorPickerSetColor(this.value);
		}
	});
	
	/* Data Tables */
	
	$(".mws-datatable").dataTable();
	$(".mws-datatable-fn").dataTable({sPaginationType: "full_numbers"});
	
	$(".mws-crop-target").imgAreaSelect({
		handles: true, 
		x1: 32, y1: 32, x2: 133, y2: 133, 
		onSelectChange: function(img, selection) {
			$("#crop_x1").val(selection.x1);
			$("#crop_y1").val(selection.y1);
			$("#crop_x2").val(selection.x2);
			$("#crop_y2").val(selection.y2);
		}
	});
	
	/* Full Calendar */
	
	var date = new Date();
	var d = date.getDate();
	var m = date.getMonth();
	var y = date.getFullYear();


	$("#mws-calendar").fullCalendar({
		header: {
			left: 'prev,next today',
			center: 'title',
			right: 'month,agendaWeek,agendaDay'
		},
		editable: true,
		events: [
			{
				title: 'All Day Event',
				start: new Date(y, m, 1)
			},
			{
				title: 'Long Event',
				start: new Date(y, m, d-5),
				end: new Date(y, m, d-2)
			},
			{
				id: 999,
				title: 'Repeating Event',
				start: new Date(y, m, d-3, 16, 0),
				allDay: false
			},
			{
				id: 999,
				title: 'Repeating Event',
				start: new Date(y, m, d+4, 16, 0),
				allDay: false
			},
			{
				title: 'Meeting',
				start: new Date(y, m, d, 10, 30),
				allDay: false
			},
			{
				title: 'Lunch',
				start: new Date(y, m, d, 12, 0),
				end: new Date(y, m, d, 14, 0),
				allDay: false
			},
			{
				title: 'Birthday Party',
				start: new Date(y, m, d+1, 19, 0),
				end: new Date(y, m, d+1, 22, 30),
				allDay: false
			},
			{
				title: 'Click for Google',
				start: new Date(y, m, 28),
				end: new Date(y, m, 29),
				url: 'http://google.com/'
			}
		]
	});
	
	/* Sourcerer */
	
	$(".mws-code-html").sourcerer('html');
	
	/* Validation Plugin */
	
	$("#mws-validate").validate({
		invalidHandler: function(form, validator) {
			var errors = validator.numberOfInvalids();
			if (errors) {
				var message = errors == 1
				? 'You missed 1 field. It has been highlighted'
				: 'You missed ' + errors + ' fields. They have been highlighted';
				$("#mws-validate-error").html(message).show();
			} else {
				$("#mws-validate-error").hide();
      		}
		}
	});
	
	/* jGrowl Notifications */
	
	$("#mws-growl-btn").bind("click", function(event) {
		$.jGrowl("Hello World!", {position: "bottom-right"});
	});
	
	$("#mws-growl-btn-1").bind("click", function(event) {
		$.jGrowl("A sticky message", {sticky: true, position: "bottom-right"});
	});
	
	$("#mws-growl-btn-2").bind("click", function(event) {
		$.jGrowl("Message with Header", {header: "Important!", position: "bottom-right"});
	});
});
