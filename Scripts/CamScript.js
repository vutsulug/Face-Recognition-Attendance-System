function takeSnapshot() {
    // Here we're using a trick that involves a hidden canvas element.    

    var hidden_canvas = document.querySelector('canvas'),
        context = hidden_canvas.getContext('2d');

    var width = video.videoWidth,
        height = video.videoHeight;

    if (width && height) {

        // Setup a canvas with the same dimensions as the video.  
        hidden_canvas.width = width;
        hidden_canvas.height = height;

        // Make a copy of the current frame in the video on the canvas.  
        context.drawImage(video, 0, 0, width, height);

        // Storing Base64String  
        var datacaptured = hidden_canvas.toDataURL('image/jpeg');

        // Ajax Post to Save Image in Folder  
        Uploadsubmit(datacaptured);
        // Turn the canvas image into a dataURL that can be used as a src for our photo.  
        return datacaptured;
    }
}

function Uploadsubmit(datacaptured) {

    if (datacaptured != "") {
        $.ajax({
            type: 'POST',
            url: ("/CamCaptureAzure/Capture"),
            dataType: 'json',
            data: { base64String: datacaptured },
            success: function (data) {
                if (data == false) {

                    alert("Photo Captured is not Proper!");
                    $('#ResponseTable').empty();
                }
                else {

                    if (data.length == 9) {
                        $('#ResponseTable').empty();
                        alert("Its not a Face!");
                    } else {
                        var _faceAttributes = JSON.parse(data);

                        $('#ResponseTable').empty();
                        var _responsetable = "";
                        var _emotiontable = "";
                        _responsetable += '<div class="panel panel-default"><div class="panel-heading">Azure Face API Response</div>';
                        _responsetable += "<div class='panel-body'>"
                        _responsetable += '<table class="table table-bordered"><thead><tr> <th>Smile</th> <th>Gender</th> <th>Age</th> <th>Glasses</th></tr></thead>';
                        _responsetable += '<tr> <th>' +
                            _faceAttributes[0].faceAttributes.smile +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.gender +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.age +
                            '</th> <th>' +
                            _faceAttributes[0].faceAttributes.glasses +
                            '</th></tr>';
                        _responsetable += "</table>"


                        _responsetable += '<table class="table table-bordered"><thead><tr> <th>Anger</th> <th>Contempt</th> <th>Disgust</th> <th>Fear</th>  <th>Happiness</th>  <th>Neutral</th> <th>Sadness</th> <th>Surprise</th> </tr></thead>';

                        _responsetable += '<tr><th>' +
                            _faceAttributes[0].faceAttributes.emotion.anger +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.emotion.contempt +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.emotion.disgust +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.emotion.fear +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.emotion.happiness +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.emotion.neutral +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.emotion.sadness +
                            '</th><th>' +
                            _faceAttributes[0].faceAttributes.emotion.surprise +
                            '</th></tr>';
                        _responsetable += "</table></div></div>";

                        $('#ResponseTable').append(_responsetable);

                    }
                }
            }
        });
    }

}  