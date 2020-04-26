var deleteImage = function () {

    var carousel = $('#myCarousel .carousel-inner');
    var items = carousel.find('.active');
    var filePath = "";
    if (items.length > 0) {
        filePath = items[0].style.cssText.match(/url\(["']?([^"']*)["']?\)/)[1];;
    }
    var journalId = $('#ID').val();




    journalDetailService.DeleteImage(journalId, filePath).done(function (e) {
        location.reload();
        alert("done");
    }).fail(function (e) {
        alert("Image failed to delete");
    }).always(function (e) {
        //alert("always");
    });
}



var deleteJournal = function () {
    var carousel = $('#myCarousel .carousel-inner');
    var items = carousel.find('.active');
    var filePath = "";
    if (items.length > 0) {
        filePath = items[0].style.cssText.match(/url\(["']?([^"']*)["']?\)/)[1];;
    }
    var journalId = $('#ID').val();

    journalDetailService.DeleteJournal(journalId, filePath).done(function (e) {
        document.location.href = "/journal/journaltable/";
    }).fail(function (e) {
        alert("Journal failed to delete");
    }).always(function (e) {
        //alert("always");
    });
}


$('#btn_plant').click(function () {

    var value = $("#PlantId option:selected").val();
    if (typeof value !== 'undefined' && value !== "" && value !== null)
        document.location.href = "/plantview/plantDisplay?plantid=" + value;
    else {
        alert("Plant does not exist");

    }
});


 