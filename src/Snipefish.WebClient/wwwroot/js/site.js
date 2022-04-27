// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


Snipfish = (function ($) {
    "use strict";

    let CommonFunctions = function () {
        //private

    };

    //public
    CommonFunctions.prototype.cHiNLoader = function (isShow, containerElement) {
        let container = containerElement;
        if (container)
            container.css("position", "relative");

        ((container && container.children()) || $("#mainBodyContainer")).addClass("blurred-background");

        let containerHeight = container ? container.height() : $(window).height();
        let imgWidth;

        if (containerHeight <= 10) {
            imgWidth = 50;
        } else if (containerHeight <= 50) {
            imgWidth = 30;
        } else if (containerHeight <= 200) {
            imgWidth = 50;
        } else {
            if (container) {
                imgWidth = (containerHeight / 6) > 60 ? 60 : (containerHeight / 6);
            } else {
                imgWidth = (containerHeight / 10);
            }
        }
        if (isShow) {
            if (container) {
                if ($("#initLoader").length === 0) {
                    container.append("<div class='loader-container'>"
                        + "<div style='width:" + imgWidth + "px;height:" + imgWidth + "px;'><div class='loader2'></div></div>"
                        + "</div>");
                }

            } else {
                $("body").append("<div class='loader-container' style='position:fixed'>"
                    + "<div style='width:" + imgWidth + "px;height:" + imgWidth + "px;'><div class='loader2'></div></div>"
                    + "</div>");
            }

        } else {
            if (container) {
                let ldr1 = container.children(".loader-container");
                ldr1.css("animation-name", "animateShadeOff");
                setTimeout(function () { ldr1.remove(); }, 750);
                if ($("#initLoader").length > 0) {
                    let ldr2 = $("body").children("#initLoader");
                    ldr2.css("animation-name", "animateShadeOff");
                    setTimeout(function () { ldr2.remove(); }, 750);
                }
                container.children().removeClass("blurred-background");
            } else {
                let ldr3 = $("body").find(".loader-container");
                ldr3.css("animation-name", "animateShadeOff");
                $("body").find(".blurred-background").removeClass("blurred-background");
                setTimeout(function () { ldr3.remove(); }, 750);
            }

        }
    };

    CommonFunctions.prototype.AjaxPost = function (url, postData) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                type: "POST",
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (data) {
                        resolve(data);
                    },
                error: function (jqXhr, textStatus, errorThrown) {
                    $.growl.error({ title: "Error occurred", message: errorThrown });
                    reject(jqXhr);
                }
            });
        });
    };

    CommonFunctions.prototype.AjaxPut = function (url, postData) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                type: "PUT",
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (data) {
                    resolve(data);
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    $.growl.error({ title: "Error occurred", message: errorThrown });
                    reject(jqXhr);
                }
            });
        });
    };

    return {
        CommonFunctions: new CommonFunctions()
    }

}(window.jQuery));