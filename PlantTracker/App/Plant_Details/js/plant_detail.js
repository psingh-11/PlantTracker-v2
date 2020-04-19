$(document).ready(function () {




  
});

var deleteImage = function(){

    var carousel = $('#myCarousel .carousel-inner');
    var items = carousel.find('.active');
    var filePath = "";
    if (items.length > 0) {
        filePath = items[0].style.cssText.match(/url\(["']?([^"']*)["']?\)/)[1];;
    }
    var plantId = $('#ID').val();


    

    plantDetailService.DeleteImage(plantId, filePath).done(function (e) {
        location.reload();
        alert("done");
    }).fail(function (e) {
        alert("Image failed to delete");
    }).always(function (e) {
        //alert("always");
    });
}