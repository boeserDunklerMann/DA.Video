<!DOCTYPE html>
<html>
<head>
	<link href="https://vjs.zencdn.net/7.10.2/video-js.css" rel="stylesheet" />
	<link rel="icon" type="image/x-icon" href="favicon.ico">
	<script src="video.js"></script>
	<style>
		div.thumb
		{ 
			width: 100px;
			float: left;
			margin-bottom: 3mm;
			margin-right: 1mm;
		}
		div.searchbox
		{
			width: 4cm;
			float: none;
			visibility: hidden;
		}
		label.vidTitle
		{
			font-family: sans-serif;
			font-size: x-small;
			font-weight: bold;
		}
		input:read-only,
		textarea:read-only {
			background-color: silver;
		}
</style>
</head>
<body>

<video id="my-video"
	   class="video-js"
	   controls
	   preload="auto"
	   width="640"
	   height="264"
	   data-setup="{}">
	<source src="<?php print ($_GET["fname"]);?>" type="video/mp4" />
	<p class="vjs-no-js">
		To view this video please enable JavaScript, and consider upgrading to a
		web browser that
		<a href="https://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a>
	</p>
</video>
<script src="https://vjs.zencdn.net/7.10.2/video.min.js"></script>

<div id="videotextdata">
<label>ID:<input type="text" id="txtID" readonly /></label><br/>
<label>Title:<input type="text" id="txtTitle"/></label><br />
<label for="txtTags">Tags:</label><textarea id="txtTags"></textarea>
<button id="btnSubmit">OK</button>
</div>

<script>
showVideoData('<?php print ($_GET["fname"]);?>');
</script>
</body>
</html>