<html>
<head>
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