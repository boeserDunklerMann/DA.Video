
function loadAllVideos()
{
	var requestOptions = 
	{
		method: 'GET',
		redirect: 'follow'
	};
	output = document.getElementById("output");
	output.innerHTML="";
	
	fetch("https://andre-nas.servebeer.com/videoApi/api/Video/", requestOptions)
	.then(response => response.text())
	.then(result => 
	{
		videos = JSON.parse(result);
		videos.forEach(v => 
		{
			output.innerHTML += "<div class='thumb'><a href='play.php?fname="+v.id+"'><img src='preview/"+v.previewFile+"' /><br/><label class='vidTitle'>"+v.title+"</label></a></div>\n";
		});
	})
	.catch(error => console.log('error', error));
}

function showVideoData(videoId)
{
	
}