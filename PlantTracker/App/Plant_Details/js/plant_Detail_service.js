﻿(function () {

    var homeService = {

        GetDetails: function (empId) {

            var errorMsg = "Missing a parameter!";
            if (empId == null) { throw errorMsg; return; }

            var pack = {
                "EmpId": empId
            };
            var r = toolbox.CreateAjaxResponse();
            $.ajax({
                type: "POST",
                url: toolbox.GetLocationUrl() + "/Home/GetDetails",
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

        SetDetails: function (empId) {

            var errorMsg = "Missing a parameter!";
            if (empId == null) { throw errorMsg; return; }

            var pack = {
                "EmpId": empId
            };
            var r = toolbox.CreateAjaxResponse();
            $.ajax({
                type: "POST",
                url: toolbox.GetLocationUrl() + "/Home/GetDetails",
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
    if(!window.homeService) { window.homeService = homeService; }

})();