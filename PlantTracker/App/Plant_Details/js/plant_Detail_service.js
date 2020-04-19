(function () {

    var plantDetailService = {

        DeleteImage: function (plantId, filePath) {

            var errorMsg = "Missing a parameter!";
            if (plantId == null || filePath == null) { throw errorMsg; return; }

            var pack = {
                "PlantId": plantId,
                "FilePath": filePath
            };
            var r = toolbox.CreateAjaxResponse();
            $.ajax({
                type: "POST",
                url: toolbox.GetLocationUrl() + "/Plant/DeleteImage",
                dataType: "json",
                data: JSON.stringify(pack),
                contentType: 'application/json; charset=utf-8',
                processData: false,
                cache: false
            }).done(function (result) {
                r._JsonResultHelper(result);
            });

            return r;
        }
    };


    //We create a shortcut for our framework, we can call the methods by clientProjectInfoBL.method();
    if (!window.plantDetailService) { window.plantDetailService = plantDetailService; }

})();