$(document).ready(function () {

    $('#btn_journal').click(function () {

        var value = $("#Journals option:selected").val();
        if (typeof value !== 'undefined' && value !== "" && value !== null)
            document.location.href = "/journalview/journalDisplay?journalid=" + value;
        else {
            alert("Journal does not exist");

        }
    });


  
});

var deletePlant = function () {
    var carousel = $('#myCarousel .carousel-inner');
    var items = carousel.find('.active');
    var filePath = "";
    if (items.length > 0) {
        filePath = items[0].style.cssText.match(/url\(["']?([^"']*)["']?\)/)[1];;
    }
    var plantId = $('#ID').val();

    plantDetailService.DeletePlant(plantId, filePath).done(function (e) {
        
        document.location.href = "/plant/planttable/";
    }).fail(function (e) {
        alert("Plant failed to delete");
    }).always(function (e) {
        //alert("always");
    });
}

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