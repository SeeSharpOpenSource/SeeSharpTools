$(document).ready(function() {
	var baseColor = "#c5d52b";
	var textColor = "#c5d52b";
	var textGlowColor = {r: 197, g: 213, b: 42, a: 0.5};
	
	var presets = {
		Default: {
			name: "Default", 
			baseColor: "c5d52b", 
			textColor: "c5d52b", 
			textGlowColor: {r: 197, g: 213, b: 42, a: 0.5}
		}, 
		IceBlue: {
			name: "Ice Blue", 
			baseColor: "a1a9ff", 
			textColor: "cef2f2", 
			textGlowColor: {r: 86, g: 126, b: 247, a: 0.6}
		}, 
		Grayscale: {
			name: "Grayscale", 
			baseColor: "d6d6d6", 
			textColor: "fafafa", 
			textGlowColor: {r: 237, g: 237, b: 237, a: 0.38}
		}, 
		FireRed: {
			name: "Fire Red", 
			baseColor: "db4444", 
			textColor: "d46a6f", 
			textGlowColor: {r: 242, g: 34, b: 34, a: 0.22}
		}, 
		Feminism: {
			name: "Feminism", 
			baseColor: "f08dcc", 
			textColor: "fcaee3", 
			textGlowColor: {r: 186, g: 9, b: 230, a: 0.5}
		}, 
		OrangeJuice: {
			name: "Orange Juice", 
			baseColor: "e6864e", 
			textColor: "e87817", 
			textGlowColor: {r: 245, g: 224, b: 201, a: 0.13}
		}, 
		YellowSkin: {
			name: "Yellow Skin", 
			baseColor: "f5e20c", 
			textColor: "f7f713", 
			textGlowColor: {r: 235, g: 191, b: 17, a: 0}
		}
	};
	
	var borderColorTargets = 
	[
	 	"div#mws-header"
	];
	
	var baseColorTargets = 
	[
		"div#mws-searchbox input.mws-search-submit", 
		".mws-panel .mws-panel-header .mws-collapse-button span", 
		"div.dataTables_wrapper .dataTables_paginate div", 
		"div.dataTables_wrapper .dataTables_paginate span.paginate_active", 
		".fc-state-highlight", 
		".ui-slider-horizontal .ui-slider-range", 
		".ui-slider-vertical .ui-slider-range", 
		".ui-progressbar .ui-progressbar-value", 
		".ui-datepicker td.ui-datepicker-current-day", 
		".ui-datepicker .ui-datepicker-prev .ui-icon", 
		".ui-datepicker .ui-datepicker-next .ui-icon", 
		".ui-accordion-header .ui-icon", 
		".ui-dialog-titlebar-close .ui-icon"
	];
	
	var textTargets = 
	[
		".mws-panel .mws-panel-header span", 
		"div#mws-navigation ul li.active a", 
		"div#mws-navigation ul li.active span", 
		"div#mws-user-tools #mws-username", 
		"div#mws-navigation ul li span.mws-nav-tooltip", 
		"div#mws-user-tools #mws-user-info #mws-user-functions #mws-username", 
		".ui-dialog .ui-dialog-title", 
		".ui-state-default", 
		".ui-state-active", 
		".ui-state-hover", 
		".ui-state-focus", 
		".ui-state-default a", 
		".ui-state-active a", 
		".ui-state-hover a", 
		".ui-state-focus a"
	];
	
	$("#mws-themer-getcss").bind("click", function(event) {
		var css = 
			borderColorTargets.join(", \n") + "\n" + 
			"{\n"+
			"	border-color:" + baseColor + ";\n"+
			"}\n\n"+
			textTargets.join(", \n") + "\n" + 
			"{\n"+
			"	color:" + textColor + ";\n"+
			"	text-shadow:0 0 6px rgba(" + getTextGlowArray().join(", ") + ");\n"+
			"}\n\n"+
			baseColorTargets.join(", \n") + "\n" + 
			"{\n"+
			"	background-color:" + baseColor + ";\n"+
			"}\n";

		$("#mws-themer-css-dialog textarea").val(css);
		$("#mws-themer-css-dialog").dialog("open");
		event.preventDefault();
	});
	
	var presetDd = $("#mws-theme-presets");
	for(var i in presets) {
		var option = $("<option></option>").text(presets[i].name).val(i);
		presetDd.append(option);
	}
	
	presetDd.bind('change', function(event) {
		updateBaseColor(presets[presetDd.val()].baseColor);
		updateTextColor(presets[presetDd.val()].textColor);
		
		updateTextGlowColor(presets[presetDd.val()].textGlowColor, presets[presetDd.val()].textGlowColor.a);
		event.preventDefault();
	});
	
	$("div#mws-themer #mws-themer-hide").bind("click", function(event) {
		$(this).toggleClass("opened").next().fadeToggle();
	});
	
	$("div#mws-themer #mws-textglow-op").slider({
		range: "min", 
		min:0, 
		max: 100, 
		value: 50, 
		slide: function(event, ui) {
			alpha = ui.value * 1.0 / 100.0;
			updateTextGlowColor(null, alpha);
		}
	});
	
	$("div#mws-themer #mws-themer-css-dialog").dialog({
		autoOpen: false, 
		title: "Theme CSS", 
		width: 500, 
		modal: true, 
		resize: false, 
		buttons: {
			"Close": function() { $(this).dialog("close"); }
		}
	});
	
	$("#mws-base-cp").ColorPicker({
		color: baseColor, 
		onShow: function (colpkr) {
				$(colpkr).fadeIn(500);
				return false;
		},
		onHide: function (colpkr) {
				$(colpkr).fadeOut(500);
				return false;
		},
		onChange: function (hsb, hex, rgb) {			
			updateBaseColor(hex);
		}
	});
	
	$("#mws-text-cp").ColorPicker({
		color: textColor, 
		onShow: function (colpkr) {
				$(colpkr).fadeIn(500);
				return false;
		},
		onHide: function (colpkr) {
				$(colpkr).fadeOut(500);
				return false;
		},
		onChange: function (hsb, hex, rgb) {			
			updateTextColor(hex);
		}
	});
	
	$("#mws-textglow-cp").ColorPicker({
		color: textGlowColor, 
		onShow: function (colpkr) {
				$(colpkr).fadeIn(500);
				return false;
		},
		onHide: function (colpkr) {
				$(colpkr).fadeOut(500);
				return false;
		},
		onChange: function (hsb, hex, rgb) {
			updateTextGlowColor(rgb, textGlowColor["a"]);
		}
	});
	
	function updateBaseColor(hex)
	{
		baseColor = "#" + hex;
		$("#mws-base-cp").css('backgroundColor', baseColor);
		
		$(baseColorTargets.join(", ")).css('backgroundColor', baseColor);
		$(borderColorTargets.join(", ")).css("borderColor", baseColor);
	}
	
	function updateTextColor(hex)
	{
		textColor = "#" + hex;
		$("#mws-text-cp").css('backgroundColor', textColor);
		$(textTargets.join(", ")).css('color', textColor);
	}
	
	function updateTextGlowColor(rgb, alpha)
	{
		if(rgb != null) {
			textGlowColor.r = rgb["r"];
			textGlowColor.g = rgb["g"];
			textGlowColor.b = rgb["b"];
			textGlowColor.a = alpha;
		} else {
			textGlowColor.a = alpha;
		}
		
		$("div#mws-themer #mws-textglow-op").slider("value", textGlowColor.a * 100);
		$("#mws-textglow-cp").css('backgroundColor', '#' + rgbToHex(textGlowColor.r, textGlowColor.g, textGlowColor.b));
		
		$(textTargets.join(", ")).css('textShadow', '0 0 6px rgba(' + getTextGlowArray().join(", ") + ')');
	}
	
	function getTextGlowArray()
	{
		var array = new Array();
		for(var i in textGlowColor)
			array.push(textGlowColor[i]);
			
		return array;
	}
	
	function rgbToHex(r, g, b)
	{
		var rgb = b | (g << 8) | (r << 16);
		return rgb.toString(16);
	}
});