<html>
<head>
	<link href="https://vjs.zencdn.net/7.10.2/video-js.css" rel="stylesheet" />
	<script src="video.js"></script>
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
<div id="videotextdata"></div>
<script>
showVideoData('<?php print ($_GET["fname"]);?>');
</script>
</body>
</html>