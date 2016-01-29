function getLastTweet(){
	username = $("#user_input").val();
	$.ajax({
		url: "https://whfzasrtsr.localtunnel.me/displaytweet/" + username,
		error: function(jqxhr, textStatus, error) { 
		  console.log("error", arguments);
		},
		contentType: "application/json; charset=utf-8",
		crossDomain: true,
        dataType: 'jsonp',
        type: 'GET',
        async: false
	});
	$(".loading-on").show();
	$(".loading-off").hide();
};

function success(data){
	$("#spLastTweet").text(data.message);
	$(".loading-on").hide();
	$(".loading-off").show();
}

function error(data){
	$("#spLastTweet").text(data.message);
	$(".loading-on").hide();
	$(".loading-off").show();
}