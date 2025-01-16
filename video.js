class VideoEntry
{
	constructor(id, title, previewFile, tags)
	{
		this.id=id;
		this.title=title;
		this.previewFile=previewFile;
		this.tags=tags;
	}
}
var currentVideo = new VideoEntry();

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
	var requestOptions = 
	{
		method: 'GET',
		redirect: 'follow'
	};
	txtID=document.getElementById("txtID");
	txtTitle=document.getElementById("txtTitle");
	txtTags= document.getElementById("txtTags");
	
	fetch("https://andre-nas.servebeer.com/videoApi/api/Video/"+videoId, requestOptions)
		.then(response=>response.json())
		.then(data =>{
			console.log(data);
			currentVideo=data;
			txtID.value = currentVideo.id;
			txtTitle.value=currentVideo.title;
			txtTags.value=currentVideo.tags.join(' ');
		})
		.catch(error => console.log('error', error));
	document.getElementById("btnSubmit").addEventListener("click", function()
	{
		// TODO DA: send data to webApi
		console.log(currentVideo);

		var requestOptions = 
		{
			method: 'PUT',
			redirect: 'follow',
			headers:
			{
			'Content-Type': 'application/json'
			},
			body: JSON.stringify(currentVideo)
		};
		fetch("https://andre-nas.servebeer.com/videoApi/api/Video/", requestOptions)
			.catch(error => console.log('error', error));
	});
	
	txtTitle.addEventListener("input", function()
	{
		currentVideo.title=txtTitle.value;
		console.log(currentVideo);
	});
	txtTags.addEventListener("input", function()
	{
		currentVideo.tags=txtTags.value.split(" ");
		console.log(currentVideo);
	})
}