(function () {

    var toolbox = {

        GetLocationUrl: function () {
            return "";
        },

        CreateAjaxResponse: function () {

            var ajaxResponse = function () {
                this._done = null;
                this._fail = null;
                this._always = null;
                this._JsonResultHelper = function (result) {

                    result.IsError ? (this._fail ? this._fail(result) : null) : (this._done ? this._done(result) : null);

                    if (this._always)
                        this._always(result);
                };
            };

            ajaxResponse.prototype.done = function (callback) {
                this._done = callback;
                return this;
            }

            ajaxResponse.prototype.fail = function (callback) {
                this._fail = callback;
                return this;
            }

            ajaxResponse.prototype.always = function (callback) {
                this._always = callback;
                return this;
            }

            return new ajaxResponse();
        }, 


    };

    //We create a shortcut for our framework, we can call the methods by toolbox.method();
    if (!window.toolbox) { window.toolbox = toolbox; }
})();