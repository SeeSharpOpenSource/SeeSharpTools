$(document).ready(function() {
	$("#elfinder").elfinder({
		url : 'plugins/elfinder/connectors/php/connector.php', 
		lang : 'en',
		docked : true,
		height: 300
	});
	
	$("#uploader").pluploadQueue({
		// General settings
		runtimes : 'html4, html5', 
		url : '../upload.html',
		max_file_size : '1000mb',
		max_file_count: 20, // user can add no more then 20 files at a time
		chunk_size : '1mb',
		unique_names : true,
		multiple_queues : true,

		// Resize images on clientside if we can
		resize : {width : 320, height : 240, quality : 90},
		
		// Rename files by clicking on their titles
		rename: true,
		
		// Sort files
		sortable: true,

		// Specify what files to browse for
		filters : [
			{title : "Image files", extensions : "jpg,gif,png"},
			{title : "Zip files", extensions : "zip,avi"}
		]
	});
});