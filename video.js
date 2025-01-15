// document.getElementById('loaddata').addEventListener('click', function()
 //{
	 /*
	const xhr = new XMLHttpRequest();
	xhr.open("GET", "https://andre-nas.servebeer.com/videoApi/api/Video/", true);
	output = document.getElementById("output");
	xhr.onload = function()
	{
		if (xhr.status==200)
		{
			output.innerHTML=xhr.responseText;
		}
		else
		{
			console.error("Failed to load data");
		}
	};
	xhr.send();
	*/
	function loadVideos()
	{
		output = document.getElementById("output");
		fetch("https://andre-nas.servebeer.com/videoApi/api/Video/")
			.then(response => response.json())
			.then(data => {
				// data is Array of String
				data.sort();
				data.forEach((v)=>{
					output.innerHTML += "<a href='play.php?fname="+v+"'><img src='preview/"+v+".gif' /></a>\n";
				});
			})
			.catch(error=>console.error("Failed to load", error));
	}
 //});