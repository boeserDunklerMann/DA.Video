<html>
<head>
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
<?php
	foreach (glob("*.mp4") as $fname)
	{
?>
<a href="play.php?fname=<?php print($fname); ?>">
<img src="preview/<?php print($fname); ?>.gif" />
</a>
<?php
	}
?>
</table>
</body>
</html>