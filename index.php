<html>
<head>
</head>
<body>
<table border="0">
<?php
	$i=0;
	foreach (glob("*.mp4") as $fname)
	{
		if ((($i % 3)==0) || $i==0)
		{
?>
	<tr>
<?php
		}
		$i++;
?>
		<td>
<a href="play.php?fname=<?php print($fname); ?>">
<img src="preview/<?php print($fname); ?>.gif" />
</a>
		</td>

<?php
	}
?>
</table>
</body>
</html>