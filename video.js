class VideoTag
{
	constructor(id, tag)
	{
		this.id=id;
		this.tag=tag;
	}
}
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

function loadVideos(url)
{
	var requestOptions = 
	{
		method: 'GET',
		redirect: 'follow'
	};
	output = document.getElementById("output");
	output.innerHTML="";
	count = 0;
	
	fetch(url, requestOptions)
	.then(response => response.text())
	.then(result => 
	{
		videos = JSON.parse(result);
		videos.forEach(v => 
		{
			count++;
			output.innerHTML += "<div class='thumb'><a href='play.php?fname="+v.id+"'><img src='preview/"+v.previewFile+"' /><br/><label class='vidTitle'>"+v.title+"</label></a></div>\n";
		});
	})
	.catch(error => console.log('error', error));
	return count;
}
function loadAllVideos()
{
	loadVideos("https://andre-nas.servebeer.com/videoApi/api/Video/");
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
			txtTags.value=currentVideo.tags;
		})
		.catch(error => console.log('error', error));

	document.getElementById("btnSubmit").addEventListener("click", function()
	{
		jsonbody = JSON.stringify(currentVideo);
		console.log(jsonbody);
		
		var requestOptions = 
		{
			method: 'PUT',
			redirect: 'follow',
			headers:
			{
			'Content-Type': 'application/json'
			},
			body: jsonbody
		};
		fetch("https://andre-nas.servebeer.com/videoApi/api/Video/", requestOptions)
			.catch(error => console.log('error', error));
	});
	
	txtTitle.addEventListener("input", function()
	{
		currentVideo.title=txtTitle.value;
	});
	txtTags.addEventListener("input", function()
	{
		currentVideo.tags=txtTags.value;
		console.log(currentVideo);
	});
}

function search()
{
	txtSearch = document.getElementById("txtSearch");
	output = document.getElementById("output");
	output.innerHTML="";
	
	searchurl = "https://andre-nas.servebeer.com/videoApi/api/Video/byTag/"+encodeURIComponent(txtSearch.value);
	console.log(searchurl);
	loadVideos(searchurl);
}