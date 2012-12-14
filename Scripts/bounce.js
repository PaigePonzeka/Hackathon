
var balls = [],
	screen_width = $(window).width(),
	screen_height = $(window).height(),
	max_speed = 3,
	forward = 1,
	backward = -1,
	stop = 0,
	colors = [],
	default_colors = [[15, 161, 255], [153, 255, 0], [153, 0, 253], [255, 0, 153], [255, 188, 0], [242, 234, 11], [255, 0, 72]],
	default_colors_used = 0;

//var colors = [[255, 0, 0], [0, 255, 0], [0, 0, 255]];
var directions = [forward,backward, stop];

setColorDemoDefaults();
$(document).ready(function () {

	generateDemoCanvasKeys();
});

/*function intializeBalls() {
	balls.push({ "x": 400, "y": 100, "speed": 40, "width": 50, "height": 50 });
}*/
function setColorDemoDefaults() {
	colors["test"] = [255, 0, 0];
	/*colors["ThirtySecondToThreeMinuteJob"] = [15, 161, 255]; //blue
	colors["ThirtySecondToMinutelyJob"] = [153, 255, 0]; //green
	colors["ThirtySecondToFiveMinuteJob"] = [153, 0, 253]; // purple
	colors["LessThanAMinuteJob"] = [255, 0, 153]; // pink
	colors["ManuallyKickOffJobJob"] = [255, 188, 0]; // amber
	colors["SendEmailJob"] = [242, 234, 11]; // yellow
	colors["MoveMessagesFromWaitingRoomToQueueJob"] = [255, 0, 72]; // amber*/

}

function addBall() {
	var random_x = Math.floor(Math.random() * screen_width);
	var random_y = Math.floor(Math.random() * screen_height);
	var random_speed_x = 1 + Math.floor(Math.random() * max_speed);
	var random_speed_y = 1 + Math.floor(Math.random() * max_speed);
	var random_direction_x = directions[Math.floor(Math.random() * 2)];
	var random_direction_y = directions[Math.floor(Math.random() * 2)];
	var starting_radius = 20;

	//we won't have a random color later
	var type_color = generateColors();
	// Direction = 1 0 or -1
	balls.push(
	{
		"x": random_x,
		"y": random_y,
		"speed_x": random_speed_x,
		"speed_y": random_speed_y,
		"radius": starting_radius,
		"direction_y": random_direction_y,
		"direction_x": random_direction_x,
		"color": type_color
	});


}


function removeBall(job_id) {
	balls.pop();
}

function generateColors(){
	//generate colors based on job type
		if (default_colors_used < default_colors.length) {
		  var current_used = default_colors_used;
		  default_colors_used += 1;
			return default_colors[current_used];
		}
		else {
			return  (Math.floor(Math.random() * 255), Math.floor(Math.random() * 255), Math.floor(Math.random() * 255));
		}

}

function toClass(string) {
	return string.replace(/\s/g, '_');
}

function generateDemoCanvasKeys() {
	/*var demo = ["test", "ThirtySecondToThreeMinuteJob", "ThirtySecondToMinutelyJob", "ThirtySecondToFiveMinuteJob", "LessThanAMinuteJob", "ManuallyKickOffJobJob", "SendEmailJob", "MoveMessagesFromWaitingRoomToQueueJob"];

	$.each(demo, function () {
		generateCanvasKey('' + this);
	});*/
	//generateCanvasKey('test');

}
/*function generateCanvasKey(type) {
	var rgb_color = colors[type];
	var key_html = "<li class='"+ toClass(type)+"'> <div class='key' style='background: rgb(" + rgb_color[0] + "," + rgb_color[1] + "," + rgb_color[2] + ") '></div> " + type.replace("Job", "") + "</li>";
	$('.js-canvas-key ul').append(key_html);
}*/

/*function removeTypeIfNone(job_type) {
	console.log(job_type);
	$.each(balls, function () {

		if (job_type === this.job_type) {
			return;
		}
	});
	console.log("Delete:" + toClass(type));
	$('.' + toClass(type)).remove();
}*/

$(document).keydown(function (evt) {
	if (evt.keyCode == 32) {
		addBall(32, "test");
	}
	if (evt.keyCode == 65) {
		removeBall(32);
	}
});
