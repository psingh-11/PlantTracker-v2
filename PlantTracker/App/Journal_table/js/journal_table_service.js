(function () {

    var journalTableService = {

        GetDetails: function (journalId) {

            var errorMsg = "Missing a parameter!";
            if (plantId == null) { throw errorMsg; return; }

            var pack = {
                "JournalID": journalId
            };
            var r = toolbox.CreateAjaxResponse();
            $.ajax({
                type: "POST",
                url: toolbox.GetLocationUrl() + "/Journal/GetJournalDetails",
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
    }


    //We create a shortcut for our framework, we can call the methods by clientProjectInfoBL.method();
    if (!window.journalTableService) { window.journalTableService = journalTableService; }

})();