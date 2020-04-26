(function () {

    var journalDetailService = {

        DeleteImage: function (journalId, filePath) {

            var errorMsg = "Missing a parameter!";
            if (journalId == null || filePath == null) { throw errorMsg; return; }

            var pack = {
                "JournalId": journalId,
                "FilePath": filePath
            };
            var r = toolbox.CreateAjaxResponse();
            $.ajax({
                type: "POST",
                url: toolbox.GetLocationUrl() + "/Journal/DeleteImage",
                dataType: "json",
                data: JSON.stringify(pack),
                contentType: 'application/json; charset=utf-8',
                processData: false,
                cache: false
            }).done(function (result) {
                r._JsonResultHelper(result);
            });

            return r;
        },


        DeleteJournal: function (journalId, filePath) {

            var errorMsg = "Missing a parameter!";
            if (journalId == null || filePath == null) { throw errorMsg; return; }

            var pack = {
                "JournalId": journalId,
                "FilePath": filePath
            };
            var r = toolbox.CreateAjaxResponse();
            $.ajax({
                type: "POST",
                url: toolbox.GetLocationUrl() + "/Journal/DeleteJournal",
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
    if (!window.journalDetailService) { window.journalDetailService = journalDetailService; }

})();